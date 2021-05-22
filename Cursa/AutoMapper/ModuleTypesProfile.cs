using AutoMapper;
using Cursa.ViewModels.ModuleTypesVM;
using DataLayer.Entities;

namespace Cursa.AutoMapper
{
    public class ModuleTypesProfile:Profile
    {
        public ModuleTypesProfile()
        {
            CreateMap<ModuleType, ModuleTypesDisplayViewModel>();
        }
    }
}