using AutoMapper;
using ExpenSpend.Core.Account;
using ExpenSpend.Core.User;
using ExpenSpend.Domain.Models;
using ExpenSpend.Domain.Shared.Account;
using ExpenSpend.Repository.Account;
using ExpenSpend.Util.Models;
using ExpenSpend.Util.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace ExpenSpend.Web.Controllers;

[Route("api/[controller]/[action]")]
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
    
    [HttpPost]
    public async Task<IActionResult> RegisterUserAsync(CreateUserDto input)
    {
        var user = _mapper.Map<User>(input);
        var registrationResult = await _accountRepository.RegisterUserAsync(user, input.Password);

        if (registrationResult.Succeeded)
        {
            
            var emailConfirmationToken = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var confirmationLink = Url.Action(nameof(ConfirmEmail), "Account", new { token = emailConfirmationToken, email = user.Email }, Request.Scheme);
            var emailMessage = await _emailService.CreateEmailValidationTemplateMessage(user.Email, confirmationLink);
            _emailService.SendEmail(emailMessage);

            return Ok(AccConsts.RegSuccessMessage);
        }

        return BadRequest($"Registration failed: {string.Join(", ", registrationResult.Errors)}");
    }

    [HttpGet]
    public async Task<ContentResult> ConfirmEmail(string token, string email)
    {
        var user = await _accountRepository.FindByEmail(email);

        if (user == null)
        {
            return Content(AccConsts.UserNotFound);
        }

        var emailConfirmationResult = await _userManager.ConfirmEmailAsync(user, token);

        if (emailConfirmationResult.Succeeded)
        {
            var htmlContent = await _emailService.EmailConfirmationPageTemplate();

            return Content(htmlContent, "text/html");
        }

        return Content($"{AccConsts.ConfEmailFailed}: {string.Join(", ", emailConfirmationResult.Errors)}");
    }


    [HttpPost]
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
    [HttpPost]
    public async Task<IActionResult> LogoutUserAsync()
    {
        await _accountRepository.LogoutUserAsync();
        return Ok();
    }
    
    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> ForgotPassword(string email)
    {
        var user = await _accountRepository.FindByEmail(email);
        if (user == null)
        {
            return BadRequest(AccConsts.UserNotFound);
        }

        var passwordResetToken = await _userManager.GeneratePasswordResetTokenAsync(user);
        var passwordResetLink = Url.Action(nameof(ResetPassword), "Account", new { token = passwordResetToken, email = user.Email }, Request.Scheme);
        var emailMessage = new Message(new[] { user.Email }, "Forgot Password Link", passwordResetLink!);
        _emailService.SendEmail(emailMessage);

        return Ok(AccConsts.PasswordResetReqSuccess);
    }
    
    [HttpGet]
    public async Task<IActionResult> ResetPassword(string token, string email)
    {
        var model = new ResetPasswordDto{Token = token, Email = email};
        return Ok(model);
    }
    
    [HttpPost]
    public async Task<IActionResult> ResetPassword(ResetPasswordDto resetPasswordDto)
    {
        var user = await _accountRepository.FindByEmail(resetPasswordDto.Email);
        if (user == null)
        {
            return BadRequest(AccConsts.UserNotFound);
        }

        var resetPasswordResult = await _userManager.ResetPasswordAsync(user, resetPasswordDto.Token, resetPasswordDto.Password);
        if (resetPasswordResult.Succeeded)
        {
            return Ok();
        }

        foreach (var error in resetPasswordResult.Errors)
        {
            ModelState.AddModelError(error.Code, error.Description);
        }
        return BadRequest(ModelState);
    }

}