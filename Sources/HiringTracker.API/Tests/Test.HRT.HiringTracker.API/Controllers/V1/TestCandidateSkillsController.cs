


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
    public class TestCandidateSkillsController : E2ETestBase, IClassFixture<WebApplicationFactory<HRT.HiringTracker.API.Startup>>
    {
        public TestCandidateSkillsController(WebApplicationFactory<HRT.HiringTracker.API.Startup> factory) : base(factory)
        {
            _testParams = GetTestParams("GenericControllerTestSettings");
        }

        [Fact]
        public void CandidateSkill_GetAll_Success()
        {
            using (var client = _factory.CreateClient())
            {
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);

                var respGetAll = client.GetAsync($"/api/v1/candidateskills");

                Assert.Equal(HttpStatusCode.OK, respGetAll.Result.StatusCode);

                IList<CandidateSkill> dtos = ExtractContentJson<List<CandidateSkill>>(respGetAll.Result.Content);

                Assert.NotEmpty(dtos);
            }
        }

        [Fact]
        public void CandidateSkill_Get_Success()
        {
            HRT.Interfaces.Entities.CandidateSkill testEntity = AddTestEntity();
            using (var client = _factory.CreateClient())
            {
                try
                {
                    var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);

                    var paramCandidateID = testEntity.CandidateID;
                    var paramSkillID = testEntity.SkillID;
                    var respGet = client.GetAsync($"/api/v1/candidateskills/{paramCandidateID}/{paramSkillID}");

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
        public void CandidateSkill_Get_InvalidID()
        {
            using (var client = _factory.CreateClient())
            {
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);

                var paramCandidateID = Int64.MaxValue;
                var paramSkillID = Int64.MaxValue;

                var respGet = client.GetAsync($"/api/v1/candidateskills/{paramCandidateID}/{paramSkillID}");

                Assert.Equal(HttpStatusCode.NotFound, respGet.Result.StatusCode);
            }
        }

        [Fact]
        public void CandidateSkill_Delete_Success()
        {
            var testEntity = AddTestEntity();
            using (var client = _factory.CreateClient())
            {
                try
                {
                    var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);

                    var paramCandidateID = testEntity.CandidateID;
                    var paramSkillID = testEntity.SkillID;

                    var respDel = client.DeleteAsync($"/api/v1/candidateskills/{paramCandidateID}/{paramSkillID}");

                    Assert.Equal(HttpStatusCode.OK, respDel.Result.StatusCode);
                }
                finally
                {
                    RemoveTestEntity(testEntity);
                }
            }
        }

        [Fact]
        public void CandidateSkill_Delete_InvalidID()
        {
            using (var client = _factory.CreateClient())
            {
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);

                var paramCandidateID = Int64.MaxValue;
                var paramSkillID = Int64.MaxValue;

                var respDel = client.DeleteAsync($"/api/v1/candidateskills/{paramCandidateID}/{paramSkillID}");

                Assert.Equal(HttpStatusCode.NotFound, respDel.Result.StatusCode);
            }
        }

        [Fact]
        public void CandidateSkill_Insert_Success()
        {
            using (var client = _factory.CreateClient())
            {
                HRT.Interfaces.Entities.CandidateSkill testEntity = CreateTestEntity();
                HRT.Interfaces.Entities.CandidateSkill respEntity = null;
                try
                {
                    var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);

                    var reqDto = CandidateSkillConvertor.Convert(testEntity, null);

                    var content = CreateContentJson(reqDto);

                    var respInsert = client.PostAsync($"/api/v1/candidateskills/", content);

                    Assert.Equal(HttpStatusCode.OK, respInsert.Result.StatusCode);

                    CandidateSkill respDto = ExtractContentJson<CandidateSkill>(respInsert.Result.Content);

                    Assert.NotNull(respDto.CandidateID);
                    Assert.NotNull(respDto.SkillID);
                    Assert.Equal(reqDto.SkillProficiencyID, respDto.SkillProficiencyID);

                    respEntity = CandidateSkillConvertor.Convert(respDto);
                }
                finally
                {
                    RemoveTestEntity(respEntity);
                }
            }
        }

        [Fact]
        public void CandidateSkill_Update_Success()
        {
            using (var client = _factory.CreateClient())
            {
                HRT.Interfaces.Entities.CandidateSkill testEntity = AddTestEntity();
                try
                {
                    var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);

                    testEntity.SkillProficiencyID = 801046;

                    var reqDto = CandidateSkillConvertor.Convert(testEntity, null);

                    var content = CreateContentJson(reqDto);

                    var respUpdate = client.PutAsync($"/api/v1/candidateskills/", content);

                    Assert.Equal(HttpStatusCode.OK, respUpdate.Result.StatusCode);

                    CandidateSkill respDto = ExtractContentJson<CandidateSkill>(respUpdate.Result.Content);

                    Assert.NotNull(respDto.CandidateID);
                    Assert.NotNull(respDto.SkillID);
                    Assert.Equal(reqDto.SkillProficiencyID, respDto.SkillProficiencyID);

                }
                finally
                {
                    RemoveTestEntity(testEntity);
                }
            }
        }

        [Fact]
        public void CandidateSkill_Update_InvalidID()
        {
            using (var client = _factory.CreateClient())
            {
                HRT.Interfaces.Entities.CandidateSkill testEntity = CreateTestEntity();
                try
                {
                    var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);

                    testEntity.CandidateID = 100003;
                    testEntity.SkillID = 6;
                    testEntity.SkillProficiencyID = 801046;

                    var reqDto = CandidateSkillConvertor.Convert(testEntity, null);

                    var content = CreateContentJson(reqDto);

                    var respUpdate = client.PutAsync($"/api/v1/candidateskills/", content);

                    Assert.Equal(HttpStatusCode.NotFound, respUpdate.Result.StatusCode);
                }
                finally
                {
                    RemoveTestEntity(testEntity);
                }
            }
        }

        #region Support methods

        protected bool RemoveTestEntity(HRT.Interfaces.Entities.CandidateSkill entity)
        {
            if (entity != null)
            {
                var dal = CreateDal();

                return dal.Delete(
                                        entity.CandidateID,
                                        entity.SkillID
                );
            }
            else
            {
                return false;
            }
        }

        protected HRT.Interfaces.Entities.CandidateSkill CreateTestEntity()
        {
            var entity = new HRT.Interfaces.Entities.CandidateSkill();
            entity.CandidateID = 100006;
            entity.SkillID = 5;
            entity.SkillProficiencyID = 3;

            return entity;
        }

        protected HRT.Interfaces.Entities.CandidateSkill AddTestEntity()
        {
            HRT.Interfaces.Entities.CandidateSkill result = null;

            var entity = CreateTestEntity();

            var dal = CreateDal();
            result = dal.Insert(entity);

            return result;
        }

        private HRT.Interfaces.ICandidateSkillDal CreateDal()
        {
            var initParams = GetTestParams("DALInitParams");

            HRT.Interfaces.ICandidateSkillDal dal = new HRT.DAL.MSSQL.CandidateSkillDal();
            var dalInitParams = dal.CreateInitParams();
            dalInitParams.Parameters["ConnectionString"] = (string)initParams.Settings["ConnectionString"];
            dal.Init(dalInitParams);

            return dal;
        }
        #endregion
    }
}
