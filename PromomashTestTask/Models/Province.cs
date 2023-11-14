#pragma warning disable CS8618
namespace PromomashTestTask.Models;

public class Province
{
    public Guid Id { get; set; }
    public string Name { get; set; }

    #region Relations

    public Guid CountryId { get; set; }
    public Country Country { get; set; }

    public ICollection<ApplicationUser> Users { get; set; }

    #endregion
}