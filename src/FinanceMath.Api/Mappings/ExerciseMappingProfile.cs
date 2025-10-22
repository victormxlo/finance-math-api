using AutoMapper;
using FinanceMath.Api.Contracts.Responses;
using FinanceMath.Application.Content.Exercises.Dtos;

namespace FinanceMath.Api.Mappings
{
    public class ExerciseMappingProfile : Profile
    {
        public ExerciseMappingProfile()
        {
            CreateMap<ValidateExerciseAnswerDto, ValidateExerciseAnswerResponse>();
        }
    }
}