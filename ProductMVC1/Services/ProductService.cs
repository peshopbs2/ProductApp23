using AutoMapper;
using Microsoft.AspNetCore.Identity;
using NuGet.Packaging;
using NuGet.Protocol.Core.Types;
using ProductMVC1.Data;
using ProductMVC1.Models.ViewModels.Products;
using ProductMVC1.Repositories;

namespace ProductMVC1.Services
{
    public class ProductService : IProductService
    {
        private readonly IRepository<Product> _productRepository;
        private readonly IRepository<Category> _categoryRepository;
        private readonly ICategoryProductRepository _categoryProductRepository;
        public readonly IMapper _mapper;

        public ProductService(IRepository<Product> productRepository,
            IRepository<Category> categoryRepository,
            ICategoryProductRepository categoryProductRepository,
            IMapper mapper)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _categoryProductRepository = categoryProductRepository;
            _mapper = mapper;
        }

        public async Task<ProductViewModel> CreateAsync(ProductViewModel item, string userId)
        {
            var _mapperProduct = _mapper.Map<ProductViewModel, Product>(item, opt => opt.AfterMap((src, dest) =>
            {
                dest.CreatedById = userId;
                dest.Created = DateTime.Now;
                dest.ModifiedById = userId;
                dest.Modified = DateTime.Now;
            }));
            
            var created = await _productRepository.CreateAsync(_mapperProduct);
            await _categoryProductRepository.SetProductCategories(created.Id,
                item.CategoryIds
                    .Select(categoryId => _categoryRepository.GetByIdAsync(categoryId)
                    .Result)
                    .ToList()
              );

            item.Id = created.Id;
            return item;
        }

        public async Task<ProductDetailsViewModel> DeleteAsync(int productId)
        {
            var product = await _productRepository.DeleteAsync(productId);
            return _mapper.Map<ProductDetailsViewModel>(product);
        }

        public async Task<ICollection<ProductDetailsViewModel>> GetAllProductsAsync()
        {
            var list = await _productRepository.GetAllAsync();
            return _mapper.Map<List<ProductDetailsViewModel>>(list);
        }

        public async Task<ProductViewModel> GetProductByIdAsync(int productId)
        {
            var product = await _productRepository.GetByIdAsync(productId);
            return _mapper.Map<ProductViewModel>(product);
        }

        public async Task<ProductDetailsViewModel> GetProductDetailsByIdAsync(int productId)
        {
            var product = await _productRepository.GetByIdAsync(productId);
            return _mapper.Map<ProductDetailsViewModel>(product);
        }

        public async Task<ProductViewModel> UpdateAsync(ProductViewModel item, string userId)
        {
            var _mapperProduct = _mapper.Map<ProductViewModel, Product>(item, opt => opt.AfterMap((src, dest) =>
            {
                dest.ModifiedById = userId;
                dest.Modified = DateTime.Now;
            }));

            
            var updatedProduct = await _productRepository.UpdateAsync(_mapperProduct);
            await _categoryProductRepository.DeleteAllCategoriesByProduct(updatedProduct.Id);
        await _categoryProductRepository.SetProductCategories(updatedProduct.Id,
                item.CategoryIds
                    .Select( categoryId => _categoryRepository.GetByIdAsync(categoryId)
                    .Result)
                    .ToList()
              );
            return item;
        }

    }
}
