namespace ExpenseDataAccessLayer.Repositories;
public interface IRepository<TKey, TValue> where TValue : class
{
    Task<IEnumerable<TValue>> GetAllAsync();
    Task<TValue?> GetByIdAsync(TKey key);
    Task AddAsync(TValue entity);
    Task UpdateAsync(TKey key, TValue entity);
    Task DeleteAsync(TKey key);
    Task LoadCacheAsync();
}
