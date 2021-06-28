using HRT.DTO;
using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using Xunit;

namespace Test.E2E.HiringTracker.API.Controllers.V1
{
    public class TestPositionsController : E2ETestBase, IClassFixture<WebApplicationFactory<HRT.HiringTracker.API.Startup>>
    {
        public TestPositionsController(WebApplicationFactory<HRT.HiringTracker.API.Startup> factory) : base(factory)
        {
            _testParams = GetTestParams("PositionsControllerTestSettings");
        }

        [Fact]
        public void Position_GetAll_Success()
        {
            using (var client = _factory.CreateClient())
            {
                var respGetAll = client.GetAsync($"/api/v1/positions");

                Assert.Equal(HttpStatusCode.OK, respGetAll.Result.StatusCode);

                IList<Position> dtos = ExtractContentJson<List<Position>>(respGetAll.Result.Content);

                Assert.NotEmpty(dtos);
            }
        }

        [Fact]
        public void Position_Get_Success()
        {
            using (var client = _factory.CreateClient())
            {
                long ID = 100002;

                var respGet = client.GetAsync($"/api/v1/positions/{ID}");

                Assert.Equal(HttpStatusCode.OK, respGet.Result.StatusCode);

                Position dto = ExtractContentJson<Position>(respGet.Result.Content);

                Assert.NotNull(dto);
                Assert.NotNull(dto.Links);
            }
        }

        [Fact]
        public void Position_Get_InvalidID()
        {
            using (var client = _factory.CreateClient())
            {
                long ID = Int64.MaxValue;

                var respGet = client.GetAsync($"/api/v1/positions/{ID}");

                Assert.Equal(HttpStatusCode.NotFound, respGet.Result.StatusCode);
            }
        }
    }
}
