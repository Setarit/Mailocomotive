using Razor.Templating.Core;

namespace Mailocomotive.Render
{
    internal class StringRenderer<TViewModel> : Renderer<TViewModel>
    {
        public async System.Threading.Tasks.Task<object> RenderAsync(TViewModel viewModel, string view)
        {
            return await RazorTemplateEngine.RenderAsync(view, viewModel);
        }
    }
}
