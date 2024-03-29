using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Options;
using SportsResultsNotifier.Utils;

namespace SportsResultsNotifier.Services;

public class MailService(IOptions<MailServerSettings> options)
{
    private readonly MailServerSettings options = options.Value;

    public void SendMail(string body)
    {
        ServicePointManager.ServerCertificateValidationCallback +=
            (sender, certificate, chain, sslPolicyErrors) => true;

        var mail = new MailMessage(options.From, options.To)
        {
            IsBodyHtml = true,
            Body = body,
            Subject = options.Subject
        };

        SmtpClient smtpClient = new(options.Host)
        {
            Credentials = new NetworkCredential(options.UserName, options.Password),
            Port = options.Port,
            EnableSsl = options.SSL
        };

        try
        {
            smtpClient.Send(mail);
            Console.WriteLine("Mail sent successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Failed to send mail: " + ex.Message);
        }
    }
}