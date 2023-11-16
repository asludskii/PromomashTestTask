using Microsoft.EntityFrameworkCore;
using PromomashTestTask.Data.Repositories.Interfaces;
using PromomashTestTask.Models;

namespace PromomashTestTask.Data.Repositories;

public class PolicyVersionRepository : RepositoryBase<PolicyVersion>, IPolicyVersionRepository
{
    public PolicyVersionRepository(AppDbContext dbContext, ILogger<PolicyVersionRepository> logger) 
        : base(dbContext, logger)
    {
    }

    #region Implementation of IPolicyVersionRepository 

    public async Task<PolicyVersion> GetActivePolicy(bool tracking = true)
    {
        return await _dbSet
            .OrderByDescending(x => x.CreatedDateTimeUtc)
            .FirstAsync();
    }

    #endregion


    #region Overrides of RepositoryBase<PolicyVersion>

    public override Task<bool> InsertAsync(PolicyVersion value)
    {
        throw new NotImplementedException("Creating new policy versions will be implemented in near future. " +
                                          "It's already in the backlog, I promise.");
    }

    public override bool Update(PolicyVersion value)
    {
        throw new NotSupportedException("Modifying existing policies is forbidden, please create a new version instead.");
    }

    public override Task<bool> DeleteAsync(Guid id)
    {
        throw new NotSupportedException("Deleting existing policy versions is forbidden.");
    }

    #endregion
}