


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
    public class TestProposalsController : E2ETestBase, IClassFixture<WebApplicationFactory<HRT.HiringTracker.API.Startup>>
    {
        public TestProposalsController(WebApplicationFactory<HRT.HiringTracker.API.Startup> factory) : base(factory)
        {
            _testParams = GetTestParams("GenericControllerTestSettings");
        }

        [Fact]
        public void Proposal_GetAll_Success()
        {
            using (var client = _factory.CreateClient())
            {
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);

                var respGetAll = client.GetAsync($"/api/v1/proposals");

                Assert.Equal(HttpStatusCode.OK, respGetAll.Result.StatusCode);

                IList<Proposal> dtos = ExtractContentJson<List<Proposal>>(respGetAll.Result.Content);

                Assert.NotEmpty(dtos);
            }
        }

        [Fact]
        public void Proposal_Get_Success()
        {
            HRT.Interfaces.Entities.Proposal testEntity = AddTestEntity();
            using (var client = _factory.CreateClient())
            {
                try
                {
                    var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);

                    var paramID = testEntity.ID;
                    var respGet = client.GetAsync($"/api/v1/proposals/{paramID}");

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
        public void Proposal_Get_InvalidID()
        {
            using (var client = _factory.CreateClient())
            {
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);

                var paramID = Int64.MaxValue;

                var respGet = client.GetAsync($"/api/v1/proposals/{paramID}");

                Assert.Equal(HttpStatusCode.NotFound, respGet.Result.StatusCode);
            }
        }

        [Fact]
        public void Proposal_Delete_Success()
        {
            var testEntity = AddTestEntity();
            using (var client = _factory.CreateClient())
            {
                try
                {
                    var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);

                    var paramID = testEntity.ID;

                    var respDel = client.DeleteAsync($"/api/v1/proposals/{paramID}");

                    Assert.Equal(HttpStatusCode.OK, respDel.Result.StatusCode);
                }
                finally
                {
                    RemoveTestEntity(testEntity);
                }
            }
        }

        [Fact]
        public void Proposal_Delete_InvalidID()
        {
            using (var client = _factory.CreateClient())
            {
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);

                var paramID = Int64.MaxValue;

                var respDel = client.DeleteAsync($"/api/v1/proposals/{paramID}");

                Assert.Equal(HttpStatusCode.NotFound, respDel.Result.StatusCode);
            }
        }

        [Fact]
        public void Proposal_Insert_Success()
        {
            using (var client = _factory.CreateClient())
            {
                HRT.Interfaces.Entities.Proposal testEntity = CreateTestEntity();
                HRT.Interfaces.Entities.Proposal respEntity = null;
                try
                {
                    var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);

                    var reqDto = ProposalConvertor.Convert(testEntity, null);

                    var content = CreateContentJson(reqDto);

                    var respInsert = client.PostAsync($"/api/v1/proposals/", content);

                    Assert.Equal(HttpStatusCode.Created, respInsert.Result.StatusCode);

                    Proposal respDto = ExtractContentJson<Proposal>(respInsert.Result.Content);

                    Assert.NotNull(respDto.ID);
                    Assert.Equal(reqDto.PositionID, respDto.PositionID);
                    Assert.Equal(reqDto.CandidateID, respDto.CandidateID);
                    Assert.Equal(reqDto.Proposed, respDto.Proposed);
                    Assert.Equal(reqDto.CurrentStepID, respDto.CurrentStepID);
                    Assert.Equal(reqDto.StepSetDate, respDto.StepSetDate);
                    Assert.Equal(reqDto.NextStepID, respDto.NextStepID);
                    Assert.Equal(reqDto.DueDate, respDto.DueDate);
                    Assert.Equal(reqDto.StatusID, respDto.StatusID);
                    Assert.Equal(reqDto.CreatedByID, respDto.CreatedByID);
                    Assert.Equal(reqDto.CreatedDate, respDto.CreatedDate);
                    Assert.Equal(reqDto.ModifiedByID, respDto.ModifiedByID);
                    Assert.Equal(reqDto.ModifiedDate, respDto.ModifiedDate);

                    respEntity = ProposalConvertor.Convert(respDto);
                }
                finally
                {
                    RemoveTestEntity(respEntity);
                }
            }
        }

        [Fact]
        public void Proposal_Insert_AlreadyProposed()
        {
            using (var client = _factory.CreateClient())
            {
                HRT.Interfaces.Entities.Proposal testEntity = AddTestEntity();

                try
                {
                    var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);

                    var reqDto = ProposalConvertor.Convert(testEntity, null);

                    var content = CreateContentJson(reqDto);

                    var respInsert = client.PostAsync($"/api/v1/proposals/", content);

                    Assert.Equal(HttpStatusCode.Conflict, respInsert.Result.StatusCode);

                    Error respDto = ExtractContentJson<Error>(respInsert.Result.Content);

                    Assert.NotNull(respDto);
                    Assert.NotNull(respDto.Message);
                    Assert.Equal((int)HttpStatusCode.Conflict, respDto.Code);
                }
                finally
                {
                    RemoveTestEntity(testEntity);
                }
            }
        }

        [Fact]
        public void Proposal_Update_Success()
        {
            using (var client = _factory.CreateClient())
            {
                HRT.Interfaces.Entities.Proposal testEntity = AddTestEntity();
                try
                {
                    var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);

                    testEntity.PositionID = 100004;
                    testEntity.CandidateID = 100005;
                    testEntity.Proposed = DateTime.Parse("4/8/2021 10:08:37 PM");
                    testEntity.CurrentStepID = 1;
                    testEntity.StepSetDate = DateTime.Parse("2/18/2024 7:55:37 AM");
                    testEntity.NextStepID = 7;
                    testEntity.DueDate = DateTime.Parse("7/7/2021 8:22:37 AM");
                    testEntity.StatusID = 23;
                    testEntity.CreatedByID = 100004;
                    testEntity.CreatedDate = DateTime.Parse("10/6/2021 3:56:37 AM");
                    testEntity.ModifiedByID = 100002;
                    testEntity.ModifiedDate = DateTime.Parse("2/23/2019 1:43:37 PM");

                    var reqDto = ProposalConvertor.Convert(testEntity, null);

                    var content = CreateContentJson(reqDto);

                    var respUpdate = client.PutAsync($"/api/v1/proposals/", content);

                    Assert.Equal(HttpStatusCode.OK, respUpdate.Result.StatusCode);

                    Proposal respDto = ExtractContentJson<Proposal>(respUpdate.Result.Content);

                    Assert.NotNull(respDto.ID);
                    Assert.Equal(reqDto.PositionID, respDto.PositionID);
                    Assert.Equal(reqDto.CandidateID, respDto.CandidateID);
                    Assert.Equal(reqDto.Proposed, respDto.Proposed);
                    Assert.Equal(reqDto.CurrentStepID, respDto.CurrentStepID);
                    Assert.Equal(reqDto.StepSetDate, respDto.StepSetDate);
                    Assert.Equal(reqDto.NextStepID, respDto.NextStepID);
                    Assert.Equal(reqDto.DueDate, respDto.DueDate);
                    Assert.Equal(reqDto.StatusID, respDto.StatusID);
                    Assert.Equal(reqDto.CreatedByID, respDto.CreatedByID);
                    Assert.Equal(reqDto.CreatedDate, respDto.CreatedDate);
                    Assert.Equal(reqDto.ModifiedByID, respDto.ModifiedByID);
                    Assert.Equal(reqDto.ModifiedDate, respDto.ModifiedDate);

                }
                finally
                {
                    RemoveTestEntity(testEntity);
                }
            }
        }

        [Fact]
        public void Proposal_Update_InvalidID()
        {
            using (var client = _factory.CreateClient())
            {
                HRT.Interfaces.Entities.Proposal testEntity = CreateTestEntity();
                try
                {
                    var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);

                    testEntity.ID = Int64.MaxValue;
                    testEntity.PositionID = 100004;
                    testEntity.CandidateID = 100005;
                    testEntity.Proposed = DateTime.Parse("4/8/2021 10:08:37 PM");
                    testEntity.CurrentStepID = 1;
                    testEntity.StepSetDate = DateTime.Parse("2/18/2024 7:55:37 AM");
                    testEntity.NextStepID = 7;
                    testEntity.DueDate = DateTime.Parse("7/7/2021 8:22:37 AM");
                    testEntity.StatusID = 23;
                    testEntity.CreatedByID = 100004;
                    testEntity.CreatedDate = DateTime.Parse("10/6/2021 3:56:37 AM");
                    testEntity.ModifiedByID = 100002;
                    testEntity.ModifiedDate = DateTime.Parse("2/23/2019 1:43:37 PM");

                    var reqDto = ProposalConvertor.Convert(testEntity, null);

                    var content = CreateContentJson(reqDto);

                    var respUpdate = client.PutAsync($"/api/v1/proposals/", content);

                    Assert.Equal(HttpStatusCode.NotFound, respUpdate.Result.StatusCode);
                }
                finally
                {
                    RemoveTestEntity(testEntity);
                }
            }
        }

        #region Support methods

        protected bool RemoveTestEntity(HRT.Interfaces.Entities.Proposal entity)
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

        protected HRT.Interfaces.Entities.Proposal CreateTestEntity()
        {
            var entity = new HRT.Interfaces.Entities.Proposal();
            entity.PositionID = 100001;
            entity.CandidateID = 100002;
            entity.Proposed = DateTime.Parse("7/20/2019 6:33:37 PM");
            entity.CurrentStepID = 8;
            entity.StepSetDate = DateTime.Parse("5/31/2022 4:20:37 AM");
            entity.NextStepID = 9;
            entity.DueDate = DateTime.Parse("10/19/2019 2:06:37 PM");
            entity.StatusID = 2;
            entity.CreatedByID = 100004;
            entity.CreatedDate = DateTime.Parse("8/28/2022 2:33:37 PM");
            entity.ModifiedByID = 100002;
            entity.ModifiedDate = DateTime.Parse("1/17/2020 12:20:37 AM");

            return entity;
        }

        protected HRT.Interfaces.Entities.Proposal AddTestEntity()
        {
            HRT.Interfaces.Entities.Proposal result = null;

            var entity = CreateTestEntity();

            var dal = CreateDal();
            result = dal.Insert(entity);

            return result;
        }

        private HRT.Interfaces.IProposalDal CreateDal()
        {
            var initParams = GetTestParams("DALInitParams");

            HRT.Interfaces.IProposalDal dal = new HRT.DAL.MSSQL.ProposalDal();
            var dalInitParams = dal.CreateInitParams();
            dalInitParams.Parameters["ConnectionString"] = (string)initParams.Settings["ConnectionString"];
            dal.Init(dalInitParams);

            return dal;
        }
        #endregion
    }
}
