using AutoMapper;
using FinanceMath.Application.Content.Contents.Dtos;

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
        }
    }
}
