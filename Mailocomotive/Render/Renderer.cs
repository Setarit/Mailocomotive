using System.Threading.Tasks;

namespace Mailocomotive.Render
{
    public interface Renderer<TViewModel>
    {
        /// <summary>
        /// Renders the email
        /// </summary>
        /// <returns></returns>
        Task<object> RenderAsync(TViewModel viewModel, string view);
    }
}
