using NuGet.Protocol.Core.Types;
using ProductMVC1.Data;
using ProductMVC1.Repositories;

namespace ProductMVC1.Services
{
    public class ProductService : IProductService
    {
        private readonly IRepository<Product> _repository;

        public ProductService(IRepository<Product> repository)
        {
            _repository = repository;
        }
    
        public Task<Product> CreateAsync(Product product)
        {
            return _repository.CreateAsync(product);
        }

        public Task<Product> DeleteAsync(int productId)
        {
            return _repository.DeleteAsync(productId);
        }

        public Task<ICollection<Product>> GetAllProductsAsync()
        {
            return _repository.GetAllAsync();
        }

        public Task<Product> GetProductByIdAsync(int productId)
        {
            return _repository.GetByIdAsync(productId);
        }

        public Task<Product> UpdateAsync(Product product)
        {
            return _repository.UpdateAsync(product);
        }
    }
}
