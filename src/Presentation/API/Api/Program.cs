using Application;
using Persistence;
namespace Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();


            // Configure External Project (dll)
            builder.Services.ConfigureApplication();
            builder.Services.ConfigurePersistence(builder.Configuration);

            //Enable Cors
            builder.Services.AddCors(option =>
            {
                option.AddPolicy("CorsPolicy", option =>
                {
                    option.AllowAnyHeader().AllowAnyMethod().WithOrigins("https://localhost:4200");
                });
            });
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();
            app.UseCors("CorsPolicy");

            app.MapControllers();

            app.Run();
        }
    }
}
