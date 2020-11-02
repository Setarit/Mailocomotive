using System.Threading.Tasks;

namespace Mailocomotive.Render
{
    internal interface Renderer<T>
    {
        /// <summary>
        /// Renders the email
        /// </summary>
        /// <returns></returns>
        Task<object> RenderAsync(T viewModel, string view);
    }
}
