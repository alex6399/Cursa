using AutoMapper;
using Cursa.ViewModels.DepartmentVM;
using DataLayer.Entities;

namespace Cursa.AutoMapper
{
    public class DepartmentsProfile:Profile
    {
        public DepartmentsProfile()
        {
            CreateMap<Department, DepartmentDisplayViewModel>();
        }
    }
}