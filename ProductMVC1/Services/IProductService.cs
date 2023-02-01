using ProductMVC1.Data;

namespace ProductMVC1.Services
{
    public interface IProductService
    {
        //TODO: Should we use DTOs here?
        public Task<Product> GetProductByIdAsync(int productId);
        public Task<ICollection<Product>> GetAllProductsAsync();
        public Task<Product> CreateAsync(Product product);
        public Task<Product> UpdateAsync(Product product);
        public Task<Product> DeleteAsync(int productId);
    }
}
