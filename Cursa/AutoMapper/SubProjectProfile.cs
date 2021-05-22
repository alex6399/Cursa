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
                // .ForMember(x => x.ProjectName,
                //     x => x.MapFrom(m => m.Project.Name))
                .ForMember(x => x.StatusName,
                    x => x.MapFrom(m => m.Status.Name))
                .ForMember(dest => dest.Employee,
                    act => act.MapFrom(src => src.Employee));
                // .ForMember(dest => dest.CreatedDate, act =>
                //     act.MapFrom(src => src.CreatedDate != null ? src.CreatedDate.Value.ToString("d") : null))
                // .ForMember(dest => dest.EndDate, act =>
                //     act.MapFrom(src => src.EndDate != null ? src.EndDate.Value.ToString("d") : null));
            CreateMap<SubProject, SubProjectCreateEditViewModel>().ReverseMap();
            CreateMap<SubProject, BaseViewModel>();
        }
    }
}