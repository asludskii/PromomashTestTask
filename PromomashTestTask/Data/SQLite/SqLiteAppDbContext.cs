using Duende.IdentityServer.EntityFramework.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace PromomashTestTask.Data.SQLite
{
    /// <summary>
    /// SQLite implementations of <see cref="AppDbContextBase"/>.
    /// Any changes not specific to SQLite should be done in the base class.
    /// </summary>
    public class SqLiteAppDbContext : AppDbContextBase
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
    }
}