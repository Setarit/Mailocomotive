using Mailocomotive.Setting.Single;

namespace Mailocomotive.Setting.Multiple
{
    public abstract class MailProviderCollection:Provider
    {
        internal Strategy Strategy { get; private set; }
        internal MailProvider[] Collection { get; private set; }

        /// <summary>
        /// Sets the strategy to use
        /// </summary>
        /// <param name="strategy"></param>
        /// <returns></returns>
        public MailProviderCollection UseStrategy(Strategy strategy)
        {
            this.Strategy = Strategy;
            return this;
        }

        public MailProviderCollection UseCollection(MailProvider[] collection)
        {
            Collection = collection;
            return this;
        }

        internal abstract MailProvider[] GetCollection();
    }
}
