using ExpenseConsult.Models;

namespace ExpenseConsult.Services.Interfaces;

public interface ICategoryService
{
    Task<IEnumerable<Category>> GetCategoriesAsync();
    Task<Category> GetCategoryByIdAsync(int id);
    Task AddCategoryAsync(Category category);
    Task UpdateCategoryAsync(int key, Category category);
    Task DeleteCategoryAsync(int id);
}
