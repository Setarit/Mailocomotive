using Mailocomotive;
using Mailocomotive.Sender;
using Mailocomotive.Sender.Multiple;
using Mailocomotive.Setting.Multiple;
using Mailocomotive.Setting.Multiple.Smtp;
using Mailocomotive.Setting.Single;
using Moq;
using Moq.Protected;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;
using Xunit;

namespace Tests.Sender.Multiple
{
    public class RandomMailCollectionSenderTest : Test
    {
        private Email<int> mail;
        private MailProviderCollection providerCollection;

        public RandomMailCollectionSenderTest()
        {
            mail = new MailStub();
            providerCollection = new SmtpMailProviderCollection();
        }
        #region stubs
        class MailStub : Email<int>
        {
            public override int BuildViewModel()
            {
                return 1;
            }
        }

        class SingleMailProviderStub : MailProvider { }

        class MailProviderCollectionStub : MailProviderCollection
        {
            internal override MailProvider[] GetCollection()
            {
                return this.Collection.ToArray();
            }
        }

        class RandomMailCollectionSenderStub : RandomMailCollectionSender<int>
        {
            public int CallCount { get; private set; }
            private IList<bool> successOrder;

            public RandomMailCollectionSenderStub(MailProviderCollection providerCollection):base(providerCollection)
            {
                CallCount = 0;
            }
            public RandomMailCollectionSenderStub SetSuccessOrder(IList<bool> order)
            {
                successOrder = order;
                return this;
            }

            protected override Task<bool> SendWithProvider(MailProvider mailProvider, Email<int> mail)
            {
                var result = (successOrder[CallCount]);
                CallCount++;
                return Task.FromResult(result);
            }

       }

        #endregion
        [Fact]
        public async void SendReturnsTrueIfOneSucceeds()
        {
            var collection = (new MailProviderCollectionStub())
                .UseCollection(new MailProvider[] {
                new SingleMailProviderStub(),
                new SingleMailProviderStub(),
            });
            Sender<int> sender = new RandomMailCollectionSenderStub(collection)
                .SetSuccessOrder(new List<bool> { true, false });
            var result = await sender.SendAsync(mail);
            Assert.True(result);
        }

        [Fact]
        public async void SendReturnsFalseIfAllFail()
        {
            var collection = (new MailProviderCollectionStub())
                .UseCollection(new MailProvider[] {
                new SingleMailProviderStub(),
                new SingleMailProviderStub(),
            });
            Sender<int> sender = new RandomMailCollectionSenderStub(collection)
                .SetSuccessOrder(new List<bool> { false, false });
            var result = await sender.SendAsync(mail);
            Assert.False(result);
            Assert.Equal(2, ((RandomMailCollectionSenderStub)sender).CallCount);
        }
    }
}
