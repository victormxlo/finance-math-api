using AutoMapper;
using FinanceMath.Application.Content.Contents.Dtos;
using FinanceMath.Domain.GamificationAggregate;

namespace FinanceMath.Application.Mappings
{
    public class ContentMappingProfile : Profile
    {
        public ContentMappingProfile()
        {
            CreateMap<Domain.ContentAggregate.Content, ContentDto>()
                .ForMember(dest => dest.ExerciseIds,
                    opt => opt.MapFrom(src => src.ContentExercises.Select(e => e.Exercise.Id)))
                .ForMember(dest => dest.SectionIds,
                    opt => opt.MapFrom(src => src.Sections.Select(s => s.Id)))
                .ReverseMap();

            CreateMap<UserContentProgress, UserContentProgressDto>()
                .ForMember(dest => dest.ProfileId,
                    opt => opt.MapFrom(src => src.GamificationProfile.Id))
                .ForMember(dest => dest.UserId,
                    opt => opt.MapFrom(src => src.GamificationProfile.User.Id))
                .ForMember(dest => dest.ContentId,
                    opt => opt.MapFrom(src => src.Content.Id))
                .ForMember(dest => dest.ContentTitle,
                    opt => opt.MapFrom(src => src.Content.Title))
                .ForMember(dest => dest.CategoryId,
                    opt => opt.MapFrom(src => src.Content.Category.Id))
                .ForMember(dest => dest.CategoryName,
                    opt => opt.MapFrom(src => src.Content.Category.Name));
        }
    }
}
