using System.Collections.Generic;

namespace Cursa.ViewModels.AccountVM
{
    public class UserRolesViewModel
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public bool IsSelected { get; set; }
    }
    public class UserRolesAndUserIdViewModel
    {
        public int Id { get; set; }
        public string RoleName { get; set; }
        public List<UserRolesViewModel> UserRolesViewModels { get; set; }
    }
    
}
