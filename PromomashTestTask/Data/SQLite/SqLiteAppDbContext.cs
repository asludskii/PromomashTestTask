using Duende.IdentityServer.EntityFramework.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using NGuid;
using PromomashTestTask.Models;

namespace PromomashTestTask.Data.SQLite
{
    /// <summary>
    /// SQLite implementations of <see cref="AppDbContext"/>.
    /// Any changes not specific to SQLite should be done in the base class.
    /// </summary>
    public class SqLiteAppDbContext : AppDbContext
    {
        private readonly SqLiteConnectionOptions _sqliteOptions;

        public SqLiteAppDbContext(
            DbContextOptions options, 
            IOptions<OperationalStoreOptions> operationalStoreOptions, 
            IOptions<SqLiteConnectionOptions> sqliteOptions)
            : base(options, operationalStoreOptions)
        {
            _sqliteOptions = sqliteOptions.Value ?? throw new ArgumentNullException(nameof(sqliteOptions));
        }

        #region Overrides of DbContext

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlite(_sqliteOptions.ConnectionString);
        }

        #endregion

        #region Overrides of AppDbContext

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            SeedTestData(builder);
        }

        private static void SeedTestData(ModelBuilder builder)
        {
            // Countries and Provinces are defined in a human-friendly format
            var countriesData = new[]
            {
            new Country
            {
                Name = "Outland",
                Provinces = new List<Province>
                {
                    new() {Name = "Netherstorm"},
                    new() {Name = "Blades Edge Mountains"},
                    new() {Name = "Hellfire Peninsula"},
                    new() {Name = "Zangarmarsh"},
                    new() {Name = "Nagrand"},
                    new() {Name = "Terokkar Forest"},
                    new() {Name = "Shadowmoon Valley"}
                }
            },
            new Country
            {
                Name = "Azeroth",
                Provinces = new List<Province>
                {
                    new() {Name = "Kalimdor"},
                    new() {Name = "Northrend"},
                    new() {Name = "Broken Isles"},
                    new() {Name = "Kul Tiras"},
                    new() {Name = "Eastern Kingdoms"},
                    new() {Name = "Zandalar"},
                    new() {Name = "Pandaria"}
                }
            }
        };

            // Here we condition the data to be acceptable for EF Migrations
            foreach (var country in countriesData)
            {
                // Generate GUIDs in a deterministic way 
                country.Id = GuidHelpers.CreateFromName(Guid.Empty, country.Name);

                foreach (var province in country.Provinces ?? Enumerable.Empty<Province>())
                {
                    // Establish relationships via FKeys
                    province.Id = GuidHelpers.CreateFromName(Guid.Empty, $"{country.Name}+{province.Name}");
                    province.CountryId = country.Id;
                }

                if (country.Provinces is not null && country.Provinces.Any())
                    builder.Entity<Province>().HasData(country.Provinces);

                // Clean up collection references, since what's convenient for us isn't so for EF
                country.Provinces = null;
            }

            builder.Entity<Country>().HasData(countriesData);

            builder.Entity<PolicyVersion>().HasData(
                new PolicyVersion
                {
                    CreatedDateTimeUtc = DateTime.UnixEpoch,
                    PolicyText = "Putting pineapple on pizza disqualifies it as food.",
                    Id = GuidHelpers.CreateFromName(Guid.Empty, "Placeholder")
                });
        }

        #endregion
    }
}