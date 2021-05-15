using System.ComponentModel.DataAnnotations;

namespace Cursa.ViewModels.Users
{
    public class UsersViewModel
    {
        public int Id { get; set; }
        [Display(Name = "ФИО")]
        public string FullName { get; set; }
        [Display(Name = "Email")]
        public string Email { get; set; }
        [Display(Name = "Телефон")]
        public string PhoneNumber { get; set; }
    }
}