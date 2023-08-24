
using ExpenSpend.Core.User;
using Microsoft.AspNetCore.Identity;



namespace ExpenSpend.Repository.User
{
    
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<Domain.Models.User> _userManager;
        private readonly SignInManager<Domain.Models.User> _signInManager;

        public UserRepository(UserManager<Domain.Models.User> userManager, SignInManager<Domain.Models.User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public async Task<IdentityResult> RegisterUserAsync(CreateUserDto user)
        {
           return await _userManager.CreateAsync(new Domain.Models.User
            {
                UserName = user.UserName,
                LastName = user.LastName,
                FirstName = user.FirstName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber
            }, user.Password);
        }

        public async Task<SignInResult> LoginUserAsync(string email, string password)
        {
            return await _signInManager.PasswordSignInAsync(email, password, false, false);
        }
        
        public async Task LogoutUserAsync()
        {
            await _signInManager.SignOutAsync();
        }
    }
}
