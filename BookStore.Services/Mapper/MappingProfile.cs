using AutoMapper;

using BookStore.Infrastructure.Models;
using BookStore.Services.DTO;

namespace BookStore.Services.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            this.CreateMap<Category, CategoryDto>()
                .ForSourceMember(category => category.Products, dto => dto.DoNotValidate());
        }
    }
}
