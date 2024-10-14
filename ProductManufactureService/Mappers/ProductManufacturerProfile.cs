using AutoMapper;
using ProductManufacturerService.Models;
using ProductManufactureService.DTOs;


namespace ProductManufacturerService.Mappers
{
    public class ProductManufacturerProfile : Profile
    {
        public ProductManufacturerProfile() {

            CreateMap<ArticleManufacter, ManufacturerDTO>()
                .ForMember(dest => dest.ArticleNumber, opt => opt.MapFrom(src => src.Article.ArticleNumber))
                 .ForMember(dest => dest.ManufacturerId, opt => opt.MapFrom(src => src.Article.DataSupplierId))
                .ForMember(dest => dest.ManufacturerName, opt => opt.MapFrom(src => src.Manufacturer.ManufacturerName))
                .ForMember(dest => dest.ManufacturerAddress, opt => opt.MapFrom(src => src.Manufacturer.ManufacturerAddress))
                .ForMember(dest => dest.ManufacturerEmail, opt => opt.MapFrom(src => src.Manufacturer.ManfucaturerEmail));
        }
    }
}


