using Mailocomotive.Configuration;
using Mailocomotive.Configuration.Multiple;
using Mailocomotive.Setting.Multiple;
using Mailocomotive.Setting.Single;
using System;

namespace Mailocomotive.Factory.Sender
{
    internal class Factory
    {
        public Factory()
        {

        }

        /// <summary>
        /// Creates a sender depending on the current configuration
        /// </summary>
        /// <returns></returns>
        internal Mailocomotive.Sender.Sender Create()
        {
            var provider = Api.Configuration().GetProvider();
            Mailocomotive.Sender.Sender result = null;
            if (provider is MailProvider)
            {
                result = BuildSender(provider);
            }
            else if(provider is MailProviderCollection)
            {
                result = BuildCollectionSender(provider);
            }
            else
            {
                throw new Exception("Unsupported provider");
            }
            return result;
        }

        private Mailocomotive.Sender.Sender BuildSender(Setting.Provider provider)
        {
            if(provider is SmtpMailProvider)
            {
                return new Mailocomotive.Sender.Smtp.SingleMailSender(provider as SmtpMailProvider);
            }
            throw new Exception("Unsupported provider");
        }

        private Mailocomotive.Sender.Sender BuildCollectionSender(Setting.Provider provider)
        {
            throw new NotImplementedException();
            if(provider is SmtpMailProviderCollection)
            {
                
            }
            throw new Exception("Unsupported provider");
        }
    }
}
