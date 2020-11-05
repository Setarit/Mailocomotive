using Mailocomotive.Configuration;
using System;

namespace Tests
{
    public class Test : IDisposable
    {
        public void Dispose()
        {
            Api.Configuration()
                .UseProvider(null);
        }
    }
}
