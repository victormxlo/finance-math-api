using AutoMapper;
using FinanceMath.Application.Exercises.Dtos;
using FinanceMath.Domain.ContentAggregate;

namespace FinanceMath.Application.Mappings
{
    public class ExerciseMappingProfile : Profile
    {
        public ExerciseMappingProfile()
        {
            CreateMap<Exercise, ExerciseDto>()
                .ForMember(d => d.Options,
                    opt => opt.MapFrom(src => src.Options.OrderBy(o => o.Order)))
                .ForMember(dest => dest.ContentIds,
                    opt => opt.MapFrom(src => src.ContentExercises.Select(ce => ce.Id)))
                .ReverseMap();

            CreateMap<ExerciseOption, ExerciseOptionPublicDto>()
                .ForMember(d => d.Description, opt => opt.MapFrom(s => s.Description))
                .ForMember(d => d.Order, opt => opt.MapFrom(s => s.Order));

        }
    }
}
