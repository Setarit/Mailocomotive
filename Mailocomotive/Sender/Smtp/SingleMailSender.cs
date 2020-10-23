using MimeKit;
using MailKit.Net.Smtp;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Mailocomotive.Configuration;
using Mailocomotive.Setting.Single;

namespace Mailocomotive.Sender.Smtp
{
    class SingleMailSender : Sender
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

        public async Task<bool> Send(Email mail)
        {            
            builder.HtmlBody = await BuildBodyAsync();
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
    }
    
}
