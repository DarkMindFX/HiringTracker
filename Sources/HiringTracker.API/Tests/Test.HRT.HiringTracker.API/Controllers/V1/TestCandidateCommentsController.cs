


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
    public class TestCandidateCommentsController : E2ETestBase, IClassFixture<WebApplicationFactory<HRT.HiringTracker.API.Startup>>
    {
        public TestCandidateCommentsController(WebApplicationFactory<HRT.HiringTracker.API.Startup> factory) : base(factory)
        {
            _testParams = GetTestParams("GenericControllerTestSettings");
        }

        [Fact]
        public void CandidateComment_GetAll_Success()
        {
            using (var client = _factory.CreateClient())
            {
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);

                var respGetAll = client.GetAsync($"/api/v1/candidatecomments");

                Assert.Equal(HttpStatusCode.OK, respGetAll.Result.StatusCode);

                IList<CandidateComment> dtos = ExtractContentJson<List<CandidateComment>>(respGetAll.Result.Content);

                Assert.NotEmpty(dtos);
            }
        }

        [Fact]
        public void CandidateComment_Get_Success()
        {
            HRT.Interfaces.Entities.CandidateComment testEntity = AddTestEntity();
            using (var client = _factory.CreateClient())
            {
                try
                {
                    var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);

                    var paramCandidateID = testEntity.CandidateID;
                    var paramCommentID = testEntity.CommentID;
                    var respGet = client.GetAsync($"/api/v1/candidatecomments/{paramCandidateID}/{paramCommentID}");

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
        public void CandidateComment_Get_InvalidID()
        {
            using (var client = _factory.CreateClient())
            {
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);

                var paramCandidateID = Int64.MaxValue;
                var paramCommentID = Int64.MaxValue;

                var respGet = client.GetAsync($"/api/v1/candidatecomments/{paramCandidateID}/{paramCommentID}");

                Assert.Equal(HttpStatusCode.NotFound, respGet.Result.StatusCode);
            }
        }

        [Fact]
        public void CandidateComment_Delete_Success()
        {
            var testEntity = AddTestEntity();
            using (var client = _factory.CreateClient())
            {
                try
                {
                    var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);

                    var paramCandidateID = testEntity.CandidateID;
                    var paramCommentID = testEntity.CommentID;

                    var respDel = client.DeleteAsync($"/api/v1/candidatecomments/{paramCandidateID}/{paramCommentID}");

                    Assert.Equal(HttpStatusCode.OK, respDel.Result.StatusCode);
                }
                finally
                {
                    RemoveTestEntity(testEntity);
                }
            }
        }

        [Fact]
        public void CandidateComment_Delete_InvalidID()
        {
            using (var client = _factory.CreateClient())
            {
                var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);

                var paramCandidateID = Int64.MaxValue;
                var paramCommentID = Int64.MaxValue;

                var respDel = client.DeleteAsync($"/api/v1/candidatecomments/{paramCandidateID}/{paramCommentID}");

                Assert.Equal(HttpStatusCode.NotFound, respDel.Result.StatusCode);
            }
        }

        [Fact]
        public void CandidateComment_Insert_Success()
        {
            using (var client = _factory.CreateClient())
            {
                HRT.Interfaces.Entities.CandidateComment testEntity = CreateTestEntity();
                HRT.Interfaces.Entities.CandidateComment respEntity = null;
                try
                {
                    var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);

                    var reqDto = CandidateCommentConvertor.Convert(testEntity, null);

                    var content = CreateContentJson(reqDto);

                    var respInsert = client.PostAsync($"/api/v1/candidatecomments/", content);

                    Assert.Equal(HttpStatusCode.OK, respInsert.Result.StatusCode);

                    CandidateComment respDto = ExtractContentJson<CandidateComment>(respInsert.Result.Content);

                    Assert.NotNull(respDto.CandidateID);
                    Assert.NotNull(respDto.CommentID);

                    respEntity = CandidateCommentConvertor.Convert(respDto);
                }
                finally
                {
                    RemoveTestEntity(respEntity);
                }
            }
        }

        [Fact]
        public void CandidateComment_Update_Success()
        {
            using (var client = _factory.CreateClient())
            {
                HRT.Interfaces.Entities.CandidateComment testEntity = AddTestEntity();
                try
                {
                    var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);


                    var reqDto = CandidateCommentConvertor.Convert(testEntity, null);

                    var content = CreateContentJson(reqDto);

                    var respUpdate = client.PutAsync($"/api/v1/candidatecomments/", content);

                    Assert.Equal(HttpStatusCode.OK, respUpdate.Result.StatusCode);

                    CandidateComment respDto = ExtractContentJson<CandidateComment>(respUpdate.Result.Content);

                    Assert.NotNull(respDto.CandidateID);
                    Assert.NotNull(respDto.CommentID);

                }
                finally
                {
                    RemoveTestEntity(testEntity);
                }
            }
        }

        [Fact]
        public void CandidateComment_Update_InvalidID()
        {
            using (var client = _factory.CreateClient())
            {
                HRT.Interfaces.Entities.CandidateComment testEntity = CreateTestEntity();
                try
                {
                    var respLogin = Login((string)_testParams.Settings["test_user_login"], (string)_testParams.Settings["test_user_pwd"]);

                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respLogin.Token);

                    testEntity.CandidateID = 100002;
                    testEntity.CommentID = 100003;

                    var reqDto = CandidateCommentConvertor.Convert(testEntity, null);

                    var content = CreateContentJson(reqDto);

                    var respUpdate = client.PutAsync($"/api/v1/candidatecomments/", content);

                    Assert.Equal(HttpStatusCode.NotFound, respUpdate.Result.StatusCode);
                }
                finally
                {
                    RemoveTestEntity(testEntity);
                }
            }
        }

        #region Support methods

        protected bool RemoveTestEntity(HRT.Interfaces.Entities.CandidateComment entity)
        {
            if (entity != null)
            {
                var dal = CreateDal();

                return dal.Delete(
                                        entity.CandidateID,
                                        entity.CommentID
                );
            }
            else
            {
                return false;
            }
        }

        protected HRT.Interfaces.Entities.CandidateComment CreateTestEntity()
        {
            var entity = new HRT.Interfaces.Entities.CandidateComment();
            entity.CandidateID = 100003;
            entity.CommentID = 100001;

            return entity;
        }

        protected HRT.Interfaces.Entities.CandidateComment AddTestEntity()
        {
            HRT.Interfaces.Entities.CandidateComment result = null;

            var entity = CreateTestEntity();

            var dal = CreateDal();
            result = dal.Insert(entity);

            return result;
        }

        private HRT.Interfaces.ICandidateCommentDal CreateDal()
        {
            var initParams = GetTestParams("DALInitParams");

            HRT.Interfaces.ICandidateCommentDal dal = new HRT.DAL.MSSQL.CandidateCommentDal();
            var dalInitParams = dal.CreateInitParams();
            dalInitParams.Parameters["ConnectionString"] = (string)initParams.Settings["ConnectionString"];
            dal.Init(dalInitParams);

            return dal;
        }
        #endregion
    }
}
