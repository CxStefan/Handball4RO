using Microsoft.AspNetCore.Identity;

namespace Handball4RO.Services
{
    public interface IAuthService
    {
        Task<IdentityResult> RegisterAsync(string email, string password, string role);
        Task<SignInResult> LoginAsync(string email, string password, bool rememberMe);
        Task LogoutAsync();
    }
}