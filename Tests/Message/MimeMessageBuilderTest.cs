using Mailocomotive.Message;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Xunit;

namespace Tests.Message
{
    public class MimeMessageBuilderTest:Test
    {
        [Fact]
        public void BuildSetsMessageBody()
        {
            var body = "FAKE BODY";
            var builder = new MimeMessageBuilder(body, "subject", null, null, null, null, null, null);
            var result = builder.Build();
            Assert.Equal(body, result.HtmlBody);
        }

        [Fact]
        public void BuildSetsSubjectOfMail()
        {
            var subject = "subject";
            var builder = new MimeMessageBuilder("", subject, null, null, null, null, null, null);
            var result = builder.Build();
            Assert.Equal(subject, result.Subject);
        }

        [Fact]
        public void BuildAddsAttachments()
        {
            string fileName1 = System.IO.Path.GetTempPath() + Guid.NewGuid().ToString() + ".txt";
            string fileName2 = System.IO.Path.GetTempPath() + Guid.NewGuid().ToString() + ".txt";
            var attachments = new List<string>(2) { fileName1, fileName2 };
            foreach (var filename in attachments)
            {
                using (var file = new StreamWriter(filename))
                {
                    file.WriteLine("content");
                }
            }
            var builder = new MimeMessageBuilder("", "", null, null, null, null, null, attachments);
            var result = builder.Build();

            Assert.True(result.Attachments.Count() == 2);

            File.Delete(fileName1);
            File.Delete(fileName2);
        }

        [Fact]
        public void BuildAddsToHeader()
        {
            var address = new Address { DisplayName = "Name", MailAddress = "address@email.com" };
            var to = new List<Address> { address };
            var builder = new MimeMessageBuilder("", "", to, null, null, null, null, null);
            var result = builder.Build();

            var headers = result.To.Mailboxes;
            Assert.Equal(to.Count, headers.Count());
            var resultAddress = headers.First();
            Assert.Equal(address.DisplayName, resultAddress.Name);
            Assert.Equal(address.MailAddress, resultAddress.Address);
        }

        [Fact]
        public void BuildAddsReplyToHeader()
        {
            var address = new Address { DisplayName = "Name", MailAddress = "address@email.com" };
            var replyTo = new List<Address> { address };
            var builder = new MimeMessageBuilder("", "", null, replyTo, null, null, null, null);
            var result = builder.Build();

            var headers = result.ReplyTo.Mailboxes;
            Assert.Equal(replyTo.Count, headers.Count());
            var resultAddress = headers.First();
            Assert.Equal(address.DisplayName, resultAddress.Name);
            Assert.Equal(address.MailAddress, resultAddress.Address);
        }

        [Fact]
        public void BuildAddsFromHeader()
        {
            var address = new Address { DisplayName = "Name", MailAddress = "address@email.com" };
            var from = new List<Address> { address };
            var builder = new MimeMessageBuilder("", "", null, null, from, null, null, null);
            var result = builder.Build();

            var headers = result.From.Mailboxes;
            Assert.Equal(from.Count, headers.Count());
            var resultAddress = headers.First();
            Assert.Equal(address.DisplayName, resultAddress.Name);
            Assert.Equal(address.MailAddress, resultAddress.Address);
        }

        [Fact]
        public void BuildAddsCcHeader()
        {
            var address = new Address { DisplayName = "Name", MailAddress = "address@email.com" };
            var cc = new List<Address> { address };
            var builder = new MimeMessageBuilder("", "", null, null, null, cc, null, null);
            var result = builder.Build();

            var headers = result.Cc.Mailboxes;
            Assert.Equal(cc.Count, headers.Count());
            var resultAddress = headers.First();
            Assert.Equal(address.DisplayName, resultAddress.Name);
            Assert.Equal(address.MailAddress, resultAddress.Address);
        }

        [Fact]
        public void BuildAddsBccHeader()
        {
            var address = new Address { DisplayName = "Name", MailAddress = "address@email.com" };
            var bcc = new List<Address> { address };
            var builder = new MimeMessageBuilder("", "", null, null, null, null, bcc, null);
            var result = builder.Build();

            var headers = result.Bcc.Mailboxes;
            Assert.Equal(bcc.Count, headers.Count());
            var resultAddress = headers.First();
            Assert.Equal(address.DisplayName, resultAddress.Name);
            Assert.Equal(address.MailAddress, resultAddress.Address);
        }
    }
}
