using Mailocomotive.Render;
using System;

namespace Mailocomotive.Factory.Render
{
    internal class Factory<T>
    {
        internal Renderer<T> Create(RenderType type)
        {
            Renderer<T> result = null;
            switch (type)
            {
                case RenderType.STRING:
                    result = new StringRenderer<T>();
                    break;
                default:
                    throw new Exception("Unsupported render type");                    
            }
            return result;
        }
    }
}
