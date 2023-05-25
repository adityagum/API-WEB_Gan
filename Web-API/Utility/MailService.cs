using System.Net.Mail;
using System.Net;

namespace Web_API.Utility;

// Kelompok 5
public class Message
{
    public string Subject { get; set; }
    public string Body { get; set; }
    public string Email { get; set; }
}

public class MailService
{
    private Message message;
    private SmtpClient smtpClient;

    public MailService()
    {
        message = new Message();
        smtpClient = new SmtpClient("localhost", 25)
        {
            DeliveryMethod = SmtpDeliveryMethod.Network,
            EnableSsl = false,
            UseDefaultCredentials = false,
            Credentials = new NetworkCredential("", "") // No authentication required for Papercut
        };
    }

    public MailService WithSubject(string subject)
    {
        message.Subject = subject;
        return this;
    }

    public MailService WithBody(string body)
    {
        message.Body = body;
        return this;
    }

    public MailService WithEmail(string email)
    {
        message.Email = email;
        return this;
    }

    public void Send()
    {
        MailMessage mailMessage = new MailMessage
        {
            From = new MailAddress("sender@example.com"),
            Subject = message.Subject,
            Body = message.Body,
        };
        mailMessage.To.Add(message.Email);

        smtpClient.Send(mailMessage);

        // Clean up resources
        mailMessage.Dispose();
        smtpClient.Dispose();
    }
}
