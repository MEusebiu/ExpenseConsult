using ExpenseConsult.Models;
using ExpenseConsult.Models.DTO;
using ExpenseConsult.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseConsult.Controllers;

[Authorize]
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
    public async Task<IActionResult> GetCategoryById(string id)
    {
        var category = await _categoryService.GetCategoryByIdAsync(id);
        if (category == null)
        {
            return NotFound();
        }

        return Ok(category);
    }

    [HttpPost]
    public async Task<IActionResult> CreateCategory([FromBody] CategoryDto categoryCreateDto)
    {
        var category = new Category
        {
            Id = Guid.NewGuid().ToString(),
            Name = categoryCreateDto.Name
        };

        try
        {
            await _categoryService.AddCategoryAsync(category);
        }
        catch (Exception ex)
        {
            return Conflict(ex.Message);
        }

        return Ok();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCategory(string id, [FromBody] CategoryDto categoryDto)
    {
        var category = await _categoryService.GetCategoryByIdAsync(id);
        category.Name = categoryDto.Name;

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
    public async Task<IActionResult> DeleteCategory(string id)
    {
        await _categoryService.DeleteCategoryAsync(id);

        return Ok(); 
    }
}
