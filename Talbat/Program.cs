using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Writers;
using Talabat.Repository.Data;

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
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
           WebApplicationBuilder.Services.AddEndpointsApiExplorer();
           WebApplicationBuilder.Services.AddSwaggerGen();
            WebApplicationBuilder.Services.AddDbContext<StoreContext>(options =>
            {
                options.UseSqlServer(WebApplicationBuilder.Configuration.GetConnectionString("DefaultConnection")); 
            });
            #endregion
            var app = WebApplicationBuilder.Build();
              using var scope = app.Services.CreateScope();
                var services = scope.ServiceProvider;
                var _dbContext = services.GetRequiredService<StoreContext>();
            // Ask Clr for Creating Object from DbContext Explicitly 

            var loggerFactory =services.GetRequiredService<ILoggerFactory>();

            try
            {
                await _dbContext.Database.MigrateAsync(); // Update-Database
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
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();
            #endregion
            app.Run();
        }
    }
}