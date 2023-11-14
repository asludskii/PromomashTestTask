using Microsoft.AspNetCore.Identity;
#pragma warning disable CS8618

namespace PromomashTestTask.Models
{
    public class ApplicationUser : IdentityUser
    {
        public DateTime PolicyAcceptDatetimeUtc { get; set; }

        #region Relations

        public Guid PolicyVersionId { get; set; }
        public PolicyVersion PolicyVersion { get; set; }
        public Guid ProvinceId { get; set; }
        public Province Province { get; set; }

        #endregion
    }
}