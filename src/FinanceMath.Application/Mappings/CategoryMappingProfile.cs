using AutoMapper;
using FinanceMath.Application.Content.Categories.Dtos;
using FinanceMath.Domain.ContentAggregate;

namespace FinanceMath.Application.Mappings
{
    public class CategoryMappingProfile : Profile
    {
        public CategoryMappingProfile()
        {
            CreateMap<Category, CategoryDto>()
                .ForMember(dest => dest.ParentCategoryId,
                    opt => opt.MapFrom(src => src.ParentCategory != null ? src.ParentCategory.Id : (Guid?)null))
                .ForMember(dest => dest.SubcategoryIds,
                    opt => opt.MapFrom(src => src.Subcategories.Select(sc => sc.Id)))
                .ForMember(dest => dest.ContentIds,
                    opt => opt.MapFrom(src => src.Contents.Select(c => c.Id)));
        }
    }
}
