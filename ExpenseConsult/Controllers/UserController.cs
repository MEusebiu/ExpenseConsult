//using ExpenseConsult.Models;
//using ExpenseConsult.Models.DTO;
//using ExpenseConsult.Services.Interfaces;
//using Microsoft.AspNetCore.Mvc;

//namespace ExpenseConsult.Controllers;

//[ApiController]
//[Route("api/[controller]")]
//public class UserController : ControllerBase
//{
//    private IUserService _userService;

//    public UserController(IUserService userService)
//    {
//        _userService = userService;
//    }

//    [HttpGet]
//    public async Task<IActionResult> GetUsers()
//    {
//        var user = await _userService.GetAllUsersAsync();

//        return Ok(user);
//    }

//    [HttpGet("{id}")]
//    public async Task<IActionResult> GetUserById(int id)
//    {
//        var user = await _userService.GetUserByIdAsync(id);
//        if (user == null)
//        {
//            return NotFound();
//        }

//        return Ok(user);
//    }

//    [HttpPost]
//    public async Task<IActionResult> CreateUser([FromBody] User user)
//    {
//        if (user == null)
//        {
//            return BadRequest("User data is required.");
//        }

//        try
//        {
//            await _userService.AddUserAsync(user);
//        }
//        catch (Exception ex)
//        {
//            return Conflict(ex.Message);
//        }

//        return Ok();
//    }

//    [HttpPut("{id}")]
//    public async Task<IActionResult> UpdateUser(int id, [FromBody] UserUpdateDto userUpdateDto)
//    {
//        var user = await _userService.GetUserByIdAsync(id);
//        user.UserName = userUpdateDto.Name;

//        await _userService.UpdateUserAsync(id, user);

//        return Ok();
//    }

//    [HttpDelete("{id}")]
//    public async Task<IActionResult> DeleteUser(int id)
//    {
//        await _userService.DeleteUserAsync(id);

//        return Ok(); 
//    }
//}
