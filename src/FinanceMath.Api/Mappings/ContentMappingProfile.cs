using AutoMapper;
using FinanceMath.Application.Content.Contents.Commands;

namespace FinanceMath.Api.Contracts.Requests
{
    public class ContentMappingProfile : Profile
    {
        public ContentMappingProfile()
        {
            CreateMap<UpdateContentRequest, UpdateContentCommand>();
        }
    }
}