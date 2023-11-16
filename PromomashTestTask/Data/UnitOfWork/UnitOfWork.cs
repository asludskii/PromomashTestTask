using PromomashTestTask.Data.Repositories.Interfaces;

namespace PromomashTestTask.Data.UnitOfWork;

/// <summary>
/// Unit of work implementation. One day, it will be used for something.
/// </summary>
public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _dbContext;
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<UnitOfWork> _logger;

    public UnitOfWork(AppDbContext dbContext, IServiceProvider serviceProvider, ILogger<UnitOfWork> logger)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    #region Implementation of IUnitOfWork

    public IRepository<T> GetRepositoryAsync<T>() where T : class
    {
        return _serviceProvider.GetRequiredService<IRepository<T>>();
    }

    public async Task<bool> SaveChangesAsync()
    {
        try
        {
            await _dbContext.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error saving changes in unit of work.");
            return false;
        }
    }

    public void DiscardChangesAsync()
    {
        _dbContext.ChangeTracker.Clear();
    }

    #endregion
}