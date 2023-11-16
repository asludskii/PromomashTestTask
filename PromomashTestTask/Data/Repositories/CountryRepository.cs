using PromomashTestTask.Models;

namespace PromomashTestTask.Data.Repositories;

public class CountryRepository : RepositoryBase<Country>
{
    public CountryRepository(AppDbContext dbContext, ILogger<CountryRepository> logger) 
        : base(dbContext, logger)
    {
    }

    #region Overrides of RepositoryBase<Country>

    public override Task<bool> InsertAsync(Country value)
    {
        throw new NotImplementedException("Creating new countries will be implemented after process requirements are approved by UN.");
    }

    public override bool Update(Country value)
    {
        throw new NotImplementedException("Renaming countries should've been implemented a long time ago, this is technical debt, technically.");
    }

    public override Task<bool> DeleteAsync(Guid id)
    {
        throw new NotSupportedException("This sounds completely illegal.");
    }

    #endregion
}