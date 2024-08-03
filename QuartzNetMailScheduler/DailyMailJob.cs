using Quartz;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MimeKit;

public class DailyMailJob : IJob
{
    public Task Execute(IJobExecutionContext context)
    {
        // Logic to send an email
        var message = new MimeMessage();
        message.From.Add(new MailboxAddress("Viral Tada", "Viraltada2001@gmail.com"));
        message.To.Add(new MailboxAddress("Recipient Name", "recipient-email@example.com"));
        message.Subject = "Daily Email";

        message.Body = new TextPart("plain")
        {
            Text = "This is an automatic daily email sent at 7 PM."
        };

        using (var client = new SmtpClient())
        {
            client.Connect("sandbox.smtp.mailtrap.io", 587, false);
            client.Authenticate("16191cfec669ca", "0c52f4cbf17d27");
            client.Send(message);
            client.Disconnect(true);
        }

        return Task.CompletedTask;
    }
}
