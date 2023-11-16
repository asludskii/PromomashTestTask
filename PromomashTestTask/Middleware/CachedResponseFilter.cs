using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Caching.Distributed;

namespace PromomashTestTask.Middleware;

public class CachedResponseFilter : IAsyncActionFilter
{
    private readonly IDistributedCache _cache;

    public CachedResponseFilter(IDistributedCache cache)
    {
        _cache = cache;
    }

    #region Implementation of IAsyncActionFilter

    public Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        return next.Invoke();
    }

    #endregion
}