


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
    public class TestInterviewRolesController : E2ETestBase, IClassFixture<WebApplicationFactory<HRT.HiringTracker.API.Startup>>
    {
        public TestInterviewRolesController(WebApplicationFactory<HRT.HiringTracker.API.Startup> factory) : base(factory)
        {
            _testParams = GetTestParams("GenericControllerTestSettings");
        }

        [Fact]
        public void InterviewRole_GetAll_Success()
        {
            using (var client = _factory.CreateClient())
            {
                var respGetAll = client.GetAsync($"/api/v1/interviewroles");

                Assert.Equal(HttpStatusCode.OK, respGetAll.Result.StatusCode);

                IList<InterviewRole> dtos = ExtractContentJson<List<InterviewRole>>(respGetAll.Result.Content);

                Assert.NotEmpty(dtos);
            }
        }

        [Fact]
        public void InterviewRole_Get_Success()
        {
            HRT.Interfaces.Entities.InterviewRole testEntity = AddTestEntity();
            using (var client = _factory.CreateClient())
            {
                try
                {
                    var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);

                    var paramInterviewID = testEntity.InterviewID;
                    var paramUserID = testEntity.UserID;
                    var respGet = client.GetAsync($"/api/v1/interviewroles/{paramInterviewID}/{paramUserID}");

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
        public void InterviewRole_Get_InvalidID()
        {
            using (var client = _factory.CreateClient())
            {
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);

                var paramInterviewID = Int64.MaxValue;
                var paramUserID = Int64.MaxValue;

                var respGet = client.GetAsync($"/api/v1/interviewroles/{paramInterviewID}/{paramUserID}");

                Assert.Equal(HttpStatusCode.NotFound, respGet.Result.StatusCode);
            }
        }

        [Fact]
        public void InterviewRole_Delete_Success()
        {
            var testEntity = AddTestEntity();
            using (var client = _factory.CreateClient())
            {
                try
                {
                    var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);

                    var paramInterviewID = testEntity.InterviewID;
                    var paramUserID = testEntity.UserID;

                    var respDel = client.DeleteAsync($"/api/v1/interviewroles/{paramInterviewID}/{paramUserID}");

                    Assert.Equal(HttpStatusCode.OK, respDel.Result.StatusCode);
                }
                finally
                {
                    RemoveTestEntity(testEntity);
                }
            }
        }

        [Fact]
        public void InterviewRole_Delete_InvalidID()
        {
            using (var client = _factory.CreateClient())
            {
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);

                var paramInterviewID = Int64.MaxValue;
                var paramUserID = Int64.MaxValue;

                var respDel = client.DeleteAsync($"/api/v1/interviewroles/{paramInterviewID}/{paramUserID}");

                Assert.Equal(HttpStatusCode.NotFound, respDel.Result.StatusCode);
            }
        }

        [Fact]
        public void InterviewRole_Insert_Success()
        {
            using (var client = _factory.CreateClient())
            {
                HRT.Interfaces.Entities.InterviewRole testEntity = CreateTestEntity();
                HRT.Interfaces.Entities.InterviewRole respEntity = null;
                try
                {
                    var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);

                    var reqDto = InterviewRoleConvertor.Convert(testEntity, null);

                    var content = CreateContentJson(reqDto);

                    var respInsert = client.PostAsync($"/api/v1/interviewroles/", content);

                    Assert.Equal(HttpStatusCode.OK, respInsert.Result.StatusCode);

                    InterviewRole respDto = ExtractContentJson<InterviewRole>(respInsert.Result.Content);

                    Assert.NotNull(respDto.InterviewID);
                    Assert.NotNull(respDto.UserID);
                    Assert.Equal(reqDto.RoleID, respDto.RoleID);

                    respEntity = InterviewRoleConvertor.Convert(respDto);
                }
                finally
                {
                    RemoveTestEntity(respEntity);
                }
            }
        }

        [Fact]
        public void InterviewRole_Update_Success()
        {
            using (var client = _factory.CreateClient())
            {
                HRT.Interfaces.Entities.InterviewRole testEntity = AddTestEntity();
                try
                {
                    var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);

                    testEntity.RoleID = 4;

                    var reqDto = InterviewRoleConvertor.Convert(testEntity, null);

                    var content = CreateContentJson(reqDto);

                    var respUpdate = client.PutAsync($"/api/v1/interviewroles/", content);

                    Assert.Equal(HttpStatusCode.OK, respUpdate.Result.StatusCode);

                    InterviewRole respDto = ExtractContentJson<InterviewRole>(respUpdate.Result.Content);

                    Assert.NotNull(respDto.InterviewID);
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
        public void InterviewRole_Update_InvalidID()
        {
            using (var client = _factory.CreateClient())
            {
                HRT.Interfaces.Entities.InterviewRole testEntity = CreateTestEntity();
                try
                {
                    var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);

                    testEntity.InterviewID = 100007;
                    testEntity.UserID = 100003;
                    testEntity.RoleID = 4;

                    var reqDto = InterviewRoleConvertor.Convert(testEntity, null);

                    var content = CreateContentJson(reqDto);

                    var respUpdate = client.PutAsync($"/api/v1/interviewroles/", content);

                    Assert.Equal(HttpStatusCode.NotFound, respUpdate.Result.StatusCode);
                }
                finally
                {
                    RemoveTestEntity(testEntity);
                }
            }
        }

        #region Support methods

        protected bool RemoveTestEntity(HRT.Interfaces.Entities.InterviewRole entity)
        {
            if (entity != null)
            {
                var dal = CreateDal();

                return dal.Delete(
                                        entity.InterviewID,
                                        entity.UserID
                );
            }
            else
            {
                return false;
            }
        }

        protected HRT.Interfaces.Entities.InterviewRole CreateTestEntity()
        {
            var entity = new HRT.Interfaces.Entities.InterviewRole();
            entity.InterviewID = 100005;
            entity.UserID = 100004;
            entity.RoleID = 7;

            return entity;
        }

        protected HRT.Interfaces.Entities.InterviewRole AddTestEntity()
        {
            HRT.Interfaces.Entities.InterviewRole result = null;

            var entity = CreateTestEntity();

            var dal = CreateDal();
            result = dal.Insert(entity);

            return result;
        }

        private HRT.Interfaces.IInterviewRoleDal CreateDal()
        {
            var initParams = GetTestParams("DALInitParams");

            HRT.Interfaces.IInterviewRoleDal dal = new HRT.DAL.MSSQL.InterviewRoleDal();
            var dalInitParams = dal.CreateInitParams();
            dalInitParams.Parameters["ConnectionString"] = (string)initParams.Settings["ConnectionString"];
            dal.Init(dalInitParams);

            return dal;
        }
        #endregion
    }
}
