using System.Security;
using Microsoft.Extensions.Options;

namespace PromomashTestTask.Data.SQLite;

/// <summary>
/// Setup class for <see cref="IOptions{SQLiteConnectionOptions}"/>.
/// Performs validation of values from configuration.
/// </summary>
public class SqLiteConnectionOptionsSetup : IConfigureOptions<SqLiteConnectionOptions>
{
    private readonly IConfiguration _configuration;

    public SqLiteConnectionOptionsSetup(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    #region Implementation of IConfigureOptions<in SQLiteConnectionOptions>

    public void Configure(SqLiteConnectionOptions options)
    {
        var databaseConnection = _configuration.GetRequiredSection("DatabaseConnection").Value;

        // If a different DB provider is used, we don't care about SQLite config
        if (!databaseConnection.Equals("SQLite"))
            return;

        var sqliteSection = _configuration.GetRequiredSection("DatabaseOptions").GetRequiredSection("SQLite");
        var dataSource = sqliteSection.GetValue<string>("DataSource");

        if (string.IsNullOrWhiteSpace(dataSource))
            throw new ArgumentNullException(dataSource,
                "DataSource can't be empty. Provide a valid file path or ':memory:'.");

        if (!dataSource.Equals(":memory:"))
        {
            try
            {
                // If new FileInfo fails, there are problems with this file path
                var _ = new FileInfo(dataSource).FullName;
            }
            catch (PathTooLongException)
            {
                throw new ArgumentException("DataSource path is too long.");
            }
            catch (Exception ex) when (ex is SecurityException or UnauthorizedAccessException)
            {
                throw new ArgumentException("Access denied to DataSource path", ex);
            }
            catch  (Exception ex)
            {
                throw new ArgumentException("DataSource path is invalid.", ex);
            }
        }
        
        sqliteSection.Bind(options);
    }

    #endregion
}