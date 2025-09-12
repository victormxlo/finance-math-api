using AutoMapper;
using FinanceMath.Api.Contracts.Responses;
using FinanceMath.Application.Exercises.Dtos;

namespace FinanceMath.Api.Mappings
{
    public class ExercisesMappingProfile : Profile
    {
        public ExercisesMappingProfile()
        {
            CreateMap<ValidateExerciseAnswerDto, ValidateExerciseAnswerResponse>();
        }
    }
}