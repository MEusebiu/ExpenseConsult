using ExpenseConsult.Models;
using ExpenseConsult.Repositories;
using ExpenseConsult.Services.Interfaces;

namespace ExpenseConsult.Services;

public class UserService : IUserService
{
    private readonly IRepository<string, User> _userRepository;

    public UserService(IRepository<string, User> userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<IEnumerable<User>> GetAllUsersAsync()
    {
        return await _userRepository.GetAllAsync();
    }

    public async Task<User> GetUserByIdAsync(string id)
    {
        return await _userRepository.GetByIdAsync(id);
    }

    public async Task AddUserAsync(User user)
    {
        CheckForDuplicateEmail(user);
        await _userRepository.AddAsync(user);
    }

    public async Task UpdateUserAsync(string key, User user)
    {
        await _userRepository.UpdateAsync(key, user);
    }

    public async Task DeleteUserAsync(string id)
    {
        await _userRepository.DeleteAsync(id);
    }

    private void CheckForDuplicateEmail(User user)
    {
        bool existingEmail = GetAllUsersAsync().Result.Any(c => c.Email == user.Email);
        if (existingEmail)
        {
            throw new InvalidOperationException("A user with the same email already exists.");
        }
    }
}
