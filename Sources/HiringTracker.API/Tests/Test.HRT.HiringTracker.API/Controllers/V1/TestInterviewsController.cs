


using HRT.DTO;
using HRT.Utils.Convertors;
using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Collections.Generic;
using System.Net;
using Xunit;


namespace Test.E2E.HiringTracker.API.Controllers.V1
{
    public class TestInterviewsController : E2ETestBase, IClassFixture<WebApplicationFactory<HRT.HiringTracker.API.Startup>>
    {
        public TestInterviewsController(WebApplicationFactory<HRT.HiringTracker.API.Startup> factory) : base(factory)
        {
            _testParams = GetTestParams("InterviewsControllerTestSettings");
        }

        [Fact]
        public void Interview_GetAll_Success()
        {
            using (var client = _factory.CreateClient())
            {
                var respGetAll = client.GetAsync($"/api/v1/interviews");

                Assert.Equal(HttpStatusCode.OK, respGetAll.Result.StatusCode);

                IList<Interview> dtos = ExtractContentJson<List<Interview>>(respGetAll.Result.Content);

                Assert.NotEmpty(dtos);
            }
        }

        [Fact]
        public void Interview_Get_Success()
        {
            HRT.Interfaces.Entities.Interview testEntity = AddTestEntity();
            using (var client = _factory.CreateClient())
            {
                try
                {
                var paramID = testEntity.ID;
                    var respGet = client.GetAsync($"/api/v1/interviews/{paramID}");

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
        public void Interview_Get_InvalidID()
        {
            using (var client = _factory.CreateClient())
            {
                var paramID = Int64.MaxValue;

                var respGet = client.GetAsync($"/api/v1/interviews/{paramID}");

                Assert.Equal(HttpStatusCode.NotFound, respGet.Result.StatusCode);
            }
        }

        [Fact]
        public void Interview_Delete_Success()
        {
            var testEntity = AddTestEntity();
            using (var client = _factory.CreateClient())
            {
                try
                {
                var paramID = testEntity.ID;

                    var respDel = client.DeleteAsync($"/api/v1/interviews/{paramID}");

                    Assert.Equal(HttpStatusCode.OK, respDel.Result.StatusCode);
                }
                finally
                {
                    RemoveTestEntity(testEntity);
                }
            }
        }

        [Fact]
        public void Interview_Delete_InvalidID()
        {
            using (var client = _factory.CreateClient())
            {
                var paramID = Int64.MaxValue;

                var respDel = client.DeleteAsync($"/api/v1/interviews/{paramID}");

                Assert.Equal(HttpStatusCode.NotFound, respDel.Result.StatusCode);
            }
        }

        [Fact]
        public void Interview_Insert_Success()
        {
            using (var client = _factory.CreateClient())
            {
                HRT.Interfaces.Entities.Interview testEntity = CreateTestEntity();
                HRT.Interfaces.Entities.Interview respEntity = null;
                try
                {
                    var reqDto = InterviewConvertor.Convert(testEntity, null);

                    var content = CreateContentJson(reqDto);

                    var respInsert = client.PostAsync($"/api/v1/interviews/", content);

                    Assert.Equal(HttpStatusCode.OK, respInsert.Result.StatusCode);

                    Interview respDto = ExtractContentJson<Interview>(respInsert.Result.Content);

                                    Assert.NotNull(respDto.ID);
                                    Assert.Equal(reqDto.ProposalID, respDto.ProposalID);
                                    Assert.Equal(reqDto.InterviewTypeID, respDto.InterviewTypeID);
                                    Assert.Equal(reqDto.StartTime, respDto.StartTime);
                                    Assert.Equal(reqDto.EndTime, respDto.EndTime);
                                    Assert.Equal(reqDto.InterviewStatusID, respDto.InterviewStatusID);
                                    Assert.Equal(reqDto.CreatedByID, respDto.CreatedByID);
                                    Assert.Equal(reqDto.CretedDate, respDto.CretedDate);
                                    Assert.Equal(reqDto.ModifiedByID, respDto.ModifiedByID);
                                    Assert.Equal(reqDto.ModifiedDate, respDto.ModifiedDate);
                
                    respEntity = InterviewConvertor.Convert(respDto);
                }
                finally
                {
                    RemoveTestEntity(respEntity);
                }
            }
        }

        [Fact]
        public void Interview_Update_Success()
        {
            using (var client = _factory.CreateClient())
            {
                HRT.Interfaces.Entities.Interview testEntity = AddTestEntity();
                try
                {
                          testEntity.ProposalID = 100001;
                            testEntity.InterviewTypeID = 3;
                            testEntity.StartTime = DateTime.Parse("1/26/2020 6:16:39 AM");
                            testEntity.EndTime = DateTime.Parse("1/26/2020 6:16:39 AM");
                            testEntity.InterviewStatusID = 2;
                            testEntity.CreatedByID = 100001;
                            testEntity.CretedDate = DateTime.Parse("6/2/2019 5:52:39 AM");
                            testEntity.ModifiedByID = 100001;
                            testEntity.ModifiedDate = DateTime.Parse("7/5/2023 1:27:39 PM");
              
                    var reqDto = InterviewConvertor.Convert(testEntity, null);

                    var content = CreateContentJson(reqDto);

                    var respUpdate = client.PutAsync($"/api/v1/interviews/", content);

                    Assert.Equal(HttpStatusCode.OK, respUpdate.Result.StatusCode);

                    Interview respDto = ExtractContentJson<Interview>(respUpdate.Result.Content);

                                     Assert.NotNull(respDto.ID);
                                    Assert.Equal(reqDto.ProposalID, respDto.ProposalID);
                                    Assert.Equal(reqDto.InterviewTypeID, respDto.InterviewTypeID);
                                    Assert.Equal(reqDto.StartTime, respDto.StartTime);
                                    Assert.Equal(reqDto.EndTime, respDto.EndTime);
                                    Assert.Equal(reqDto.InterviewStatusID, respDto.InterviewStatusID);
                                    Assert.Equal(reqDto.CreatedByID, respDto.CreatedByID);
                                    Assert.Equal(reqDto.CretedDate, respDto.CretedDate);
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
        public void Interview_Update_InvalidID()
        {
            using (var client = _factory.CreateClient())
            {
                HRT.Interfaces.Entities.Interview testEntity = CreateTestEntity();
                try
                {
                             testEntity.ID = Int64.MaxValue;
                             testEntity.ProposalID = 100001;
                            testEntity.InterviewTypeID = 3;
                            testEntity.StartTime = DateTime.Parse("1/26/2020 6:16:39 AM");
                            testEntity.EndTime = DateTime.Parse("1/26/2020 6:16:39 AM");
                            testEntity.InterviewStatusID = 2;
                            testEntity.CreatedByID = 100001;
                            testEntity.CretedDate = DateTime.Parse("6/2/2019 5:52:39 AM");
                            testEntity.ModifiedByID = 100001;
                            testEntity.ModifiedDate = DateTime.Parse("7/5/2023 1:27:39 PM");
              
                    var reqDto = InterviewConvertor.Convert(testEntity, null);

                    var content = CreateContentJson(reqDto);

                    var respUpdate = client.PutAsync($"/api/v1/interviews/", content);

                    Assert.Equal(HttpStatusCode.NotFound, respUpdate.Result.StatusCode);
                }
                finally
                {
                    RemoveTestEntity(testEntity);
                }
            }
        }

        #region Support methods

        protected bool RemoveTestEntity(HRT.Interfaces.Entities.Interview entity)
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

        protected HRT.Interfaces.Entities.Interview CreateTestEntity()
        {
            var entity = new HRT.Interfaces.Entities.Interview();
                          entity.ProposalID = 100005;
                            entity.InterviewTypeID = 3;
                            entity.StartTime = DateTime.Parse("2/8/2021 10:51:39 PM");
                            entity.EndTime = DateTime.Parse("2/8/2021 10:51:39 PM");
                            entity.InterviewStatusID = 1;
                            entity.CreatedByID = 100001;
                            entity.CretedDate = DateTime.Parse("10/22/2019 10:04:39 PM");
                            entity.ModifiedByID = 33000067;
                            entity.ModifiedDate = DateTime.Parse("7/12/2021 1:40:39 AM");
              
            return entity;
        }

        protected HRT.Interfaces.Entities.Interview AddTestEntity()
        {
            HRT.Interfaces.Entities.Interview result = null;

            var entity = CreateTestEntity();

            var dal = CreateDal();
            result = dal.Insert(entity);

            return result;
        }

        private HRT.Interfaces.IInterviewDal CreateDal()
        {
            var initParams = GetTestParams("DALInitParams");

            HRT.Interfaces.IInterviewDal dal = new HRT.DAL.MSSQL.InterviewDal();
            var dalInitParams = dal.CreateInitParams();
            dalInitParams.Parameters["ConnectionString"] = (string)initParams.Settings["ConnectionString"];
            dal.Init(dalInitParams);

            return dal;
        }
        #endregion
    }
}
