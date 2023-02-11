namespace ProductMVC1.Data
{
    public class Product : BaseEntity
    {
        public Product()
        {
            CategoryProducts = new HashSet<CategoryProduct>();
        }
        public string Title { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public virtual ICollection<CategoryProduct> CategoryProducts { get; set; }
    }
}