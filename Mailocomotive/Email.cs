using Mailocomotive.Render;
using MimeKit;
using System.Collections.Generic;

namespace Mailocomotive
{
    public abstract class Email<T>
    {
        internal MimeMessage Message { get; private set; }
        internal IList<string> AttachmentPaths { get; private set; }

        public Email()
        {
            Message = new MimeMessage();
            AttachmentPaths = new List<string>(5);
        }

        /// <summary>
        /// Where the email should be sent to
        /// </summary>
        /// <param name="displayName">The display name of the recipient</param>
        /// <param name="address">The emailaddress of the recipient</param>
        /// <param name="flush">False if the previous recipients in the <code>TO</code> header should be kept</param>
        /// <returns></returns>
        public Email<T> To(string displayName, string address, bool flush = true)
        {
            if (flush) Message.To.Clear();
            Message.To.Add(new MailboxAddress(displayName, address));
            return this;
        }

        /// <summary>
        /// Sets the <code>REPLYTO</code> header
        /// </summary>
        /// <param name="displayName">The display name of the reply to address</param>
        /// <param name="address">The emailaddress for replying</param>
        /// <param name="flush">False if the previous addresses in the <code>REPLYTO</code> header should be kept</param>
        /// <returns></returns>
        public Email<T> ReplyTo(string displayName, string address, bool flush = true)
        {
            if (flush) Message.ReplyTo.Clear();
            Message.ReplyTo.Add(new MailboxAddress(displayName, address));
            return this;
        }

        /// <summary>
        /// The origin address(es)
        /// </summary>
        /// <param name="displayName">The display name of the origins emailaddress</param>
        /// <param name="address">The emailaddress of the origin</param>
        /// <param name="flush">False if the previous origins in the <code>FROM</code> header should be kept</param>
        /// <returns></returns>
        public Email<T> From(string displayName, string address, bool flush = true) {
            if (flush) Message.From.Clear();
            Message.From.Add(new MailboxAddress(displayName, address));
            return this;
        }

        /// <summary>
        /// Sets the CC field
        /// </summary>
        /// <param name="displayName">The display name om the recipient</param>
        /// <param name="address">The emailaddress of the recipient</param>
        /// <param name="flush">False if the previous recipients in the <code>CC</code> header should be kept</param>
        /// <returns></returns>
        public Email<T> Cc(string displayName, string address, bool flush = true)
        {
            if (flush) Message.Cc.Clear();
            Message.Cc.Add(new MailboxAddress(displayName, address));
            return this;
        }

        /// <summary>
        /// Sets the BCC field
        /// </summary>
        /// <param name="displayName">The display name om the recipient</param>
        /// <param name="address">The emailaddress of the recipient</param>
        /// <param name="flush">False if the previous recipients in the <code>BCC</code> header should be kept</param>
        /// <returns></returns>
        public Email<T> Bcc(string displayName, string address, bool flush = true)
        {
            if (flush) Message.Bcc.Clear();
            Message.Bcc.Add(new MailboxAddress(displayName, address));
            return this;
        }

        /// <summary>
        /// Sets the subject of the email
        /// </summary>
        /// <param name="subject">The email subject</param>
        /// <returns></returns>
        public Email<T> Subject(string subject)
        {
            Message.Subject = subject;
            return this;
        }

        /// <summary>
        /// Adds an attachment to the email
        /// </summary>
        /// <param name="absolutePathToAttachment">The full absolute path to the attachment file</param>
        /// <returns></returns>
        public Email<T> Attach(string absolutePathToAttachment)
        {
            AttachmentPaths.Add(absolutePathToAttachment);
            return this;
        }

        /// <summary>
        /// Builds the viewmodel for the email
        /// Override this method
        /// </summary>
        /// <returns></returns>
        public abstract T BuildViewModel();

        /// <summary>
        /// The path to the razor view of the email
        /// </summary>
        public string ViewPath { internal get; set; }

        /// <summary>
        /// Renders the email to string
        /// </summary>
        /// <returns>The rendered email</returns>
        public async System.Threading.Tasks.Task<string> RenderAsync(RenderType type)
        {
            var renderer = (new Factory.Render.Factory<T>()).Create(type);
            return (await renderer.RenderAsync(BuildViewModel(), ViewPath)).ToString();
        }

        /// <summary>
        /// Sends the email
        /// </summary>
        /// <returns>True if sent</returns>
        public async System.Threading.Tasks.Task<bool> SendAsync()
        {
            var sender = (new Factory.Sender.Factory()).Create();
            return await sender.Send(this);
        }
    }
}
