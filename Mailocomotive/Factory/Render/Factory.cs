using Mailocomotive.Render;
using System;

namespace Mailocomotive.Factory.Render
{
    internal class Factory<TViewModel>
    {
        internal virtual Renderer<TViewModel> Create(RenderType type)
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
