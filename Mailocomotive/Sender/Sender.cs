using System.Threading.Tasks;

namespace Mailocomotive.Sender
{
    public interface Sender<TViewModel>
    {
        /// <summary>
        /// Sends the <paramref name="mail"/>
        /// </summary>
        /// <param name="mail"></param>
        /// <returns>True if success else false</returns>
        Task<bool> SendAsync(Email<TViewModel> mail);
    }
}
