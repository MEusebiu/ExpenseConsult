using ExpenseDataAccessLayer.Interfaces;
using ExpenseDataAccessLayer.Models;
using ExpenseServices.Services.Interfaces;
using ExpenseServices.Services;
using FluentAssertions;
using NUnit.Framework;
using NSubstitute;

namespace ExpenseTests;

[TestFixture]
public class CategoryServiceTests
{
    private ICategoryRepository _categoryRepository;
    private IRepository<string, Category> _repository;
    private ICategoryService _categoryService;

    [SetUp]
    public void SetUp()
    {
        _repository = Substitute.For<IRepository<string, Category>>();
        _categoryRepository = Substitute.For<ICategoryRepository>();
        _categoryService = Substitute.For<ICategoryService>();

        _categoryService = new CategoryService(_repository, _categoryRepository, _categoryService);
    }

    [Test]
    public async Task When_GetCategoriesAsync_Then_Should_Return_Categories()
    {
        // Arrange
        var categories = new List<Category>
        {
            new () { Id = "1", Name = "Food" },
            new () { Id = "2", Name = "Transport" }
        };
        _repository.GetAllAsync().Returns(categories);

        // Act
        var result = await _categoryService.GetCategoriesAsync();

        // Assert
        result.Should().BeEquivalentTo(categories);
    }

    [Test]
    public async Task When_GetCategoryByIdAsync_Then_Should_Return_Category()
    {
        // Arrange
        var category = new Category { Id = "1", Name = "Food" };
        _repository.GetByIdAsync("1").Returns(category);

        // Act
        var result = await _categoryService.GetCategoryByIdAsync("1");

        // Assert
        result.Should().BeEquivalentTo(category);
    }

    [Test]
    public async Task When_CategoryDoesNotExist_Then_GetCategoryByIdAsync_Should_Return_Null()
    {
        // Arrange
        _repository.GetByIdAsync("99").Returns((Category)null);

        // Act
        var result = await _categoryService.GetCategoryByIdAsync("99");

        // Assert
        result.Should().BeNull();
    }

    [Test]
    public async Task When_DuplicateCategoryName_Then_AddCategoryAsync_Should_ThrowException()
    {
        // Arrange
        var existingCategory = new Category { Id = "1", Name = "Food" };
        var newCategory = new Category { Id = "2", Name = "Food" };
        _repository.GetAllAsync().Returns(new List<Category> { existingCategory });

        // Act & Assert
        Func<Task> act = async () => await _categoryService.AddCategoryAsync(newCategory);
        await act.Should().ThrowAsync<InvalidOperationException>()
            .WithMessage("A category with the same name already exists.");
    }

    [Test]
    public async Task When_AddCategoryAsync_Then_Should_CallRepositoryAdd()
    {
        // Arrange
        var newCategory = new Category { Id = "2", Name = "Transport" };

        // Act
        await _categoryService.AddCategoryAsync(newCategory);

        // Assert
        await _repository.Received(1).AddAsync(newCategory);
    }

    [Test]
    public async Task When_DuplicateCategory_Then_Should_ThrowException()
    {
        // Arrange
        var existingCategory = new Category { Id = "1", Name = "Food" };
        var categoryToUpdate = new Category { Id = "2", Name = "Food" };
        _repository.GetAllAsync().Returns(new List<Category> { existingCategory });

        // Act & Assert
        Func<Task> act = async () => await _categoryService.UpdateCategoryAsync("2", categoryToUpdate);
        await act.Should().ThrowAsync<InvalidOperationException>()
            .WithMessage("A category with the same name already exists.");
    }

    [Test]
    public async Task When_UpdateCategoryAsync_Then_Should_CallRepositoryUpdate()
    {
        // Arrange
        var categoryToUpdate = new Category { Id = "2", Name = "Transport" };

        // Act
        await _categoryService.UpdateCategoryAsync("2", categoryToUpdate);

        // Assert
        await _repository.Received(1).UpdateAsync("2", categoryToUpdate);
    }

    [Test]
    public async Task When_DeleteCategoryAsync_Then_Should_CallRepositoryDelete()
    {
        // Arrange
        _repository.DeleteAsync("1").Returns(Task.CompletedTask);

        // Act
        await _categoryService.DeleteCategoryAsync("1");

        // Assert
        await _repository.Received(1).DeleteAsync("1");
    }
}