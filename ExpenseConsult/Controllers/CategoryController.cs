using ExpenseConsult.Models;
using ExpenseConsult.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseConsult.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoryController : ControllerBase
{
    private ICategoryService _categoryService;

    public CategoryController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    [HttpGet]
    public async Task<IActionResult> GetCategories()
    {
        var categories = await _categoryService.GetCategoriesAsync();

        return Ok(categories);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetCategoryById(int id)
    {
        var category = await _categoryService.GetCategoryByIdAsync(id);
        if (category == null)
        {
            return NotFound();
        }

        return Ok(category);
    }

    [HttpPost]
    public async Task<IActionResult> CreateCategory([FromBody] Category category)
    {
        try
        {
            await _categoryService.AddCategoryAsync(category);
        }
        catch (Exception ex)
        {
            return Conflict(ex.Message);
        }

        await _categoryService.AddCategoryAsync(category);

        return Ok();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCategory(int id, [FromBody] Category category)
    {
        try
        {
            await _categoryService.UpdateCategoryAsync(id, category);
        }
        catch (Exception ex)
        {
            return Conflict(ex.Message);
        }

        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCategory(int id)
    {
        await _categoryService.DeleteCategoryAsync(id);

        return Ok(); 
    }
}
