using AutoMapper;
using Cursa.ViewModels.Base;
using Cursa.ViewModels.SubProjectVM;
using DataLayer.Entities;

namespace Cursa.AutoMapper
{
    public class SubProjectProfile : Profile
    {
        public SubProjectProfile()
        {
            CreateMap<SubProject, SubProjectsDisplayViewModel>()
                .ForMember(x => x.ProjectName,
                    x => x.MapFrom(m => m.Project.Name))
                .ForMember(x => x.StatusName,
                    x => x.MapFrom(m => m.Status.NameStatus))
                .ForMember(dest => dest.Employee,
                    act => act.MapFrom(src =>src.Employee ));
            CreateMap<SubProject, BaseViewModel>();
        }
    }
}