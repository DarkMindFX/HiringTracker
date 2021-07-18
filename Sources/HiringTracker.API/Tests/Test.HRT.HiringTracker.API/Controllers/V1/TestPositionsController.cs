


using HRT.DTO;
using HRT.Utils.Convertors;
using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http.Headers;
using Xunit;


namespace Test.E2E.HiringTracker.API.Controllers.V1
{
    public class TestPositionsController : E2ETestBase, IClassFixture<WebApplicationFactory<HRT.HiringTracker.API.Startup>>
    {
        public TestPositionsController(WebApplicationFactory<HRT.HiringTracker.API.Startup> factory) : base(factory)
        {
            _testParams = GetTestParams("GenericControllerTestSettings");
        }

        [Fact]
        public void Position_GetAll_Success()
        {
            using (var client = _factory.CreateClient())
            {
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);

                var respGetAll = client.GetAsync($"/api/v1/positions");

                Assert.Equal(HttpStatusCode.OK, respGetAll.Result.StatusCode);

                IList<Position> dtos = ExtractContentJson<List<Position>>(respGetAll.Result.Content);

                Assert.NotEmpty(dtos);
            }
        }

        [Fact]
        public void Position_Get_Success()
        {
            HRT.Interfaces.Entities.Position testEntity = AddTestEntity();
            using (var client = _factory.CreateClient())
            {
                try
                {
                    var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);

                    var paramID = testEntity.ID;
                    var respGet = client.GetAsync($"/api/v1/positions/{paramID}");

                    Assert.Equal(HttpStatusCode.OK, respGet.Result.StatusCode);

                    Position dto = ExtractContentJson<Position>(respGet.Result.Content);

                    Assert.NotNull(dto);
                    Assert.NotNull(dto.Links);
                }
                finally
                {
                    RemoveTestEntity(testEntity);
                }
            }
        }

        [Fact]
        public void Position_Get_InvalidID()
        {
            using (var client = _factory.CreateClient())
            {
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);

                var paramID = Int64.MaxValue;

                var respGet = client.GetAsync($"/api/v1/positions/{paramID}");

                Assert.Equal(HttpStatusCode.NotFound, respGet.Result.StatusCode);
            }
        }

        [Fact]
        public void Position_Delete_Success()
        {
            var testEntity = AddTestEntity();
            using (var client = _factory.CreateClient())
            {
                try
                {
                    var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);

                    var paramID = testEntity.ID;

                    var respDel = client.DeleteAsync($"/api/v1/positions/{paramID}");

                    Assert.Equal(HttpStatusCode.OK, respDel.Result.StatusCode);
                }
                finally
                {
                    RemoveTestEntity(testEntity);
                }
            }
        }

        [Fact]
        public void Position_Delete_InvalidID()
        {
            using (var client = _factory.CreateClient())
            {
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);

                var paramID = Int64.MaxValue;

                var respDel = client.DeleteAsync($"/api/v1/positions/{paramID}");

                Assert.Equal(HttpStatusCode.NotFound, respDel.Result.StatusCode);
            }
        }

        [Fact]
        public void Position_Insert_Success()
        {
            using (var client = _factory.CreateClient())
            {
                HRT.Interfaces.Entities.Position testEntity = CreateTestEntity();
                HRT.Interfaces.Entities.Position respEntity = null;
                try
                {
                    var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);

                    var reqDto = PositionConvertor.Convert(testEntity, null);

                    var content = CreateContentJson(reqDto);

                    var respInsert = client.PostAsync($"/api/v1/positions/", content);

                    Assert.Equal(HttpStatusCode.OK, respInsert.Result.StatusCode);

                    Position respDto = ExtractContentJson<Position>(respInsert.Result.Content);

                    Assert.NotNull(respDto.ID);
                    Assert.Equal(reqDto.DepartmentID, respDto.DepartmentID);
                    Assert.Equal(reqDto.Title, respDto.Title);
                    Assert.Equal(reqDto.ShortDesc, respDto.ShortDesc);
                    Assert.Equal(reqDto.Description, respDto.Description);
                    Assert.Equal(reqDto.StatusID, respDto.StatusID);
                    Assert.Equal(reqDto.CreatedDate, respDto.CreatedDate);
                    Assert.Equal(reqDto.CreatedByID, respDto.CreatedByID);
                    Assert.Equal(reqDto.ModifiedDate, respDto.ModifiedDate);
                    Assert.Equal(reqDto.ModifiedByID, respDto.ModifiedByID);

                    respEntity = PositionConvertor.Convert(respDto);
                }
                finally
                {
                    RemoveTestEntity(respEntity);
                }
            }
        }

        [Fact]
        public void Position_Update_Success()
        {
            using (var client = _factory.CreateClient())
            {
                HRT.Interfaces.Entities.Position testEntity = AddTestEntity();
                try
                {
                    var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);

                    testEntity.Title = "Title c93d3fd6c8954fc881145214b530c287";
                    testEntity.ShortDesc = "ShortDesc c93d3fd6c8954fc881145214b530c287";
                    testEntity.Description = "Description c93d3fd6c8954fc881145214b530c287";
                    testEntity.StatusID = 2;
                    testEntity.CreatedDate = DateTime.Parse("12/17/2018 8:30:37 AM");
                    testEntity.CreatedByID = 100004;
                    testEntity.ModifiedDate = DateTime.Parse("6/15/2019 2:17:37 PM");
                    testEntity.ModifiedByID = 100001;

                    var reqDto = PositionConvertor.Convert(testEntity, null);

                    var content = CreateContentJson(reqDto);

                    var respUpdate = client.PutAsync($"/api/v1/positions/", content);

                    Assert.Equal(HttpStatusCode.OK, respUpdate.Result.StatusCode);

                    Position respDto = ExtractContentJson<Position>(respUpdate.Result.Content);

                    Assert.NotNull(respDto.ID);
                    Assert.Equal(reqDto.DepartmentID, respDto.DepartmentID);
                    Assert.Equal(reqDto.Title, respDto.Title);
                    Assert.Equal(reqDto.ShortDesc, respDto.ShortDesc);
                    Assert.Equal(reqDto.Description, respDto.Description);
                    Assert.Equal(reqDto.StatusID, respDto.StatusID);
                    Assert.Equal(reqDto.CreatedDate, respDto.CreatedDate);
                    Assert.Equal(reqDto.CreatedByID, respDto.CreatedByID);
                    Assert.Equal(reqDto.ModifiedDate, respDto.ModifiedDate);
                    Assert.Equal(reqDto.ModifiedByID, respDto.ModifiedByID);

                }
                finally
                {
                    RemoveTestEntity(testEntity);
                }
            }
        }

        [Fact]
        public void Position_Update_InvalidID()
        {
            using (var client = _factory.CreateClient())
            {
                HRT.Interfaces.Entities.Position testEntity = CreateTestEntity();
                try
                {
                    var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);

                    testEntity.ID = Int64.MaxValue;
                    testEntity.Title = "Title c93d3fd6c8954fc881145214b530c287";
                    testEntity.ShortDesc = "ShortDesc c93d3fd6c8954fc881145214b530c287";
                    testEntity.Description = "Description c93d3fd6c8954fc881145214b530c287";
                    testEntity.StatusID = 2;
                    testEntity.CreatedDate = DateTime.Parse("12/17/2018 8:30:37 AM");
                    testEntity.CreatedByID = 100004;
                    testEntity.ModifiedDate = DateTime.Parse("6/15/2019 2:17:37 PM");
                    testEntity.ModifiedByID = 100001;

                    var reqDto = PositionConvertor.Convert(testEntity, null);

                    var content = CreateContentJson(reqDto);

                    var respUpdate = client.PutAsync($"/api/v1/positions/", content);

                    Assert.Equal(HttpStatusCode.NotFound, respUpdate.Result.StatusCode);
                }
                finally
                {
                    RemoveTestEntity(testEntity);
                }
            }
        }

        #region Support methods

        protected bool RemoveTestEntity(HRT.Interfaces.Entities.Position entity)
        {
            if (entity != null)
            {
                var dal = CreateDal();

                return dal.Delete(
                                        entity.ID
                );
            }
            else
            {
                return false;
            }
        }

        protected HRT.Interfaces.Entities.Position CreateTestEntity()
        {
            var entity = new HRT.Interfaces.Entities.Position();
            entity.Title = "Title c032aeeb21d542d297a145020a28d1f5";
            entity.ShortDesc = "ShortDesc c032aeeb21d542d297a145020a28d1f5";
            entity.Description = "Description c032aeeb21d542d297a145020a28d1f5";
            entity.StatusID = 2;
            entity.CreatedDate = DateTime.Parse("8/19/2022 1:17:37 PM");
            entity.CreatedByID = 100005;
            entity.ModifiedDate = DateTime.Parse("4/5/2020 9:18:37 AM");
            entity.ModifiedByID = 100003;

            return entity;
        }

        protected HRT.Interfaces.Entities.Position AddTestEntity()
        {
            HRT.Interfaces.Entities.Position result = null;

            var entity = CreateTestEntity();

            var dal = CreateDal();
            result = dal.Insert(entity);

            return result;
        }

        private HRT.Interfaces.IPositionDal CreateDal()
        {
            var initParams = GetTestParams("DALInitParams");

            HRT.Interfaces.IPositionDal dal = new HRT.DAL.MSSQL.PositionDal();
            var dalInitParams = dal.CreateInitParams();
            dalInitParams.Parameters["ConnectionString"] = (string)initParams.Settings["ConnectionString"];
            dal.Init(dalInitParams);

            return dal;
        }
        #endregion
    }
}
