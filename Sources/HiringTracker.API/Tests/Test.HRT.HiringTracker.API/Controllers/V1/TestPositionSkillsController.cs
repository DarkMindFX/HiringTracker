


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
    public class TestPositionSkillsController : E2ETestBase, IClassFixture<WebApplicationFactory<HRT.HiringTracker.API.Startup>>
    {
        public TestPositionSkillsController(WebApplicationFactory<HRT.HiringTracker.API.Startup> factory) : base(factory)
        {
            _testParams = GetTestParams("GenericControllerTestSettings");
        }

        [Fact]
        public void PositionSkill_GetAll_Success()
        {
            using (var client = _factory.CreateClient())
            {
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);

                var respGetAll = client.GetAsync($"/api/v1/positionskills");

                Assert.Equal(HttpStatusCode.OK, respGetAll.Result.StatusCode);

                IList<PositionSkill> dtos = ExtractContentJson<List<PositionSkill>>(respGetAll.Result.Content);

                Assert.NotEmpty(dtos);
            }
        }

        [Fact]
        public void PositionSkill_Get_Success()
        {
            HRT.Interfaces.Entities.PositionSkill testEntity = AddTestEntity();
            using (var client = _factory.CreateClient())
            {
                try
                {
                    var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);

                    var paramPositionID = testEntity.PositionID;
                    var paramSkillID = testEntity.SkillID;
                    var respGet = client.GetAsync($"/api/v1/positionskills/{paramPositionID}/{paramSkillID}");

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
        public void PositionSkill_Get_InvalidID()
        {
            using (var client = _factory.CreateClient())
            {
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);

                var paramPositionID = Int64.MaxValue;
                var paramSkillID = Int64.MaxValue;

                var respGet = client.GetAsync($"/api/v1/positionskills/{paramPositionID}/{paramSkillID}");

                Assert.Equal(HttpStatusCode.NotFound, respGet.Result.StatusCode);
            }
        }

        [Fact]
        public void PositionSkill_Delete_Success()
        {
            var testEntity = AddTestEntity();
            using (var client = _factory.CreateClient())
            {
                try
                {
                    var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);

                    var paramPositionID = testEntity.PositionID;
                    var paramSkillID = testEntity.SkillID;

                    var respDel = client.DeleteAsync($"/api/v1/positionskills/{paramPositionID}/{paramSkillID}");

                    Assert.Equal(HttpStatusCode.OK, respDel.Result.StatusCode);
                }
                finally
                {
                    RemoveTestEntity(testEntity);
                }
            }
        }

        [Fact]
        public void PositionSkill_Delete_InvalidID()
        {
            using (var client = _factory.CreateClient())
            {
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);

                var paramPositionID = Int64.MaxValue;
                var paramSkillID = Int64.MaxValue;

                var respDel = client.DeleteAsync($"/api/v1/positionskills/{paramPositionID}/{paramSkillID}");

                Assert.Equal(HttpStatusCode.NotFound, respDel.Result.StatusCode);
            }
        }

        [Fact]
        public void PositionSkill_Insert_Success()
        {
            using (var client = _factory.CreateClient())
            {
                HRT.Interfaces.Entities.PositionSkill testEntity = CreateTestEntity();
                HRT.Interfaces.Entities.PositionSkill respEntity = null;
                try
                {
                    var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);

                    var reqDto = PositionSkillConvertor.Convert(testEntity, null);

                    var content = CreateContentJson(reqDto);

                    var respInsert = client.PostAsync($"/api/v1/positionskills/", content);

                    Assert.Equal(HttpStatusCode.OK, respInsert.Result.StatusCode);

                    PositionSkill respDto = ExtractContentJson<PositionSkill>(respInsert.Result.Content);

                    Assert.NotNull(respDto.PositionID);
                    Assert.NotNull(respDto.SkillID);
                    Assert.Equal(reqDto.IsMandatory, respDto.IsMandatory);
                    Assert.Equal(reqDto.SkillProficiencyID, respDto.SkillProficiencyID);

                    respEntity = PositionSkillConvertor.Convert(respDto);
                }
                finally
                {
                    RemoveTestEntity(respEntity);
                }
            }
        }

        [Fact]
        public void PositionSkill_Update_Success()
        {
            using (var client = _factory.CreateClient())
            {
                HRT.Interfaces.Entities.PositionSkill testEntity = AddTestEntity();
                try
                {
                    var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);

                    testEntity.IsMandatory = false;
                    testEntity.SkillProficiencyID = 4;

                    var reqDto = PositionSkillConvertor.Convert(testEntity, null);

                    var content = CreateContentJson(reqDto);

                    var respUpdate = client.PutAsync($"/api/v1/positionskills/", content);

                    Assert.Equal(HttpStatusCode.OK, respUpdate.Result.StatusCode);

                    PositionSkill respDto = ExtractContentJson<PositionSkill>(respUpdate.Result.Content);

                    Assert.NotNull(respDto.PositionID);
                    Assert.NotNull(respDto.SkillID);
                    Assert.Equal(reqDto.IsMandatory, respDto.IsMandatory);
                    Assert.Equal(reqDto.SkillProficiencyID, respDto.SkillProficiencyID);

                }
                finally
                {
                    RemoveTestEntity(testEntity);
                }
            }
        }

        [Fact]
        public void PositionSkill_Update_InvalidID()
        {
            using (var client = _factory.CreateClient())
            {
                HRT.Interfaces.Entities.PositionSkill testEntity = CreateTestEntity();
                try
                {
                    var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);

                    testEntity.PositionID = 100006;
                    testEntity.SkillID = 2;
                    testEntity.IsMandatory = false;
                    testEntity.SkillProficiencyID = 4;

                    var reqDto = PositionSkillConvertor.Convert(testEntity, null);

                    var content = CreateContentJson(reqDto);

                    var respUpdate = client.PutAsync($"/api/v1/positionskills/", content);

                    Assert.Equal(HttpStatusCode.NotFound, respUpdate.Result.StatusCode);
                }
                finally
                {
                    RemoveTestEntity(testEntity);
                }
            }
        }

        #region Support methods

        protected bool RemoveTestEntity(HRT.Interfaces.Entities.PositionSkill entity)
        {
            if (entity != null)
            {
                var dal = CreateDal();

                return dal.Delete(
                                        entity.PositionID,
                                        entity.SkillID
                );
            }
            else
            {
                return false;
            }
        }

        protected HRT.Interfaces.Entities.PositionSkill CreateTestEntity()
        {
            var entity = new HRT.Interfaces.Entities.PositionSkill();
            entity.PositionID = 100004;
            entity.SkillID = 16;
            entity.IsMandatory = true;
            entity.SkillProficiencyID = 2;

            return entity;
        }

        protected HRT.Interfaces.Entities.PositionSkill AddTestEntity()
        {
            HRT.Interfaces.Entities.PositionSkill result = null;

            var entity = CreateTestEntity();

            var dal = CreateDal();
            result = dal.Insert(entity);

            return result;
        }

        private HRT.Interfaces.IPositionSkillDal CreateDal()
        {
            var initParams = GetTestParams("DALInitParams");

            HRT.Interfaces.IPositionSkillDal dal = new HRT.DAL.MSSQL.PositionSkillDal();
            var dalInitParams = dal.CreateInitParams();
            dalInitParams.Parameters["ConnectionString"] = (string)initParams.Settings["ConnectionString"];
            dal.Init(dalInitParams);

            return dal;
        }
        #endregion
    }
}
