﻿using Mailocomotive.Message;
using Mailocomotive.Render;
using MimeKit;
using System.Collections.Generic;

namespace Mailocomotive
{
    public abstract class Email<TViewModel>
    {
        internal IList<Address> ToHeaders { get; private set; }
        internal IList<Address> ReplyToHeaders { get; private set; }
        internal IList<Address> FromHeaders { get; private set; }
        internal IList<Address> CcHeaders { get; private set; }
        internal IList<Address> BccHeaders { get; private set; }
        internal string MessageSubject { get; private set; }
        internal IList<string> AttachmentPaths { get; private set; }

        public Email()
        {
            ToHeaders = new List<Address>();
            ReplyToHeaders = new List<Address>(1);
            FromHeaders = new List<Address>(1);
            BccHeaders = new List<Address>();
            CcHeaders = new List<Address>();
            AttachmentPaths = new List<string>(5);
        }

        /// <summary>
        /// Where the email should be sent to
        /// </summary>
        /// <param name="displayName">The display name of the recipient</param>
        /// <param name="address">The emailaddress of the recipient</param>
        /// <param name="flush">False if the previous recipients in the <code>TO</code> header should be kept</param>
        /// <returns></returns>
        public Email<TViewModel> To(string displayName, string address, bool flush = true)
        {
            if (flush) ToHeaders.Clear();
            ToHeaders.Add(new Address
            {
                DisplayName = displayName,
                MailAddress = address
            });
            return this;
        }

        /// <summary>
        /// Sets the <code>REPLYTO</code> header
        /// </summary>
        /// <param name="displayName">The display name of the reply to address</param>
        /// <param name="address">The emailaddress for replying</param>
        /// <param name="flush">False if the previous addresses in the <code>REPLYTO</code> header should be kept</param>
        /// <returns></returns>
        public Email<TViewModel> ReplyTo(string displayName, string address, bool flush = true)
        {
            if (flush) ReplyToHeaders.Clear();
            ReplyToHeaders.Add(new Address
            {
                DisplayName = displayName,
                MailAddress = address
            });
            return this;
        }

        /// <summary>
        /// The origin address(es)
        /// </summary>
        /// <param name="displayName">The display name of the origins emailaddress</param>
        /// <param name="address">The emailaddress of the origin</param>
        /// <param name="flush">False if the previous origins in the <code>FROM</code> header should be kept</param>
        /// <returns></returns>
        public Email<TViewModel> From(string displayName, string address, bool flush = true) {
            if (flush) FromHeaders.Clear();
            FromHeaders.Add(new Address
            {
                DisplayName = displayName,
                MailAddress = address
            });
            return this;
        }

        /// <summary>
        /// Sets the CC field
        /// </summary>
        /// <param name="displayName">The display name om the recipient</param>
        /// <param name="address">The emailaddress of the recipient</param>
        /// <param name="flush">False if the previous recipients in the <code>CC</code> header should be kept</param>
        /// <returns></returns>
        public Email<TViewModel> Cc(string displayName, string address, bool flush = true)
        {
            if (flush) CcHeaders.Clear();
            CcHeaders.Add(new Address
            {
                DisplayName = displayName,
                MailAddress = address
            });
            return this;
        }

        /// <summary>
        /// Sets the BCC field
        /// </summary>
        /// <param name="displayName">The display name om the recipient</param>
        /// <param name="address">The emailaddress of the recipient</param>
        /// <param name="flush">False if the previous recipients in the <code>BCC</code> header should be kept</param>
        /// <returns></returns>
        public Email<TViewModel> Bcc(string displayName, string address, bool flush = true)
        {
            if (flush) BccHeaders.Clear();
            BccHeaders.Add(new Address
            {
                DisplayName = displayName,
                MailAddress = address
            });
            return this;
        }

        /// <summary>
        /// Sets the subject of the email
        /// </summary>
        /// <param name="subject">The email subject</param>
        /// <returns></returns>
        public Email<TViewModel> Subject(string subject)
        {
            MessageSubject = subject;
            return this;
        }

        /// <summary>
        /// Adds an attachment to the email
        /// </summary>
        /// <param name="absolutePathToAttachment">The full absolute path to the attachment file</param>
        /// <returns></returns>
        public Email<TViewModel> Attach(string absolutePathToAttachment)
        {
            AttachmentPaths.Add(absolutePathToAttachment);
            return this;
        }

        /// <summary>
        /// Builds the viewmodel for the email
        /// Override this method
        /// </summary>
        /// <returns></returns>
        public abstract TViewModel BuildViewModel();

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
            Renderer<TViewModel> renderer = (new Factory.Render.Factory<TViewModel>()).Create(type);
            return (await renderer.RenderAsync(BuildViewModel(), ViewPath)).ToString();
        }

        /// <summary>
        /// Sends the email
        /// </summary>
        /// <returns>True if sent</returns>
        public async System.Threading.Tasks.Task<bool> SendAsync()
        {
            var sender = (new Factory.Sender.Factory<TViewModel>()).Create();
            return await sender.SendAsync(this);
        }
    }
}
