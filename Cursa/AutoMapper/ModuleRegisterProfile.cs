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
                .ForMember(dest => dest.OrderCardId, act
                    => act.MapFrom(src => src.ActualOrderCardId))
                .ForMember(dest => dest.OrderCardId, act
                    => act.MapFrom(src => src.ActualOrderCard.Name));
        }
    }
}