using Microsoft.Extensions.DependencyInjection;

namespace FinanceMath.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(c =>
            {
                c.RegisterServicesFromAssembly(typeof(ApplicationAssemblyMarker).Assembly);
            });

            services.AddAutoMapper(typeof(ApplicationAssemblyMarker).Assembly);

            return services;
        }
    }
}
