using System.ComponentModel.DataAnnotations;

namespace Cursa.ViewModels.SubProjectVM
{
    public class SubProjectComplexDisplayViewModel
    {
        public int ProjectId { get; set; }
        [Display(Name = "Проект")] 
        public string ProjectName { get; set; }
        public SubProjectsDisplayViewModel SubProjectVM { get; set; }
    }
}