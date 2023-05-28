using System.Net.Mail;
using Web_API.Contracts;
using Web_API.Others;

namespace Web_API.Repositories;

public class EmailService : IEmailService
{
    private readonly string smtpServer;
    private readonly int smtpPort;
    private readonly string fromEmailAddress;

    private FluentEmail fluentEmail = new();

    public EmailService(string smtpServer, int smtpPort, string fromEmailAddress)
    {
        this.smtpServer = smtpServer;
        this.smtpPort = smtpPort;
        this.fromEmailAddress = fromEmailAddress;
    }

    public EmailService SetEmail(string email)
    {
        fluentEmail.Email = email;
        return this;
    }

    public EmailService SetSubject(string subject)
    {
        fluentEmail.Subject = subject;
        return this;
    }

    public EmailService SetHtmlMessage(string htmlMessage)
    {
        fluentEmail.HtmlMessage = htmlMessage;
        return this;
    }

    /*
     
<summary>
Send email
</summary>
<param name="email">Email address</param>
<param name="subject">Email subject</param>
<param name="htmlMessage">Email message</param>
<returns>Task</returns>
<remarks>
This method will send email to email address
</remarks>
*/
    public void SendEmailAsync()
    {
        var message = new MailMessage
        {
            From = new MailAddress(fromEmailAddress),
            Subject = fluentEmail.Subject,
            Body = fluentEmail.HtmlMessage,
            To = { fluentEmail.Email },
            IsBodyHtml = true
        };

        using var client = new SmtpClient(smtpServer, smtpPort);
        client.Send(message);

        message.Dispose();
        client.Dispose();
    }


}

