namespace Mailocomotive.Configuration
{
    public sealed class Api
    {
        private static GlobalConfiguration globalConfiguration = new GlobalConfiguration();

        public static GlobalConfiguration Configuration()
        {
            return globalConfiguration;
        }
    }
}
