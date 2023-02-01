using Microsoft.EntityFrameworkCore;
using ProductMVC1.Data;

namespace ProductMVC1.Repositories
{
    public class Repository<T> : IRepository<T>
        where T: BaseEntity
    {
        private readonly ApplicationDbContext _context;

        public Repository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<T> CreateAsync(T item)
        {
            _context.Set<T>().Add(item);
            await _context.SaveChangesAsync();

            return item;
        }

        public async Task<T> DeleteAsync(int id)
        {
            var item = await GetByIdAsync(id);
            _context.Remove(item);
            await _context.SaveChangesAsync();
            return item;
        }

        public async Task<ICollection<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public ICollection<T> GetByFilter(Func<T, bool> predicate)
        {
            return _context.Set<T>()
                .Where(predicate)
                .ToList();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _context.Set<T>()
                .FindAsync(id);

        }

        public async Task<T> UpdateAsync(T item)
        {
            _context.Set<T>().Update(item);
            await _context.SaveChangesAsync();

            return item;
        }
    }
}
