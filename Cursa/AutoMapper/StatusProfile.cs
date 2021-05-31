using AutoMapper;
using Cursa.ViewModels.StatusVM;
using DataLayer.Entities;

namespace Cursa.AutoMapper
{
    public class StatusProfile:Profile
    {
        public StatusProfile()
        {
            CreateMap<Status, StatusDisplayViewModel>();
            // .ForMember(dest=>dest.StatusTypeName,act=>
            //     act.MapFrom(src=>src.StatusType.StatusTypeName));
        }
    }
}