using ProductMVC1.Data;

namespace ProductMVC1.Repositories
{
    public interface ICategoryProductRepository
    {
        public Task<ICollection<Category>> GetAllCategoriesByProductAsync(int productId);
        public Task<ICollection<Product>> GetAllProductsByCategoryAsync(int categoryId);
        public Task SetProductCategories(int productId, ICollection<Category> categories);
        public Task DeleteAllCategoriesByProduct(int productId);
    }
}
