using KleinAppDesktopUI.Library.Models;
using KleinMessage.Models;
using System.Threading.Tasks;


 namespace KleinAppDesktopUI.Library.Api
{
    public interface IAPIHelper
    {
        Task<AuthenticatedUser> Authenticate(string username, string password);
        Task<LoggedInUserModel> GetLoggedInUserInfo(string token);
        Task<bool> Register(string email, string password, string confirmpassword, string FirstName, string LastName);
        Task<bool> ChangePassword(string token, string oldPassword, string newPassword, string confirmPassword);
    }
}