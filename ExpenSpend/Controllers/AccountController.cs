using ExpenSpend.Core.Account;
using ExpenSpend.Core.User;
using ExpenSpend.Repository.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ExpenSpend.Web.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AccountController : ControllerBase
{
    
    private readonly IAccountRepository _accountRepository;

    public AccountController(IAccountRepository accountRepository)
    {
        _accountRepository = accountRepository;
    }
    
    [HttpPost("register")]
    public async Task<IActionResult> RegisterUserAsync(CreateUserDto input)
    {
        var result = await _accountRepository.RegisterUserAsync(input);
        if (result.Succeeded)
        {
            return Ok();
        }
        return BadRequest(result.Errors);
    }
    
    [HttpPost("login")]
    public async Task<IActionResult> LoginUserAsync(LoginDto login)
    {
        var result = await _accountRepository.LoginUserAsync(login.UserName, login.Password);
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
        await _accountRepository.LogoutUserAsync();
        return Ok();
    }
}