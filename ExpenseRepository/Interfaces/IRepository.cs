namespace ExpenseDataAccessLayer.Interfaces;

public interface IRepository<TKey, TValue> where TValue : class
{
    Task<IEnumerable<TValue>> GetAllAsync(Func<IQueryable<TValue>, IQueryable<TValue>>? include = null);
    Task<TValue?> GetByIdAsync(TKey key, Func<IQueryable<TValue>, IQueryable<TValue>>? include = null);
    Task AddAsync(TValue entity);
    Task UpdateAsync(TKey key, TValue entity);
    Task DeleteAsync(TKey key);
    Task LoadCacheAsync();
}
