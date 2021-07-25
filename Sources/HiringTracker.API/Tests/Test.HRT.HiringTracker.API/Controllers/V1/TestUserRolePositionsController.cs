


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
    public class TestUserRolePositionsController : E2ETestBase, IClassFixture<WebApplicationFactory<HRT.HiringTracker.API.Startup>>
    {
        public TestUserRolePositionsController(WebApplicationFactory<HRT.HiringTracker.API.Startup> factory) : base(factory)
        {
            _testParams = GetTestParams("GenericControllerTestSettings");
        }

        [Fact]
        public void UserRolePosition_GetAll_Success()
        {
            using (var client = _factory.CreateClient())
            {
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);

                var respGetAll = client.GetAsync($"/api/v1/userrolepositions");

                Assert.Equal(HttpStatusCode.OK, respGetAll.Result.StatusCode);

                IList<UserRolePosition> dtos = ExtractContentJson<List<UserRolePosition>>(respGetAll.Result.Content);

                Assert.NotEmpty(dtos);
            }
        }

        [Fact]
        public void UserRolePosition_Get_Success()
        {
            HRT.Interfaces.Entities.UserRolePosition testEntity = AddTestEntity();
            using (var client = _factory.CreateClient())
            {
                try
                {
                    var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);

                    var paramPositionID = testEntity.PositionID;
                    var paramUserID = testEntity.UserID;
                    var respGet = client.GetAsync($"/api/v1/userrolepositions/{paramPositionID}/{paramUserID}");

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
        public void UserRolePosition_Get_InvalidID()
        {
            using (var client = _factory.CreateClient())
            {
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);

                var paramPositionID = Int64.MaxValue;
                var paramUserID = Int64.MaxValue;

                var respGet = client.GetAsync($"/api/v1/userrolepositions/{paramPositionID}/{paramUserID}");

                Assert.Equal(HttpStatusCode.NotFound, respGet.Result.StatusCode);
            }
        }

        [Fact]
        public void UserRolePosition_Delete_Success()
        {
            var testEntity = AddTestEntity();
            using (var client = _factory.CreateClient())
            {
                try
                {
                    var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);

                    var paramPositionID = testEntity.PositionID;
                    var paramUserID = testEntity.UserID;

                    var respDel = client.DeleteAsync($"/api/v1/userrolepositions/{paramPositionID}/{paramUserID}");

                    Assert.Equal(HttpStatusCode.OK, respDel.Result.StatusCode);
                }
                finally
                {
                    RemoveTestEntity(testEntity);
                }
            }
        }

        [Fact]
        public void UserRolePosition_Delete_InvalidID()
        {
            using (var client = _factory.CreateClient())
            {
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);

                var paramPositionID = Int64.MaxValue;
                var paramUserID = Int64.MaxValue;

                var respDel = client.DeleteAsync($"/api/v1/userrolepositions/{paramPositionID}/{paramUserID}");

                Assert.Equal(HttpStatusCode.NotFound, respDel.Result.StatusCode);
            }
        }

        [Fact]
        public void UserRolePosition_Insert_Success()
        {
            using (var client = _factory.CreateClient())
            {
                HRT.Interfaces.Entities.UserRolePosition testEntity = CreateTestEntity();
                HRT.Interfaces.Entities.UserRolePosition respEntity = null;
                try
                {
                    var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);

                    var reqDto = UserRolePositionConvertor.Convert(testEntity, null);

                    var content = CreateContentJson(reqDto);

                    var respInsert = client.PostAsync($"/api/v1/userrolepositions/", content);

                    Assert.Equal(HttpStatusCode.Created, respInsert.Result.StatusCode);

                    UserRolePosition respDto = ExtractContentJson<UserRolePosition>(respInsert.Result.Content);

                    Assert.NotNull(respDto.PositionID);
                    Assert.NotNull(respDto.UserID);
                    Assert.Equal(reqDto.RoleID, respDto.RoleID);

                    respEntity = UserRolePositionConvertor.Convert(respDto);
                }
                finally
                {
                    RemoveTestEntity(respEntity);
                }
            }
        }

        [Fact]
        public void UserRolePosition_Update_Success()
        {
            using (var client = _factory.CreateClient())
            {
                HRT.Interfaces.Entities.UserRolePosition testEntity = AddTestEntity();
                try
                {
                    var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);

                    testEntity.RoleID = 8;

                    var reqDto = UserRolePositionConvertor.Convert(testEntity, null);

                    var content = CreateContentJson(reqDto);

                    var respUpdate = client.PutAsync($"/api/v1/userrolepositions/", content);

                    Assert.Equal(HttpStatusCode.OK, respUpdate.Result.StatusCode);

                    UserRolePosition respDto = ExtractContentJson<UserRolePosition>(respUpdate.Result.Content);

                    Assert.NotNull(respDto.PositionID);
                    Assert.NotNull(respDto.UserID);
                    Assert.Equal(reqDto.RoleID, respDto.RoleID);

                }
                finally
                {
                    RemoveTestEntity(testEntity);
                }
            }
        }

        [Fact]
        public void UserRolePosition_Update_InvalidID()
        {
            using (var client = _factory.CreateClient())
            {
                HRT.Interfaces.Entities.UserRolePosition testEntity = CreateTestEntity();
                try
                {
                    var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);

                    testEntity.PositionID = 100009;
                    testEntity.UserID = 100003;
                    testEntity.RoleID = 8;

                    var reqDto = UserRolePositionConvertor.Convert(testEntity, null);

                    var content = CreateContentJson(reqDto);

                    var respUpdate = client.PutAsync($"/api/v1/userrolepositions/", content);

                    Assert.Equal(HttpStatusCode.NotFound, respUpdate.Result.StatusCode);
                }
                finally
                {
                    RemoveTestEntity(testEntity);
                }
            }
        }

        #region Support methods

        protected bool RemoveTestEntity(HRT.Interfaces.Entities.UserRolePosition entity)
        {
            if (entity != null)
            {
                var dal = CreateDal();

                return dal.Delete(
                                        entity.PositionID,
                                        entity.UserID
                );
            }
            else
            {
                return false;
            }
        }

        protected HRT.Interfaces.Entities.UserRolePosition CreateTestEntity()
        {
            var entity = new HRT.Interfaces.Entities.UserRolePosition();
            entity.PositionID = 100001;
            entity.UserID = 100005;
            entity.RoleID = 9;

            return entity;
        }

        protected HRT.Interfaces.Entities.UserRolePosition AddTestEntity()
        {
            HRT.Interfaces.Entities.UserRolePosition result = null;

            var entity = CreateTestEntity();

            var dal = CreateDal();
            result = dal.Insert(entity);

            return result;
        }

        private HRT.Interfaces.IUserRolePositionDal CreateDal()
        {
            var initParams = GetTestParams("DALInitParams");

            HRT.Interfaces.IUserRolePositionDal dal = new HRT.DAL.MSSQL.UserRolePositionDal();
            var dalInitParams = dal.CreateInitParams();
            dalInitParams.Parameters["ConnectionString"] = (string)initParams.Settings["ConnectionString"];
            dal.Init(dalInitParams);

            return dal;
        }
        #endregion
    }
}
