#pragma warning disable CS8618
namespace PromomashTestTask.Models;

public class PolicyVersion
{
    public Guid Id { get; set; }
    public string PolicyText { get; set; }
    public DateTime CreatedDateTimeUtc { get; set; }

    #region Relations

    public ICollection<ApplicationUser> Users { get; set; }

    #endregion
}