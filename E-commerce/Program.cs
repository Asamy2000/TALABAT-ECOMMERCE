using E_commerce.Core.Entities.identity;
using E_commerce.Extensions;
using E_commerce.Middlewares;
using E_commerce.Repos.Data;
using E_commerce.Repos.identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;

namespace E_commerce
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            #region Configure Services
            builder.Services.AddControllers();
            builder.Services.AddSwaggerServices();
            //Configuration is a collection of app Configurations
            //register EcommerceContext 
            builder.Services.AddDbContext<EcommerceContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            //register AppIdentityDbContext
            builder.Services.AddDbContext<AppIdentityDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("identityConnection"));
            });

            builder.Services.AddSingleton<IConnectionMultiplexer>(S =>
            {
                var connection = builder.Configuration.GetConnectionString("Redis");
                return ConnectionMultiplexer.Connect(connection);
            });

            //ApplicationServeciesExtensions.AddApplicationServices(builder.Services);
            builder.Services.AddApplicationServices();
            builder.Services.AddIdentityServices(builder.Configuration);

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("ApiPolicy", options =>
                {
                    options.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();
                });
            });

            #endregion

            var app = builder.Build();


            #region Migrate database
            //creates a new scope for dependency injection using the CreateScope() method.
            // Creating a scope allows you to control the lifetime of objects and services within that scope.
            using var scope = app.Services.CreateScope();
            //reference to the service provider associated with the created scope
            //The service provider is responsible for resolving and providing instances of services registered in the application's dependency injection container.
            var services = scope.ServiceProvider;
            //obtains an ILoggerFactory instance from the dependency injection container.
            //logger factory is used to create loggers that you can use for logging messages and exceptions.
            var loggerFactory = services.GetRequiredService<ILoggerFactory>();
            try
            {
                //explicitly CLR create an obj from EcommerceContext
                var dbContext = services.GetRequiredService<EcommerceContext>();
                //now apply migrations
                await dbContext.Database.MigrateAsync();
                //seed data
                await EcommerceContextSeed.SeedAsync(dbContext);

                var identityContext = services.GetRequiredService<AppIdentityDbContext>();
                await identityContext.Database.MigrateAsync();
                var UserManager = services.GetRequiredService<UserManager<AppUser>>();
                await AppIdentityDbContextSeed.SeedUsersAsync(UserManager);
            }
            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<Program>();
                logger.LogError(ex, ex.Message);
            }

            #endregion



            #region Configure kestrel middlewares
            app.UseMiddleware<ExceptionMiddleware>();
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwaggerMiddleWares();
            }
            app.UseStatusCodePagesWithReExecute("/errors/{0}");
            app.UseHttpsRedirection();


            app.UseStaticFiles();
            //allow cross domain requests 
            app.UseCors("ApiPolicy");
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();
            #endregion

            app.Run();
        }
    }
}