using AutoMapper;
using Cursa.ViewModels.ModuleVM;
using DataLayer.Entities;

namespace Cursa.AutoMapper
{
    public class ModuleProfile : Profile
    {
        public ModuleProfile()
        {
            CreateMap<Module, ModuleDisplayViewModel>()
                .ForMember(dest=>dest.DestinationOrderCardName,act
                    =>act.MapFrom(src=>src.DestinationOrderCard.Name))
                .ForMember(dest=>dest.DestinationOrderCardNumber,act
                    =>act.MapFrom(src=>src.DestinationOrderCard.Number))
                .ForMember(dest=>dest.ActualOrderCardName,act
                    =>act.MapFrom(src=>src.ActualOrderCard.Name))
                .ForMember(dest=>dest.ActualOrderCardNumber,act
                    =>act.MapFrom(src=>src.ActualOrderCard.Number));


            CreateMap<Module, ModuleCreateEditViewModel>()
                .ForMember(dest => dest.DestinationOrderCardName, act
                    => act.MapFrom(src => src.DestinationOrderCard.Name))
                .ForMember(dest => dest.DestinationOrderCardNumber, act
                    => act.MapFrom(src => src.DestinationOrderCard.Number))
                .ForMember(dest => dest.ModuleTypeName, act
                    => act.MapFrom(src => src.ModuleType.Name));
            CreateMap<ModuleCreateEditViewModel, Module>();
               
            
            


            CreateMap<ModuleCreateEditViewModel, Module>();
        }
    }
}