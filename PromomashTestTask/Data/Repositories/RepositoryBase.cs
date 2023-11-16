using Microsoft.EntityFrameworkCore;
using PromomashTestTask.Data.Repositories.Interfaces;

namespace PromomashTestTask.Data.Repositories;

/// <summary>
/// Base generic repository for data entities.
/// </summary>
public abstract class RepositoryBase<T> : IRepository<T> where T : class
{
    protected readonly DbSet<T> _dbSet;
    protected readonly ILogger _logger;

    protected RepositoryBase(AppDbContext dbContext, ILogger logger)
    {
        _dbSet = dbContext.Set<T>() ?? throw new ArgumentNullException(nameof(dbContext));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    #region Implementation of IRepository<T>

    public virtual async Task<IEnumerable<T>> GetAllAsync(bool tracking = true)
    {
        try
        {
            if (tracking)
            {
                return await _dbSet.ToListAsync();
            }
            else
            {
                return await _dbSet.AsNoTracking().ToListAsync();
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error in repository method {nameof(GetAllAsync)} for entity {typeof(T).FullName}");
            return Enumerable.Empty<T>().ToList();
        }
    }

    public virtual async Task<T?> GetByIdAsync(Guid id)
    {
        try
        {
            return await _dbSet.FindAsync(id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error in repository method {nameof(GetByIdAsync)} for entity {typeof(T).FullName}");
            return null;
        }
    }

    public virtual async Task<bool> InsertAsync(T value)
    {
        try
        {
            await _dbSet.AddAsync(value);
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error in repository method {nameof(InsertAsync)} for entity {typeof(T).FullName}");
            return false;
        }
    }

    public virtual bool Update(T value)
    {
        try
        {
            _dbSet.Update(value);
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error in repository method {nameof(Update)} for entity {typeof(T).FullName}");
            return false;
        }
    }

    public virtual async Task<bool> DeleteAsync(Guid id)
    {
        try
        {
            var entity = await _dbSet.FindAsync(id);

            if (entity is not null)
            {
                _dbSet.Remove(entity);
                return true;
            }

            _logger.LogWarning($"Entity of type {typeof(T)} with id {id} not found for deletion.");
            return false;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error in repository method {nameof(DeleteAsync)} for entity {typeof(T).FullName} with ID {id}");
            return false;
        }
    }

    #endregion
}