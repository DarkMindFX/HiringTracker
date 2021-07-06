


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
    public class TestUsersController : E2ETestBase, IClassFixture<WebApplicationFactory<HRT.HiringTracker.API.Startup>>
    {
        public TestUsersController(WebApplicationFactory<HRT.HiringTracker.API.Startup> factory) : base(factory)
        {
            _testParams = GetTestParams("UsersControllerTestSettings");
        }

        [Fact]
        public void User_GetAll_Success()
        {
            using (var client = _factory.CreateClient())
            {
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);

                var respGetAll = client.GetAsync($"/api/v1/users");

                Assert.Equal(HttpStatusCode.OK, respGetAll.Result.StatusCode);

                IList<User> dtos = ExtractContentJson<List<User>>(respGetAll.Result.Content);

                Assert.NotEmpty(dtos);
            }
        }

        [Fact]
        public void User_Get_Success()
        {
            HRT.Interfaces.Entities.User testEntity = AddTestEntity();
            using (var client = _factory.CreateClient())
            {
                try
                {
                    var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);

                    var paramID = testEntity.ID;
                    var respGet = client.GetAsync($"/api/v1/users/{paramID}");

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
        public void User_Get_InvalidID()
        {
            using (var client = _factory.CreateClient())
            {
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);

                var paramID = Int64.MaxValue;

                var respGet = client.GetAsync($"/api/v1/users/{paramID}");

                Assert.Equal(HttpStatusCode.NotFound, respGet.Result.StatusCode);
            }
        }

        [Fact]
        public void User_Delete_Success()
        {
            var testEntity = AddTestEntity();
            using (var client = _factory.CreateClient())
            {
                try
                {
                    var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);

                    var paramID = testEntity.ID;

                    var respDel = client.DeleteAsync($"/api/v1/users/{paramID}");

                    Assert.Equal(HttpStatusCode.OK, respDel.Result.StatusCode);
                }
                finally
                {
                    RemoveTestEntity(testEntity);
                }
            }
        }

        [Fact]
        public void User_Delete_InvalidID()
        {
            using (var client = _factory.CreateClient())
            {
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);

                var paramID = Int64.MaxValue;

                var respDel = client.DeleteAsync($"/api/v1/users/{paramID}");

                Assert.Equal(HttpStatusCode.NotFound, respDel.Result.StatusCode);
            }
        }

        [Fact]
        public void User_Insert_Success()
        {
            using (var client = _factory.CreateClient())
            {
                HRT.Interfaces.Entities.User testEntity = CreateTestEntity();
                HRT.Interfaces.Entities.User respEntity = null;
                try
                {
                    var reqDto = UserConvertor.Convert(testEntity, null);
                    reqDto.Password = "TestPassword b424c4c5819a44509cc54630e7c4e5da";

                    var content = CreateContentJson(reqDto);

                    var respInsert = client.PostAsync($"/api/v1/users/", content);

                    Assert.Equal(HttpStatusCode.OK, respInsert.Result.StatusCode);

                    User respDto = ExtractContentJson<User>(respInsert.Result.Content);

                    Assert.NotNull(respDto.ID);
                    Assert.Equal(reqDto.Login, respDto.Login);
                    Assert.Equal(reqDto.FirstName, respDto.FirstName);
                    Assert.Equal(reqDto.LastName, respDto.LastName);
                    Assert.Equal(reqDto.Email, respDto.Email);
                    Assert.Equal(reqDto.Description, respDto.Description);

                    respEntity = UserConvertor.Convert(respDto);
                }
                finally
                {
                    RemoveTestEntity(respEntity);
                }
            }
        }

        [Fact]
        public void User_Login_Success()
        {
            using (var client = _factory.CreateClient())
            {
                HRT.Interfaces.Entities.User testEntity = CreateTestEntity();
                HRT.Interfaces.Entities.User respEntity = null;
                try
                {
                    var reqDto = UserConvertor.Convert(testEntity, null);
                    reqDto.Password = "TestPassword b424c4c5819a44509cc54630e7c4e5da";
                    
                    var content = CreateContentJson(reqDto);

                    // inserting new user
                    var respInsert = client.PostAsync($"/api/v1/users/", content);

                    Assert.Equal(HttpStatusCode.OK, respInsert.Result.StatusCode);

                    User respDto = ExtractContentJson<User>(respInsert.Result.Content);

                    Assert.NotNull(respDto.ID);
                    Assert.Equal(reqDto.Login, respDto.Login);
                    Assert.Equal(reqDto.FirstName, respDto.FirstName);
                    Assert.Equal(reqDto.LastName, respDto.LastName);
                    Assert.Equal(reqDto.Email, respDto.Email);
                    Assert.Equal(reqDto.Description, respDto.Description);

                    respEntity = UserConvertor.Convert(respDto);

                    // sending login
                    var dtoLogin = new HRT.DTO.LoginRequest()
                    {
                        Login = testEntity.Login,
                        Password = reqDto.Password
                    };
                    content = CreateContentJson(dtoLogin);

                    var respLogin = client.PostAsync($"/api/v1/users/login/", content);

                    Assert.Equal(HttpStatusCode.OK, respLogin.Result.StatusCode);
                    var dtoResponse = ExtractContentJson<LoginResponse>(respLogin.Result.Content);

                    Assert.NotNull(dtoResponse.User);
                    Assert.NotNull(dtoResponse.User.ID);
                    Assert.NotNull(dtoResponse.Token);
                    Assert.True(dtoResponse.Expires >= DateTime.UtcNow);
                }
                finally
                {
                    RemoveTestEntity(respEntity);
                }
            }
        }

        [Fact]
        public void User_Update_Success()
        {
            using (var client = _factory.CreateClient())
            {
                HRT.Interfaces.Entities.User testEntity = AddTestEntity();
                try
                {
                    var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);

                    testEntity.Login = "Login b424c4c5819a44509cc54630e7c4e5da";
                    testEntity.FirstName = "FirstName b424c4c5819a44509cc54630e7c4e5da";
                    testEntity.LastName = "LastName b424c4c5819a44509cc54630e7c4e5da";
                    testEntity.Email = "Email b424c4c5819a44509cc54630e7c4e5da";
                    testEntity.Description = "Description b424c4c5819a44509cc54630e7c4e5da";
                    testEntity.PwdHash = "PwdHash b424c4c5819a44509cc54630e7c4e5da";
                    testEntity.Salt = "Salt b424c4c5819a44509cc54630e7c4e5da";

                    var reqDto = UserConvertor.Convert(testEntity, null);

                    var content = CreateContentJson(reqDto);

                    var respUpdate = client.PutAsync($"/api/v1/users/", content);

                    Assert.Equal(HttpStatusCode.OK, respUpdate.Result.StatusCode);

                    User respDto = ExtractContentJson<User>(respUpdate.Result.Content);

                    Assert.NotNull(respDto.ID);
                    Assert.Equal(reqDto.Login, respDto.Login);
                    Assert.Equal(reqDto.FirstName, respDto.FirstName);
                    Assert.Equal(reqDto.LastName, respDto.LastName);
                    Assert.Equal(reqDto.Email, respDto.Email);
                    Assert.Equal(reqDto.Description, respDto.Description);
                }
                finally
                {
                    RemoveTestEntity(testEntity);
                }
            }
        }

        [Fact]
        public void User_Update_InvalidID()
        {
            using (var client = _factory.CreateClient())
            {
                HRT.Interfaces.Entities.User testEntity = CreateTestEntity();
                try
                {
                    var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);

                    testEntity.ID = Int64.MaxValue;
                    testEntity.Login = "Login b424c4c5819a44509cc54630e7c4e5da";
                    testEntity.FirstName = "FirstName b424c4c5819a44509cc54630e7c4e5da";
                    testEntity.LastName = "LastName b424c4c5819a44509cc54630e7c4e5da";
                    testEntity.Email = "Email b424c4c5819a44509cc54630e7c4e5da";
                    testEntity.Description = "Description b424c4c5819a44509cc54630e7c4e5da";
                    testEntity.PwdHash = "PwdHash b424c4c5819a44509cc54630e7c4e5da";
                    testEntity.Salt = "Salt b424c4c5819a44509cc54630e7c4e5da";

                    var reqDto = UserConvertor.Convert(testEntity, null);

                    var content = CreateContentJson(reqDto);

                    var respUpdate = client.PutAsync($"/api/v1/users/", content);

                    Assert.Equal(HttpStatusCode.NotFound, respUpdate.Result.StatusCode);
                }
                finally
                {
                    RemoveTestEntity(testEntity);
                }
            }
        }

        #region Support methods

        protected bool RemoveTestEntity(HRT.Interfaces.Entities.User entity)
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

        protected HRT.Interfaces.Entities.User CreateTestEntity()
        {
            var entity = new HRT.Interfaces.Entities.User();
            entity.Login = "Login 84d9880834da4522aaad3c7905cb5230";
            entity.FirstName = "FirstName 84d9880834da4522aaad3c7905cb5230";
            entity.LastName = "LastName 84d9880834da4522aaad3c7905cb5230";
            entity.Email = "Email 84d9880834da4522aaad3c7905cb5230";
            entity.Description = "Description 84d9880834da4522aaad3c7905cb5230";
            entity.PwdHash = "PwdHash 84d9880834da4522aaad3c7905cb5230";
            entity.Salt = "Salt 84d9880834da4522aaad3c7905cb5230";

            return entity;
        }

        protected HRT.Interfaces.Entities.User AddTestEntity()
        {
            HRT.Interfaces.Entities.User result = null;

            var entity = CreateTestEntity();

            var dal = CreateDal();
            result = dal.Insert(entity);

            return result;
        }

        private HRT.Interfaces.IUserDal CreateDal()
        {
            var initParams = GetTestParams("DALInitParams");

            HRT.Interfaces.IUserDal dal = new HRT.DAL.MSSQL.UserDal();
            var dalInitParams = dal.CreateInitParams();
            dalInitParams.Parameters["ConnectionString"] = (string)initParams.Settings["ConnectionString"];
            dal.Init(dalInitParams);

            return dal;
        }
        #endregion
    }
}
