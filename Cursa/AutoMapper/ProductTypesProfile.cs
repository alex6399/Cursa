using AutoMapper;
using Cursa.ViewModels.ProductTypesVM;
using DataLayer.Entities;

namespace Cursa.AutoMapper
{
    public class ProductTypesProfile : Profile
    {
        public ProductTypesProfile()
        {
            CreateMap<ProductType, ProductTypesDisplayViewModel>()
                .ForMember(dest => dest.ProductTypeName, act =>
                    act.MapFrom(src => src.ProductSubType.Name));
        }
    }
}