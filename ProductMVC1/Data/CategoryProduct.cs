using System.ComponentModel.DataAnnotations;

namespace ProductMVC1.Data
{
    public class CategoryProduct
    {
        public int CategoriesId { get; set; }
        public virtual Category Category { get; set; }
        public int ProductsId { get; set; }
        public virtual Product Product { get; set; }
    }
}
