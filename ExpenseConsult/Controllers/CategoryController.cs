using Asp.Versioning;
using ExpenseDataAccessLayer.Models;
using ExpenseServices.Services.Interfaces;
using ExpenseWebApi.Models.DTO;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseWebApi.Controllers;

[ApiVersion("1.0")]
[ApiVersion("2.0")]
[Route("api/v{version:apiVersion}/[Controller]")]
[ApiController]
public class CategoryController : ControllerBase
{
    private ICategoryService _categoryService;

    public CategoryController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    [HttpGet]
    [MapToApiVersion("1.0")]
    public async Task<IActionResult> GetCategories()
    {
        var categories = await _categoryService.GetCategoriesAsync();

        return Ok(categories);
    }

    [HttpGet("{id}")]
    [MapToApiVersion("1.0")]
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
    [MapToApiVersion("1.0")]
    public async Task<IActionResult> CreateCategory([FromBody] CategoryDto categoryDto)
    {
        var category = new Category
        {
            Id = Guid.NewGuid().ToString(),
            Name = categoryDto.Name
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
    [MapToApiVersion("1.0")]
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
    [MapToApiVersion("1.0")]
    public async Task<IActionResult> DeleteCategory(string id)
    {
        await _categoryService.DeleteCategoryAsync(id);

        return Ok(); 
    }

    [HttpGet]
    [MapToApiVersion("2.0")]
    public async Task<IActionResult> GetCategoriesNames()
    {
        var categories = await _categoryService.GetCategoriesAsync();
        var categoryNames = categories.Select(c => c.Name);

        return Ok(categoryNames);
    }
}
