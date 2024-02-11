using Microsoft.Extensions.Configuration;
using MimeKit;
using MailKit.Net.Smtp;

namespace SolarTestTask_lvl5.AppData.Contexts.Mail
{
    public class MailService : IMailService
    {
        public IConfiguration _configuration;

        public MailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<string> SendMessage(string message, string email, CancellationToken cancellation)
        {
            //var currentUser = await _userRepository.FindById((await _userService.GetCurrentUser(cancellation)).Id,cancellation);
            var subject = "Congratulation mail";
            var mes = message;
            try
            {
                using var emailMessage = new MimeMessage();

                emailMessage.From.Add(new MailboxAddress("Solar", _configuration["Mail:Address"]));
                emailMessage.To.Add(new MailboxAddress("", email));
                emailMessage.Subject = subject;
                emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
                {
                    Text = message
                };

                using (var client = new SmtpClient())
                {
                    await client.ConnectAsync("smtp.mail.ru", 587, false);
                    await client.AuthenticateAsync(_configuration["Mail:Address"], _configuration["Mail:Pass"]);
                    await client.SendAsync(emailMessage);

                    await client.DisconnectAsync(true);
                }
                return "Сообщение успешно отправлено";
            }
            catch
            {
                return "Ошибка";
            }
        }
    }
}
