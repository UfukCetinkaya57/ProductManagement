using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace ProductManagement.Application.Services
{
    public interface IUserService
    {
        Task<IdentityResult> RegisterUserAsync(string username, string email, string password);
        Task<SignInResult> LoginUserAsync(string username, string password);
        Task<IdentityUser> FindByNameAsync(string username);

    }

    public class UserService : IUserService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public UserService(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<IdentityUser> FindByNameAsync(string username)
        {
            return await _userManager.FindByNameAsync(username);
        }
        public async Task<IdentityResult> RegisterUserAsync(string username, string email, string password)
        {
            var user = new IdentityUser { UserName = username, Email = email };
            var result = await _userManager.CreateAsync(user, password);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "User");
            }

            return result;
        }

        public async Task<SignInResult> LoginUserAsync(string username, string password)
        {
            var result = await _signInManager.PasswordSignInAsync(username, password, false, false);
            return result;
        }
    }
}
