using Mailocomotive.Render;
using System;
using System.IO;
using Xunit;

namespace Tests.Render
{
    public class StringRendererTest : Test
    {
        [Fact]
        public async void RenderAsyncFillsViewModel()
        {
            var renderer = new StringRenderer<int>();
            var content = await renderer.RenderAsync(94, "/Views/Dummy.cshtml");
            Assert.Equal("Text in dummy file. The number is 94", content);
        }
    }
}
