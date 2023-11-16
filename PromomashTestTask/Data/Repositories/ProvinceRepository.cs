using Microsoft.EntityFrameworkCore;
using PromomashTestTask.Data.Repositories.Interfaces;
using PromomashTestTask.Models;

namespace PromomashTestTask.Data.Repositories;

public class ProvinceRepository : RepositoryBase<Province>, IProvinceRepository
{
    public ProvinceRepository(AppDbContext dbContext, ILogger<ProvinceRepository> logger) 
        : base(dbContext, logger)
    {
    }

    #region Implementation of IProvinceRepository

    public async Task<IEnumerable<Province>> GetByCountryIdAsync(Guid countryId, bool tracking = true)
    {
        var query = _dbSet.Where(x => x.CountryId == countryId);

        if (!tracking)
        {
            query = query.AsNoTracking();
        }

        return await query.ToListAsync();
    }

    #endregion

    #region Overrides of RepositoryBase<Province>

    public override Task<bool> InsertAsync(Province value)
    {
        throw new NotImplementedException("Creating new provinces will be supported when UI design of admin panel is finished.");
    }

    public override bool Update(Province value)
    {
        throw new NotImplementedException("Modifying existing provinces will be supported when UI design of admin panel is finished.");
    }

    public override Task<bool> DeleteAsync(Guid id)
    {
        throw new NotImplementedException("Deleting existing provinces will be supported when some region secedes from some country.");
    }

    #endregion
}