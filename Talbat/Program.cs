namespace Talbat
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var WebApplicationWebApplicationBuilder= WebApplication.CreateBuilder(args);

            // Add services to DI the container.
            #region Configure Services
            WebApplicationWebApplicationBuilder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            WebApplicationWebApplicationBuilder.Services.AddEndpointsApiExplorer();
            WebApplicationWebApplicationBuilder.Services.AddSwaggerGen();
            #endregion
            var app = WebApplicationWebApplicationBuilder.Build();
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