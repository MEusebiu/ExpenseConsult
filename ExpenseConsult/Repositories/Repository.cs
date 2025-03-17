using ExpenseConsult.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Concurrent;

namespace ExpenseConsult.Repositories
{
    public class Repository<TKey, TValue> : IRepository<TKey, TValue> where TValue : class
    {
        private readonly AppDbContext _context;
        private readonly DbSet<TValue> _dbSet;

        private ConcurrentDictionary<TKey, TValue> _cache;

        public Repository(ConcurrentDictionary<TKey, TValue> cache, AppDbContext context)
        {
            _cache = cache;
            _context = context;
            _dbSet = context.Set<TValue>();
        }

        public async Task LoadCacheAsync()
        {
            var items = await _dbSet.ToListAsync();
            foreach (var item in items)
            {
                var key = GetEntityKey(item);
                _cache.TryAdd(key, item);
            }
        }

        public async Task<IEnumerable<TValue>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<TValue?> GetByIdAsync(TKey key)
        {
            if (_cache.TryGetValue(key, out var cachedValue))
            {
                return cachedValue;
            }

            var entity = await _dbSet.FindAsync(key);
            if (entity != null)
            {
                _cache.TryAdd(key, entity);
            }

            return entity;
        }

        public async Task AddAsync(TValue entity)
        {
            var item = await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();

            var key = GetEntityKey(entity);
            _cache.TryAdd(key, entity);
        }

        public async Task UpdateAsync(TValue entity)
        {
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();

            var key = GetEntityKey(entity);
            _cache.AddOrUpdate(key, entity, (OldValue, NewValue) => entity);
        }

        public async Task DeleteAsync(TKey key)
        {
            var entity = await _dbSet.FindAsync(key);
            if (entity == null) return;

            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();

            _cache.TryRemove(key, out _);
        }

        private static TKey GetEntityKey(TValue entity)
        {
            var property = typeof(TValue).GetProperty("Id") ??
                           typeof(TValue).GetProperties().FirstOrDefault(p => p.Name.StartsWith("Id"));

            if (property == null)
            {
                throw new InvalidOperationException($"No key property found for {typeof(TValue).Name}");
            }

            return (TKey)property.GetValue(entity)!;
        }
    }
}
