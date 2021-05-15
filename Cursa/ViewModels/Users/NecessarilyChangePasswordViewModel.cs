using System.ComponentModel.DataAnnotations;

namespace Cursa.ViewModels.Users
{
    public class NecessarilyChangePasswordViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Поле обязательно для заполнения")]
        public string OldPassword { get; set; }

        [Required(ErrorMessage = "Поле обязательно для заполнения")]
        public string NewPassword { get; set; }
    }
}