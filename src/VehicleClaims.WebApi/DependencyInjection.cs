using VehicleClaims.Application;
using VehicleClaims.Infrastructure;

namespace VehicleClaims.WebApi
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddWebServices(this IServiceCollection services)
        {
            services.AddApplicationServices()
                .AddInfrastructureServices();
            return services;
        }
    }
}
