using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Threading.Tasks;
using UdemyDownloader.Configuration;
using UdemyDownloader.Services.Udemy.Authentication.Token;
using Microsoft.Extensions.DependencyInjection;

namespace UdemyDownloader.Tests.Services.Udemy.Authentication
{
    [TestClass]
    public class CsrfTokenExtractorTest
    {
        private const string UdemyOrganizationLoginPageFilename = "Testfiles\\UdemyOrganizationLoginPage.html";
        private const string UdemyPersonalLoginPageFilename = "Testfiles\\UdemyPersonalLoginPage.html";

        [TestMethod]
        public async Task CanExtractTokenFromPersonalUdemy()
        {
            var configuration = Program.BuildServiceProvider().GetService<UdemyRegexConfigurationSection>();
            var fileContent = await File.ReadAllTextAsync(UdemyPersonalLoginPageFilename);

            var tokenExtractor = new CsrfTokenExtractor(configuration);
            var token = tokenExtractor.GetToken(fileContent);

            Assert.AreEqual("CiwL3eQE4xV8FynVCJw4XwYxwGzn4Ukr", token);
        }

        [TestMethod]
        public async Task CanExtractTokenFromOrganizationUdemy()
        {
            var configuration = Program.BuildServiceProvider().GetService<UdemyRegexConfigurationSection>();
            var fileContent = await File.ReadAllTextAsync(UdemyOrganizationLoginPageFilename);

            var tokenExtractor = new CsrfTokenExtractor(configuration);
            var token = tokenExtractor.GetToken(fileContent);

            Assert.AreEqual("cgz3NkDfFyolSajU3GB5XZfhaVIWYYYt", token);
        }

        [TestMethod]
        public void ExtractTokenThrowsTokenExtractionFailedException()
        {
            var configuration = Program.BuildServiceProvider().GetService<UdemyRegexConfigurationSection>();
            var content = "no csrf token available";

            var tokenExtractor = new CsrfTokenExtractor(configuration);

            Assert.ThrowsException<TokenExtractionFailedException>(() => tokenExtractor.GetToken(content));
        }
    }
}