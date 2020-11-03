using Mailocomotive.Setting.Multiple;
using Mailocomotive.Setting.Single;
using System.Threading.Tasks;

namespace Mailocomotive.Sender.Smtp
{
    class RandomMailCollectionSender<T> : Multiple.RandomMailCollectionSender<T>
    {
        public RandomMailCollectionSender(MailProviderCollection mailProviderCollection) : base(mailProviderCollection)
        {

        }

        protected override async Task<bool> SendWithProvider(MailProvider mailProvider, Email<T> mail)
        {
            SingleMailSender<T> sender = (new SingleMailSender<T>((SmtpMailProvider)mailProvider));
            return await sender.SendAsync(mail);
        }
    }
}
