using Mailocomotive.Setting.Multiple.Smtp;
using Mailocomotive.Setting.Single;
using System.Linq;
using Xunit;

namespace Tests.Setting.Multiple.Smtp
{
    public class SmtpMailProviderCollectionTest
    {
        [Fact]
        public void GetCollectionReturnsArrayOfSmtpMailProviders()
        {
            var singleProvider = new SmtpMailProvider();
            var provider = new SmtpMailProviderCollection();
            provider.UseCollection(new MailProvider[] { singleProvider });

            var returned = provider.GetCollection();
            Assert.Single(returned);
            var first = returned.First();
            Assert.NotNull(first);
            Assert.IsType(singleProvider.GetType(), first);
        }
    }
}
