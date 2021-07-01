


using HRT.DTO;
using HRT.Utils.Convertors;
using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Collections.Generic;
using System.Net;
using Xunit;


namespace Test.E2E.HiringTracker.API.Controllers.V1
{
    public class TestCandidatePropertiesController : E2ETestBase, IClassFixture<WebApplicationFactory<HRT.HiringTracker.API.Startup>>
    {
        public TestCandidatePropertiesController(WebApplicationFactory<HRT.HiringTracker.API.Startup> factory) : base(factory)
        {
            _testParams = GetTestParams("CandidatePropertiesControllerTestSettings");
        }

        [Fact]
        public void CandidateProperty_GetAll_Success()
        {
            using (var client = _factory.CreateClient())
            {
                var respGetAll = client.GetAsync($"/api/v1/candidateproperties");

                Assert.Equal(HttpStatusCode.OK, respGetAll.Result.StatusCode);

                IList<CandidateProperty> dtos = ExtractContentJson<List<CandidateProperty>>(respGetAll.Result.Content);

                Assert.NotEmpty(dtos);
            }
        }

        [Fact]
        public void CandidateProperty_Get_Success()
        {
            HRT.Interfaces.Entities.CandidateProperty testEntity = AddTestEntity();
            using (var client = _factory.CreateClient())
            {
                try
                {
                var paramID = testEntity.ID;
                    var respGet = client.GetAsync($"/api/v1/candidateproperties/{paramID}");

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
        public void CandidateProperty_Get_InvalidID()
        {
            using (var client = _factory.CreateClient())
            {
                var paramID = Int64.MaxValue;

                var respGet = client.GetAsync($"/api/v1/candidateproperties/{paramID}");

                Assert.Equal(HttpStatusCode.NotFound, respGet.Result.StatusCode);
            }
        }

        [Fact]
        public void CandidateProperty_Delete_Success()
        {
            var testEntity = AddTestEntity();
            using (var client = _factory.CreateClient())
            {
                try
                {
                var paramID = testEntity.ID;

                    var respDel = client.DeleteAsync($"/api/v1/candidateproperties/{paramID}");

                    Assert.Equal(HttpStatusCode.OK, respDel.Result.StatusCode);
                }
                finally
                {
                    RemoveTestEntity(testEntity);
                }
            }
        }

        [Fact]
        public void CandidateProperty_Delete_InvalidID()
        {
            using (var client = _factory.CreateClient())
            {
                var paramID = Int64.MaxValue;

                var respDel = client.DeleteAsync($"/api/v1/candidateproperties/{paramID}");

                Assert.Equal(HttpStatusCode.NotFound, respDel.Result.StatusCode);
            }
        }

        [Fact]
        public void CandidateProperty_Insert_Success()
        {
            using (var client = _factory.CreateClient())
            {
                HRT.Interfaces.Entities.CandidateProperty testEntity = CreateTestEntity();
                HRT.Interfaces.Entities.CandidateProperty respEntity = null;
                try
                {
                    var reqDto = CandidatePropertyConvertor.Convert(testEntity, null);

                    var content = CreateContentJson(reqDto);

                    var respInsert = client.PostAsync($"/api/v1/candidateproperties/", content);

                    Assert.Equal(HttpStatusCode.OK, respInsert.Result.StatusCode);

                    CandidateProperty respDto = ExtractContentJson<CandidateProperty>(respInsert.Result.Content);

                                    Assert.NotNull(respDto.ID);
                                    Assert.Equal(reqDto.Name, respDto.Name);
                                    Assert.Equal(reqDto.Value, respDto.Value);
                                    Assert.Equal(reqDto.CandidateID, respDto.CandidateID);
                
                    respEntity = CandidatePropertyConvertor.Convert(respDto);
                }
                finally
                {
                    RemoveTestEntity(respEntity);
                }
            }
        }

        [Fact]
        public void CandidateProperty_Update_Success()
        {
            using (var client = _factory.CreateClient())
            {
                HRT.Interfaces.Entities.CandidateProperty testEntity = AddTestEntity();
                try
                {
                          testEntity.Name = "Name 5d01f60429c44d25937d67b05996208a";
                            testEntity.Value = "Value 5d01f60429c44d25937d67b05996208a";
                            testEntity.CandidateID = 100005;
              
                    var reqDto = CandidatePropertyConvertor.Convert(testEntity, null);

                    var content = CreateContentJson(reqDto);

                    var respUpdate = client.PutAsync($"/api/v1/candidateproperties/", content);

                    Assert.Equal(HttpStatusCode.OK, respUpdate.Result.StatusCode);

                    CandidateProperty respDto = ExtractContentJson<CandidateProperty>(respUpdate.Result.Content);

                                     Assert.NotNull(respDto.ID);
                                    Assert.Equal(reqDto.Name, respDto.Name);
                                    Assert.Equal(reqDto.Value, respDto.Value);
                                    Assert.Equal(reqDto.CandidateID, respDto.CandidateID);
                
                }
                finally
                {
                    RemoveTestEntity(testEntity);
                }
            }
        }

        [Fact]
        public void CandidateProperty_Update_InvalidID()
        {
            using (var client = _factory.CreateClient())
            {
                HRT.Interfaces.Entities.CandidateProperty testEntity = CreateTestEntity();
                try
                {
                             testEntity.ID = Int64.MaxValue;
                             testEntity.Name = "Name 5d01f60429c44d25937d67b05996208a";
                            testEntity.Value = "Value 5d01f60429c44d25937d67b05996208a";
                            testEntity.CandidateID = 100005;
              
                    var reqDto = CandidatePropertyConvertor.Convert(testEntity, null);

                    var content = CreateContentJson(reqDto);

                    var respUpdate = client.PutAsync($"/api/v1/candidateproperties/", content);

                    Assert.Equal(HttpStatusCode.NotFound, respUpdate.Result.StatusCode);
                }
                finally
                {
                    RemoveTestEntity(testEntity);
                }
            }
        }

        #region Support methods

        protected bool RemoveTestEntity(HRT.Interfaces.Entities.CandidateProperty entity)
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

        protected HRT.Interfaces.Entities.CandidateProperty CreateTestEntity()
        {
            var entity = new HRT.Interfaces.Entities.CandidateProperty();
                          entity.Name = "Name 63e3abd9466c400cbbcb9e85ee7d0923";
                            entity.Value = "Value 63e3abd9466c400cbbcb9e85ee7d0923";
                            entity.CandidateID = 110084;
              
            return entity;
        }

        protected HRT.Interfaces.Entities.CandidateProperty AddTestEntity()
        {
            HRT.Interfaces.Entities.CandidateProperty result = null;

            var entity = CreateTestEntity();

            var dal = CreateDal();
            result = dal.Insert(entity);

            return result;
        }

        private HRT.Interfaces.ICandidatePropertyDal CreateDal()
        {
            var initParams = GetTestParams("DALInitParams");

            HRT.Interfaces.ICandidatePropertyDal dal = new HRT.DAL.MSSQL.CandidatePropertyDal();
            var dalInitParams = dal.CreateInitParams();
            dalInitParams.Parameters["ConnectionString"] = (string)initParams.Settings["ConnectionString"];
            dal.Init(dalInitParams);

            return dal;
        }
        #endregion
    }
}
