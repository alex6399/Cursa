using AutoMapper;
using Cursa.ViewModels.ProjectRegisterVM;
using DataLayer.Entities;

namespace Cursa.AutoMapper
{
    public class ProjectRegisterProfile:Profile
    {
        public ProjectRegisterProfile()
        {
            CreateMap<SubProject, ProjectRegisterDisplayViewModel>()
                .ForMember(x => x.StatusName,
                    x => x.MapFrom(m => m.Status.Name))
                .ForMember(dest => dest.Employee,
                    act => act.MapFrom(src => src.Employee))
                .ForMember(dest => dest.ProjectId,
                    act => act.MapFrom(src => src.ProjectId))
                .ForMember(dest => dest.ProjectName,
                    act => act.MapFrom(src => src.Project.Name))
                .ForMember(dest => dest.Contractor,
                    act => act.MapFrom(src => src.Contractor.Name))
                .ForMember(dest => dest.Owner,
                    act => act.MapFrom(src => src.Project.Owner.Name));
        }
    }
}