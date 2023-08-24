using ExpenSpend.Core.User;
using ExpenSpend.Repository.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ExpenSpend.Web.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class UserController : ControllerBase
{
    private readonly IUserRepository _userRepository;
    private readonly UserManager<Domain.Models.User> _userManager;


    public UserController(IUserRepository userRepository, UserManager<Domain.Models.User> userManager)
    {
        _userRepository = userRepository;
        _userManager = userManager;
    }
    
    [HttpGet("logged-in-user")]
    public async Task<IActionResult> GetLoggedInUser()
    {
        var user =  await _userManager.GetUserAsync(User);
        if (user == null)
        {
            return NotFound("User not found");
        }
        return Ok(user);
    }
    
    [HttpGet("users")]
    public async Task<IActionResult> GetAllUsers()
    {
        var users = await _userRepository.GetAllUsersAsync();
        return Ok(users);
    }
    
    [HttpGet("users/{id}")]
    public async Task<IActionResult> GetUserById(string id)
    {
        var user = await _userRepository.GetUserByIdAsync(id);
        if (user == null)
        {
            return NotFound("User not found");
        }
        return Ok(user);
    }
    
    [HttpPut("users/{id}")]
    public async Task<IActionResult> UpdateUser(string id, UpdateUserDto input)
    {
        var user = await _userRepository.GetUserByIdAsync(id);
        if (user == null)
        {
            return NotFound("User not found");
        }
        user.UserName = input.UserName;
        user.FirstName = input.FirstName;
        user.LastName = input.LastName;
        user.Email = input.Email;
        user.PhoneNumber = input.PhoneNumber;
        var result = await _userRepository.UpdateUserAsync(user);
        
        if (result.Succeeded)
        {
            return Ok("User updated successfully");
        }
        return BadRequest(result.Errors);
    }
    
    [HttpDelete("users/{id}")]
    public async Task<IActionResult> DeleteUser(string id)
    {
        var user = await _userRepository.GetUserByIdAsync(id);
        if (user == null)
        {
            return NotFound("User not found");
        }
        var result = await _userRepository.DeleteUserAsync(user);
        if (result.Succeeded)
        {
            return Ok("User deleted successfully");
        }
        return BadRequest(result.Errors);
    }
}