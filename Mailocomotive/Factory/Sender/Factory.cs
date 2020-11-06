using Mailocomotive.Configuration;
using Mailocomotive.Setting.Multiple;
using Mailocomotive.Setting.Single;
using System;

namespace Mailocomotive.Factory.Sender
{
    internal class Factory<TViewModel>
    {
        public Factory()
        {

        }

        /// <summary>
        /// Creates a sender depending on the current configuration
        /// </summary>
        /// <returns></returns>
        internal Mailocomotive.Sender.Sender<TViewModel> Create()
        {
            var provider = Api.Configuration().GetProvider();
            Mailocomotive.Sender.Sender<TViewModel> result = null;
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

        private Mailocomotive.Sender.Sender<TViewModel> BuildSender(Setting.Provider provider)
        {
            if(provider is SmtpMailProvider)
            {
                return new Mailocomotive.Sender.Smtp.SingleMailSender<TViewModel>(provider as SmtpMailProvider);
            }
            throw new Exception("Unsupported provider");
        }

        private Mailocomotive.Sender.Sender<TViewModel> BuildCollectionSender(Setting.Provider provider)
        {
            var multipleProvider = provider as MailProviderCollection;
            Mailocomotive.Sender.Sender<TViewModel> result = null;
            switch (multipleProvider.Strategy)
            {
                case Strategy.ROTATE:
                    result = new Mailocomotive.Sender.Smtp.RotatingMailCollectionSender<TViewModel>(multipleProvider);
                    break;
                case Strategy.RANDOM:
                    result = new Mailocomotive.Sender.Smtp.RandomMailCollectionSender<TViewModel>(multipleProvider);
                    break;
                default:
                    throw new Exception("Unsupported provider");
                    break;
            }
            return result;
        }
    }
}
