using ExpenseConsult.Models;
using ExpenseConsult.Repositories;
using ExpenseConsult.Services.Interfaces;

namespace ExpenseConsult.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IRepository<int, Category> _categoryRepository;

        public CategoryService(IRepository<int, Category> categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<IEnumerable<Category>> GetCategoriesAsync()
        {
            return await _categoryRepository.GetAllAsync();
        }

        public async Task<Category> GetCategoryByIdAsync(int id)
        {
            return await _categoryRepository.GetByIdAsync(id);
        }

        public async Task AddCategoryAsync(Category category)
        {
            CheckForDuplicateCategory(category);

            await _categoryRepository.AddAsync(category);
        }

        public async Task UpdateCategoryAsync(int key, Category category)
        {
            CheckForDuplicateCategory(category);

            await _categoryRepository.UpdateAsync(key, category);
        }

        public async Task DeleteCategoryAsync(int id)
        {
            await _categoryRepository.DeleteAsync(id);
        }

        private void CheckForDuplicateCategory(Category category)
        {
            bool existingCategory = GetCategoriesAsync().Result.Any(c => c.Name == category.Name);
            if (existingCategory)
            {
                throw new InvalidOperationException("A category with the same name already exists.");
            }
        }
    }
}
