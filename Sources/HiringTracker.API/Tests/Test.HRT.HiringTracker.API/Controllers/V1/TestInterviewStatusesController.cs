


using HRT.DTO;
using HRT.Utils.Convertors;
using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Collections.Generic;
using System.Net;
using Xunit;


namespace Test.E2E.HiringTracker.API.Controllers.V1
{
    public class TestInterviewStatusesController : E2ETestBase, IClassFixture<WebApplicationFactory<HRT.HiringTracker.API.Startup>>
    {
        public TestInterviewStatusesController(WebApplicationFactory<HRT.HiringTracker.API.Startup> factory) : base(factory)
        {
            _testParams = GetTestParams("InterviewStatusesControllerTestSettings");
        }

        [Fact]
        public void InterviewStatus_GetAll_Success()
        {
            using (var client = _factory.CreateClient())
            {
                var respGetAll = client.GetAsync($"/api/v1/interviewstatuses");

                Assert.Equal(HttpStatusCode.OK, respGetAll.Result.StatusCode);

                IList<InterviewStatus> dtos = ExtractContentJson<List<InterviewStatus>>(respGetAll.Result.Content);

                Assert.NotEmpty(dtos);
            }
        }

        [Fact]
        public void InterviewStatus_Get_Success()
        {
            HRT.Interfaces.Entities.InterviewStatus testEntity = AddTestEntity();
            using (var client = _factory.CreateClient())
            {
                try
                {
                var paramID = testEntity.ID;
                    var respGet = client.GetAsync($"/api/v1/interviewstatuses/{paramID}");

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
        public void InterviewStatus_Get_InvalidID()
        {
            using (var client = _factory.CreateClient())
            {
                var paramID = Int64.MaxValue;

                var respGet = client.GetAsync($"/api/v1/interviewstatuses/{paramID}");

                Assert.Equal(HttpStatusCode.NotFound, respGet.Result.StatusCode);
            }
        }

        [Fact]
        public void InterviewStatus_Delete_Success()
        {
            var testEntity = AddTestEntity();
            using (var client = _factory.CreateClient())
            {
                try
                {
                var paramID = testEntity.ID;

                    var respDel = client.DeleteAsync($"/api/v1/interviewstatuses/{paramID}");

                    Assert.Equal(HttpStatusCode.OK, respDel.Result.StatusCode);
                }
                finally
                {
                    RemoveTestEntity(testEntity);
                }
            }
        }

        [Fact]
        public void InterviewStatus_Delete_InvalidID()
        {
            using (var client = _factory.CreateClient())
            {
                var paramID = Int64.MaxValue;

                var respDel = client.DeleteAsync($"/api/v1/interviewstatuses/{paramID}");

                Assert.Equal(HttpStatusCode.NotFound, respDel.Result.StatusCode);
            }
        }

        [Fact]
        public void InterviewStatus_Insert_Success()
        {
            using (var client = _factory.CreateClient())
            {
                HRT.Interfaces.Entities.InterviewStatus testEntity = CreateTestEntity();
                HRT.Interfaces.Entities.InterviewStatus respEntity = null;
                try
                {
                    var reqDto = InterviewStatusConvertor.Convert(testEntity, null);

                    var content = CreateContentJson(reqDto);

                    var respInsert = client.PostAsync($"/api/v1/interviewstatuses/", content);

                    Assert.Equal(HttpStatusCode.OK, respInsert.Result.StatusCode);

                    InterviewStatus respDto = ExtractContentJson<InterviewStatus>(respInsert.Result.Content);

                                    Assert.NotNull(respDto.ID);
                                    Assert.Equal(reqDto.Name, respDto.Name);
                
                    respEntity = InterviewStatusConvertor.Convert(respDto);
                }
                finally
                {
                    RemoveTestEntity(respEntity);
                }
            }
        }

        [Fact]
        public void InterviewStatus_Update_Success()
        {
            using (var client = _factory.CreateClient())
            {
                HRT.Interfaces.Entities.InterviewStatus testEntity = AddTestEntity();
                try
                {
                          testEntity.Name = "Name f79c00444cd848c1a296e84502a91b27";
              
                    var reqDto = InterviewStatusConvertor.Convert(testEntity, null);

                    var content = CreateContentJson(reqDto);

                    var respUpdate = client.PutAsync($"/api/v1/interviewstatuses/", content);

                    Assert.Equal(HttpStatusCode.OK, respUpdate.Result.StatusCode);

                    InterviewStatus respDto = ExtractContentJson<InterviewStatus>(respUpdate.Result.Content);

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
        public void InterviewStatus_Update_InvalidID()
        {
            using (var client = _factory.CreateClient())
            {
                HRT.Interfaces.Entities.InterviewStatus testEntity = CreateTestEntity();
                try
                {
                             testEntity.ID = Int64.MaxValue;
                             testEntity.Name = "Name f79c00444cd848c1a296e84502a91b27";
              
                    var reqDto = InterviewStatusConvertor.Convert(testEntity, null);

                    var content = CreateContentJson(reqDto);

                    var respUpdate = client.PutAsync($"/api/v1/interviewstatuses/", content);

                    Assert.Equal(HttpStatusCode.NotFound, respUpdate.Result.StatusCode);
                }
                finally
                {
                    RemoveTestEntity(testEntity);
                }
            }
        }

        #region Support methods

        protected bool RemoveTestEntity(HRT.Interfaces.Entities.InterviewStatus entity)
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

        protected HRT.Interfaces.Entities.InterviewStatus CreateTestEntity()
        {
            var entity = new HRT.Interfaces.Entities.InterviewStatus();
                          entity.Name = "Name e9cbe73666354a6daa67a4ea2ac5c8d5";
              
            return entity;
        }

        protected HRT.Interfaces.Entities.InterviewStatus AddTestEntity()
        {
            HRT.Interfaces.Entities.InterviewStatus result = null;

            var entity = CreateTestEntity();

            var dal = CreateDal();
            result = dal.Insert(entity);

            return result;
        }

        private HRT.Interfaces.IInterviewStatusDal CreateDal()
        {
            var initParams = GetTestParams("DALInitParams");

            HRT.Interfaces.IInterviewStatusDal dal = new HRT.DAL.MSSQL.InterviewStatusDal();
            var dalInitParams = dal.CreateInitParams();
            dalInitParams.Parameters["ConnectionString"] = (string)initParams.Settings["ConnectionString"];
            dal.Init(dalInitParams);

            return dal;
        }
        #endregion
    }
}
