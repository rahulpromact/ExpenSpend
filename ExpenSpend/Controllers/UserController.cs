using ExpenSpend.Core.Account;
using ExpenSpend.Core.User;
using ExpenSpend.Repository.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ExpenSpend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserRepository _userRepository;
    
    public UserController(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    
    [HttpPost("register")]
    public async Task<IActionResult> RegisterUserAsync(CreateUserDto user)
    {
        var result = await _userRepository.RegisterUserAsync(user);
        if (result.Succeeded)
        {
            return Ok();
        }
        return BadRequest(result.Errors);
    }
    
    [HttpPost("login")]
    public async Task<IActionResult> LoginUserAsync(LoginDto login)
    {
        var result = await _userRepository.LoginUserAsync(login.UserName, login.Password);
        if (result.Succeeded)
        {
            return Ok();
        }
        return BadRequest(result);
    }
    
    [Authorize]
    [HttpPost("logout")]
    public async Task<IActionResult> LogoutUserAsync()
    {
        await _userRepository.LogoutUserAsync();
        return Ok();
    }
}