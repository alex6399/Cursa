using AutoMapper;
using Cursa.ViewModels.Base;
using Cursa.ViewModels.OrderCardVM;
using Cursa.ViewModels.ProjectVM;
using DataLayer.Entities;

namespace Cursa.AutoMapper
{
    public class OrderCardProfile:Profile
    {
        public OrderCardProfile()
        {
            CreateMap<OrderCard, OrderCardCreateEditVM>().ReverseMap();
            CreateMap<OrderCard, OrderCardDisplayViewModel>()
                .ForMember(dest => dest.ProductName,
                    act => act.MapFrom(src => src.Product.Name))
                .ForMember(x => x.Product, x
                    => x.MapFrom(p => p.Product));
            // CreateMap<Project, ProjectEditViewModel>().ReverseMap();
            // CreateMap<Project, ProjectViewModel>()
            //     .ForMember(p => p.Employee, act
            //         => act.MapFrom(m => m.Employee.FirstName + " " + m.Employee.MiddleName + " " + m.Employee.LastName))
            //     .ForMember(dest => dest.Owner, act
            //         => act.MapFrom(m => m.Owner.Name))
            //     ;
            CreateMap<OrderCard, BaseViewModel>();
        
        }
    }
}