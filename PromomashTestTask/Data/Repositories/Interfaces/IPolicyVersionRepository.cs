using PromomashTestTask.Models;

namespace PromomashTestTask.Data.Repositories.Interfaces;

public interface IPolicyVersionRepository : IRepository<PolicyVersion>
{
    /// <summary>
    /// Returns currently active policy version.
    /// </summary>
    Task<PolicyVersion> GetActivePolicy(bool tracking = true);
}