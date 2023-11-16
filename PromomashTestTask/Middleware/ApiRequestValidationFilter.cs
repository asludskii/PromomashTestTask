using FluentValidation;
using Microsoft.AspNetCore.Mvc.Filters;

namespace PromomashTestTask.Middleware;

public class ApiRequestValidationFilter<T> : IAsyncActionFilter where T : class
{
    private readonly IValidator<T> _validator;

    public ApiRequestValidationFilter(IValidator<T> validator)
    {
        _validator = validator;
    }

    #region Implementation of IAsyncActionFilter

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        await next();
    }

    #endregion
}