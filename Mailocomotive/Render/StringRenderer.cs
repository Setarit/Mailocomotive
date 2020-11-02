using Razor.Templating.Core;

namespace Mailocomotive.Render
{
    internal class StringRenderer<T> : Renderer<T>
    {
        public async System.Threading.Tasks.Task<object> RenderAsync(T viewModel, string view)
        {
            return await RazorTemplateEngine.RenderAsync(view, viewModel);
        }
    }
}
