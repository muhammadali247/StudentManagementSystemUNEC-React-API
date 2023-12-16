using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using MimeKit;
using MimeKit.Text;
using StudentManagementSystemUNEC.Business.HelperServices.Interfaces;

namespace StudentManagementSystemUNEC.Business.HelperServices.Implementations;

public class EmailService : IEmailService
{
    private readonly IConfiguration _config;

    public EmailService(IConfiguration config)
    {
        _config = config;
    }

    public void Send(string to, string subject, string body)
    {
        // Create message
        var email = new MimeMessage();
        email.From.Add(MailboxAddress.Parse(_config.GetSection("Smtp:FromAddress").Value));
        email.To.Add(MailboxAddress.Parse(to));
        email.Subject = subject;
        email.Body = new TextPart(TextFormat.Html) { Text = body };

        // Send email
        using SmtpClient smtp = new SmtpClient();
        smtp.Connect(_config.GetSection("Smtp:Server").Value,
            int.Parse(_config.GetSection("Smtp:Port").Value),
            MailKit.Security.SecureSocketOptions.StartTls);
        smtp.Authenticate(_config.GetSection("Smtp:FromAddress").Value,
            _config.GetSection("Smtp:Password").Value);
        smtp.Send(email);
        smtp.Disconnect(true);
    }
}