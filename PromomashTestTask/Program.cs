using System.Reflection;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using PromomashTestTask.Data;
using PromomashTestTask.Data.Repositories;
using PromomashTestTask.Data.Repositories.Interfaces;
using PromomashTestTask.Data.SQLite;
using PromomashTestTask.Data.UnitOfWork;
using PromomashTestTask.Models;
using PromomashTestTask.Validation.Identity;

namespace PromomashTestTask
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            ConfigureOptions(builder.Services);
            ConfigureDbContext(builder.Configuration, builder.Services);
            ConfigureIdentity(builder.Services);
            ConfigureServices(builder.Services);
            ConfigureFluentValidation(builder.Services);
            ConfigureCaching(builder.Services);
            ConfigureLogging(builder.Services);

            if (builder.Environment.IsDevelopment())
            {
                builder.Services.AddSwaggerGen();
            }

            var app = builder.Build();

            ApplyMigrations(app.Services);

            if (!app.Environment.IsDevelopment())
            {
                app.UseHttpsRedirection();
            }

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseStaticFiles();
            app.UseRouting();

            app.UseAuthentication();
            app.UseIdentityServer();
            app.UseAuthorization();

            app.MapControllers();
            app.MapRazorPages();
            app.MapFallbackToFile("index.html");

            app.UseResponseCaching();

            app.Run();
        }

        private static void ConfigureLogging(IServiceCollection services)
        {
            services.AddLogging();
        }

        private static void ConfigureCaching(IServiceCollection services)
        {
            services.AddResponseCaching(options =>
            {
                options.UseCaseSensitivePaths = false;
            });
        }

        private static void ConfigureFluentValidation(IServiceCollection services)
        {
            services.AddFluentValidationAutoValidation();
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly(), ServiceLifetime.Transient);
        }

        private static void ApplyMigrations(IServiceProvider services)
        {
            using var scope = services.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            dbContext.Database.Migrate();
        }

        private static void ConfigureOptions(IServiceCollection services)
        {
            services.ConfigureOptions<SqLiteConnectionOptionsSetup>();
        }

        private static void ConfigureIdentity(IServiceCollection services)
        {
            services.AddDefaultIdentity<ApplicationUser>(options =>
                {
                    options.User.RequireUniqueEmail = true;
                    options.SignIn.RequireConfirmedAccount = false;
                    options.SignIn.RequireConfirmedEmail = false;
                    options.SignIn.RequireConfirmedPhoneNumber = false;

                    // Password validation is handled in a dedicated class, so all default checks are turned off
                    options.Password.RequireDigit = false;
                    options.Password.RequireLowercase = false;
                    options.Password.RequireUppercase = false;
                    options.Password.RequiredLength = 0;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequiredUniqueChars = 0;
                })
                .AddEntityFrameworkStores<SqLiteAppDbContext>()
                .AddPasswordValidator<PasswordValidator>();

            services.AddIdentityServer()
                .AddApiAuthorization<ApplicationUser, SqLiteAppDbContext>();

            services.AddAuthentication()
                .AddIdentityServerJwt();
        }

        private static void ConfigureDbContext(IConfiguration config, IServiceCollection services)
        {
            var dbConnection = config.GetRequiredSection("DatabaseConnection").Value;

            switch (dbConnection)
            {
                case "SQLite":
                {
                    services.AddDbContext<AppDbContext, SqLiteAppDbContext>();
                    break;
                }
                default:
                {
                    throw new ArgumentOutOfRangeException("DatabaseConnection",
                        $"Invalid DatabaseConnection config value: {dbConnection}.");
                }
            }
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddRazorPages();

            // Add base unit of work
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            // Add repositories
            services.AddScoped<IRepository<Country>, CountryRepository>();
            services.AddScoped<IProvinceRepository, ProvinceRepository>();
            services.AddScoped<IPolicyVersionRepository, PolicyVersionRepository>();
        }
    }
}