using AutoMapper;
using FinanceMath.Application.Content.Exercises.Dtos;
using FinanceMath.Domain.ContentAggregate;

namespace FinanceMath.Application.Mappings
{
    public class ExerciseMappingProfile : Profile
    {
        public ExerciseMappingProfile()
        {
            CreateMap<Domain.ContentAggregate.Exercise, ExerciseDto>()
                .ForMember(dest => dest.Options,
                    opt => opt.MapFrom(src => src.Options.OrderBy(o => o.Order)))
                .ForMember(dest => dest.ContentIds,
                    opt => opt.MapFrom(src => src.ContentExercises.Select(ce => ce.Id)))
                .ReverseMap();

            CreateMap<ExerciseOption, ExerciseOptionPublicDto>()
                .ForMember(d => d.Description, opt => opt.MapFrom(s => s.Description))
                .ForMember(d => d.Order, opt => opt.MapFrom(s => s.Order));

            CreateMap<ExerciseHint, ExerciseHintDto>()
                .ForMember(dest => dest.ExerciseId,
                    opt => opt.MapFrom(src => src.Exercise.Id))
                .ReverseMap();
        }
    }
}
