


using HRT.DTO;
using HRT.Utils.Convertors;
using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Collections.Generic;
using System.Net;
using Xunit;


namespace Test.E2E.HiringTracker.API.Controllers.V1
{
    public class TestProposalStepsController : E2ETestBase, IClassFixture<WebApplicationFactory<HRT.HiringTracker.API.Startup>>
    {
        public TestProposalStepsController(WebApplicationFactory<HRT.HiringTracker.API.Startup> factory) : base(factory)
        {
            _testParams = GetTestParams("ProposalStepsControllerTestSettings");
        }

        [Fact]
        public void ProposalStep_GetAll_Success()
        {
            using (var client = _factory.CreateClient())
            {
                var respGetAll = client.GetAsync($"/api/v1/proposalsteps");

                Assert.Equal(HttpStatusCode.OK, respGetAll.Result.StatusCode);

                IList<ProposalStep> dtos = ExtractContentJson<List<ProposalStep>>(respGetAll.Result.Content);

                Assert.NotEmpty(dtos);
            }
        }

        [Fact]
        public void ProposalStep_Get_Success()
        {
            HRT.Interfaces.Entities.ProposalStep testEntity = AddTestEntity();
            using (var client = _factory.CreateClient())
            {
                try
                {
                var paramID = testEntity.ID;
                    var respGet = client.GetAsync($"/api/v1/proposalsteps/{paramID}");

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
        public void ProposalStep_Get_InvalidID()
        {
            using (var client = _factory.CreateClient())
            {
                var paramID = Int64.MaxValue;

                var respGet = client.GetAsync($"/api/v1/proposalsteps/{paramID}");

                Assert.Equal(HttpStatusCode.NotFound, respGet.Result.StatusCode);
            }
        }

        [Fact]
        public void ProposalStep_Delete_Success()
        {
            var testEntity = AddTestEntity();
            using (var client = _factory.CreateClient())
            {
                try
                {
                var paramID = testEntity.ID;

                    var respDel = client.DeleteAsync($"/api/v1/proposalsteps/{paramID}");

                    Assert.Equal(HttpStatusCode.OK, respDel.Result.StatusCode);
                }
                finally
                {
                    RemoveTestEntity(testEntity);
                }
            }
        }

        [Fact]
        public void ProposalStep_Delete_InvalidID()
        {
            using (var client = _factory.CreateClient())
            {
                var paramID = Int64.MaxValue;

                var respDel = client.DeleteAsync($"/api/v1/proposalsteps/{paramID}");

                Assert.Equal(HttpStatusCode.NotFound, respDel.Result.StatusCode);
            }
        }

        [Fact]
        public void ProposalStep_Insert_Success()
        {
            using (var client = _factory.CreateClient())
            {
                HRT.Interfaces.Entities.ProposalStep testEntity = CreateTestEntity();
                HRT.Interfaces.Entities.ProposalStep respEntity = null;
                try
                {
                    var reqDto = ProposalStepConvertor.Convert(testEntity, null);

                    var content = CreateContentJson(reqDto);

                    var respInsert = client.PostAsync($"/api/v1/proposalsteps/", content);

                    Assert.Equal(HttpStatusCode.OK, respInsert.Result.StatusCode);

                    ProposalStep respDto = ExtractContentJson<ProposalStep>(respInsert.Result.Content);

                                    Assert.NotNull(respDto.ID);
                                    Assert.Equal(reqDto.Name, respDto.Name);
                                    Assert.Equal(reqDto.ReqDueDate, respDto.ReqDueDate);
                                    Assert.Equal(reqDto.RequiresRespInDays, respDto.RequiresRespInDays);
                
                    respEntity = ProposalStepConvertor.Convert(respDto);
                }
                finally
                {
                    RemoveTestEntity(respEntity);
                }
            }
        }

        [Fact]
        public void ProposalStep_Update_Success()
        {
            using (var client = _factory.CreateClient())
            {
                HRT.Interfaces.Entities.ProposalStep testEntity = AddTestEntity();
                try
                {
                          testEntity.Name = "Name 4091cff2d89e420fae27a4e0901246da";
                            testEntity.ReqDueDate = false;              
                            testEntity.RequiresRespInDays = 398;
              
                    var reqDto = ProposalStepConvertor.Convert(testEntity, null);

                    var content = CreateContentJson(reqDto);

                    var respUpdate = client.PutAsync($"/api/v1/proposalsteps/", content);

                    Assert.Equal(HttpStatusCode.OK, respUpdate.Result.StatusCode);

                    ProposalStep respDto = ExtractContentJson<ProposalStep>(respUpdate.Result.Content);

                                     Assert.NotNull(respDto.ID);
                                    Assert.Equal(reqDto.Name, respDto.Name);
                                    Assert.Equal(reqDto.ReqDueDate, respDto.ReqDueDate);
                                    Assert.Equal(reqDto.RequiresRespInDays, respDto.RequiresRespInDays);
                
                }
                finally
                {
                    RemoveTestEntity(testEntity);
                }
            }
        }

        [Fact]
        public void ProposalStep_Update_InvalidID()
        {
            using (var client = _factory.CreateClient())
            {
                HRT.Interfaces.Entities.ProposalStep testEntity = CreateTestEntity();
                try
                {
                             testEntity.ID = Int64.MaxValue;
                             testEntity.Name = "Name 4091cff2d89e420fae27a4e0901246da";
                            testEntity.ReqDueDate = false;              
                            testEntity.RequiresRespInDays = 398;
              
                    var reqDto = ProposalStepConvertor.Convert(testEntity, null);

                    var content = CreateContentJson(reqDto);

                    var respUpdate = client.PutAsync($"/api/v1/proposalsteps/", content);

                    Assert.Equal(HttpStatusCode.NotFound, respUpdate.Result.StatusCode);
                }
                finally
                {
                    RemoveTestEntity(testEntity);
                }
            }
        }

        #region Support methods

        protected bool RemoveTestEntity(HRT.Interfaces.Entities.ProposalStep entity)
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

        protected HRT.Interfaces.Entities.ProposalStep CreateTestEntity()
        {
            var entity = new HRT.Interfaces.Entities.ProposalStep();
                          entity.Name = "Name 1843961bf2ae45bea9188d8e4e1d24a3";
                            entity.ReqDueDate = true;              
                            entity.RequiresRespInDays = 875;
              
            return entity;
        }

        protected HRT.Interfaces.Entities.ProposalStep AddTestEntity()
        {
            HRT.Interfaces.Entities.ProposalStep result = null;

            var entity = CreateTestEntity();

            var dal = CreateDal();
            result = dal.Insert(entity);

            return result;
        }

        private HRT.Interfaces.IProposalStepDal CreateDal()
        {
            var initParams = GetTestParams("DALInitParams");

            HRT.Interfaces.IProposalStepDal dal = new HRT.DAL.MSSQL.ProposalStepDal();
            var dalInitParams = dal.CreateInitParams();
            dalInitParams.Parameters["ConnectionString"] = (string)initParams.Settings["ConnectionString"];
            dal.Init(dalInitParams);

            return dal;
        }
        #endregion
    }
}
