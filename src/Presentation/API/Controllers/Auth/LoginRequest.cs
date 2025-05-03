namespace Mriguel.API.Controllers.Auth
{
    /// <summary>
    /// Login request model
    /// </summary>
    public class LoginRequest
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
