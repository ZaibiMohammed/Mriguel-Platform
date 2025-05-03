using Mriguel.Application.Common.Interfaces;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using MimeKit;
using MimeKit.Text;

namespace Mriguel.Infrastructure.Services
{
    /// <summary>
    /// Service for sending emails
    /// </summary>
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendEmailAsync(string to, string subject, string body, bool isHtml = false)
        {
            // Create a new MimeMessage
            var email = new MimeMessage();
            
            email.From.Add(MailboxAddress.Parse(_configuration["Email:From"]));
            email.To.Add(MailboxAddress.Parse(to));
            email.Subject = subject;
            
            // Set the body of the email
            email.Body = new TextPart(isHtml ? TextFormat.Html : TextFormat.Plain)
            {
                Text = body
            };
            
            // Send the email
            using var smtp = new SmtpClient();
            await smtp.ConnectAsync(
                _configuration["Email:SmtpServer"],
                int.Parse(_configuration["Email:Port"] ?? "587"),
                SecureSocketOptions.StartTls);
                
            await smtp.AuthenticateAsync(_configuration["Email:Username"], _configuration["Email:Password"]);
            await smtp.SendAsync(email);
            await smtp.DisconnectAsync(true);
        }

        public async Task SendTemplatedEmailAsync(string to, string templateName, object model)
        {
            // This is a basic implementation that could be extended with a template engine
            // like Razor, Handlebars, etc.
            
            var subject = string.Empty;
            var body = string.Empty;
            
            // Simple template selection
            switch (templateName)
            {
                case "WelcomeEmail":
                    subject = "Welcome to AlloVoisin!";
                    body = $"<h1>Welcome to AlloVoisin!</h1><p>Thank you for joining our platform.</p>";
                    break;
                
                case "RentalRequest":
                    var rental = model as dynamic;
                    subject = "New Rental Request";
                    body = $"<h1>New Rental Request</h1><p>You have received a new rental request for {rental?.ItemTitle}.</p>";
                    break;
                
                case "RentalConfirmation":
                    rental = model as dynamic;
                    subject = "Rental Confirmed";
                    body = $"<h1>Rental Confirmed</h1><p>Your rental request for {rental?.ItemTitle} has been confirmed.</p>";
                    break;
                
                default:
                    throw new ArgumentException($"Unknown email template: {templateName}");
            }
            
            await SendEmailAsync(to, subject, body, true);
        }
    }
}
