using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Writers;
using Talabat.Core.Repoisteries.Contract;
using Talabat.Repository;
using Talabat.Repository.Data;
using Talbat.Errors;
using Talbat.Extensions;
using Talbat.Helpers;
using Talbat.MiddleWares;

namespace Talbat
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var WebApplicationBuilder= WebApplication.CreateBuilder(args);

            // Add services to DI the container.
            #region Configure Services
          WebApplicationBuilder.Services.AddControllers();
         WebApplicationBuilder.Services.AddSwaggerServices();
            WebApplicationBuilder.Services.AddDbContext<StoreContext>(options =>
            {
                options.UseSqlServer(WebApplicationBuilder.Configuration.GetConnectionString("DefaultConnection")); 
            });
            
            WebApplicationBuilder.Services.AddApplicationServices(); 
            #endregion
              var app = WebApplicationBuilder.Build();
            app.UseMiddleware<ExceptionMiddleWares>();
            using var scope = app.Services.CreateScope();
                var services = scope.ServiceProvider;
                var _dbContext = services.GetRequiredService<StoreContext>();
            // Ask Clr for Creating Object from DbContext Explicitly 

            var loggerFactory =services.GetRequiredService<ILoggerFactory>();

            try
            {
                await _dbContext.Database.MigrateAsync(); // Update-Database
                await StoreContextSeed.SeedAsync(_dbContext); //Data Seeding 
            }
            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<Program>();
                logger.LogError(ex,"an error has been occured during apply the migration");  
            }
         
            #region Configure Kestrel Services
            
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
             app.UseSwaggerMiddlewares();
            }
            app.UseStatusCodePagesWithRedirects("Errors/{0}");
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseAuthorization();

            app.MapControllers();
            #endregion
            app.Run();
        }
    }
}