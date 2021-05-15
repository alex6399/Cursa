using System.ComponentModel.DataAnnotations;

namespace Cursa.ViewModels.OrderCardVM
{
    public class OrderCardCreateVM
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
    }
}
