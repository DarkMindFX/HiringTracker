


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
    public class TestUserRoleCandidatesController : E2ETestBase, IClassFixture<WebApplicationFactory<HRT.HiringTracker.API.Startup>>
    {
        public TestUserRoleCandidatesController(WebApplicationFactory<HRT.HiringTracker.API.Startup> factory) : base(factory)
        {
            _testParams = GetTestParams("GenericControllerTestSettings");
        }

        [Fact]
        public void UserRoleCandidate_GetAll_Success()
        {
            using (var client = _factory.CreateClient())
            {
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);

                var respGetAll = client.GetAsync($"/api/v1/userrolecandidates");

                Assert.Equal(HttpStatusCode.OK, respGetAll.Result.StatusCode);

                IList<UserRoleCandidate> dtos = ExtractContentJson<List<UserRoleCandidate>>(respGetAll.Result.Content);

                Assert.NotEmpty(dtos);
            }
        }

        [Fact]
        public void UserRoleCandidate_Get_Success()
        {
            HRT.Interfaces.Entities.UserRoleCandidate testEntity = AddTestEntity();
            using (var client = _factory.CreateClient())
            {
                try
                {
                    var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);

                    var paramCandidateID = testEntity.CandidateID;
                    var paramUserID = testEntity.UserID;
                    var respGet = client.GetAsync($"/api/v1/userrolecandidates/{paramCandidateID}/{paramUserID}");

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
        public void UserRoleCandidate_Get_InvalidID()
        {
            using (var client = _factory.CreateClient())
            {
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);

                var paramCandidateID = Int64.MaxValue;
                var paramUserID = Int64.MaxValue;

                var respGet = client.GetAsync($"/api/v1/userrolecandidates/{paramCandidateID}/{paramUserID}");

                Assert.Equal(HttpStatusCode.NotFound, respGet.Result.StatusCode);
            }
        }

        [Fact]
        public void UserRoleCandidate_Delete_Success()
        {
            var testEntity = AddTestEntity();
            using (var client = _factory.CreateClient())
            {
                try
                {
                    var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);

                    var paramCandidateID = testEntity.CandidateID;
                    var paramUserID = testEntity.UserID;

                    var respDel = client.DeleteAsync($"/api/v1/userrolecandidates/{paramCandidateID}/{paramUserID}");

                    Assert.Equal(HttpStatusCode.OK, respDel.Result.StatusCode);
                }
                finally
                {
                    RemoveTestEntity(testEntity);
                }
            }
        }

        [Fact]
        public void UserRoleCandidate_Delete_InvalidID()
        {
            using (var client = _factory.CreateClient())
            {
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);

                var paramCandidateID = Int64.MaxValue;
                var paramUserID = Int64.MaxValue;

                var respDel = client.DeleteAsync($"/api/v1/userrolecandidates/{paramCandidateID}/{paramUserID}");

                Assert.Equal(HttpStatusCode.NotFound, respDel.Result.StatusCode);
            }
        }

        [Fact]
        public void UserRoleCandidate_Insert_Success()
        {
            using (var client = _factory.CreateClient())
            {
                HRT.Interfaces.Entities.UserRoleCandidate testEntity = CreateTestEntity();
                HRT.Interfaces.Entities.UserRoleCandidate respEntity = null;
                try
                {
                    var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);

                    var reqDto = UserRoleCandidateConvertor.Convert(testEntity, null);

                    var content = CreateContentJson(reqDto);

                    var respInsert = client.PostAsync($"/api/v1/userrolecandidates/", content);

                    Assert.Equal(HttpStatusCode.Created, respInsert.Result.StatusCode);

                    UserRoleCandidate respDto = ExtractContentJson<UserRoleCandidate>(respInsert.Result.Content);

                    Assert.NotNull(respDto.CandidateID);
                    Assert.NotNull(respDto.UserID);
                    Assert.Equal(reqDto.RoleID, respDto.RoleID);

                    respEntity = UserRoleCandidateConvertor.Convert(respDto);
                }
                finally
                {
                    RemoveTestEntity(respEntity);
                }
            }
        }

        [Fact]
        public void UserRoleCandidate_Update_Success()
        {
            using (var client = _factory.CreateClient())
            {
                HRT.Interfaces.Entities.UserRoleCandidate testEntity = AddTestEntity();
                try
                {
                    var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);

                    testEntity.RoleID = 1;

                    var reqDto = UserRoleCandidateConvertor.Convert(testEntity, null);

                    var content = CreateContentJson(reqDto);

                    var respUpdate = client.PutAsync($"/api/v1/userrolecandidates/", content);

                    Assert.Equal(HttpStatusCode.OK, respUpdate.Result.StatusCode);

                    UserRoleCandidate respDto = ExtractContentJson<UserRoleCandidate>(respUpdate.Result.Content);

                    Assert.NotNull(respDto.CandidateID);
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
        public void UserRoleCandidate_Update_InvalidID()
        {
            using (var client = _factory.CreateClient())
            {
                HRT.Interfaces.Entities.UserRoleCandidate testEntity = CreateTestEntity();
                try
                {
                    var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);

                    testEntity.CandidateID = 100005;
                    testEntity.UserID = 100003;
                    testEntity.RoleID = 1;

                    var reqDto = UserRoleCandidateConvertor.Convert(testEntity, null);

                    var content = CreateContentJson(reqDto);

                    var respUpdate = client.PutAsync($"/api/v1/userrolecandidates/", content);

                    Assert.Equal(HttpStatusCode.NotFound, respUpdate.Result.StatusCode);
                }
                finally
                {
                    RemoveTestEntity(testEntity);
                }
            }
        }

        #region Support methods

        protected bool RemoveTestEntity(HRT.Interfaces.Entities.UserRoleCandidate entity)
        {
            if (entity != null)
            {
                var dal = CreateDal();

                return dal.Delete(
                                        entity.CandidateID,
                                        entity.UserID
                );
            }
            else
            {
                return false;
            }
        }

        protected HRT.Interfaces.Entities.UserRoleCandidate CreateTestEntity()
        {
            var entity = new HRT.Interfaces.Entities.UserRoleCandidate();
            entity.CandidateID = 100003;
            entity.UserID = 100002;
            entity.RoleID = 6;

            return entity;
        }

        protected HRT.Interfaces.Entities.UserRoleCandidate AddTestEntity()
        {
            HRT.Interfaces.Entities.UserRoleCandidate result = null;

            var entity = CreateTestEntity();

            var dal = CreateDal();
            result = dal.Insert(entity);

            return result;
        }

        private HRT.Interfaces.IUserRoleCandidateDal CreateDal()
        {
            var initParams = GetTestParams("DALInitParams");

            HRT.Interfaces.IUserRoleCandidateDal dal = new HRT.DAL.MSSQL.UserRoleCandidateDal();
            var dalInitParams = dal.CreateInitParams();
            dalInitParams.Parameters["ConnectionString"] = (string)initParams.Settings["ConnectionString"];
            dal.Init(dalInitParams);

            return dal;
        }
        #endregion
    }
}
