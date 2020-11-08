using Mailocomotive;
using Mailocomotive.Render;
using Mailocomotive.Sender;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Tests
{
    public class EmailTest:IDisposable
    {
        private Email<int> email;

        public EmailTest()
        {
            email = new EmailStub();
        }

        class EmailStub : Email<int>
        {
            public override int BuildViewModel()
            {
                return 1;
            }
        }

        class RendererStub : Renderer<int>
        {
            public bool Called { get; private set; }
            public async Task<object> RenderAsync(int viewModel, string view)
            {
                Called = true;
                return Task.FromResult(viewModel);
            }
        }

        class SenderStub : Sender<int>
        {
            public bool Called { get; private set; }

            public Task<bool> SendAsync(Email<int> mail)
            {
                Called = true;
                return Task.FromResult(true);
            }
        }

        public void Dispose()
        {
            email = null;
        }

        [Fact]
        public void ToAddsAddressToHeaderWhenFlushIsFalse() {
            email.To("name", "email@e.e", false)
                .To("name2", "email2@e.e", false);
            Assert.Equal(2, email.ToHeaders.Count);
        }

        [Fact]
        public void ToOnlySavesLastEmailInToHeaderIfFlushIsActive()
        {
            email.To("name", "email@e.e", false)
                .To("name2", "email2@e.e", true);
            Assert.Single(email.ToHeaders);
            var address = email.ToHeaders.First();
            Assert.Equal("name2", address.DisplayName);
            Assert.Equal("email2@e.e", address.MailAddress);
        }

        [Fact]
        public void ReplyToAddsAddressReplyToHeaderWhenFlushIsFalse()
        {
            email.ReplyTo("name", "email@e.e", false)
                .ReplyTo("name2", "email2@e.e", false);
            Assert.Equal(2, email.ReplyToHeaders.Count);
        }

        [Fact]
        public void ReplyToOnlySavesLastEmailInReplyToHeaderIfFlushIsActive()
        {
            email.ReplyTo("name", "email@e.e", false)
                .ReplyTo("name2", "email2@e.e", true);
            Assert.Single(email.ReplyToHeaders);
            var address = email.ReplyToHeaders.First();
            Assert.Equal("name2", address.DisplayName);
            Assert.Equal("email2@e.e", address.MailAddress);
        }

        [Fact]
        public void FromAddsAddressToHeaderWhenFlushIsFalse()
        {
            email.From("name", "email@e.e", false)
                .From("name2", "email2@e.e", false);
            Assert.Equal(2, email.FromHeaders.Count);
        }

        [Fact]
        public void FromOnlySavesLastEmailInToHeaderIfFlushIsActive()
        {
            email.From("name", "email@e.e", false)
                .From("name2", "email2@e.e", true);
            Assert.Single(email.FromHeaders);
            var address = email.FromHeaders.First();
            Assert.Equal("name2", address.DisplayName);
            Assert.Equal("email2@e.e", address.MailAddress);
        }

        [Fact]
        public void CcAddsAddressToHeaderWhenFlushIsFalse()
        {
            email.Cc("name", "email@e.e", false)
                .Cc("name2", "email2@e.e", false);
            Assert.Equal(2, email.CcHeaders.Count);
        }

        [Fact]
        public void CcOnlySavesLastEmailInToHeaderIfFlushIsActive()
        {
            email.Cc("name", "email@e.e", false)
                .Cc("name2", "email2@e.e", true);
            Assert.Single(email.CcHeaders);
            var address = email.CcHeaders.First();
            Assert.Equal("name2", address.DisplayName);
            Assert.Equal("email2@e.e", address.MailAddress);
        }

        [Fact]
        public void BccAddsAddressToHeaderWhenFlushIsFalse()
        {
            email.Bcc("name", "email@e.e", false)
                .Bcc("name2", "email2@e.e", false);
            Assert.Equal(2, email.BccHeaders.Count);
        }

        [Fact]
        public void BccOnlySavesLastEmailInToHeaderIfFlushIsActive()
        {
            email.Bcc("name", "email@e.e", false)
                .Bcc("name2", "email2@e.e", true);
            Assert.Single(email.BccHeaders);
            var address = email.BccHeaders.First();
            Assert.Equal("name2", address.DisplayName);
            Assert.Equal("email2@e.e", address.MailAddress);
        }

        [Fact]
        public void SubjectSetEmailSubject()
        {
            email.Subject("subject");
            Assert.Equal("subject", email.MessageSubject);
        }

        [Fact]
        public void AttachAddsAttachmentPaths()
        {
            email.Attach("path")
                .Attach("path2")
                .Attach("path3");
            Assert.Equal(3, email.AttachmentPaths.Count);
        }

        [Fact]
        public void BuildViewModelReturnsTheModel()
        {
            var model = email.BuildViewModel();
            Assert.IsType(int.MinValue.GetType(), model);
            Assert.Equal(1, model);
        }

        //[Fact]
        //public async void RenderCallsFactoryAndRenderer()
        //{
        //    var renderer = new RendererStub();
        //    var mockedFactory = new Mock<Mailocomotive.Factory.Render.Factory<int>>();
        //    mockedFactory                
        //        .Setup(f => f.Create(Mailocomotive.Render.RenderType.STRING))
        //        .Returns(renderer);

        //    var renderResult = await email.RenderAsync(RenderType.STRING);
        //    Assert.Equal(1.ToString(), renderResult);
        //    mockedFactory.Verify(f => f.Create(RenderType.STRING));
        //    Assert.True(renderer.Called);
        //}

        //[Fact]
        //public async void SendCallsFactoryAndSender()
        //{
        //    var sender = new SenderStub();
        //    var mockedFactory = new Mock<Mailocomotive.Factory.Sender.Factory<int>>();
        //    mockedFactory
        //        .Setup(f => f.Create())
        //        .Returns(sender);

        //    var renderResult = await email.RenderAsync(RenderType.STRING);
        //    Assert.Equal(1.ToString(), renderResult);
        //    mockedFactory.Verify(f => f.Create());
        //    Assert.True(sender.Called);
        //}
    }
}
