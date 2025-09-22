using AutoMapper;
using FinanceMath.Application.Gamification.Achievements.Commands;
using FinanceMath.Application.Gamification.Levels.Commands;
using FinanceMath.Application.Gamification.Profiles.Commands;

namespace FinanceMath.Api.Contracts.Requests
{
    public class GamificationMappingProfile : Profile
    {
        public GamificationMappingProfile()
        {
            CreateMap<UpdateGamificationProfileRequest, UpdateGamificationProfileCommand>();

            CreateMap<UpdateLevelRequest, UpdateLevelCommand>();

            CreateMap<UpdateAchievementRequest, UpdateAchievementCommand>();
        }
    }
}