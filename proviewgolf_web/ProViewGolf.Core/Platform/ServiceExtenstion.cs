using System;
using Microsoft.Extensions.DependencyInjection;
using ProViewGolf.Core.Services;

namespace ProViewGolf.Core.Platform
{
    public static class ServiceExtenstion
    {
        public static IServiceCollection AddCoreServices(this IServiceCollection services)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));

            services.AddAutoMapper(cfg => cfg.AddProfile(new AutoMapperProfile()));

            // register all of your services here
            services.AddScoped<ClubPracticeService>();
            services.AddScoped<EquipmentService>();
            services.AddScoped<GameService>();
            services.AddScoped<InstructorService>();
            services.AddScoped<ReportService>();
            services.AddScoped<SessionService>();
            services.AddScoped<ShotPracticeService>();
            services.AddScoped<SkillService>();
            services.AddScoped<StatisticsService>();
            services.AddScoped<UserService>();
            services.AddScoped<ReviewService>();
            services.AddScoped<BookingService>();

            return services;
        }
    }
}