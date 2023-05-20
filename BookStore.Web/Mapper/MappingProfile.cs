using AutoMapper;

using BookStore.Services.DTO;
using BookStore.Web.ViewModels;

namespace BookStore.Web.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            this.CreateMap<CategoryDto, CategoryViewModel>();

            this.CreateMap<ProductDto, ProductViewModel>();
        }
    }
}
