using System.ComponentModel.DataAnnotations;

namespace SalesManagementApplication.Models
{
    public class Seller
    {
        public int SellerId { get; set; }

        [Required]
        [MaxLength(100)]
        [Display(Name = "First Name")]
        public string Name { get; set; } = null!;

        [Required]
        [MaxLength(100)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; } = null!;

        public virtual List<Sale>? Sales { get; set; }
    }
}
