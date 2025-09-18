using FinanceMath.Application.Interfaces;
using FinanceMath.Domain.Repositories;
using FinanceMath.Infrastructure.Data;
using FinanceMath.Infrastructure.Persistence.Repositories;
using FinanceMath.Infrastructure.Security;
using FinanceMath.Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FinanceMath.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IContentRepository, ContentRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IExerciseRepository, ExerciseRepository>();
            services.AddScoped<IContentSectionRepository, ContentSectionRepository>();
            services.AddScoped<IGamificationProfileRepository, GamificationProfileRepository>();
            services.AddScoped<ILevelRepository, LevelRepository>();
            services.AddScoped<IAchievementRepository, AchievementRepository>();
            services.AddScoped<IChallengeRepository, ChallengeRepository>();
            services.AddScoped<IPasswordHasher, BCryptPasswordHasher>();
            services.AddSingleton<IJwtProvider, JwtProvider>();
            services.AddScoped<IGamificationService, GamificationService>();

            var sessionFactory = NHibernateHelper.CreateSessionFactory(
                configuration.GetConnectionString("FinanceMathDb")!);
            services.AddSingleton(sessionFactory);
            services.AddScoped(factory => sessionFactory.OpenSession());

            return services;
        }
    }
}
