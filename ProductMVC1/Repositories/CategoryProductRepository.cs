using Microsoft.EntityFrameworkCore;
using ProductMVC1.Data;

namespace ProductMVC1.Repositories
{
    public class CategoryProductRepository : ICategoryProductRepository
    {
        private readonly ApplicationDbContext _context;

        public CategoryProductRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ICollection<Category>> GetAllCategoriesByProductAsync(int productId)
        {
            return await _context.CategoryProduct
                .Where(item => item.Product.Id == productId)
                .Select(item => item.Category)
                .ToListAsync();
        }

        public async Task<ICollection<Product>> GetAllProductsByCategoryAsync(int categoryId)
        {
            return await _context.CategoryProduct
                .Where(item => item.Category.Id == categoryId)
                .Select(item => item.Product)
                .ToListAsync();
        }
        public async Task DeleteAllCategoriesByProduct(int productId)
        {
            List<CategoryProduct> items = _context.CategoryProduct
                .Where(item => item.ProductsId == productId)
                .ToList();
            foreach (var item in items)
            {
                _context.CategoryProduct.Remove(item);
            }
            await _context.SaveChangesAsync();
        }

        public async Task SetProductCategories(int productId, ICollection<Category> categories)
        {
            foreach (var category in categories)
            {
                _context.CategoryProduct.Add(new CategoryProduct()
                {
                    CategoriesId = category.Id,
                    ProductsId = productId
                });
            }

            await _context.SaveChangesAsync();
        }
    }
}
