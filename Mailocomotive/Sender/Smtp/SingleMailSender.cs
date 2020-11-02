using MimeKit;
using MailKit.Net.Smtp;
using System.Threading.Tasks;
using Mailocomotive.Setting.Single;
using System;

namespace Mailocomotive.Sender.Smtp
{
    class SingleMailSender<T> : Sender<T>
    {
        private readonly SmtpMailProvider mailProvider;
        private MimeMessage message;
        private BodyBuilder builder;

        public SingleMailSender(SmtpMailProvider mailProvider)
        {
            message = new MimeMessage();
            builder = new BodyBuilder();
            this.mailProvider = mailProvider;
        }

        public async Task<bool> SendAsync(Email<T> mail)
        {            
            builder.HtmlBody = await BuildBodyAsync(mail);
            message.Body = builder.ToMessageBody();
            using (var client = new SmtpClient())
            {
                //client.ServerCertificateValidationCallback = (s, c, h, e) => true;//security vulnerability!
                await client.ConnectAsync(mailProvider.Host, mailProvider.Port, MailKit.Security.SecureSocketOptions.StartTls);
                await client.AuthenticateAsync(mailProvider.Username, mailProvider.Password);
                await client.SendAsync(message);
                await client.DisconnectAsync(true);
                client.Dispose();//release attachments
            }
            return true;
        }

        private Task<string> BuildBodyAsync(Email<T> mail)
        {
            foreach(var attachnment in mail.GetAttachmentPaths())
            {
                builder.Attachments.Add(attachnment);
            }
            //todo render to string
        }
    }
    
}
