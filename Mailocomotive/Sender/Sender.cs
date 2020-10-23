using System.Threading.Tasks;

namespace Mailocomotive.Sender
{
    internal interface Sender
    {
        /// <summary>
        /// Sends the <paramref name="mail"/>
        /// </summary>
        /// <param name="mail"></param>
        /// <returns>True if success else false</returns>
        Task<bool> Send(Email mail);
    }
}
