using Web_API.Repositories;
using Web_API.Utility;

namespace Web_API.Contracts
{
    public interface IEmailService
    {
        void SendEmailAsync();   

        EmailService SetEmail(string email);
        EmailService SetSubject(string subject);
        EmailService SetHtmlMessage(string htmlMessage);
    }
}
