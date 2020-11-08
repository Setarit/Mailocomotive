using Mailocomotive.Render;

namespace Mailocomotive.Factory.Render
{
    public interface Contract<TViewModel>
    {
        public Renderer<TViewModel> Create(RenderType type);
    }
}
