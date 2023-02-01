using ProductMVC1.Data;
using ProductMVC1.Repositories;

namespace ProductMVC1.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IRepository<Category> _repository;

        public CategoryService(IRepository<Category> repository)
        {
            _repository = repository;
        }

        public Task<Category> CreateAsync(Category category)
        {
            return _repository.CreateAsync(category);
        }

        public Task<Category> DeleteAsync(int categoryId)
        {
            return _repository.DeleteAsync(categoryId);
        }

        public Task<ICollection<Category>> GetAllCategorysAsync()
        {
            return _repository.GetAllAsync();
        }

        public Task<Category> GetCategoryByIdAsync(int categoryId)
        {
            return _repository.GetByIdAsync(categoryId);
        }

        public Task<Category> UpdateAsync(Category category)
        {
            return _repository.UpdateAsync(category);
        }
    }
}
