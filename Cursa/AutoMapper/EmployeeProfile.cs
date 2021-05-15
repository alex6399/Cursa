using AutoMapper;
using Cursa.ViewModels.EmployeesVM;
using Cursa.ViewModels.SubProjectVM;
using DataLayer.Entities;

namespace Cursa.AutoMapper
{
    public class EmployeeProfile:Profile
    {
        public EmployeeProfile()
        {
            CreateMap<Employee, EmployeesViewModel>()
                .ForMember(x => x.FullName,
                    x
                        => x.MapFrom(m => m.FirstName + " " + m.MiddleName + " " + m.LastName))
                .ForMember(x => x.DepartmentName, x 
                    => x.MapFrom(m => m.Department.Name));
            
            CreateMap<Employee, EmployeeCreateEditViewModel>().ReverseMap();

            CreateMap<Employee, EmployeePartDisplayViewModel>()
                .ForMember(x => x.FullName,
                    x
                        => x.MapFrom(m => m.FirstName + " " + m.MiddleName + " " + m.LastName));
        }
    }
}