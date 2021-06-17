using System;
using Microsoft.Extensions.DependencyInjection;
using ProViewGolf.Core.Dbo;
using ProViewGolf.Core.Platform;
using ProViewGolf.DataLayer;
using AutoMapper;

namespace ProViewGolf.Platform
{
    public static class ServiceExtenstion
    {
        public static IServiceCollection AddProGolfServices(this IServiceCollection services)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));
              
         services.AddDbContext<IProGolfContext, ProGolfContext>();

            services.AddAutoMapper(cfg => cfg.AddProfile(new AutoMapperProfile()));

            // register all of your services here
            services.AddCoreServices();

            return services;
        }
    }
}
