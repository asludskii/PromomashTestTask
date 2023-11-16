using PromomashTestTask.Models;

namespace PromomashTestTask.Data.Repositories.Interfaces;

public interface IProvinceRepository : IRepository<Province>
{
    Task<IEnumerable<Province>> GetByCountryIdAsync(Guid countryId, bool tracking = true);
}