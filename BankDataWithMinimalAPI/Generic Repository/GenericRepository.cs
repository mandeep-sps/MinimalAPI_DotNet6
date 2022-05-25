using BankDataWithMinimalAPI.Data;
using Microsoft.EntityFrameworkCore;

namespace BankDataWithMinimalAPI.Generic_Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private AppDbContext _context = null;
        private DbSet<T> table = null;
        
        public GenericRepository(AppDbContext _context)
        {
            this._context = _context;
            table = _context.Set<T>();
        }
        public async Task<IEnumerable<T>> GetAll()
        {
            var objList = await table.ToListAsync();
            return objList;
        }
        public async Task<T> GetById(object id)
        {
            var obj = await table.FindAsync(id);
            return obj;
        }
        public async Task<T> Insert(T obj)
        {
            await table.AddAsync(obj);
            await _context.SaveChangesAsync();
            return obj;
        }
        public async Task<T> Update(T obj)
        {
            table.Attach(obj);
            _context.Entry(obj).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return obj;
        }
        public async Task<T> Delete(object id)
        {
            T existing = await table.FindAsync(id);
            if (existing != null)
            {
                table.Remove(existing);
                await _context.SaveChangesAsync();
                return existing;
            }
            else
            {
                return null;
            }
        }
    }
}
