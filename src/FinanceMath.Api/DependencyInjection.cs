namespace FinanceMath.Api
{
    public static class DepedendencyInjection
    {
        public static IServiceCollection AddApi(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(ApiAssemblyMarker).Assembly);

            return services;
        }
    }
}