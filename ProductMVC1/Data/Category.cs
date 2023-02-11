namespace ProductMVC1.Data
{
    public class Category : BaseEntity
    {
        public Category()
        {
            CategoryProducts = new HashSet<CategoryProduct>();
        }
        public string Title { get; set; }
        public virtual ICollection<CategoryProduct> CategoryProducts { get; set; }
    }
}
