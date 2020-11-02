﻿using MimeKit;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Mailocomotive
{
    public abstract class Email<T>
    {
        private MimeMessage message;
        private IList<string> attachmentPaths;

        /// <summary>
        /// Where the email should be sent to
        /// </summary>
        /// <param name="displayName">The display name of the recipient</param>
        /// <param name="address">The emailaddress of the recipient</param>
        /// <param name="flush">False if the previous recipients in the <code>TO</code> header should be kept</param>
        /// <returns></returns>
        public Email<T> To(string displayName, string address, bool flush = true)
        {
            if (flush) message.To.Clear();
            message.To.Add(new MailboxAddress(displayName, address));
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
            if (flush) message.ReplyTo.Clear();
            message.ReplyTo.Add(new MailboxAddress(displayName, address));
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
            if (flush) message.From.Clear();
            message.From.Add(new MailboxAddress(displayName, address));
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
            if (flush) message.Cc.Clear();
            message.Cc.Add(new MailboxAddress(displayName, address));
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
            if (flush) message.Bcc.Clear();
            message.Bcc.Add(new MailboxAddress(displayName, address));
            return this;
        }

        /// <summary>
        /// Sets the subject of the email
        /// </summary>
        /// <param name="subject">The email subject</param>
        /// <returns></returns>
        public Email<T> Subject(string subject)
        {
            message.Subject = subject;
            return this;
        }

        /// <summary>
        /// Adds an attachment to the email
        /// </summary>
        /// <param name="absolutePathToAttachment">The full absolute path to the attachment file</param>
        /// <returns></returns>
        public Email<T> Attach(string absolutePathToAttachment)
        {
            attachmentPaths.Add(absolutePathToAttachment);
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
        public string Render()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Sends the email
        /// </summary>
        /// <returns>True if sent</returns>
        public bool Send()
        {
            throw new NotImplementedException();
        }
    }
}
