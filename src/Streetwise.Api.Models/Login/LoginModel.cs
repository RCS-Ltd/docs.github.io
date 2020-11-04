namespace Streetwise.Api.Models
{
    /// <summary>
    /// Used to login to the API with users ClientId and Secret
    /// </summary>
    public class LoginModel
    {
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
    }
}
