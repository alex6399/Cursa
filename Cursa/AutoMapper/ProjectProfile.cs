using AutoMapper;
using Cursa.ViewModels.Base;
using Cursa.ViewModels.ProjectVM;
using DataLayer.Entities;

namespace Cursa.AutoMapper
{
    public class ProjectProfile:Profile
    {
        public ProjectProfile()
        {
            CreateMap<Project, ProjectCreateViewModel>().ReverseMap();
            CreateMap<Project, ProjectEditViewModel>().ReverseMap();
            CreateMap<Project, BaseViewModel>();
        
        }
    }
}