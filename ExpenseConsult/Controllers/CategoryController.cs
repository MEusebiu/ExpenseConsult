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
    private ILogger<CategoryController> _logger;

    public CategoryController(ICategoryService categoryService, ILogger<CategoryController> logger)
    {
        _categoryService = categoryService;
        _logger = logger;
    }

    [HttpGet]
    [MapToApiVersion("1.0")]
    public async Task<IActionResult> GetCategories()
    {
        var categories = await _categoryService.GetCategoriesAsync();
        _logger.LogInformation("Successfully fetched {Count} categories", categories.ToList().Count);

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
        _logger.LogInformation("Successfully fetched {categoryName}", category.Name);

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

        _logger.LogInformation("Successfully created {categoryName}", category.Name);

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

        _logger.LogInformation("Successfully updated {categoryName}", category.Name);

        return Ok();
    }

    [HttpDelete("{id}")]
    [MapToApiVersion("1.0")]
    public async Task<IActionResult> DeleteCategory(string id)
    {
        await _categoryService.DeleteCategoryAsync(id);

        _logger.LogInformation("Category successfully deleted");

        return Ok(); 
    }


    // Version 2 API

    [HttpGet]
    [MapToApiVersion("2.0")]
    public async Task<IActionResult> GetCategoriesNames()
    {
        var categories = await _categoryService.GetCategoriesAsync();
        var categoryNames = categories.Select(c => c.Name);

        _logger.LogInformation("Successfully fetched {Count} category names", categories.ToList().Count);

        return Ok(categoryNames);
    }
}
