using ExpenseDataAccessLayer.Interfaces;
using ExpenseDataAccessLayer.Models;
using ExpenseServices.Services.Interfaces;
using ExpenseServices.Services;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;

namespace ExpenseTests
{

    [TestFixture]
    public class ExpenseServiceTests
    {
        private IRepository<string, Expense> _repository;
        private IExpenseRepository _expenseRepository;
        private IExpenseService _expenseService;

        [SetUp]
        public void SetUp()
        {
            _repository = Substitute.For<IRepository<string, Expense>>();
            _expenseRepository = Substitute.For<IExpenseRepository>();

            _expenseService = new ExpenseService(_repository, _expenseRepository);
        }

        [Test]
        public async Task When_GetExpensesAsync_Then_Should_ReturnExpenses()
        {
            // Arrange
            var expenses = new List<Expense> { new() { Id = "1", Amount = 100 } };
            _repository.GetAllAsync().Returns(expenses);

            // Act
            var result = await _expenseService.GetExpensesAsync();

            // Assert
            result.Should().BeEquivalentTo(expenses);
        }

        [Test]
        public async Task When_GetExpenseByIdAsync_Then_Should_ReturnCorrectExpense()
        {
            // Arrange
            var expense = new Expense { Id = "1", Amount = 100 };
            _repository.GetByIdAsync("1").Returns(expense);

            // Act
            var result = await _expenseService.GetExpenseByIdAsync("1");

            // Assert
            result.Should().Be(expense);
        }

        [Test]
        public async Task When_AddExpenseAsync_Then_Should_CallAddAsync()
        {
            // Arrange
            var expense = new Expense { Id = "1", Amount = 100 };

            // Act
            await _expenseService.AddExpenseAsync(expense);

            // Assert
            await _repository.Received(1).AddAsync(expense);
        }

        [Test]
        public async Task When_UpdateExpenseAsync_Then_Should_CallUpdateAsync()
        {
            // Arrange
            var expense = new Expense { Id = "1", Amount = 100 };

            // Act
            await _expenseService.UpdateExpenseAsync("1", expense);

            // Assert
            await _repository.Received(1).UpdateAsync("1", expense);
        }

        [Test]
        public async Task When_DeleteExpenseAsync_Then_Should_CallDeleteAsync()
        {
            // Act
            await _expenseService.DeleteExpenseAsync("1");

            // Assert
            await _repository.Received(1).DeleteAsync("1");
        }

        [Test]
        public async Task When_GetExpensesByCategoryAsync_Then_Should_ReturnExpensesForCategory()
        {
            // Arrange
            var categoryId = Guid.NewGuid().ToString();
            var expenses = new List<Expense> { new() { Id = "1", Amount = 100, CategoryId = categoryId } };
            _expenseRepository.GetExpensesByCategoryAsync(categoryId).Returns(expenses);

            // Act
            var result = await _expenseService.GetExpensesByCategoryAsync(categoryId);

            // Assert
            result.Should().BeEquivalentTo(expenses);
        }

        [Test]
        public async Task When_GetExpensesAmountInterval_Then_Should_ReturnExpensesWithinAmountRange()
        {
            // Arrange
            var expenses = new List<Expense> { new() { Id = "1", Amount = 150 } };
            _expenseRepository.GetExpensesAmountInterval(100, 200).Returns(expenses);

            // Act
            var result = await _expenseService.GetExpensesAmountInterval(100, 200);

            // Assert
            result.Should().BeEquivalentTo(expenses);
        }

        [Test]
        public async Task When_GetExpensesDatesInterval_Then_Should_ReturnExpensesWithinDateRange()
        {
            // Arrange
            var minDate = new DateTime(2024, 1, 1);
            var maxDate = new DateTime(2024, 12, 31);
            var expenses = new List<Expense> { new() { Id = "1", CreatedDate = new DateTime(2024, 6, 15) } };
            _expenseRepository.GetExpensesDatesInterval(minDate, maxDate).Returns(expenses);

            // Act
            var result = await _expenseService.GetExpensesDatesInterval(minDate, maxDate);

            // Assert
            result.Should().BeEquivalentTo(expenses);
        }
    }
}
