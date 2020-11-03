using MimeKit;
using MailKit.Net.Smtp;
using System.Threading.Tasks;
using Mailocomotive.Setting.Single;
using System;
using Mailocomotive.Message;

namespace Mailocomotive.Sender.Smtp
{
    internal class SingleMailSender<T> : Sender<T>
    {
        private readonly SmtpMailProvider mailProvider;

        public SingleMailSender(SmtpMailProvider mailProvider)
        {
            this.mailProvider = mailProvider;
        }

        async Task<bool> Sender<T>.SendAsync(Email<T> mail)
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

        private async Task<MimeMessage> LoadMimeMessageAsync(Email<T> mail)
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
