using AutoMapper;
using Cursa.ViewModels.Base;
using Cursa.ViewModels.ProjectVM;
using DataLayer.Entities;

namespace Cursa.AutoMapper
{
    public class ProjectProfile : Profile
    {
        public ProjectProfile()
        {
            CreateMap<Project, ProjectCreateViewModel>().ReverseMap();
            CreateMap<Project, ProjectEditViewModel>().ReverseMap();
            CreateMap<Project, ProjectViewModel>()
                .ForMember(p => p.Employee, act
                    => act.MapFrom(m =>
                                              m.Employee.FirstName + " "+
                                              m.Employee.MiddleName + " "+
                                              m.Employee.LastName))
                .ForMember(dest => dest.Owner, act
                    => act.MapFrom(m => m.Owner.Name))
                ;
            CreateMap<Project, BaseViewModel>();
        }
    }
}