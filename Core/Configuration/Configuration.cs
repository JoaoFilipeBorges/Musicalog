using Microsoft.Extensions.DependencyInjection;

namespace Core
{
    public static class Configuration
    {
        public static IServiceCollection AddHandlers(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Configuration).Assembly));

            return services;
        }
    }
}
