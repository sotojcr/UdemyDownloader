using System.Text.RegularExpressions;
using UdemyDownloader.Configuration;

namespace UdemyDownloader.Services.Udemy.Authentication.Token
{
    public class CsrfTokenExtractor : ICsrfTokenExtractor
    {
        private readonly UdemyRegexConfigurationSection _udemyRegexConfigurationSection;

        private const string _tokenGroupName = "csrftoken";

        public CsrfTokenExtractor(UdemyRegexConfigurationSection udemyRegexConfigurationSection)
        {
            _udemyRegexConfigurationSection = udemyRegexConfigurationSection;
        }

        public string GetToken(string loginPageContent)
        {
            var token = ExtractToken(loginPageContent, _udemyRegexConfigurationSection.PersonalTokenRegex);

            if(!string.IsNullOrEmpty(token))
            {
                return token;
            }

            token = ExtractToken(loginPageContent, _udemyRegexConfigurationSection.OrganizationTokenRegex);

            if(!string.IsNullOrWhiteSpace(token))
            {
                return token;
            }

            throw new TokenExtractionFailedException();
        }

        private string ExtractToken(string loginPageContent, string regex)
        {
            return Regex.Match(loginPageContent, regex).Groups[_tokenGroupName].Value;
        }
    }
}