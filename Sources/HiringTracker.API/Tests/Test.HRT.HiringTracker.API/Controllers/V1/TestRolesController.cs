


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
    public class TestRolesController : E2ETestBase, IClassFixture<WebApplicationFactory<HRT.HiringTracker.API.Startup>>
    {
        public TestRolesController(WebApplicationFactory<HRT.HiringTracker.API.Startup> factory) : base(factory)
        {
            _testParams = GetTestParams("GenericControllerTestSettings");
        }

        [Fact]
        public void Role_GetAll_Success()
        {
            using (var client = _factory.CreateClient())
            {
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);

                var respGetAll = client.GetAsync($"/api/v1/roles");

                Assert.Equal(HttpStatusCode.OK, respGetAll.Result.StatusCode);

                IList<Role> dtos = ExtractContentJson<List<Role>>(respGetAll.Result.Content);

                Assert.NotEmpty(dtos);
            }
        }

        [Fact]
        public void Role_Get_Success()
        {
            HRT.Interfaces.Entities.Role testEntity = AddTestEntity();
            using (var client = _factory.CreateClient())
            {
                try
                {
                    var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);

                    var paramID = testEntity.ID;
                    var respGet = client.GetAsync($"/api/v1/roles/{paramID}");

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
        public void Role_Get_InvalidID()
        {
            using (var client = _factory.CreateClient())
            {
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);

                var paramID = Int64.MaxValue;

                var respGet = client.GetAsync($"/api/v1/roles/{paramID}");

                Assert.Equal(HttpStatusCode.NotFound, respGet.Result.StatusCode);
            }
        }

        [Fact]
        public void Role_Delete_Success()
        {
            var testEntity = AddTestEntity();
            using (var client = _factory.CreateClient())
            {
                try
                {
                    var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);

                    var paramID = testEntity.ID;

                    var respDel = client.DeleteAsync($"/api/v1/roles/{paramID}");

                    Assert.Equal(HttpStatusCode.OK, respDel.Result.StatusCode);
                }
                finally
                {
                    RemoveTestEntity(testEntity);
                }
            }
        }

        [Fact]
        public void Role_Delete_InvalidID()
        {
            using (var client = _factory.CreateClient())
            {
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);

                var paramID = Int64.MaxValue;

                var respDel = client.DeleteAsync($"/api/v1/roles/{paramID}");

                Assert.Equal(HttpStatusCode.NotFound, respDel.Result.StatusCode);
            }
        }

        [Fact]
        public void Role_Insert_Success()
        {
            using (var client = _factory.CreateClient())
            {
                HRT.Interfaces.Entities.Role testEntity = CreateTestEntity();
                HRT.Interfaces.Entities.Role respEntity = null;
                try
                {
                    var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);

                    var reqDto = RoleConvertor.Convert(testEntity, null);

                    var content = CreateContentJson(reqDto);

                    var respInsert = client.PostAsync($"/api/v1/roles/", content);

                    Assert.Equal(HttpStatusCode.Created, respInsert.Result.StatusCode);

                    Role respDto = ExtractContentJson<Role>(respInsert.Result.Content);

                    Assert.NotNull(respDto.ID);
                    Assert.Equal(reqDto.Name, respDto.Name);

                    respEntity = RoleConvertor.Convert(respDto);
                }
                finally
                {
                    RemoveTestEntity(respEntity);
                }
            }
        }

        [Fact]
        public void Role_Update_Success()
        {
            using (var client = _factory.CreateClient())
            {
                HRT.Interfaces.Entities.Role testEntity = AddTestEntity();
                try
                {
                    var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);

                    testEntity.Name = "Name 90256dcf5d0a4a85919945732c302bbf";

                    var reqDto = RoleConvertor.Convert(testEntity, null);

                    var content = CreateContentJson(reqDto);

                    var respUpdate = client.PutAsync($"/api/v1/roles/", content);

                    Assert.Equal(HttpStatusCode.OK, respUpdate.Result.StatusCode);

                    Role respDto = ExtractContentJson<Role>(respUpdate.Result.Content);

                    Assert.NotNull(respDto.ID);
                    Assert.Equal(reqDto.Name, respDto.Name);

                }
                finally
                {
                    RemoveTestEntity(testEntity);
                }
            }
        }

        [Fact]
        public void Role_Update_InvalidID()
        {
            using (var client = _factory.CreateClient())
            {
                HRT.Interfaces.Entities.Role testEntity = CreateTestEntity();
                try
                {
                    var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);

                    testEntity.ID = Int64.MaxValue;
                    testEntity.Name = "Name 90256dcf5d0a4a85919945732c302bbf";

                    var reqDto = RoleConvertor.Convert(testEntity, null);

                    var content = CreateContentJson(reqDto);

                    var respUpdate = client.PutAsync($"/api/v1/roles/", content);

                    Assert.Equal(HttpStatusCode.NotFound, respUpdate.Result.StatusCode);
                }
                finally
                {
                    RemoveTestEntity(testEntity);
                }
            }
        }

        #region Support methods

        protected bool RemoveTestEntity(HRT.Interfaces.Entities.Role entity)
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

        protected HRT.Interfaces.Entities.Role CreateTestEntity()
        {
            var entity = new HRT.Interfaces.Entities.Role();
            entity.Name = "Name a7ad5c2b4fc74f049716e71a45ad7f35";

            return entity;
        }

        protected HRT.Interfaces.Entities.Role AddTestEntity()
        {
            HRT.Interfaces.Entities.Role result = null;

            var entity = CreateTestEntity();

            var dal = CreateDal();
            result = dal.Insert(entity);

            return result;
        }

        private HRT.Interfaces.IRoleDal CreateDal()
        {
            var initParams = GetTestParams("DALInitParams");

            HRT.Interfaces.IRoleDal dal = new HRT.DAL.MSSQL.RoleDal();
            var dalInitParams = dal.CreateInitParams();
            dalInitParams.Parameters["ConnectionString"] = (string)initParams.Settings["ConnectionString"];
            dal.Init(dalInitParams);

            return dal;
        }
        #endregion
    }
}
