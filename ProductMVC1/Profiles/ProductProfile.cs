using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Query;
using ProductMVC1.Data;
using ProductMVC1.Models.ViewModels.Products;

namespace ProductMVC1.Profiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<ProductViewModel, Product>()
                .ForMember(
                    dest => dest.Title,
                    opt => opt.MapFrom(src => $"{src.Title}")
                )
                .ForMember(
                    dest => dest.Description,
                    opt => opt.MapFrom(src => $"{src.Description}")
                )
                .ForMember(
                    dest => dest.Price,
                    opt => opt.MapFrom(src => src.Price)
                );

            CreateMap<Product, ProductViewModel>()
                .ForMember(
                    dest => dest.Id,
                    opt => opt.MapFrom(src => src.Id)
                )
                .ForMember(
                    dest => dest.Title,
                    opt => opt.MapFrom(src => $"{src.Title}")
                )
                .ForMember(
                    dest => dest.Description,
                    opt => opt.MapFrom(src => $"{src.Description}")
                )
                .ForMember(
                    dest => dest.Price,
                    opt => opt.MapFrom(src => src.Price)
                )
                .ForMember(
                    dest => dest.CategoryIds,
                    opt => opt.MapFrom(src => src.CategoryProducts.Select(item => item.CategoriesId).ToList())
                );

            CreateMap<Product, ProductDetailsViewModel>()
                .ForMember(
                    dest => dest.Id,
                    opt => opt.MapFrom(src => src.Id)
                )
                .ForMember(
                    dest => dest.Title,
                    opt => opt.MapFrom(src => $"{src.Title}")
                )
                .ForMember(
                    dest => dest.Description,
                    opt => opt.MapFrom(src => $"{src.Description}")
                )
                .ForMember(
                    dest => dest.Price,
                    opt => opt.MapFrom(src => src.Price)
                )
                .ForMember(
                    dest => dest.CategoriesName,
                    opt => opt.MapFrom(src => src.CategoryProducts.Select(item => item.Category.Title).ToList())
                )
                .ForMember(
                    dest => dest.Created,
                    opt => opt.MapFrom(src => src.Created)
                )
                .ForMember(
                    dest => dest.Modified,
                    opt => opt.MapFrom(src => src.Modified)
                )
                .ForMember(
                    dest => dest.CreatedByName,
                    opt => opt.MapFrom(src => src.CreatedBy.UserName)
                )
                .ForMember(
                    dest => dest.ModifiedByName,
                    opt => opt.MapFrom(src => src.ModifiedBy.UserName)
                );

        }


    }
}
