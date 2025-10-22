using FinanceMath.Application.Interfaces;
using FinanceMath.Application.Services;
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
            services.AddScoped<IAchievementRepository, AchievementRepository>();
            services.AddScoped<IContentRepository, ContentRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IChallengeRepository, ChallengeRepository>();
            services.AddScoped<IContentSectionRepository, ContentSectionRepository>();
            services.AddScoped<IExerciseRepository, ExerciseRepository>();
            services.AddScoped<IGamificationService, GamificationService>();
            services.AddScoped<IGamificationProfileRepository, GamificationProfileRepository>();
            services.AddScoped<ILevelRepository, LevelRepository>();
            services.AddSingleton<IJwtProvider, JwtProvider>();
            services.AddScoped<IPasswordHasher, BCryptPasswordHasher>();
            services.AddScoped<IRecommendationService, RecommendationService>();
            services.AddScoped<IUserAchievementProgressRepository, UserAchievementProgressRepository>();
            services.AddScoped<IUserChallengeProgressRepository, UserChallengeProgressRepository>();
            services.AddScoped<IUserContentProgressRepository, UserContentProgressRepository>();
            services.AddScoped<IUserExerciseProgressRepository, UserExerciseProgressRepository>();
            services.AddScoped<IUserRepository, UserRepository>();

            var sessionFactory = NHibernateHelper.CreateSessionFactory(
                configuration.GetConnectionString("FinanceMathDb")!);
            services.AddSingleton(sessionFactory);
            services.AddScoped(factory => sessionFactory.OpenSession());

            return services;
        }
    }
}
