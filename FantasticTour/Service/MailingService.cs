using System.Collections.Generic;
using System.Threading.Tasks;
using Hangfire;
using MailKit.Net.Smtp;
using MimeKit;

namespace FantasticTour.Service
{
    public class MailingService : IMailingService
    {
        private readonly IUserService _userService;

        public MailingService(IUserService userService)
        {
            _userService = userService;
        }

        public bool FireSendMailing(string titulo, string cuerpo)
        {
            List<string> mails = _userService.GetMailsForMailing();
            foreach (string mail in mails)
            {
                BackgroundJob.Enqueue(() => SendMailing(titulo, cuerpo, mail));
            }
            return true;
        }

        public async Task SendMailing(string titulo, string cuerpo, string direccion)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Fantur", "fan@tour.com"));
            message.To.Add(new MailboxAddress("", direccion));
            message.Subject = titulo;

            message.Body = new TextPart("plain")
            {
                Text = cuerpo
            };

            using (var client = new SmtpClient())
            {
                client.ServerCertificateValidationCallback = (s, c, h, e) => true;
                client.Connect("smtp.gmail.com", 587, false);
                client.Authenticate("fanturgrupo2@gmail.com", "fanturdacs");
                client.Send(message);
                client.Disconnect(true);
            }
        }
    }
}