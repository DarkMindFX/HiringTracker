


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
    public class TestInterviewFeedbacksController : E2ETestBase, IClassFixture<WebApplicationFactory<HRT.HiringTracker.API.Startup>>
    {
        public TestInterviewFeedbacksController(WebApplicationFactory<HRT.HiringTracker.API.Startup> factory) : base(factory)
        {
            _testParams = GetTestParams("GenericControllerTestSettings");
        }

        [Fact]
        public void InterviewFeedback_GetAll_Success()
        {
            using (var client = _factory.CreateClient())
            {
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);

                var respGetAll = client.GetAsync($"/api/v1/interviewfeedbacks");

                Assert.Equal(HttpStatusCode.OK, respGetAll.Result.StatusCode);

                IList<InterviewFeedback> dtos = ExtractContentJson<List<InterviewFeedback>>(respGetAll.Result.Content);

                Assert.NotEmpty(dtos);
            }
        }

        [Fact]
        public void InterviewFeedback_Get_Success()
        {
            HRT.Interfaces.Entities.InterviewFeedback testEntity = AddTestEntity();
            using (var client = _factory.CreateClient())
            {
                try
                {
                    var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);

                    var paramID = testEntity.ID;
                    var respGet = client.GetAsync($"/api/v1/interviewfeedbacks/{paramID}");

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
        public void InterviewFeedback_Get_InvalidID()
        {
            using (var client = _factory.CreateClient())
            {
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);

                var paramID = Int64.MaxValue;

                var respGet = client.GetAsync($"/api/v1/interviewfeedbacks/{paramID}");

                Assert.Equal(HttpStatusCode.NotFound, respGet.Result.StatusCode);
            }
        }

        [Fact]
        public void InterviewFeedback_Delete_Success()
        {
            var testEntity = AddTestEntity();
            using (var client = _factory.CreateClient())
            {
                try
                {
                    var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);

                    var paramID = testEntity.ID;

                    var respDel = client.DeleteAsync($"/api/v1/interviewfeedbacks/{paramID}");

                    Assert.Equal(HttpStatusCode.OK, respDel.Result.StatusCode);
                }
                finally
                {
                    RemoveTestEntity(testEntity);
                }
            }
        }

        [Fact]
        public void InterviewFeedback_Delete_InvalidID()
        {
            using (var client = _factory.CreateClient())
            {
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);

                var paramID = Int64.MaxValue;

                var respDel = client.DeleteAsync($"/api/v1/interviewfeedbacks/{paramID}");

                Assert.Equal(HttpStatusCode.NotFound, respDel.Result.StatusCode);
            }
        }

        [Fact]
        public void InterviewFeedback_Insert_Success()
        {
            using (var client = _factory.CreateClient())
            {
                HRT.Interfaces.Entities.InterviewFeedback testEntity = CreateTestEntity();
                HRT.Interfaces.Entities.InterviewFeedback respEntity = null;
                try
                {
                    var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);

                    var reqDto = InterviewFeedbackConvertor.Convert(testEntity, null);

                    var content = CreateContentJson(reqDto);

                    var respInsert = client.PostAsync($"/api/v1/interviewfeedbacks/", content);

                    Assert.Equal(HttpStatusCode.OK, respInsert.Result.StatusCode);

                    InterviewFeedback respDto = ExtractContentJson<InterviewFeedback>(respInsert.Result.Content);

                    Assert.NotNull(respDto.ID);
                    Assert.Equal(reqDto.Comment, respDto.Comment);
                    Assert.Equal(reqDto.Rating, respDto.Rating);
                    Assert.Equal(reqDto.InterviewID, respDto.InterviewID);
                    Assert.Equal(reqDto.InterviewerID, respDto.InterviewerID);
                    Assert.Equal(reqDto.CreatedByID, respDto.CreatedByID);
                    Assert.Equal(reqDto.CreatedDate, respDto.CreatedDate);
                    Assert.Equal(reqDto.ModifiedByID, respDto.ModifiedByID);
                    Assert.Equal(reqDto.ModifiedDate, respDto.ModifiedDate);

                    respEntity = InterviewFeedbackConvertor.Convert(respDto);
                }
                finally
                {
                    RemoveTestEntity(respEntity);
                }
            }
        }

        [Fact]
        public void InterviewFeedback_Update_Success()
        {
            using (var client = _factory.CreateClient())
            {
                HRT.Interfaces.Entities.InterviewFeedback testEntity = AddTestEntity();
                try
                {
                    var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);

                    testEntity.Comment = "Comment 0b2c4944c8964df984821267db4fe84e";
                    testEntity.Rating = 119;
                    testEntity.InterviewID = 100006;
                    testEntity.InterviewerID = 100002;
                    testEntity.CreatedByID = 100003;
                    testEntity.CreatedDate = DateTime.Parse("9/1/2019 2:35:27 PM");
                    testEntity.ModifiedByID = 100001;
                    testEntity.ModifiedDate = DateTime.Parse("9/1/2019 2:35:27 PM");

                    var reqDto = InterviewFeedbackConvertor.Convert(testEntity, null);

                    var content = CreateContentJson(reqDto);

                    var respUpdate = client.PutAsync($"/api/v1/interviewfeedbacks/", content);

                    Assert.Equal(HttpStatusCode.OK, respUpdate.Result.StatusCode);

                    InterviewFeedback respDto = ExtractContentJson<InterviewFeedback>(respUpdate.Result.Content);

                    Assert.NotNull(respDto.ID);
                    Assert.Equal(reqDto.Comment, respDto.Comment);
                    Assert.Equal(reqDto.Rating, respDto.Rating);
                    Assert.Equal(reqDto.InterviewID, respDto.InterviewID);
                    Assert.Equal(reqDto.InterviewerID, respDto.InterviewerID);
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
        public void InterviewFeedback_Update_InvalidID()
        {
            using (var client = _factory.CreateClient())
            {
                HRT.Interfaces.Entities.InterviewFeedback testEntity = CreateTestEntity();
                try
                {
                    var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);

                    testEntity.ID = Int64.MaxValue;
                    testEntity.Comment = "Comment 0b2c4944c8964df984821267db4fe84e";
                    testEntity.Rating = 119;
                    testEntity.InterviewID = 100006;
                    testEntity.InterviewerID = 100002;
                    testEntity.CreatedByID = 100003;
                    testEntity.CreatedDate = DateTime.Parse("9/1/2019 2:35:27 PM");
                    testEntity.ModifiedByID = 100001;
                    testEntity.ModifiedDate = DateTime.Parse("9/1/2019 2:35:27 PM");

                    var reqDto = InterviewFeedbackConvertor.Convert(testEntity, null);

                    var content = CreateContentJson(reqDto);

                    var respUpdate = client.PutAsync($"/api/v1/interviewfeedbacks/", content);

                    Assert.Equal(HttpStatusCode.NotFound, respUpdate.Result.StatusCode);
                }
                finally
                {
                    RemoveTestEntity(testEntity);
                }
            }
        }

        #region Support methods

        protected bool RemoveTestEntity(HRT.Interfaces.Entities.InterviewFeedback entity)
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

        protected HRT.Interfaces.Entities.InterviewFeedback CreateTestEntity()
        {
            var entity = new HRT.Interfaces.Entities.InterviewFeedback();
            entity.Comment = "Comment 1cb7cddc5efe414abf403ec026c921ef";
            entity.Rating = 984;
            entity.InterviewID = 100005;
            entity.InterviewerID = 100004;
            entity.CreatedByID = 100003;
            entity.CreatedDate = DateTime.Parse("12/6/2018 10:34:27 PM");
            entity.ModifiedByID = 100004;
            entity.ModifiedDate = DateTime.Parse("12/6/2018 10:34:27 PM");

            return entity;
        }

        protected HRT.Interfaces.Entities.InterviewFeedback AddTestEntity()
        {
            HRT.Interfaces.Entities.InterviewFeedback result = null;

            var entity = CreateTestEntity();

            var dal = CreateDal();
            result = dal.Insert(entity);

            return result;
        }

        private HRT.Interfaces.IInterviewFeedbackDal CreateDal()
        {
            var initParams = GetTestParams("DALInitParams");

            HRT.Interfaces.IInterviewFeedbackDal dal = new HRT.DAL.MSSQL.InterviewFeedbackDal();
            var dalInitParams = dal.CreateInitParams();
            dalInitParams.Parameters["ConnectionString"] = (string)initParams.Settings["ConnectionString"];
            dal.Init(dalInitParams);

            return dal;
        }
        #endregion
    }
}
