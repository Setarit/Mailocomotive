using Mailocomotive.Configuration;
using Mailocomotive.Configuration.Multiple.Smtp;
using Mailocomotive.Factory.Sender;
using Mailocomotive.Sender.Smtp;
using Mailocomotive.Setting.Single;
using System;
using Xunit;

namespace Tests.Factory.Sender
{
    public class Factory:IDisposable
    {
        private Factory<int> factory;

        public Factory()
        {
            factory = new Mailocomotive.Factory.Sender.Factory<int>();
        }

        [Fact]
        public void CreateReturnsSingleSmtpSenderIfSingleSmtpProvider()
        {
            var provider = new SmtpMailProvider();
            Api.Configuration().UseProvider(provider);
            var result = factory.Create();
            Assert.NotNull(result);
            Assert.IsType((new SingleMailSender<int>(provider)).GetType(), result);
        }

        [Fact]
        public void CreateReturnsRotatingCollectionSmtpSenderIfCollectionProviderWithRotatingStrategy()
        {
            var provider = new SmtpMailProviderCollection();
            provider.UseStrategy(Mailocomotive.Setting.Multiple.Strategy.ROTATE);
            Api.Configuration().UseProvider(provider);
            var result = factory.Create();

            Assert.NotNull(result);
            Assert.IsType((new RotatingMailCollectionSender<int>(provider)).GetType(), result);
        }

        [Fact]
        public void CreateReturnsRandomCollectionSmtpSenderIfCollectionProviderWithRandomStrategy()
        {
            var provider = new SmtpMailProviderCollection();
            provider.UseStrategy(Mailocomotive.Setting.Multiple.Strategy.RANDOM);
            Api.Configuration().UseProvider(provider);
            var result = factory.Create();

            Assert.NotNull(result);
            Assert.IsType((new RotatingMailCollectionSender<int>(provider)).GetType(), result);
        }

        [Fact]
        public void CreateThrowsExceptionIfNoProviderSet()
        {
            Assert.Throws<Exception>(() => factory.Create());
        }

        public void Dispose()
        {
            Api.Configuration().UseProvider(null);
        }
    }
}
