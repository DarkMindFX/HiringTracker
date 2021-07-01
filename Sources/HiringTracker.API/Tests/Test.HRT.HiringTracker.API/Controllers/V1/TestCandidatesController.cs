


using HRT.DTO;
using HRT.Utils.Convertors;
using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Collections.Generic;
using System.Net;
using Xunit;


namespace Test.E2E.HiringTracker.API.Controllers.V1
{
    public class TestCandidatesController : E2ETestBase, IClassFixture<WebApplicationFactory<HRT.HiringTracker.API.Startup>>
    {
        public TestCandidatesController(WebApplicationFactory<HRT.HiringTracker.API.Startup> factory) : base(factory)
        {
            _testParams = GetTestParams("CandidatesControllerTestSettings");
        }

        [Fact]
        public void Candidate_GetAll_Success()
        {
            using (var client = _factory.CreateClient())
            {
                var respGetAll = client.GetAsync($"/api/v1/candidates");

                Assert.Equal(HttpStatusCode.OK, respGetAll.Result.StatusCode);

                IList<Candidate> dtos = ExtractContentJson<List<Candidate>>(respGetAll.Result.Content);

                Assert.NotEmpty(dtos);
            }
        }

        [Fact]
        public void Candidate_Get_Success()
        {
            HRT.Interfaces.Entities.Candidate testEntity = AddTestEntity();
            using (var client = _factory.CreateClient())
            {
                try
                {
                var paramID = testEntity.ID;
                    var respGet = client.GetAsync($"/api/v1/candidates/{paramID}");

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
        public void Candidate_Get_InvalidID()
        {
            using (var client = _factory.CreateClient())
            {
                var paramID = Int64.MaxValue;

                var respGet = client.GetAsync($"/api/v1/candidates/{paramID}");

                Assert.Equal(HttpStatusCode.NotFound, respGet.Result.StatusCode);
            }
        }

        [Fact]
        public void Candidate_Delete_Success()
        {
            var testEntity = AddTestEntity();
            using (var client = _factory.CreateClient())
            {
                try
                {
                var paramID = testEntity.ID;

                    var respDel = client.DeleteAsync($"/api/v1/candidates/{paramID}");

                    Assert.Equal(HttpStatusCode.OK, respDel.Result.StatusCode);
                }
                finally
                {
                    RemoveTestEntity(testEntity);
                }
            }
        }

        [Fact]
        public void Candidate_Delete_InvalidID()
        {
            using (var client = _factory.CreateClient())
            {
                var paramID = Int64.MaxValue;

                var respDel = client.DeleteAsync($"/api/v1/candidates/{paramID}");

                Assert.Equal(HttpStatusCode.NotFound, respDel.Result.StatusCode);
            }
        }

        [Fact]
        public void Candidate_Insert_Success()
        {
            using (var client = _factory.CreateClient())
            {
                HRT.Interfaces.Entities.Candidate testEntity = CreateTestEntity();
                HRT.Interfaces.Entities.Candidate respEntity = null;
                try
                {
                    var reqDto = CandidateConvertor.Convert(testEntity, null);

                    var content = CreateContentJson(reqDto);

                    var respInsert = client.PostAsync($"/api/v1/candidates/", content);

                    Assert.Equal(HttpStatusCode.OK, respInsert.Result.StatusCode);

                    Candidate respDto = ExtractContentJson<Candidate>(respInsert.Result.Content);

                                    Assert.NotNull(respDto.ID);
                                    Assert.Equal(reqDto.FirstName, respDto.FirstName);
                                    Assert.Equal(reqDto.MiddleName, respDto.MiddleName);
                                    Assert.Equal(reqDto.LastName, respDto.LastName);
                                    Assert.Equal(reqDto.Email, respDto.Email);
                                    Assert.Equal(reqDto.Phone, respDto.Phone);
                                    Assert.Equal(reqDto.CVLink, respDto.CVLink);
                                    Assert.Equal(reqDto.CreatedByID, respDto.CreatedByID);
                                    Assert.Equal(reqDto.CreatedDate, respDto.CreatedDate);
                                    Assert.Equal(reqDto.ModifiedByID, respDto.ModifiedByID);
                                    Assert.Equal(reqDto.ModifiedDate, respDto.ModifiedDate);
                
                    respEntity = CandidateConvertor.Convert(respDto);
                }
                finally
                {
                    RemoveTestEntity(respEntity);
                }
            }
        }

        [Fact]
        public void Candidate_Update_Success()
        {
            using (var client = _factory.CreateClient())
            {
                HRT.Interfaces.Entities.Candidate testEntity = AddTestEntity();
                try
                {
                          testEntity.FirstName = "FirstName dd94ab632102434ab73a646cd1fb2ce0";
                            testEntity.MiddleName = "MiddleName dd94ab632102434ab73a646cd1fb2ce0";
                            testEntity.LastName = "LastName dd94ab632102434ab73a646cd1fb2ce0";
                            testEntity.Email = "Email dd94ab632102434ab73a646cd1fb2ce0";
                            testEntity.Phone = "Phone dd94ab632102434ab73a646cd1fb2ce0";
                            testEntity.CVLink = "CVLink dd94ab632102434ab73a646cd1fb2ce0";
                            testEntity.CreatedByID = 100002;
                            testEntity.CreatedDate = DateTime.Parse("12/4/2022 1:42:39 PM");
                            testEntity.ModifiedByID = 100002;
                            testEntity.ModifiedDate = DateTime.Parse("8/30/2023 5:43:39 AM");
              
                    var reqDto = CandidateConvertor.Convert(testEntity, null);

                    var content = CreateContentJson(reqDto);

                    var respUpdate = client.PutAsync($"/api/v1/candidates/", content);

                    Assert.Equal(HttpStatusCode.OK, respUpdate.Result.StatusCode);

                    Candidate respDto = ExtractContentJson<Candidate>(respUpdate.Result.Content);

                                     Assert.NotNull(respDto.ID);
                                    Assert.Equal(reqDto.FirstName, respDto.FirstName);
                                    Assert.Equal(reqDto.MiddleName, respDto.MiddleName);
                                    Assert.Equal(reqDto.LastName, respDto.LastName);
                                    Assert.Equal(reqDto.Email, respDto.Email);
                                    Assert.Equal(reqDto.Phone, respDto.Phone);
                                    Assert.Equal(reqDto.CVLink, respDto.CVLink);
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
        public void Candidate_Update_InvalidID()
        {
            using (var client = _factory.CreateClient())
            {
                HRT.Interfaces.Entities.Candidate testEntity = CreateTestEntity();
                try
                {
                             testEntity.ID = Int64.MaxValue;
                             testEntity.FirstName = "FirstName dd94ab632102434ab73a646cd1fb2ce0";
                            testEntity.MiddleName = "MiddleName dd94ab632102434ab73a646cd1fb2ce0";
                            testEntity.LastName = "LastName dd94ab632102434ab73a646cd1fb2ce0";
                            testEntity.Email = "Email dd94ab632102434ab73a646cd1fb2ce0";
                            testEntity.Phone = "Phone dd94ab632102434ab73a646cd1fb2ce0";
                            testEntity.CVLink = "CVLink dd94ab632102434ab73a646cd1fb2ce0";
                            testEntity.CreatedByID = 100002;
                            testEntity.CreatedDate = DateTime.Parse("12/4/2022 1:42:39 PM");
                            testEntity.ModifiedByID = 100002;
                            testEntity.ModifiedDate = DateTime.Parse("8/30/2023 5:43:39 AM");
              
                    var reqDto = CandidateConvertor.Convert(testEntity, null);

                    var content = CreateContentJson(reqDto);

                    var respUpdate = client.PutAsync($"/api/v1/candidates/", content);

                    Assert.Equal(HttpStatusCode.NotFound, respUpdate.Result.StatusCode);
                }
                finally
                {
                    RemoveTestEntity(testEntity);
                }
            }
        }

        #region Support methods

        protected bool RemoveTestEntity(HRT.Interfaces.Entities.Candidate entity)
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

        protected HRT.Interfaces.Entities.Candidate CreateTestEntity()
        {
            var entity = new HRT.Interfaces.Entities.Candidate();
                          entity.FirstName = "FirstName bee98cb1d9ee4b13b02f5c4f804ff2e8";
                            entity.MiddleName = "MiddleName bee98cb1d9ee4b13b02f5c4f804ff2e8";
                            entity.LastName = "LastName bee98cb1d9ee4b13b02f5c4f804ff2e8";
                            entity.Email = "Email bee98cb1d9ee4b13b02f5c4f804ff2e8";
                            entity.Phone = "Phone bee98cb1d9ee4b13b02f5c4f804ff2e8";
                            entity.CVLink = "CVLink bee98cb1d9ee4b13b02f5c4f804ff2e8";
                            entity.CreatedByID = 100002;
                            entity.CreatedDate = DateTime.Parse("8/10/2021 2:56:39 PM");
                            entity.ModifiedByID = 100001;
                            entity.ModifiedDate = DateTime.Parse("9/24/2019 4:44:39 PM");
              
            return entity;
        }

        protected HRT.Interfaces.Entities.Candidate AddTestEntity()
        {
            HRT.Interfaces.Entities.Candidate result = null;

            var entity = CreateTestEntity();

            var dal = CreateDal();
            result = dal.Insert(entity);

            return result;
        }

        private HRT.Interfaces.ICandidateDal CreateDal()
        {
            var initParams = GetTestParams("DALInitParams");

            HRT.Interfaces.ICandidateDal dal = new HRT.DAL.MSSQL.CandidateDal();
            var dalInitParams = dal.CreateInitParams();
            dalInitParams.Parameters["ConnectionString"] = (string)initParams.Settings["ConnectionString"];
            dal.Init(dalInitParams);

            return dal;
        }
        #endregion
    }
}
