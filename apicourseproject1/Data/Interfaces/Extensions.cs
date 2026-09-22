using apicourseproject1.Services;
using apicourseproject1.Data.Repositories;

namespace apicourseproject1.Data.Interfaces
{
    public static class Extensions
    {
        public static IServiceCollection RegisterDataAccessDependencies(this IServiceCollection services)
        {
            services.AddTransient<ICarRepository, CarRepository>();
            services.AddTransient<CarService, CarService>();

            return services;
        }
    }
}
