using Application.MappingProfiles;
using Application.Presistence.Contracts;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Persistence.Configuration.Entities;
using Persistence.DatabaseContext;
using Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Persistence
{
    public static class DependancyInjection
    {
        public static IServiceCollection ConfigurePersistence(this IServiceCollection services, IConfiguration configuration)
        {
            // configure DateBase
            services.AddDbContext<ApplicationDbContext>(option =>
            {
                option.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });

            //configure
            services.AddScoped(typeof(IGenericRepository<>),typeof(GenericRepository<>));
            services.AddScoped<IMessageRepository, MessageRepository>();

            //configure token
            services.AddScoped<ITokenServices, TokenServices>();

            //configure identity
            services.AddIdentity<AppUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();
            services.AddMemoryCache();
            services.AddAuthentication(opt =>
            {
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(options => 
                {
                    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = false,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Token:Key"])),
                        ValidIssuer = configuration["Token:Issuer"]

                    };
                });

            return services;
        }
        public static async void ConfigMiddleware(this IApplicationBuilder app)
        {
            using (var scope=app.ApplicationServices.CreateScope())
            {
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                await IdentitySeed.SeedUserAsync(userManager, roleManager);
            }
        }
    }
}
