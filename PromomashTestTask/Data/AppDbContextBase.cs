using Duende.IdentityServer.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using PromomashTestTask.Models;

// DBSets are initialized by base DbContext
#pragma warning disable CS8618

namespace PromomashTestTask.Data;

/// <summary>
/// Base DbContext implementation for this application.
/// All configuration agnostic of implementation should happen here.
/// </summary>
public abstract class AppDbContextBase : ApiAuthorizationDbContext<ApplicationUser>
{
    /// <summary>
    /// A collection of valid country names.
    /// </summary>
    public DbSet<Country> Countries { get; set; }
    /// <summary>
    /// Top-level administrative subdivisions of countries.
    /// </summary>
    public DbSet<Province> Provinces { get; set; }
    /// <summary>
    /// Terms of service policy versions.
    /// These will likely change in the future and it may be important to track which exact version user has agreed to.
    /// </summary>
    public DbSet<PolicyVersion> PolicyVersions { get; set; }

    protected AppDbContextBase(DbContextOptions options, IOptions<OperationalStoreOptions> operationalStoreOptions) 
        : base(options, operationalStoreOptions)
    {
    }

    #region Overrides of ApiAuthorizationDbContext<ApplicationUser>

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // Code-first definitions of data properties.
        // Relationships are defined in dependent entities.

        builder.Entity<Country>(country =>
        {
            country.HasComment("A collection of valid country names.");
            country.HasKey(x => x.Id);

            country.Property(x => x.Id)
                .ValueGeneratedOnAdd();

            country.Property(x => x.Name)
                .IsRequired()
                .HasComment("Country name.");

            country.HasIndex(x => x.Name)
                .IsUnique();
        });

        builder.Entity<Province>(province =>
        {
            province.HasComment("Top-level administrative subdivisions of countries.");
            province.HasKey(x => x.Id);
            province.Property(x => x.Id)
                .ValueGeneratedOnAdd();

            province.Property(x => x.Name)
                .IsRequired()
                .HasComment("Province name.");

            province.Property(x => x.CountryId)
                .HasComment("FK, Country which this province belongs to.");

            // A country should not have multiple provinces with the same name.
            province.HasIndex(x => new {x.CountryId, x.Name})
                .IsUnique();

            province.HasOne(x => x.Country)
                .WithMany(x => x.Provinces)
                .HasForeignKey(x => x.CountryId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
        });

        builder.Entity<ApplicationUser>(user =>
        {
            user.HasComment("Application user.");

            user.Property(x => x.PolicyAcceptDatetimeUtc)
                .IsRequired()
                .HasComment("Timestamp of user accepting terms of service policy, UTC.");

            user.Property(x => x.PolicyVersionId)
                .HasComment("FK, Exact version of terms of service accepted by this user.");

            user.Property(x => x.ProvinceId)
                .HasComment("FK, User's province of residence.");

            user.HasOne(x => x.PolicyVersion)
                .WithMany(x => x.Users)
                .HasForeignKey(x => x.PolicyVersionId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            user.HasOne(x => x.Province)
                .WithMany(x => x.Users)
                .HasForeignKey(x => x.ProvinceId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
        });

        builder.Entity<PolicyVersion>(policyVersion =>
        {
            policyVersion.HasComment("Terms of service policy versions.");
            policyVersion.HasKey(x => x.Id);
            policyVersion.Property(x => x.Id)
                .ValueGeneratedOnAdd();

            policyVersion.Property(x => x.CreatedDateTimeUtc)
                .IsRequired()
                .HasComment("Timestamp of when this policy version was created, UTC.");

            policyVersion.Property(x => x.PolicyText)
                .HasMaxLength(1 << 16) // 64k
                .HasComment("Text content of this policy version.");
        });
    }

    #endregion
}