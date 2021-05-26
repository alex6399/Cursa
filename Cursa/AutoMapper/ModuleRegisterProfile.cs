using AutoMapper;
using Cursa.ViewModels.ModuleRegisterVM;
using DataLayer.Entities;

namespace Cursa.AutoMapper
{
    public class ModuleRegisterProfile : Profile
    {
        public ModuleRegisterProfile()
        {
            CreateMap<Module, ModuleRegisterViewModel>()
                .ForMember(dest => dest.SubProjectId, act
                    => act.MapFrom(src => src.ActualOrderCard.Product.SubProjectId))
                .ForMember(dest => dest.SubProjectName, act
                    => act.MapFrom(src => src.ActualOrderCard.Product.SubProject.Name))
                .ForMember(dest => dest.ActualOrderCardId, act
                    => act.MapFrom(src => src.ActualOrderCardId))
                .ForMember(dest => dest.ActualOrderCardNumber, act
                    => act.MapFrom(src => src.ActualOrderCard.Number))
                .ForMember(dest => dest.DestOrderCardId, act
                    => act.MapFrom(src => src.DestinationOrderCardId))
                .ForMember(dest => dest.DestOrderCardNumber, act
                    => act.MapFrom(src => src.DestinationOrderCard.Number))
                .ForMember(dest => dest.ModuleTypeName, act
                    => act.MapFrom(src => src.ModuleType.Name))
                .ForMember(dest => dest.ProductName, act
                    => act.MapFrom(src => src.ActualOrderCard.Product.Name))
                .ForMember(dest => dest.ProductNumber, act
                    => act.MapFrom(src => src.ActualOrderCard.Product.SerialNum));
        }
    }
}