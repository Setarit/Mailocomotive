using MimeKit;
using MailKit.Net.Smtp;
using System.Threading.Tasks;
using Mailocomotive.Setting.Single;
using System;
using Mailocomotive.Message;

namespace Mailocomotive.Sender.Smtp
{
    class SingleMailSender<TViewModel> : Sender<TViewModel>
    {
        private readonly SmtpMailProvider mailProvider;

        public SingleMailSender(SmtpMailProvider mailProvider)
        {
            this.mailProvider = mailProvider;
        }

        public async Task<bool> SendAsync(Email<TViewModel> mail)
        {
            var message = await LoadMimeMessageAsync(mail);
            using (var client = new SmtpClient())
            {
                await client.ConnectAsync(mailProvider.Host, mailProvider.Port, MailKit.Security.SecureSocketOptions.StartTls);
                await client.AuthenticateAsync(mailProvider.Username, mailProvider.Password);
                await client.SendAsync(message);
                await client.DisconnectAsync(true);
                client.Dispose();//release attachments
            }
            return true;
        }

        private async Task<MimeMessage> LoadMimeMessageAsync(Email<TViewModel> mail)
        {
            var builder = new MimeMessageBuilder(
                body: (await mail.RenderAsync(Render.RenderType.STRING)).ToString(),
                subject: mail.MessageSubject,
                ToHeaders: mail.ToHeaders,
                ReplyToHeaders: mail.ReplyToHeaders,
                FromHeaders: mail.FromHeaders,
                CcHeaders: mail.CcHeaders,
                BccHeaders: mail.BccHeaders,
                attachmentPaths: mail.AttachmentPaths
                );
            return builder.Build();
        }
    }
    
}
