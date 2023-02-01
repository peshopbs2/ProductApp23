using ProductMVC1.Data;

namespace ProductMVC1.Services
{
    public interface ICategoryService
    {
        //TODO: Should we use DTOs here?
        public Task<Category> GetCategoryByIdAsync(int categoryId);
        public Task<ICollection<Category>> GetAllCategorysAsync();
        public Task<Category> CreateAsync(Category category);
        public Task<Category> UpdateAsync(Category category);
        public Task<Category> DeleteAsync(int categoryId);
    }
}
