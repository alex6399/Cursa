using Cursa.ViewModels.AccountVM;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Cursa.ViewModels.Users
{
    public class DetailsUserViewModel:EditUserViewModel
    {
        [Display(Name = "Дата регистрации")]
        public DateTime CreatedData { get; set; }

    }
}
