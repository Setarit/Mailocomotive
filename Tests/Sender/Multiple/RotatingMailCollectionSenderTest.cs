using Mailocomotive;
using Mailocomotive.Sender;
using Mailocomotive.Sender.Multiple;
using Mailocomotive.Setting.Multiple;
using Mailocomotive.Setting.Single;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Tests.Sender.Multiple
{
    public class RotatingMailCollectionSenderTest:Test
    {
        private Email<int> mail;
        private MailProviderCollection collection;

        public RotatingMailCollectionSenderTest()
        {
            mail = new MailStub();
            collection = (new MailProviderCollectionStub())
                .UseCollection(new MailProvider[] {
                new SingleMailProviderStub(),
                new SingleMailProviderStub(),
                new SingleMailProviderStub(),
            });
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

        class RotatingMailCollectionSenderStub : RotatingMailCollectionSender<int>
        {
            public int CallCount { get; private set; }
            private IList<bool> successOrder;
            private IList<bool> throwOrder;

            public RotatingMailCollectionSenderStub(MailProviderCollection providerCollection) : base(providerCollection)
            {
                CallCount = 0;
                successOrder = Enumerable.Repeat(false, providerCollection.Collection.Length).ToList();
                throwOrder = Enumerable.Repeat(false, providerCollection.Collection.Length).ToList();
            }
            public RotatingMailCollectionSenderStub SetSuccessOrder(IList<bool> order)
            {
                successOrder = order;
                return this;
            }

            public RotatingMailCollectionSenderStub SetThrowOrder(IList<bool> order)
            {
                throwOrder = order;
                return this;
            }

            protected override Task<bool> SendWithProvider(MailProvider mailProvider, Email<int> mail)
            {
                var currentCallCount = CallCount;
                CallCount++;
                var result = (successOrder[currentCallCount]);
                if (throwOrder[currentCallCount])
                {
                    throw new Exception("Failure " + currentCallCount);
                }
                return Task.FromResult(result);
            }
        }

        #endregion

        [Fact]
        public async Task SendStopsAtFirstProviderIfFirstSucceedsAsync()
        {
            Sender<int> sender = new RotatingMailCollectionSenderStub(collection)
                .SetSuccessOrder(new List<bool> { true, false, false });
            var result = await sender.SendAsync(mail);
            Assert.True(result);
            Assert.Equal(1, ((RotatingMailCollectionSenderStub)sender).CallCount);
        }

        [Fact]
        public async Task SendTriesAllProvidersIfAllFailing()
        {
            Sender<int> sender = new RotatingMailCollectionSenderStub(collection)
                .SetSuccessOrder(new List<bool> { false, false, false });
            var result = await sender.SendAsync(mail);
            Assert.False(result);
            Assert.Equal(3, ((RotatingMailCollectionSenderStub)sender).CallCount);
        }

        [Fact]
        public async Task SendReturnsTrueEvenIfOneThrowing()
        {
            Sender<int> sender = new RotatingMailCollectionSenderStub(collection)
                .SetSuccessOrder(new List<bool> { false, true, false })
                .SetThrowOrder(new List<bool> { true, false, false });

            var result = await sender.SendAsync(mail);
            Assert.True(result);
            Assert.Equal(2, ((RotatingMailCollectionSenderStub)sender).CallCount);
        }

        [Fact]
        public async void SendThrowsErrorLastErrorIfAllFailingAndThrowing()
        {
            Sender<int> sender = new RotatingMailCollectionSenderStub(collection)
                .SetSuccessOrder(new List<bool> { false, false, false })
                .SetThrowOrder(new List<bool> { true, true, true });
            var message = await Assert.ThrowsAsync<Exception>(() => sender.SendAsync(mail));
            Assert.Equal(3, ((RotatingMailCollectionSenderStub)sender).CallCount);
            Assert.Equal("Failure 2", message.Message);
        }
    }
}
