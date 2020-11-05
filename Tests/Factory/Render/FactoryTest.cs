using Mailocomotive.Render;
using Xunit;

namespace Tests.Factory.Render
{
    public class FactoryTest:Test
    {
        private Mailocomotive.Factory.Render.Factory<int> factory;

        public FactoryTest()
        {
            factory = new Mailocomotive.Factory.Render.Factory<int>();
        }

        public void Dispose()
        {
            base.Dispose();
            factory = null;
        }

        [Fact]
        public void CreateReturnsStringRendererIfRenderTypeIsString()
        {
            var result = factory.Create(Mailocomotive.Render.RenderType.STRING);
            Assert.IsType((new StringRenderer<int>()).GetType(), result);
        }
    }
}
