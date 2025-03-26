using ExpenseDataAccessLayer.Interfaces;
using ExpenseDataAccessLayer.Models;
using ExpenseServices.Services.Interfaces;

namespace ExpenseServices.Services;

public class CategoryService : ICategoryService
{
    private readonly IRepository<string, Category> _repository;
    private readonly ICategoryRepository _categoryRepository;
    private readonly IExpenseService _expenseService;

    public CategoryService(IRepository<string, Category> repository, ICategoryRepository categoryRepository, IExpenseService expenseService)
    {
        _categoryRepository = categoryRepository;
        _repository = repository;
        _expenseService = expenseService;
    }

    public async Task<IEnumerable<Category>> GetCategoriesAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<Category> GetCategoryByIdAsync(string id)
    {
        return await _repository.GetByIdAsync(id);
    }

    public async Task AddCategoryAsync(Category category)
    {
        CheckForDuplicateCategory(category);

        await _repository.AddAsync(category);
    }

    public async Task UpdateCategoryAsync(string key, Category category)
    {
        CheckForDuplicateCategory(category);

        await _repository.UpdateAsync(key, category);
    }

    public async Task DeleteCategoryAsync(string id)
    {
        await _repository.DeleteAsync(id);
    }

    private void CheckForDuplicateCategory(Category category)
    {
        bool existingCategory = GetCategoriesAsync().Result.Any(c => c.Name == category.Name);
        if (existingCategory)
        {
            throw new InvalidOperationException("A category with the same name already exists.");
        }
    }

    public async Task<Dictionary<string, List<Expense>>> GetReportInformation()
    {
        var allExpenses = await _expenseService.GetExpensesAsync();

        var expenseCategoryIds = allExpenses.Select(e => e.CategoryId).Distinct();

        var categoriesWithNames = await _categoryRepository.GetCategoryNamesByIdsAsync(expenseCategoryIds);
        //--

        var groupedExpenses = allExpenses.GroupBy(e => e.CategoryId)
            .Where(g => categoriesWithNames.ContainsKey(g.Key)) 
            .ToDictionary(g => categoriesWithNames[g.Key], g => g.ToList());

        return groupedExpenses;
    }
}
