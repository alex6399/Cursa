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
                    =>act.MapFrom(src=>src.DestinationOrderCard.Number));
        }
    }
}