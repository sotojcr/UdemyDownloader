using System;
using System.Net.Http;
using System.Threading.Tasks;
using UdemyDownloader.Configuration;
using UdemyDownloader.Services.Udemy.Authentication.Token;

namespace UdemyDownloader.Services.Udemy.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly UdemyUrlConfigurationSection _udemyUrlConfigurationSection;
        private readonly ICsrfTokenExtractor _tokenExtractor;

        public AuthenticationService(UdemyUrlConfigurationSection udemyUrlConfigurationSection, ICsrfTokenExtractor tokenExtractor)
        {
            _udemyUrlConfigurationSection = udemyUrlConfigurationSection;
            _tokenExtractor = tokenExtractor;
        }

        public async Task AuthenticateAsync(string username, string password, string udemyBaseUrl)
        {
            var csrfToken = await GetCsrfTokenAsync(udemyBaseUrl);
        }

        private async Task<string> GetCsrfTokenAsync(string udemyBaseUrl)
        {
            var uri = GetLoginPageUrl(udemyBaseUrl);

            var loginPageContent = await new HttpClient().GetStringAsync(uri);
            return _tokenExtractor.GetToken(loginPageContent);
        }

        private Uri GetLoginPageUrl(string udemyBaseUrl)
        {
            var baseUrl = new Uri(udemyBaseUrl);

            if(UdemyHelper.IsOrganizationUrl(udemyBaseUrl))
            {
                return new Uri(baseUrl, _udemyUrlConfigurationSection.OrganizationLoginPageUrl);
            }

            return new Uri(baseUrl, _udemyUrlConfigurationSection.PersonalLoginPageUrl);
        }
    }
}