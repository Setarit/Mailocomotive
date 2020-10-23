using Mailocomotive.Setting;

namespace Mailocomotive.Configuration
{
    /// <summary>
    /// The global configuration for Mailocomotive
    /// DON'T ACCESS THIS CLASS DIRECTLY. ALWAYS USE <see cref="Api"/>!
    /// </summary>
    public class GlobalConfiguration
    {
        private Provider mailProviderSetting;

        /// <summary>
        /// Set the mail provider setting
        /// </summary>
        /// <param name="provider"></param>
        /// <returns></returns>
        public GlobalConfiguration UseProvider(Provider provider)
        {
            this.mailProviderSetting = provider;
            return this;
        }

        /// <summary>
        /// Gets the mail provider setting
        /// </summary>
        /// <returns></returns>
        internal Provider GetProvider() { return mailProviderSetting; }
    }
}
