
using ArticleManufacturerService.Domain.Entities;
using ArticleManufacturerService.Infrastructure.HttpClients.TecDoc.DTOs;
using AutoMapper;



namespace ArticleManufacturerService.Application.Mappers
{
    public class ManufacturerProfile : Profile
    {
        public ManufacturerProfile()
        {
            CreateMap<ArticleResponse, Article>()
                .ForMember(dest => dest.ArticleNumber, opt => opt.MapFrom(src => src.ArticleNumber))
                .ForMember(dest => dest.DataSupplierId, opt => opt.MapFrom(src => src.DataSupplierId))
                .ForMember(dest => dest.ManufacturerId, opt => opt.MapFrom(src => src.MfrId));

            CreateMap<AddressResponse, Address>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.City))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.Street, opt => opt.MapFrom(src => src.Street))
                .ForMember(dest => dest.Zip, opt => opt.MapFrom(src => src.Zip));
        }
    }
}


