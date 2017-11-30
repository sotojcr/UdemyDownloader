using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using UdemyDownloader.Configuration;
using UdemyDownloader.Services.Udemy.Authentication;
using UdemyDownloader.Services.Udemy.Authentication.Token;

namespace UdemyDownloader
{
    public class Program
    {
        //public static async Task Main(string[] args)
        public static void Main(string[] args)
        {
            var serviceProvider = BuildServiceProvider();

            var authService = serviceProvider.GetService<IAuthenticationService>();
            authService.AuthenticateAsync("username", "password", "https://omikron.udemy.com/").Wait();

            Console.WriteLine("Hello World!");
        }

        public static ServiceProvider BuildServiceProvider()
        {
            var serviceCollection = new ServiceCollection();
            InitializeConfiguration(serviceCollection);
            serviceCollection.AddTransient<IAuthenticationService, AuthenticationService>();
            serviceCollection.AddTransient<ICsrfTokenExtractor, CsrfTokenExtractor>();

            return serviceCollection.BuildServiceProvider();
        }

        private static void InitializeConfiguration(IServiceCollection serviceCollection)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", false, true);

            var configuration = builder.Build();

            var udemyUrlConfigurationSection = new UdemyUrlConfigurationSection();
            configuration.GetSection(nameof(UdemyUrlConfigurationSection)).Bind(udemyUrlConfigurationSection);
            serviceCollection.AddSingleton(udemyUrlConfigurationSection);

            var udemyRegexConfigurationSection = new UdemyRegexConfigurationSection();
            configuration.GetSection(nameof(UdemyRegexConfigurationSection)).Bind(udemyRegexConfigurationSection);
            serviceCollection.AddSingleton(udemyRegexConfigurationSection);
        }
    }
}