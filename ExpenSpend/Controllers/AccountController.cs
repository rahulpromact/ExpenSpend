using AutoMapper;
using ExpenSpend.Core.Account;
using ExpenSpend.Core.User;
using ExpenSpend.Domain.Models;
using ExpenSpend.Repository.Account;
using ExpenSpend.Util.Models;
using ExpenSpend.Util.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ExpenSpend.Web.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AccountController : ControllerBase
{
    
    private readonly IAccountRepository _accountRepository;
    private readonly IMapper _mapper;
    private readonly UserManager<User> _userManager;
    private readonly IEmailService _emailService;
    public AccountController(IAccountRepository accountRepository, IMapper mapper, UserManager<User> userManager, IEmailService emailService)
    {
        _accountRepository = accountRepository;
        _mapper = mapper;
        _userManager = userManager;
        _emailService = emailService;
    }
    
    [HttpPost("register")]
    public async Task<IActionResult> RegisterUserAsync(CreateUserDto input)
    {
        var user = _mapper.Map<User>(input);
        var result = await _accountRepository.RegisterUserAsync(user, input.Password);
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
    
    [HttpGet("TestMail")]
    public async Task<IActionResult> TestEmail()
    {
        var message = new Message(new string[] { "r1303yadav@gmail.com" }, "Test", "<h1>Hello</h1>");
        _emailService.SendEmail(message);
        return Ok("Message sand successfully");
    }
}