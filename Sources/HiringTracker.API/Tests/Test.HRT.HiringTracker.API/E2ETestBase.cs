
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;

namespace Test.E2E.HiringTracker.API
{
    public abstract class E2ETestBase
    {
        public class TestParams
        {
            public TestParams()
            {
                Settings = new Dictionary<string, object>();
            }

            public Dictionary<string, object> Settings
            {
                get;
                set;
            }
        }

        protected readonly WebApplicationFactory<HRT.HiringTracker.API.Startup> _factory;
        protected TestParams _testParams;

        public E2ETestBase(WebApplicationFactory<HRT.HiringTracker.API.Startup> factory)
        {
            this._factory = factory;
        }

        protected HttpContent CreateContentJson(object data)
        {
            var content = JsonSerializer.Serialize(data);
            var buffer = System.Text.Encoding.UTF8.GetBytes(content);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            return byteContent;
        }

        protected TResult ExtractContentJson<TResult>(HttpContent content)
        {
            var bytes = content.ReadAsByteArrayAsync().Result;
            var sContent = System.Text.Encoding.UTF8.GetString(bytes);
            TResult result = JsonSerializer.Deserialize<TResult>(sContent);

            return result;
        }

        protected TestParams GetTestParams(string name)
        {
            TestParams testParams = new TestParams();

            var config = GetConfiguration();

            testParams.Settings = config.GetSection(name).GetChildren().ToDictionary(x => x.Key, x => (object)x.Value);

            return testParams;
        }

        protected IConfiguration GetConfiguration()
        {
            IConfiguration config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            return config;
        }
      
    }
}
