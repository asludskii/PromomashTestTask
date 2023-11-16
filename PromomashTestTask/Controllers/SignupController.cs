using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.EntityFrameworkCore;
using PromomashTestTask.Data;
using PromomashTestTask.Data.Repositories.Interfaces;
using PromomashTestTask.DTOs.APIRequests;
using PromomashTestTask.Models;

namespace PromomashTestTask.Controllers;

[ApiController]
public class SignupController : ControllerBase
{
    private readonly IServiceProvider _serviceProvider;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ILogger<SignupController> _logger;

    public SignupController(
        IServiceProvider serviceProvider,
        UserManager<ApplicationUser> userManager,
        ILogger<SignupController> logger)
    {
        _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
        _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    [HttpPost("[controller]")]
    public async Task<IActionResult> PostNew([FromBody] SignupNewRequestDto requestDto)
    {
        var policyVersionRepo = _serviceProvider.GetRequiredService<IPolicyVersionRepository>();
        var activePolicy = await policyVersionRepo.GetActivePolicy(false);

        var user = new ApplicationUser
        {
            UserName = requestDto.Login,
            Email = requestDto.Login,
            ProvinceId = requestDto.ProvinceId,
            PolicyAcceptDatetimeUtc = DateTime.UtcNow,
            PolicyVersionId = activePolicy.Id
        };

        var createResult = await _userManager.CreateAsync(user, requestDto.Password);

        if (createResult.Succeeded)
        {
            user = await _userManager.FindByEmailAsync(requestDto.Login);
            return Created("/no/URLs/for/now", new {user.Id, user.UserName});
        }

        var errors = createResult.Errors.Select(x => x.Description).ToArray();
        _logger.LogError($"Failed to create a user. Errors: {string.Join(";", errors)}");
        return UnprocessableEntity();
    }
}