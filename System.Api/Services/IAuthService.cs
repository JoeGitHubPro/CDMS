using System.Api.Models;

namespace System.Api.Services
{
    public interface IAuthService
    {
        Task<AuthModel> RegisterAsync(RegisterModel model);
        Task<AuthModel> GetTokenAsync(TokenRequestModel model);
        Task<string> AddRoleAsync(AddRoleModel model);
        Task<AuthResult> ConfirmEmailAsync(ConfirmEmailModel model);
        Task<AuthResult> ResetPasswordAsync(ResetPasswordModel model);
        Task<AuthResult> LogoutAsync(LogoutModel model);
    }
}