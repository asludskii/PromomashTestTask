using System.Text;
#pragma warning disable CS8618

namespace PromomashTestTask.Data.SQLite;

/// <summary>
/// Holds options for SQLite connection.
/// </summary>
public class SqLiteConnectionOptions
{
    public string DataSource { get; set; }
    public string Password { get; set; }

    public string ConnectionString
    {
        get
        {
            var sb = new StringBuilder();
            sb.Append($"Data Source={DataSource};");

            if (!string.IsNullOrWhiteSpace(Password))
                sb.Append($"Password={Password};");

            return sb.ToString();
        }
    }
}