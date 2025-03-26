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

        private const string UserId = "userId";

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
            var expenses = new List<Expense> { new() { Id = "1", Amount = 100, UserId = UserId } };
            _repository.GetAllAsync().Returns(expenses);

            // Act
            var result = await _expenseService.GetUserExpensesAsync(UserId);

            // Assert
            result.Should().BeEquivalentTo(expenses);
        }

        [Test]
        public async Task When_GetExpenseByIdAsync_Then_Should_ReturnCorrectExpense()
        {
            // Arrange
            var expense = new Expense { Id = "1", Amount = 100, UserId = UserId };
            _repository.GetByIdAsync("1").Returns(expense);

            // Act
            var result = await _expenseService.GetExpenseByIdAsync(UserId, "1");

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
            var expenses = new List<Expense> { new() { Id = "1", Amount = 100, CategoryId = categoryId, UserId = UserId } };
            _expenseRepository.GetExpensesByCategoryAsync(UserId, categoryId).Returns(expenses);

            // Act
            var result = await _expenseService.GetExpensesByCategoryAsync(UserId, categoryId);

            // Assert
            result.Should().BeEquivalentTo(expenses);
        }

        [Test]
        public async Task When_GetExpensesAmountInterval_Then_Should_ReturnExpensesWithinAmountRange()
        {
            // Arrange
            var expenses = new List<Expense> { new() { Id = "1", Amount = 150, UserId = UserId } };
            _expenseRepository.GetExpensesAmountInterval(UserId, 100, 200).Returns(expenses);

            // Act
            var result = await _expenseService.GetExpensesAmountInterval(UserId, 100, 200);

            // Assert
            result.Should().BeEquivalentTo(expenses);
        }

        [Test]
        public async Task When_GetExpensesByCategoryAsync_Then_Should_ReturnExpensesByCategory()
        {
            // Arrange
            var categoryId = "category1";
            var expectedExpenses = new List<Expense>
            {
                new () { Id = "1", UserId = UserId, CategoryId = categoryId, Amount = 100 },
                new () { Id = "2", UserId = UserId, CategoryId = categoryId, Amount = 200 }
            };

            _expenseRepository.GetExpensesByCategoryAsync(UserId, categoryId).Returns(expectedExpenses);

            // Act
            var result = await _expenseService.GetExpensesByCategoryAsync(UserId, categoryId);

            // Assert
            result.Should().BeEquivalentTo(expectedExpenses);
            await _expenseRepository.Received(1).GetExpensesByCategoryAsync(UserId, categoryId);
        }

        [Test]
        public async Task When_GetExpensesAmountInterval_Then_Should_ReturnExpensesWithinInterval()
        {
            // Arrange
            var minAmount = 50m;
            var maxAmount = 200m;
            var expectedExpenses = new List<Expense>
            {
                new () { Id = "1", UserId = UserId, Amount = 75 },
                new () { Id = "2", UserId = UserId, Amount = 150 }
            };

            _expenseRepository.GetExpensesAmountInterval(UserId, minAmount, maxAmount).Returns(expectedExpenses);

            // Act
            var result = await _expenseService.GetExpensesAmountInterval(UserId, minAmount, maxAmount);

            // Assert
            result.Should().BeEquivalentTo(expectedExpenses);
            await _expenseRepository.Received(1).GetExpensesAmountInterval(UserId, minAmount, maxAmount);
        }

        [Test]
        public async Task When_GetExpensesDatesInterval_Then_Should_ReturnExpensesWithinDateRange()
        {
            // Arrange
            var minDate = new DateTime(2024, 1, 1);
            var maxDate = new DateTime(2024, 12, 31);
            var expectedExpenses = new List<Expense>
            {
                new () { Id = "1", UserId = UserId, CreatedDate = new DateTime(2024, 3, 15) },
                new () { Id = "2", UserId = UserId, CreatedDate = new DateTime(2024, 6, 10) }
            };

            _expenseRepository.GetExpensesDatesInterval(UserId, minDate, maxDate).Returns(expectedExpenses);

            // Act
            var result = await _expenseService.GetExpensesDatesInterval(UserId, minDate, maxDate);

            // Assert
            result.Should().BeEquivalentTo(expectedExpenses);
            await _expenseRepository.Received(1).GetExpensesDatesInterval(UserId, minDate, maxDate);
        }
    }
}
