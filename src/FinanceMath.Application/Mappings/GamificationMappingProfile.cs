using AutoMapper;
using FinanceMath.Application.Gamification.Achievements.Dtos;
using FinanceMath.Application.Gamification.Challenges.Dtos;
using FinanceMath.Application.Gamification.Levels.Dtos;
using FinanceMath.Application.Gamification.Profiles.Dtos;
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
                .ForMember(dest => dest.Username,
                    opt => opt.MapFrom(src => src.User.Username))
                .ForMember(dest => dest.LevelId,
                    opt => opt.MapFrom(src => src.Level.Id))
                .ForMember(dest => dest.LevelName,
                    opt => opt.MapFrom(src => src.Level.Name))
                .ForMember(dest => dest.AchievementsIds,
                    opt => opt.MapFrom(src => src.Achievements.Select(a => a.Achievement.Id)))
                .ForMember(dest => dest.ChallengeProgressesIds,
                    opt => opt.MapFrom(src => src.ChallengeProgresses.Select(c => c.Challenge.Id)));

            CreateMap<Level, LevelDto>()
                .ReverseMap();

            CreateMap<Achievement, AchievementDto>()
                .ReverseMap();

            CreateMap<UserAchievementProgress, UserAchievementProgressDto>()
                .ForMember(dest => dest.ProfileId,
                    opt => opt.MapFrom(src => src.GamificationProfile.Id))
                .ForMember(dest => dest.UserId,
                    opt => opt.MapFrom(src => src.GamificationProfile.User.Id))
                .ForMember(dest => dest.AchievementId,
                    opt => opt.MapFrom(src => src.Achievement.Id))
                .ForMember(dest => dest.AchievementName,
                    opt => opt.MapFrom(src => src.Achievement.Name))
                .ForMember(dest => dest.CriteriaKey,
                    opt => opt.MapFrom(src => src.Achievement.CriteriaKey));

            CreateMap<UserChallengeProgress, UserChallengeProgressDto>()
                .ForMember(dest => dest.ProfileId,
                    opt => opt.MapFrom(src => src.GamificationProfile.Id))
                .ForMember(dest => dest.UserId,
                    opt => opt.MapFrom(src => src.GamificationProfile.User.Id))
                .ForMember(dest => dest.ChallengeId,
                    opt => opt.MapFrom(src => src.Challenge.Id))
                .ForMember(dest => dest.ChallengeName,
                    opt => opt.MapFrom(src => src.Challenge.Name))
                .ForMember(dest => dest.ChallengeDescription,
                    opt => opt.MapFrom(src => src.Challenge.Description))
                .ForMember(dest => dest.CriteriaKey,
                    opt => opt.MapFrom(src => src.Challenge.CriteriaKey));

            CreateMap<Challenge, ChallengeDto>()
                .ReverseMap();
        }
    }
}
