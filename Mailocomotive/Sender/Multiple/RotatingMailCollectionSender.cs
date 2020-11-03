using Mailocomotive.Setting.Multiple;
using Mailocomotive.Setting.Single;
using System;
using System.Threading.Tasks;

namespace Mailocomotive.Sender.Multiple
{
    /// <summary>
    /// Tries to send the mail with the first provider.
    /// If that fail it tries the second, if the second fails then it tries the third and so on
    /// Throws the last exception if all failed with exception
    /// </summary>
    /// <typeparam name="T"></typeparam>
    internal abstract class RotatingMailCollectionSender<T> : Sender<T>
    {
        private readonly MailProviderCollection mailProviderCollection;

        public RotatingMailCollectionSender(MailProviderCollection mailProviderCollection)
        {
            this.mailProviderCollection = mailProviderCollection;
        }

        async Task<bool> Sender<T>.SendAsync(Email<T> mail)
        {
            Exception lastException = null;
            bool sent = false;
            for (int i = 0; i < mailProviderCollection.Collection.Length; i++)
            {
                MailProvider provider = mailProviderCollection.Collection[i];
                try
                {
                    sent = await SendWithProvider(provider, mail);
                    if (sent) break;
                }
                catch (Exception ex)
                {
                    lastException = ex;
                }
            }
            if (!sent && lastException != null) throw lastException;
            return sent;
        }

        protected abstract Task<bool> SendWithProvider(Setting.Single.MailProvider mailProvider, Email<T> mail);
    }
}
