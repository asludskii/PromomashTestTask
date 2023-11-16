using PromomashTestTask.Data.Repositories.Interfaces;

namespace PromomashTestTask.Data.UnitOfWork;

/// <summary>
/// Unit of work interface.
/// </summary>
public interface IUnitOfWork
{
    IRepository<T> GetRepositoryAsync<T>() where T : class;
    Task<bool> SaveChangesAsync();
    void DiscardChangesAsync();
}