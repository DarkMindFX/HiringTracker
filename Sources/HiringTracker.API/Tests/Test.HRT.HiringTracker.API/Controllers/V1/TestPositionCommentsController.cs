


using HRT.DTO;
using HRT.Utils.Convertors;
using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Collections.Generic;
using System.Net;
using Xunit;


namespace Test.E2E.HiringTracker.API.Controllers.V1
{
    public class TestPositionCommentsController : E2ETestBase, IClassFixture<WebApplicationFactory<HRT.HiringTracker.API.Startup>>
    {
        public TestPositionCommentsController(WebApplicationFactory<HRT.HiringTracker.API.Startup> factory) : base(factory)
        {
            _testParams = GetTestParams("PositionCommentsControllerTestSettings");
        }

        [Fact]
        public void PositionComment_GetAll_Success()
        {
            using (var client = _factory.CreateClient())
            {
                var respGetAll = client.GetAsync($"/api/v1/positioncomments");

                Assert.Equal(HttpStatusCode.OK, respGetAll.Result.StatusCode);

                IList<PositionComment> dtos = ExtractContentJson<List<PositionComment>>(respGetAll.Result.Content);

                Assert.NotEmpty(dtos);
            }
        }

        [Fact]
        public void PositionComment_Get_Success()
        {
            HRT.Interfaces.Entities.PositionComment testEntity = AddTestEntity();
            using (var client = _factory.CreateClient())
            {
                try
                {
                var paramPositionID = testEntity.PositionID;
                var paramCommentID = testEntity.CommentID;
                    var respGet = client.GetAsync($"/api/v1/positioncomments/{paramPositionID}/{paramCommentID}");

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
        public void PositionComment_Get_InvalidID()
        {
            using (var client = _factory.CreateClient())
            {
                var paramPositionID = Int64.MaxValue;
                var paramCommentID = Int64.MaxValue;

                var respGet = client.GetAsync($"/api/v1/positioncomments/{paramPositionID}/{paramCommentID}");

                Assert.Equal(HttpStatusCode.NotFound, respGet.Result.StatusCode);
            }
        }

        [Fact]
        public void PositionComment_Delete_Success()
        {
            var testEntity = AddTestEntity();
            using (var client = _factory.CreateClient())
            {
                try
                {
                var paramPositionID = testEntity.PositionID;
                var paramCommentID = testEntity.CommentID;

                    var respDel = client.DeleteAsync($"/api/v1/positioncomments/{paramPositionID}/{paramCommentID}");

                    Assert.Equal(HttpStatusCode.OK, respDel.Result.StatusCode);
                }
                finally
                {
                    RemoveTestEntity(testEntity);
                }
            }
        }

        [Fact]
        public void PositionComment_Delete_InvalidID()
        {
            using (var client = _factory.CreateClient())
            {
                var paramPositionID = Int64.MaxValue;
                var paramCommentID = Int64.MaxValue;

                var respDel = client.DeleteAsync($"/api/v1/positioncomments/{paramPositionID}/{paramCommentID}");

                Assert.Equal(HttpStatusCode.NotFound, respDel.Result.StatusCode);
            }
        }

        [Fact]
        public void PositionComment_Insert_Success()
        {
            using (var client = _factory.CreateClient())
            {
                HRT.Interfaces.Entities.PositionComment testEntity = CreateTestEntity();
                HRT.Interfaces.Entities.PositionComment respEntity = null;
                try
                {
                    var reqDto = PositionCommentConvertor.Convert(testEntity, null);

                    var content = CreateContentJson(reqDto);

                    var respInsert = client.PostAsync($"/api/v1/positioncomments/", content);

                    Assert.Equal(HttpStatusCode.OK, respInsert.Result.StatusCode);

                    PositionComment respDto = ExtractContentJson<PositionComment>(respInsert.Result.Content);

                                    Assert.NotNull(respDto.PositionID);
                                    Assert.NotNull(respDto.CommentID);
                
                    respEntity = PositionCommentConvertor.Convert(respDto);
                }
                finally
                {
                    RemoveTestEntity(respEntity);
                }
            }
        }

        [Fact]
        public void PositionComment_Update_Success()
        {
            using (var client = _factory.CreateClient())
            {
                HRT.Interfaces.Entities.PositionComment testEntity = AddTestEntity();
                try
                {
            
                    var reqDto = PositionCommentConvertor.Convert(testEntity, null);

                    var content = CreateContentJson(reqDto);

                    var respUpdate = client.PutAsync($"/api/v1/positioncomments/", content);

                    Assert.Equal(HttpStatusCode.OK, respUpdate.Result.StatusCode);

                    PositionComment respDto = ExtractContentJson<PositionComment>(respUpdate.Result.Content);

                                     Assert.NotNull(respDto.PositionID);
                                    Assert.NotNull(respDto.CommentID);
                
                }
                finally
                {
                    RemoveTestEntity(testEntity);
                }
            }
        }

        [Fact]
        public void PositionComment_Update_InvalidID()
        {
            using (var client = _factory.CreateClient())
            {
                HRT.Interfaces.Entities.PositionComment testEntity = CreateTestEntity();
                try
                {
                            testEntity.PositionID = 100007;
                            testEntity.CommentID = 100005;
              
                    var reqDto = PositionCommentConvertor.Convert(testEntity, null);

                    var content = CreateContentJson(reqDto);

                    var respUpdate = client.PutAsync($"/api/v1/positioncomments/", content);

                    Assert.Equal(HttpStatusCode.NotFound, respUpdate.Result.StatusCode);
                }
                finally
                {
                    RemoveTestEntity(testEntity);
                }
            }
        }

        #region Support methods

        protected bool RemoveTestEntity(HRT.Interfaces.Entities.PositionComment entity)
        {
            if (entity != null)
            {
                var dal = CreateDal();

                return dal.Delete(
                                        entity.PositionID,
                                        entity.CommentID
                );
            }
            else
            {
                return false;
            }
        }

        protected HRT.Interfaces.Entities.PositionComment CreateTestEntity()
        {
            var entity = new HRT.Interfaces.Entities.PositionComment();
                          entity.PositionID = 100007;
                            entity.CommentID = 100005;
              
            return entity;
        }

        protected HRT.Interfaces.Entities.PositionComment AddTestEntity()
        {
            HRT.Interfaces.Entities.PositionComment result = null;

            var entity = CreateTestEntity();

            var dal = CreateDal();
            result = dal.Insert(entity);

            return result;
        }

        private HRT.Interfaces.IPositionCommentDal CreateDal()
        {
            var initParams = GetTestParams("DALInitParams");

            HRT.Interfaces.IPositionCommentDal dal = new HRT.DAL.MSSQL.PositionCommentDal();
            var dalInitParams = dal.CreateInitParams();
            dalInitParams.Parameters["ConnectionString"] = (string)initParams.Settings["ConnectionString"];
            dal.Init(dalInitParams);

            return dal;
        }
        #endregion
    }
}
