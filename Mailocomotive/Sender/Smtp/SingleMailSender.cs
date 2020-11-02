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
        private BodyBuilder builder;

        public SingleMailSender(SmtpMailProvider mailProvider)
        {            
            builder = new BodyBuilder();
            this.mailProvider = mailProvider;
        }

        public async Task<bool> SendAsync(Email<T> mail)
        {            
            builder.HtmlBody = await BuildBodyAsync(mail);            
            mail.Message.Body = builder.ToMessageBody();
            using (var client = new SmtpClient())
            {
                //client.ServerCertificateValidationCallback = (s, c, h, e) => true;//security vulnerability!
                await client.ConnectAsync(mailProvider.Host, mailProvider.Port, MailKit.Security.SecureSocketOptions.StartTls);
                await client.AuthenticateAsync(mailProvider.Username, mailProvider.Password);
                await client.SendAsync(mail.Message);
                await client.DisconnectAsync(true);
                client.Dispose();//release attachments
            }
            return true;
        }

        private Task<string> BuildBodyAsync(Email<T> mail)
        {
            foreach(var attachnment in mail.AttachmentPaths)
            {
                builder.Attachments.Add(attachnment);
            }
            return mail.RenderAsync(Render.RenderType.STRING);
        }
    }
    
}
