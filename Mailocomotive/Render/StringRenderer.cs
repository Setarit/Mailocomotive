using Mailocomotive.Configuration;

namespace Mailocomotive.Render
{
    internal class StringRenderer<TViewModel> : Renderer<TViewModel>
    {
        public async System.Threading.Tasks.Task<object> RenderAsync(TViewModel viewModel, string view)
        {
            var engine = Api.Configuration().GetRazorEngine();
            return await engine.CompileRenderAsync<TViewModel>(view, viewModel);
        }
    }
}
