using ExpenseConsult.Models;
using ExpenseConsult.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseConsult.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoryController : ControllerBase
{
    private IRepository<int, Category> _categoryRepository;

    public CategoryController(IRepository<int, Category> categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetCategories()
    {
        var categories = await _categoryRepository.GetAllAsync();

        return Ok(categories);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetCategoryById(int id)
    {
        var category = await _categoryRepository.GetByIdAsync(id);
        if (category == null)
        {
            return NotFound();
        }

        return Ok(category);
    }

    [HttpPost]
    public async Task<IActionResult> CreateCategory([FromBody] Category category)
    {
        if (category == null)
        {
            return BadRequest("Category data is required.");
        }

        await _categoryRepository.AddAsync(category);

        return Ok();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCategory(int id, [FromBody] Category category)
    {
        await _categoryRepository.UpdateAsync(category);

        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCategory(int id)
    {
        await _categoryRepository.DeleteAsync(id);

        return Ok(); 
    }
}
