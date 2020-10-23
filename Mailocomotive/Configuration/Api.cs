namespace Mailocomotive.Configuration
{
    public static class Api
    {
        private static GlobalConfiguration globalConfiguration;

        public static GlobalConfiguration Configuration()
        {
            return (globalConfiguration != null) ? globalConfiguration : new GlobalConfiguration();
        }
    }
}
