using AutoMapper;
using FinanceMath.Application.Gamification.Dtos;
using FinanceMath.Domain.GamificationAggregate;

namespace FinanceMath.Application.Mappings
{
    public class GamificationMappingProfile : Profile
    {
        public GamificationMappingProfile()
        {
            CreateMap<GamificationProfile, GamificationProfileDto>()
                .ForMember(dest => dest.UserId,
                    opt => opt.MapFrom(src => src.User.Id))
                .ForMember(dest => dest.AchievementsIds,
                    opt => opt.MapFrom(src => src.Achievements.Select(a => a.Achievement.Id)));
        }
    }
}
