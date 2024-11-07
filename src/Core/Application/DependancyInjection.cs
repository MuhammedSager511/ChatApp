using Application.Helper;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
    public static class DependancyInjection
    {
        public static IServiceCollection ConfigureApplication(this IServiceCollection services)
        {
            //configure AutoMapper
            services.AddAutoMapper(typeof(MappingProfiles.MappingProfile));


            //configure mediatR
            services.AddMediatR(AppDomain.CurrentDomain.GetAssemblies());

            services.AddScoped<LogUserActivity>();

            return services;
        }
    }
}
