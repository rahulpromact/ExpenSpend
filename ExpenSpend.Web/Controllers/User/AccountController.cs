using System.IdentityModel.Tokens.Jwt;
using AutoMapper;
using ExpenSpend.Core.Account;
using ExpenSpend.Core.User;
using ExpenSpend.Domain.Shared.Account;
using ExpenSpend.Repository.Account;
using ExpenSpend.Service.Email.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ExpenSpend.Web.Controllers.User;

[Route("api/[controller]/[action]")]
[ApiController]
public class AccountController: ControllerBase
{
    
    private readonly IAccountRepository _accountRepository;
    private readonly IMapper _mapper;
    private readonly IEmailService _emailService;
    public AccountController(IAccountRepository accountRepository, IMapper mapper, IEmailService emailService)
    {
        _accountRepository = accountRepository;
        _mapper = mapper;
        _emailService = emailService;
    }
    
    [HttpPost]
    public async Task<IActionResult> RegisterUserAsync(CreateUserDto input)
    {
        var user = _mapper.Map<Domain.Models.Users.User>(input);
        var registrationResult = await _accountRepository.RegisterUserAsync(user, input.Password);

        if (registrationResult.Succeeded)
        {
            
            var emailConfirmationToken = await _accountRepository.GenerateEmailConfirmationTokenAsync(user);
            var confirmationLink = Url.Action(nameof(ConfirmEmail), "Account", new { token = emailConfirmationToken, email = user.Email }, Request.Scheme);
            var emailMessage = await _emailService.CreateEmailValidationTemplateMessage(user.Email, confirmationLink);
            _emailService.SendEmail(emailMessage);

            return Ok(AccConsts.RegSuccessMessage);
        }
        registrationResult.Errors.ToList().ForEach(error => ModelState.AddModelError(error.Code, error.Description));
        return BadRequest(ModelState);
    }

    [HttpGet]
    public async Task<ContentResult> ConfirmEmail(string token, string email)
    {
        var user = await _accountRepository.FindByEmail(email);

        if (user == null)
        {
            return Content(AccConsts.UserNotFound);
        }

        var emailConfirmationResult = await _accountRepository.ConfirmEmailAsync(user, token);

        if (emailConfirmationResult.Succeeded)
        {
            var htmlContent = await _emailService.EmailConfirmationPageTemplate();

            return Content(htmlContent, "text/html");
        }

        emailConfirmationResult.Errors.ToList().ForEach(error => ModelState.AddModelError(error.Code, error.Description));
        return Content(ModelState.ToString()!);
    }


    //[HttpPost]
    //public async Task<IActionResult> LoginUserAsync(LoginDto login)
    //{
    //    var result = await _accountRepository.LoginUserAsync(login.UserName, login.Password);
    //    if (result.Succeeded)
    //    {
    //        return Ok();
    //    }
    //    return BadRequest(result);
    //}

    [HttpPost]
    public async Task<IActionResult> LoginUserAsync(LoginDto login)
    {   
        var userToken = await _accountRepository.LoginUserJwtAsync(login.UserName, login.Password, login.RememberMe);
        if (userToken != null)
        {
            return Ok(new { token = new JwtSecurityTokenHandler().WriteToken(userToken) });
        }
        return Unauthorized();
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

        var resetToken = await _accountRepository.GenerateResetToken(user);
        var resetLink = Url.Action(nameof(ResetPassword), "Account", new { token = resetToken, email = user.Email }, Request.Scheme);

        _emailService.SendPasswordResetEmail(user.Email, resetLink!);

        return Ok(AccConsts.PasswordResetReqSuccess);
    }
    
    [HttpGet]
    public IActionResult ResetPassword(string token, string email)
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

        var resetPasswordResult = await _accountRepository.ResetPasswordAsync(user, resetPasswordDto.Token, resetPasswordDto.Password);
        if (resetPasswordResult.Succeeded)
        {
            _emailService.SandPasswordChangeNotification(user.Email, user.FirstName);
            return Ok();
        }

        resetPasswordResult.Errors.ToList().ForEach(error => ModelState.AddModelError(error.Code, error.Description));
        return BadRequest(ModelState);
    }

}