using Xunit;

namespace Tests.Configuration
{
    public class ApiTest
    {
        [Fact]
        public void ApiReturnsNewConfigurationIfNotCalledYet()
        {
            var result = Mailocomotive.Configuration.Api.Configuration();
            Assert.NotNull(result);            
        }

        [Fact]
        public void ApiReturnsExistingConfigurationIfCalledInThePast()
        {
            var provider = new Mailocomotive.Setting.Single.SmtpMailProvider()
            {
                Host = "test.mail.com",
            };
            var first = Mailocomotive.Configuration.Api.Configuration();
            first.UseProvider(provider);
            var second = Mailocomotive.Configuration.Api.Configuration();

            Assert.IsType(provider.GetType(), second.GetProvider());
            Assert.Equal(provider.Host,
                ((Mailocomotive.Setting.Single.SmtpMailProvider)second.GetProvider()).Host);
        }
    }
}
