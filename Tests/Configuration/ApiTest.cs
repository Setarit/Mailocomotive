using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Tests.Configuration
{
    public class ApiTest
    {
        [Fact]
        public void ApiReturnsNewConfigurationIfNotCalledYet()
        {
            var result = Mailocomotive.Configuration.Api.Configuration();
            Assert.NotNull(result);            
        }

        [Fact]
        public void ApiReturnsExistingConfigurationIfCalledInThePast()
        {
            var first = Mailocomotive.Configuration.Api.Configuration();
            var second = Mailocomotive.Configuration.Api.Configuration();            
            Assert.True(first.Equals(second));
        }
    }
}
