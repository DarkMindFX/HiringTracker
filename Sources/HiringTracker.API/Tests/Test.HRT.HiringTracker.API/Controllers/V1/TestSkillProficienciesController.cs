


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
    public class TestSkillProficienciesController : E2ETestBase, IClassFixture<WebApplicationFactory<HRT.HiringTracker.API.Startup>>
    {
        public TestSkillProficienciesController(WebApplicationFactory<HRT.HiringTracker.API.Startup> factory) : base(factory)
        {
            _testParams = GetTestParams("GenericControllerTestSettings");
        }

        [Fact]
        public void SkillProficiency_GetAll_Success()
        {
            using (var client = _factory.CreateClient())
            {
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);

                var respGetAll = client.GetAsync($"/api/v1/skillproficiencies");

                Assert.Equal(HttpStatusCode.OK, respGetAll.Result.StatusCode);

                IList<SkillProficiency> dtos = ExtractContentJson<List<SkillProficiency>>(respGetAll.Result.Content);

                Assert.NotEmpty(dtos);
            }
        }

        [Fact]
        public void SkillProficiency_Get_Success()
        {
            HRT.Interfaces.Entities.SkillProficiency testEntity = AddTestEntity();
            using (var client = _factory.CreateClient())
            {
                try
                {
                    var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);

                    var paramID = testEntity.ID;
                    var respGet = client.GetAsync($"/api/v1/skillproficiencies/{paramID}");

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
        public void SkillProficiency_Get_InvalidID()
        {
            using (var client = _factory.CreateClient())
            {
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);

                var paramID = Int64.MaxValue;

                var respGet = client.GetAsync($"/api/v1/skillproficiencies/{paramID}");

                Assert.Equal(HttpStatusCode.NotFound, respGet.Result.StatusCode);
            }
        }

        [Fact]
        public void SkillProficiency_Delete_Success()
        {
            var testEntity = AddTestEntity();
            using (var client = _factory.CreateClient())
            {
                try
                {
                    var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);

                    var paramID = testEntity.ID;

                    var respDel = client.DeleteAsync($"/api/v1/skillproficiencies/{paramID}");

                    Assert.Equal(HttpStatusCode.OK, respDel.Result.StatusCode);
                }
                finally
                {
                    RemoveTestEntity(testEntity);
                }
            }
        }

        [Fact]
        public void SkillProficiency_Delete_InvalidID()
        {
            using (var client = _factory.CreateClient())
            {
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);

                var paramID = Int64.MaxValue;

                var respDel = client.DeleteAsync($"/api/v1/skillproficiencies/{paramID}");

                Assert.Equal(HttpStatusCode.NotFound, respDel.Result.StatusCode);
            }
        }

        [Fact]
        public void SkillProficiency_Insert_Success()
        {
            using (var client = _factory.CreateClient())
            {
                HRT.Interfaces.Entities.SkillProficiency testEntity = CreateTestEntity();
                HRT.Interfaces.Entities.SkillProficiency respEntity = null;
                try
                {
                    var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);

                    var reqDto = SkillProficiencyConvertor.Convert(testEntity, null);

                    var content = CreateContentJson(reqDto);

                    var respInsert = client.PostAsync($"/api/v1/skillproficiencies/", content);

                    Assert.Equal(HttpStatusCode.Created, respInsert.Result.StatusCode);

                    SkillProficiency respDto = ExtractContentJson<SkillProficiency>(respInsert.Result.Content);

                    Assert.NotNull(respDto.ID);
                    Assert.Equal(reqDto.Name, respDto.Name);

                    respEntity = SkillProficiencyConvertor.Convert(respDto);
                }
                finally
                {
                    RemoveTestEntity(respEntity);
                }
            }
        }

        [Fact]
        public void SkillProficiency_Update_Success()
        {
            using (var client = _factory.CreateClient())
            {
                HRT.Interfaces.Entities.SkillProficiency testEntity = AddTestEntity();
                try
                {
                    var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);

                    testEntity.Name = "Name c6bb80e17d174063bd57cbf8bca392bd";

                    var reqDto = SkillProficiencyConvertor.Convert(testEntity, null);

                    var content = CreateContentJson(reqDto);

                    var respUpdate = client.PutAsync($"/api/v1/skillproficiencies/", content);

                    Assert.Equal(HttpStatusCode.OK, respUpdate.Result.StatusCode);

                    SkillProficiency respDto = ExtractContentJson<SkillProficiency>(respUpdate.Result.Content);

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
        public void SkillProficiency_Update_InvalidID()
        {
            using (var client = _factory.CreateClient())
            {
                HRT.Interfaces.Entities.SkillProficiency testEntity = CreateTestEntity();
                try
                {
                    var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);

                    testEntity.ID = 10324;
                    testEntity.Name = "Name c6bb80e17d174063bd57cbf8bca392bd";

                    var reqDto = SkillProficiencyConvertor.Convert(testEntity, null);

                    var content = CreateContentJson(reqDto);

                    var respUpdate = client.PutAsync($"/api/v1/skillproficiencies/", content);

                    Assert.Equal(HttpStatusCode.NotFound, respUpdate.Result.StatusCode);
                }
                finally
                {
                    RemoveTestEntity(testEntity);
                }
            }
        }

        #region Support methods

        protected bool RemoveTestEntity(HRT.Interfaces.Entities.SkillProficiency entity)
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

        protected HRT.Interfaces.Entities.SkillProficiency CreateTestEntity()
        {
            var entity = new HRT.Interfaces.Entities.SkillProficiency();
            entity.ID = 10324;
            entity.Name = "Name b2238e24feaf447cbca5e71c6a46ad8c";

            return entity;
        }

        protected HRT.Interfaces.Entities.SkillProficiency AddTestEntity()
        {
            HRT.Interfaces.Entities.SkillProficiency result = null;

            var entity = CreateTestEntity();

            var dal = CreateDal();
            result = dal.Insert(entity);

            return result;
        }

        private HRT.Interfaces.ISkillProficiencyDal CreateDal()
        {
            var initParams = GetTestParams("DALInitParams");

            HRT.Interfaces.ISkillProficiencyDal dal = new HRT.DAL.MSSQL.SkillProficiencyDal();
            var dalInitParams = dal.CreateInitParams();
            dalInitParams.Parameters["ConnectionString"] = (string)initParams.Settings["ConnectionString"];
            dal.Init(dalInitParams);

            return dal;
        }
        #endregion
    }
}
