namespace AlloVoisinClone.Application.Common.Interfaces
{
    /// <summary>
    /// Interface for email service
    /// </summary>
    public interface IEmailService
    {
        /// <summary>
        /// Sends an email
        /// </summary>
        Task SendEmailAsync(string to, string subject, string body, bool isHtml = false);
        
        /// <summary>
        /// Sends a templated email
        /// </summary>
        Task SendTemplatedEmailAsync(string to, string templateName, object model);
    }
}
