using ExpenseDataAccessLayer.Models;

namespace ExpenseServices.Services.Interfaces;

public interface ICategoryService
{
    Task<IEnumerable<Category>> GetCategoriesAsync();
    Task<Category> GetCategoryByIdAsync(string id);
    Task AddCategoryAsync(Category category);
    Task UpdateCategoryAsync(string key, Category category);
    Task DeleteCategoryAsync(string id);
}
