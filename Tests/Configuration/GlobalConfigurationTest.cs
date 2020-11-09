using Mailocomotive.Configuration;
using Mailocomotive.Setting.Single;
using Xunit;

namespace Tests.Configuration
{
    public class GlobalConfigurationTest:Test
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

        [Fact]
        public void UseProjectRootSetsTheNewProjectRoot()
        {
            var config = new GlobalConfiguration();
            var before = config.GetProjectRoot();
            config.UseProjectRoot("/");
            var after = config.GetProjectRoot();

            Assert.NotEqual(before, after);
            Assert.Equal("/", after);
        }

        [Fact]
        public void UseProjectRootTriggersNewEngineBuilding()
        {
            var config = new GlobalConfiguration();
            var before = config.GetRazorEngine();
            config.UseProjectRoot("/");
            var after = config.GetRazorEngine();

            Assert.NotEqual(before, after);
        }

        [Fact]
        public void GetRazorEngineReturnsSameEngineIfNothingChanged()
        {
            var config = new GlobalConfiguration();
            var first = config.GetRazorEngine();
            var second = config.GetRazorEngine();

            Assert.Equal(first, second);
        }
    }
}
