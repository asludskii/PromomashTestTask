#pragma warning disable CS8618
namespace PromomashTestTask.Models;

public class Country
{
    public Guid Id { get; set; }
    public string Name { get; set; }

    #region Relations

    public ICollection<Province>? Provinces { get; set; }

    #endregion
}