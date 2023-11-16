using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PromomashTestTask.Data;
using PromomashTestTask.Data.Repositories.Interfaces;
using PromomashTestTask.Models;

namespace PromomashTestTask.Controllers;

/// <summary>
/// Provides methods to access Country and Province collections.
/// </summary>
[ApiController]
public class CountriesController : ControllerBase
{
    private readonly IServiceProvider _serviceProvider;

    public CountriesController(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
    }

    /// <summary>
    /// Returns a list of countries, sorted alphabetically.
    /// Cached.
    /// </summary>
    [HttpGet("[controller]")]
    [ResponseCache(Duration = 60)]
    public async Task<IActionResult> GetAll()
    {
        var countriesRepo = _serviceProvider.GetRequiredService<IRepository<Country>>();
        var countries = await countriesRepo.GetAllAsync(false);
        var result = countries
            .OrderBy(x => x.Name)
            .Select(x => new {x.Id, x.Name});

        return Ok(result);
    }

    /// <summary>
    /// Returns a list of provinces by <param name="countryId"></param>, sorted alphabetically.
    /// Cached. 
    /// </summary>
    [HttpGet("[controller]/{countryId:guid}/provinces")]
    [ResponseCache(Duration = 60, VaryByQueryKeys = new [] {"countryId"})]
    public async Task<IActionResult> GetCountryProvinces([FromRoute] Guid countryId)
    {
        var countriesRepo = _serviceProvider.GetRequiredService<IRepository<Country>>();
        var country = await countriesRepo.GetByIdAsync(countryId);

        if (country is null)
            return NotFound();

        var provincesRepo = _serviceProvider.GetRequiredService<IProvinceRepository>();
        var provinces = await provincesRepo.GetByCountryIdAsync(countryId, false);
        var result = provinces
            .OrderBy(x => x.Name)
            .Select(x => new {x.Id, x.Name});

        return Ok(result);
    }
}