using AutoMapper;
using Cursa.ViewModels.ModuleVM;
using DataLayer.Entities;

namespace Cursa.AutoMapper
{
    public class ModuleProfile : Profile
    {
        public ModuleProfile()
        {
            CreateMap<Module, ModuleDisplayViewModel>();
        }
    }
}