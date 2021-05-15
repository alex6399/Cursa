using AutoMapper;
using Cursa.ViewModels.EmployeesVM;
using Cursa.ViewModels.Users;
using DataLayer.Entities;

namespace Cursa.AutoMapper
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UsersViewModel>()
                .ForMember(x => x.FullName,
                    x
                        => x.MapFrom(m => m.FirstName + " " + m.MiddleName + " " + m.LastName));
        }
    }
}