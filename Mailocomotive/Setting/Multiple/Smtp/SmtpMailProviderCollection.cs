using Mailocomotive.Setting.Multiple;
using Mailocomotive.Setting.Single;
using System.Linq;

namespace Mailocomotive.Configuration.Multiple.Smtp
{
    public class SmtpMailProviderCollection : MailProviderCollection
    {
        internal override MailProvider[] GetCollection()
        {
            return Collection.Select(p => (SmtpMailProvider)p).ToArray();
        }
    }
}
