using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ProductMVC1.Models.ViewModels.Products
{
    public class ProductViewModel
    {
        public int Id { get; set; }
        [Required]
        [MinLength(3)]
        [MaxLength(80)]
        public string Title { get; set; }
        [MaxLength(500)]
        public string Description { get; set; }
        [Required]
        [Range(0, double.MaxValue)]
        public double Price { get; set; }
        [DisplayName("Categories")]
        public List<int> CategoryIds { get; set; }
    }
}
