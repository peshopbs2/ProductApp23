using ProductMVC1.Data;
using ProductMVC1.Models.ViewModels.Products;

namespace ProductMVC1.Services
{
    public interface IProductService
    {
        //TODO: Should we use DTOs here?
        public Task<ProductViewModel> GetProductByIdAsync(int productId);
        public Task<ProductDetailsViewModel> GetProductDetailsByIdAsync(int productId);
        public Task<ICollection<ProductDetailsViewModel>> GetAllProductsAsync();
        public Task<ProductViewModel> CreateAsync(ProductViewModel item, string userId);
        public Task<ProductViewModel> UpdateAsync(ProductViewModel item, string userId);
        public Task<ProductDetailsViewModel> DeleteAsync(int productId);
    }
}
