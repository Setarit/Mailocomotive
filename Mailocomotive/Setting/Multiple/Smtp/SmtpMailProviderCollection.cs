using Mailocomotive.Setting.Single;
using System.Linq;

namespace Mailocomotive.Setting.Multiple.Smtp
{
    public class SmtpMailProviderCollection : MailProviderCollection
    {
        internal override MailProvider[] GetCollection()
        {
            return Collection.Select(p => (SmtpMailProvider)p).ToArray();
        }
    }
}
