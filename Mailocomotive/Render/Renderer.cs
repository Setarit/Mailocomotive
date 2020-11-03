using System.Threading.Tasks;

namespace Mailocomotive.Render
{
    internal interface Renderer<TViewModel>
    {
        /// <summary>
        /// Renders the email
        /// </summary>
        /// <returns></returns>
        Task<object> RenderAsync(TViewModel viewModel, string view);
    }
}
