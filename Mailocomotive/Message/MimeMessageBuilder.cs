using MimeKit;
using System.Collections.Generic;

namespace Mailocomotive.Message
{
    internal class MimeMessageBuilder
    {
        private readonly string subject;
        private readonly string body;
        private readonly IList<Address> toHeaders;
        private readonly IList<Address> replyToHeaders;
        private readonly IList<Address> fromHeaders;
        private readonly IList<Address> ccHeaders;
        private readonly IList<Address> bccHeaders;
        private readonly IList<string> attachmentPaths;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="body">The email body</param>
        /// <param name="subject">The email subject</param>
        /// <param name="ToHeaders"></param>
        /// <param name="ReplyToHeaders"></param>
        /// <param name="FromHeaders"></param>
        /// <param name="CcHeaders"></param>
        /// <param name="BccHeaders"></param>
        /// <param name="attachmentPaths">The absolute paths to attachments</param>
        public MimeMessageBuilder(string body, string subject, IList<Address> ToHeaders, IList<Address> ReplyToHeaders,
            IList<Address> FromHeaders, IList<Address> CcHeaders, IList<Address> BccHeaders, IList<string> attachmentPaths)
        {
            this.body = body;
            this.subject = subject;
            toHeaders = ToHeaders;
            replyToHeaders = ReplyToHeaders;
            fromHeaders = FromHeaders;
            ccHeaders = CcHeaders;
            bccHeaders = BccHeaders;
            this.attachmentPaths = attachmentPaths;
        }

        /// <summary>
        /// Builds the MimeMessage
        /// </summary>
        /// <returns></returns>
        internal MimeMessage Build()
        {
            var message = new MimeMessage();
            message.Subject = subject;
            BuildHeaders(ref message);
            BuildBody(ref message);
            return message;
        }

        private void BuildBody(ref MimeMessage message)
        {
            var bodyBuilder = new BodyBuilder();
            AddAttachments(bodyBuilder);
            bodyBuilder.HtmlBody = body;
            message.Body = bodyBuilder.ToMessageBody();
        }

        private void AddAttachments(BodyBuilder bodyBuilder)
        {
            if (attachmentPaths != null && attachmentPaths.Count > 0)
            {
                foreach (var attachment in attachmentPaths)
                {
                    bodyBuilder.Attachments.Add(attachment);
                }
            }
        }

        private void BuildHeaders(ref MimeMessage message)
        {
            if (toHeaders != null)
                foreach (var header in toHeaders)
                {
                    message.To.Add(new MailboxAddress(header.DisplayName, header.MailAddress));
                }
            if (replyToHeaders != null)
                foreach (var header in replyToHeaders)
                {
                    message.ReplyTo.Add(new MailboxAddress(header.DisplayName, header.MailAddress));
                }
            if (fromHeaders != null)
                foreach (var header in fromHeaders)
                {
                    message.From.Add(new MailboxAddress(header.DisplayName, header.MailAddress));
                }
            if (ccHeaders != null)
                foreach (var header in ccHeaders)
                {
                    message.Cc.Add(new MailboxAddress(header.DisplayName, header.MailAddress));
                }
            if (bccHeaders != null)
                foreach (var header in bccHeaders)
                {
                    message.Bcc.Add(new MailboxAddress(header.DisplayName, header.MailAddress));
                }
        }
    }
}
