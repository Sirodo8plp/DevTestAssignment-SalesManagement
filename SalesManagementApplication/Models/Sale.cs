using System.ComponentModel.DataAnnotations;
namespace SalesManagementApplication.Models
{
    public class Sale
    {
        public int SaleId { get; set; }

        [Required]
        [Display(Name = "Amount of Transaction")]
        [DataType(DataType.Currency)]
        [Range(0,Double.MaxValue)]
        public decimal Price { get; set; }

        [Required]
        [Display(Name = "Sale Date")]
        [DataType(DataType.DateTime)]
        public DateTime CreatedDate { get; set; }

        [Required]
        [Display(Name = "Seller ID")]
        public int SellerId { get; set; }

        [Required]
        public Seller Seller { get; set; } = null!;
    }
}
