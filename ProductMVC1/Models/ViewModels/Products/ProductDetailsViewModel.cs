namespace ProductMVC1.Models.ViewModels.Products
{
    public class ProductDetailsViewModel : ProductViewModel
    {
        public List<string> CategoriesName { get; set; }
        public string CreatedByName { get; set; }
        public DateTime Created { get; set; }
        public string ModifiedByName { get; set; }
        public DateTime Modified { get; set; }

    }
}
