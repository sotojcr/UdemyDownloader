namespace UdemyDownloader.Services.Udemy.Authentication.Token
{
    public interface ICsrfTokenExtractor
    {
        string GetToken(string loginPageContent);
    }
}