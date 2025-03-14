//using ExpenseConsult.Models;
//using ExpenseConsult.Repositories;

//namespace ExpenseConsult.Services
//{
//    public class CategoryService
//    {
//        private readonly IRepository<Category> _categoryRepository;

//        public CategoryService(IRepository<Category> categoryRepository)
//        {
//            _categoryRepository = categoryRepository;
//        }

//        public async Task<IEnumerable<Category>> GetCategoriesAsync()
//        {
//            return await _categoryRepository.GetAllAsync();
//        }

//        public async Task<Category> GetCategoryByIdAsync(int id)
//        {
//            return await _categoryRepository.GetByIdAsync(id);
//        }

//        public async Task AddCategoryAsync(Category category)
//        {
//            await _categoryRepository.AddAsync(category);
//        }

//        public async Task UpdateCategoryAsync(Category category)
//        {
//            await _categoryRepository.UpdateAsync(category);
//        }

//        public async Task DeleteCategoryAsync(int id)
//        {
//            await _categoryRepository.DeleteAsync(id);
//        }
//    }
//}
