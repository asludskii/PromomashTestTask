using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using PromomashTestTask.Data;
using PromomashTestTask.Data.SQLite;
using PromomashTestTask.Identity;
using PromomashTestTask.Models;

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

            app.Run();
        }

        private static void ApplyMigrations(IServiceProvider services)
        {
            using var scope = services.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContextBase>();
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
                    services.AddDbContext<AppDbContextBase, SqLiteAppDbContext>();
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
        }
    }
}