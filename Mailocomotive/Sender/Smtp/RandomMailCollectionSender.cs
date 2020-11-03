using Mailocomotive.Setting.Multiple;
using Mailocomotive.Setting.Single;
using System.Threading.Tasks;

namespace Mailocomotive.Sender.Smtp
{
    class RandomMailCollectionSender<TViewModel> : Multiple.RandomMailCollectionSender<TViewModel>
    {
        public RandomMailCollectionSender(MailProviderCollection mailProviderCollection) : base(mailProviderCollection)
        {

        }

        protected override async Task<bool> SendWithProvider(MailProvider mailProvider, Email<TViewModel> mail)
        {
            SingleMailSender<TViewModel> sender = (new SingleMailSender<TViewModel>((SmtpMailProvider)mailProvider));
            return await sender.SendAsync(mail);
        }
    }
}
