


using HRT.DTO;
using HRT.Utils.Convertors;
using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Collections.Generic;
using System.Net;
using Xunit;


namespace Test.E2E.HiringTracker.API.Controllers.V1
{
    public class TestProposalsController : E2ETestBase, IClassFixture<WebApplicationFactory<HRT.HiringTracker.API.Startup>>
    {
        public TestProposalsController(WebApplicationFactory<HRT.HiringTracker.API.Startup> factory) : base(factory)
        {
            _testParams = GetTestParams("ProposalsControllerTestSettings");
        }

        [Fact]
        public void Proposal_GetAll_Success()
        {
            using (var client = _factory.CreateClient())
            {
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
                    var reqDto = ProposalConvertor.Convert(testEntity, null);

                    var content = CreateContentJson(reqDto);

                    var respInsert = client.PostAsync($"/api/v1/proposals/", content);

                    Assert.Equal(HttpStatusCode.OK, respInsert.Result.StatusCode);

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
        public void Proposal_Update_Success()
        {
            using (var client = _factory.CreateClient())
            {
                HRT.Interfaces.Entities.Proposal testEntity = AddTestEntity();
                try
                {
                          testEntity.PositionID = 100005;
                            testEntity.CandidateID = 110084;
                            testEntity.Proposed = DateTime.Parse("3/2/2024 12:08:40 AM");
                            testEntity.CurrentStepID = 4;
                            testEntity.StepSetDate = DateTime.Parse("3/2/2024 12:08:40 AM");
                            testEntity.NextStepID = 10;
                            testEntity.DueDate = DateTime.Parse("7/20/2021 9:55:40 AM");
                            testEntity.StatusID = 4;
                            testEntity.CreatedByID = 100002;
                            testEntity.CreatedDate = DateTime.Parse("3/8/2019 5:56:40 AM");
                            testEntity.ModifiedByID = 100003;
                            testEntity.ModifiedDate = DateTime.Parse("1/15/2022 6:22:40 AM");
              
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
                             testEntity.ID = Int64.MaxValue;
                             testEntity.PositionID = 100005;
                            testEntity.CandidateID = 110084;
                            testEntity.Proposed = DateTime.Parse("3/2/2024 12:08:40 AM");
                            testEntity.CurrentStepID = 4;
                            testEntity.StepSetDate = DateTime.Parse("3/2/2024 12:08:40 AM");
                            testEntity.NextStepID = 10;
                            testEntity.DueDate = DateTime.Parse("7/20/2021 9:55:40 AM");
                            testEntity.StatusID = 4;
                            testEntity.CreatedByID = 100002;
                            testEntity.CreatedDate = DateTime.Parse("3/8/2019 5:56:40 AM");
                            testEntity.ModifiedByID = 100003;
                            testEntity.ModifiedDate = DateTime.Parse("1/15/2022 6:22:40 AM");
              
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
                          entity.PositionID = 100006;
                            entity.CandidateID = 100002;
                            entity.Proposed = DateTime.Parse("1/28/2020 4:33:40 PM");
                            entity.CurrentStepID = 4;
                            entity.StepSetDate = DateTime.Parse("12/9/2022 2:20:40 AM");
                            entity.NextStepID = 6;
                            entity.DueDate = DateTime.Parse("12/9/2022 2:20:40 AM");
                            entity.StatusID = 4;
                            entity.CreatedByID = 100002;
                            entity.CreatedDate = DateTime.Parse("3/8/2023 12:34:40 PM");
                            entity.ModifiedByID = 100003;
                            entity.ModifiedDate = DateTime.Parse("7/26/2020 10:20:40 PM");
              
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
