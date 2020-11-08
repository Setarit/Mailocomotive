using Mailocomotive.Render;
using System;

namespace Mailocomotive.Factory.Render
{
    public class Factory<TViewModel> : Contract<TViewModel>
    {
        public Renderer<TViewModel> Create(RenderType type)
        {
            Renderer<TViewModel> result = null;
            switch (type)
            {
                case RenderType.STRING:
                    result = new StringRenderer<TViewModel>();
                    break;
                default:
                    throw new Exception("Unsupported render type");
            }
            return result;
        }
    }
}
