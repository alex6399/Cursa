using AutoMapper;
using Cursa.ViewModels.Base;
using Cursa.ViewModels.ProductsVM;
using DataLayer.Entities;

namespace Cursa.AutoMapper
{
    public class ProductProfile:Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductCreateViewModel>().ReverseMap();
            CreateMap<Product, ProductDisplayViewModel>()
                .ForMember(x => x.SubProject, x
                    => x.MapFrom(p => p.SubProject));
        }
    }
}