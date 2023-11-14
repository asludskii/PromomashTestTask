using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace PromomashTestTask.Development;

/// <summary>
/// Filter for methods designed for debug and diagnostics in non-production environments.
/// </summary>
public class DevOnlyAttrbute : Attribute, IResourceFilter
{
    #region Implementation of IResourceFilter

    public void OnResourceExecuting(ResourceExecutingContext context)
    {
        var env = context.HttpContext.RequestServices.GetService<IHostEnvironment>();

        if (!env.IsDevelopment())
            context.Result = new NotFoundResult();
    }

    public void OnResourceExecuted(ResourceExecutedContext context)
    {
    }

    #endregion
}