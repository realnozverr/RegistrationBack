using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;
using Registration.Web.Models;

namespace Registration.Web.Services
{
    public class MessageEmail : IMessageEmail
    {
        private readonly IConfiguration _configuration;
        public MessageEmail(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task SendEmail(RegisterModel model)
        {
            var message = new MimeMessage();
            message.From.Add(MailboxAddress.Parse(_configuration["Smtp:SmtpUsername"]));
            message.To.Add(MailboxAddress.Parse(model.Email));
            message.Subject = "Code";
            message.Body = new TextPart(TextFormat.Html) {
                Text = $@"
        <html>
            <body>
                <h1>Ваш код</h1>
                <p>{model.Code}</p> <!-- Здесь вставляется код из модели -->
            </body>
        </html>"
            };

            using (var client = new SmtpClient())
            {
                try
                {
                    // Подключение к SMTP серверу Gmail
                    await client.ConnectAsync(_configuration["Smtp:SmtpHost"], int.Parse(_configuration["Smtp:SmtpPort"]), SecureSocketOptions.StartTls);

                    // Аутентификация с использованием учетных данных Gmail
                    await client.AuthenticateAsync(_configuration["Smtp:SmtpUsername"], _configuration["Smtp:SmtpPassword"]);

                    // Отправка сообщения
                    await client.SendAsync(message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка при отправке электронной почты: {ex.Message}");
                    // Обработка ошибок
                }
                finally
                {
                    // Отключение от сервера
                    await client.DisconnectAsync(true);
                }
            }
        }
    }
}
