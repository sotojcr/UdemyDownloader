namespace UdemyDownloader.Services.Udemy
{
    internal static class UdemyHelper
    {
        public static bool IsOrganizationUrl(string url)
        {
            const string organizationUrlPattern = ".udemy.com";
            return url.Contains(organizationUrlPattern);
        }
    }
}