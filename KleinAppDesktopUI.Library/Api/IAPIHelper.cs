using KleinMessage.Models;
using System.Threading.Tasks;


 namespace KleinAppDesktopUI.Library.Api
{
    public interface IAPIHelper
    {
        Task<AuthenticatedUser> Authenticate(string username, string password);
        Task GetLoggedInUserInfo(string token);
    }
}