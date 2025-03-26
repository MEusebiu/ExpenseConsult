using System.Collections.Concurrent;
using ExpenseDataAccessLayer.Interfaces;
using ExpenseDataAccessLayer.Models;

namespace ExpenseDataAccessLayer.Repositories;

public class CategoryRepository : ICategoryRepository
{
    private IRepository<string, Category> _repository;

    public CategoryRepository(IRepository<string, Category> repository)
    {
        _repository = repository;
    }

    public async Task<string> GetCategoryNameByIdAsync(string categoryId)
    {
        var category = await _repository.GetByIdAsync(categoryId);
        return category.Name;
    }

    public async Task<ConcurrentDictionary<string, string>> GetCategoryNamesByIdsAsync(IEnumerable<string> categoryIds)
    {
        var categories = await _repository.GetAllAsync();
        var filtered = categories.Where(x => categoryIds.Contains(x.Id));

        return new(categories.ToDictionary(x => x.Id, y => y.Name));
    }
}