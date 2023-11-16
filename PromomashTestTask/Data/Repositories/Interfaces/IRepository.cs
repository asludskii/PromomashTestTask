namespace PromomashTestTask.Data.Repositories.Interfaces;

/// <summary>
/// Generic repository interface.
/// </summary>
/// <typeparam name="T">Data entity type.</typeparam>
public interface IRepository<T> where T : class
{
    Task<IEnumerable<T>> GetAllAsync(bool tracking = true);
    Task<T?> GetByIdAsync(Guid id);
    Task<bool> InsertAsync(T value);
    bool Update(T value);
    Task<bool> DeleteAsync(Guid id);
}