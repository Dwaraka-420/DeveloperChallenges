using System.Net;
using System.Net.Mail;

public class Mail
{

    private static readonly string _smtpServer = "smtp.outlook.com";
    private static readonly int _smtpPort = 587;

    public static void NotifyHrTeam(string senderEmail, string senderPassword)
    {
        string subject = "New challenge notification";
        string body = "Hi, New challenge has been logged in the Developer challenge portal feel free to login and watch the challenge \n\n";

        SendEmail(senderEmail, senderPassword, "bissati.venkata@pditechnologies.com", subject, body);
    }

    public static void SendEmail(string senderEmail, string senderPassword, string receiverEmail, string subject, string body)
    {
        try
        {
            // Create SMTP client
            var smtpClient = new SmtpClient(_smtpServer)
            {
                Port = _smtpPort,
                Credentials = new NetworkCredential(senderEmail, senderPassword),
                EnableSsl = true,
            };

            // Create the email message
            var mailMessage = new MailMessage
            {
                From = new MailAddress(senderEmail),
                Subject = subject,
                Body = body,
                IsBodyHtml = false,
            };
            mailMessage.To.Add(receiverEmail);

            // Send the email
            smtpClient.Send(mailMessage);


        }
        catch (Exception ex)
        {

        }
    }
}
