using Application;
using Microsoft.OpenApi.Models;
using Persistence;
using System.Reflection;
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
            builder.Services.AddSwaggerGen(s =>
            {
                var securitySchema = new OpenApiSecurityScheme()
                {
                    Name="Authorization",
                    Description="JWt Auth Bearer",
                    In=ParameterLocation.Header,
                    Type=SecuritySchemeType.Http,
                    Scheme= "bearer",
                    Reference =new OpenApiReference()
                    {
                        Id= "Bearer",
                        Type=ReferenceType.SecurityScheme,
                    }
                };
                s.AddSecurityDefinition("Bearer", securitySchema);

                var securityRequirement = new OpenApiSecurityRequirement { { securitySchema, new[] { "Bearer" } } };
                s.AddSecurityRequirement(securityRequirement);


                //xml
                var xmlFile=$"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath=Path.Combine(AppContext.BaseDirectory, xmlFile);    
                s.IncludeXmlComments(xmlPath);
            });


            // Configure External Project (dll)
            builder.Services.ConfigureApplication();
            builder.Services.ConfigurePersistence(builder.Configuration);

            //Enable Cors
            builder.Services.AddCors(option =>
            {
                option.AddPolicy("CorsPolicy", option =>
                {
                    option.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:4200");
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
            app.UseCors("CorsPolicy");

            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();
            Persistence.DependancyInjection.ConfigMiddleware(app);
            app.Run();
        }
    }
}
