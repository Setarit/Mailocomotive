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

    }
}
