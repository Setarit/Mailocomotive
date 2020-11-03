using Mailocomotive.Setting.Multiple;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mailocomotive.Sender.Multiple
{
    /// <summary>
    /// Uses a random provider from the collection to send
    /// If the provider fails, another provider will be selected at random.
    /// When all the providers in the collection fail, it will throw the exception from the last provider
    /// </summary>
    /// <typeparam name="T"></typeparam>
    internal abstract class RandomMailCollectionSender<T> : Sender<T>
    {
        private readonly MailProviderCollection mailProviderCollection;

        public RandomMailCollectionSender(MailProviderCollection mailProviderCollection)
        {
            this.mailProviderCollection = mailProviderCollection;
        }

        async Task<bool> Sender<T>.SendAsync(Email<T> mail)
        {
            var random = new Random();
            IList<bool> used = new bool[mailProviderCollection.Collection.Length];
            bool sent = false;
            Exception lastException = null;
            do
            {
                var providerIndex = random.Next(0, mailProviderCollection.Collection.Length);
                try
                {
                    sent = await SendWithProvider(mailProviderCollection.Collection[providerIndex], mail);
                }
                catch (Exception e)
                {
                    lastException = e;
                }
                finally
                {
                    used[providerIndex] = true;
                }
            } while (!sent && used.Any(u => !u));
            if(!sent && lastException != null)
            {
                throw lastException;
            }
            return sent;
        }

        protected abstract Task<bool> SendWithProvider(Setting.Single.MailProvider mailProvider, Email<T> mail);
    }
}
