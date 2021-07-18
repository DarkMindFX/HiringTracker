


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
    public class TestCandidatesController : E2ETestBase, IClassFixture<WebApplicationFactory<HRT.HiringTracker.API.Startup>>
    {
        public TestCandidatesController(WebApplicationFactory<HRT.HiringTracker.API.Startup> factory) : base(factory)
        {
            _testParams = GetTestParams("GenericControllerTestSettings");
        }

        [Fact]
        public void Candidate_GetAll_Success()
        {
            using (var client = _factory.CreateClient())
            {
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);

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
                    var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);

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
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);

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
                    var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);

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
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);

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
                    var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);

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
                    var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);

                    testEntity.FirstName = "FirstName 39884d612bb24869b1904a3c4a9a9e31";
                    testEntity.MiddleName = "MiddleName 39884d612bb24869b1904a3c4a9a9e31";
                    testEntity.LastName = "LastName 39884d612bb24869b1904a3c4a9a9e31";
                    testEntity.Email = "Email 39884d612bb24869b1904a3c4a9a9e31";
                    testEntity.Phone = "Phone 39884d612bb24869b1904a3c4a9a9e31";
                    testEntity.CVLink = "CVLink 39884d612bb24869b1904a3c4a9a9e31";
                    testEntity.CreatedByID = 100002;
                    testEntity.CreatedDate = DateTime.Parse("3/13/2022 12:31:36 PM");
                    testEntity.ModifiedByID = 100003;
                    testEntity.ModifiedDate = DateTime.Parse("12/8/2022 1:52:36 PM");

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
                    var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);

                    testEntity.ID = Int64.MaxValue;
                    testEntity.FirstName = "FirstName 39884d612bb24869b1904a3c4a9a9e31";
                    testEntity.MiddleName = "MiddleName 39884d612bb24869b1904a3c4a9a9e31";
                    testEntity.LastName = "LastName 39884d612bb24869b1904a3c4a9a9e31";
                    testEntity.Email = "Email 39884d612bb24869b1904a3c4a9a9e31";
                    testEntity.Phone = "Phone 39884d612bb24869b1904a3c4a9a9e31";
                    testEntity.CVLink = "CVLink 39884d612bb24869b1904a3c4a9a9e31";
                    testEntity.CreatedByID = 100002;
                    testEntity.CreatedDate = DateTime.Parse("3/13/2022 12:31:36 PM");
                    testEntity.ModifiedByID = 100003;
                    testEntity.ModifiedDate = DateTime.Parse("12/8/2022 1:52:36 PM");

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
            entity.FirstName = "FirstName 5808bc09063145e386ab1ee36f418fba";
            entity.MiddleName = "MiddleName 5808bc09063145e386ab1ee36f418fba";
            entity.LastName = "LastName 5808bc09063145e386ab1ee36f418fba";
            entity.Email = "Email 5808bc09063145e386ab1ee36f418fba";
            entity.Phone = "Phone 5808bc09063145e386ab1ee36f418fba";
            entity.CVLink = "CVLink 5808bc09063145e386ab1ee36f418fba";
            entity.CreatedByID = 100002;
            entity.CreatedDate = DateTime.Parse("7/20/2020 11:54:36 AM");
            entity.ModifiedByID = 100001;
            entity.ModifiedDate = DateTime.Parse("5/30/2019 5:43:36 AM");

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
