using System.Threading.Tasks;

namespace UdemyDownloader.Services.Udemy.Authentication
{
    public interface IAuthenticationService
    {
        Task AuthenticateAsync(string username, string password, string udemyBaseUrl);
    }
}