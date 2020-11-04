using Mailocomotive.Configuration;
using Mailocomotive.Setting.Single;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Tests.Configuration
{
    public class GlobalConfigurationTest
    {
        [Fact]
        public void UseProviderSetsTheProvider()
        {
            var config = new GlobalConfiguration();
            Assert.Null(config.GetProvider());
            config.UseProvider(new SmtpMailProvider() { DisplayName = "TEST" });
            Assert.NotNull(config.GetProvider());
        }

        [Fact]
        public void UseProviderReturnsSameObject()
        {
            var config = new GlobalConfiguration();
            var returned = config.UseProvider(new SmtpMailProvider() { DisplayName = "TEST" });
            Assert.Equal(config, returned);
        }
    }
}
