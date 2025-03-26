using System.Collections.Concurrent;

namespace ExpenseDataAccessLayer.Interfaces
{
    public interface ICategoryRepository
    {
        Task<string> GetCategoryNameByIdAsync(string categoryId);
        Task<ConcurrentDictionary<string, string>> GetCategoryNamesByIdsAsync(IEnumerable<string> categoryIds);
    }
}
