using Mailocomotive.Setting;
using RazorLight;
using System.IO;

namespace Mailocomotive.Configuration
{
    /// <summary>
    /// The global configuration for Mailocomotive
    /// DON'T ACCESS THIS CLASS DIRECTLY. ALWAYS USE <see cref="Api"/>!
    /// </summary>
    public class GlobalConfiguration
    {
        private Provider mailProviderSetting;
        private string projectRoot;
        private RazorLightEngine razorEngine;

        public GlobalConfiguration()
        {
            projectRoot = Directory.GetCurrentDirectory();
            RebuildRazorEngine();
        }

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

        /// <summary>
        /// Overrides the root folder of the project
        /// </summary>
        /// <param name="absolutePath"></param>
        /// <returns></returns>
        public GlobalConfiguration UseProjectRoot(string absolutePath)
        {
            projectRoot = absolutePath;
            RebuildRazorEngine();
            return this;
        }

        /// <summary>
        /// Gets the project root
        /// </summary>
        /// <returns></returns>
        internal string GetProjectRoot()
        {
            return projectRoot;
        }

        /// <summary>
        /// Gets the Razor engine to compile the views
        /// <returns>The razor engine</returns>
        /// </summary>
        internal RazorLightEngine GetRazorEngine()
        {
            return razorEngine;
        }

        private void RebuildRazorEngine()
        {
            razorEngine = new RazorLightEngineBuilder().UseFileSystemProject(GetProjectRoot()).UseMemoryCachingProvider().Build();
        }
    }
}
