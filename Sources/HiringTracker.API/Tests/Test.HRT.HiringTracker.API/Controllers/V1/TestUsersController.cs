


using HRT.DTO;
using HRT.Utils.Convertors;
using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Collections.Generic;
using System.Net;
using Xunit;


namespace Test.E2E.HiringTracker.API.Controllers.V1
{
    public class TestUsersController : E2ETestBase, IClassFixture<WebApplicationFactory<HRT.HiringTracker.API.Startup>>
    {
        public TestUsersController(WebApplicationFactory<HRT.HiringTracker.API.Startup> factory) : base(factory)
        {
            _testParams = GetTestParams("UsersControllerTestSettings");
        }

        [Fact]
        public void User_GetAll_Success()
        {
            using (var client = _factory.CreateClient())
            {
                var respGetAll = client.GetAsync($"/api/v1/users");

                Assert.Equal(HttpStatusCode.OK, respGetAll.Result.StatusCode);

                IList<User> dtos = ExtractContentJson<List<User>>(respGetAll.Result.Content);

                Assert.NotEmpty(dtos);
            }
        }

        [Fact]
        public void User_Get_Success()
        {
            HRT.Interfaces.Entities.User testEntity = AddTestEntity();
            using (var client = _factory.CreateClient())
            {
                try
                {
                var paramID = testEntity.ID;
                    var respGet = client.GetAsync($"/api/v1/users/{paramID}");

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
        public void User_Get_InvalidID()
        {
            using (var client = _factory.CreateClient())
            {
                var paramID = Int64.MaxValue;

                var respGet = client.GetAsync($"/api/v1/users/{paramID}");

                Assert.Equal(HttpStatusCode.NotFound, respGet.Result.StatusCode);
            }
        }

        [Fact]
        public void User_Delete_Success()
        {
            var testEntity = AddTestEntity();
            using (var client = _factory.CreateClient())
            {
                try
                {
                var paramID = testEntity.ID;

                    var respDel = client.DeleteAsync($"/api/v1/users/{paramID}");

                    Assert.Equal(HttpStatusCode.OK, respDel.Result.StatusCode);
                }
                finally
                {
                    RemoveTestEntity(testEntity);
                }
            }
        }

        [Fact]
        public void User_Delete_InvalidID()
        {
            using (var client = _factory.CreateClient())
            {
                var paramID = Int64.MaxValue;

                var respDel = client.DeleteAsync($"/api/v1/users/{paramID}");

                Assert.Equal(HttpStatusCode.NotFound, respDel.Result.StatusCode);
            }
        }

        [Fact]
        public void User_Insert_Success()
        {
            using (var client = _factory.CreateClient())
            {
                HRT.Interfaces.Entities.User testEntity = CreateTestEntity();
                HRT.Interfaces.Entities.User respEntity = null;
                try
                {
                    var reqDto = UserConvertor.Convert(testEntity, null);

                    var content = CreateContentJson(reqDto);

                    var respInsert = client.PostAsync($"/api/v1/users/", content);

                    Assert.Equal(HttpStatusCode.OK, respInsert.Result.StatusCode);

                    User respDto = ExtractContentJson<User>(respInsert.Result.Content);

                                    Assert.NotNull(respDto.ID);
                                    Assert.Equal(reqDto.Login, respDto.Login);
                                    Assert.Equal(reqDto.FirstName, respDto.FirstName);
                                    Assert.Equal(reqDto.LastName, respDto.LastName);
                                    Assert.Equal(reqDto.Email, respDto.Email);
                                    Assert.Equal(reqDto.Description, respDto.Description);
                                    Assert.Equal(reqDto.PwdHash, respDto.PwdHash);
                                    Assert.Equal(reqDto.Salt, respDto.Salt);
                
                    respEntity = UserConvertor.Convert(respDto);
                }
                finally
                {
                    RemoveTestEntity(respEntity);
                }
            }
        }

        [Fact]
        public void User_Update_Success()
        {
            using (var client = _factory.CreateClient())
            {
                HRT.Interfaces.Entities.User testEntity = AddTestEntity();
                try
                {
                          testEntity.Login = "Login f573c67023144d569962450895d8f890";
                            testEntity.FirstName = "FirstName f573c67023144d569962450895d8f890";
                            testEntity.LastName = "LastName f573c67023144d569962450895d8f890";
                            testEntity.Email = "Email f573c67023144d569962450895d8f890";
                            testEntity.Description = "Description f573c67023144d569962450895d8f890";
                            testEntity.PwdHash = "PwdHash f573c67023144d569962450895d8f890";
                            testEntity.Salt = "Salt f573c67023144d569962450895d8f890";
              
                    var reqDto = UserConvertor.Convert(testEntity, null);

                    var content = CreateContentJson(reqDto);

                    var respUpdate = client.PutAsync($"/api/v1/users/", content);

                    Assert.Equal(HttpStatusCode.OK, respUpdate.Result.StatusCode);

                    User respDto = ExtractContentJson<User>(respUpdate.Result.Content);

                                     Assert.NotNull(respDto.ID);
                                    Assert.Equal(reqDto.Login, respDto.Login);
                                    Assert.Equal(reqDto.FirstName, respDto.FirstName);
                                    Assert.Equal(reqDto.LastName, respDto.LastName);
                                    Assert.Equal(reqDto.Email, respDto.Email);
                                    Assert.Equal(reqDto.Description, respDto.Description);
                                    Assert.Equal(reqDto.PwdHash, respDto.PwdHash);
                                    Assert.Equal(reqDto.Salt, respDto.Salt);
                
                }
                finally
                {
                    RemoveTestEntity(testEntity);
                }
            }
        }

        [Fact]
        public void User_Update_InvalidID()
        {
            using (var client = _factory.CreateClient())
            {
                HRT.Interfaces.Entities.User testEntity = CreateTestEntity();
                try
                {
                             testEntity.ID = Int64.MaxValue;
                             testEntity.Login = "Login f573c67023144d569962450895d8f890";
                            testEntity.FirstName = "FirstName f573c67023144d569962450895d8f890";
                            testEntity.LastName = "LastName f573c67023144d569962450895d8f890";
                            testEntity.Email = "Email f573c67023144d569962450895d8f890";
                            testEntity.Description = "Description f573c67023144d569962450895d8f890";
                            testEntity.PwdHash = "PwdHash f573c67023144d569962450895d8f890";
                            testEntity.Salt = "Salt f573c67023144d569962450895d8f890";
              
                    var reqDto = UserConvertor.Convert(testEntity, null);

                    var content = CreateContentJson(reqDto);

                    var respUpdate = client.PutAsync($"/api/v1/users/", content);

                    Assert.Equal(HttpStatusCode.NotFound, respUpdate.Result.StatusCode);
                }
                finally
                {
                    RemoveTestEntity(testEntity);
                }
            }
        }

        #region Support methods

        protected bool RemoveTestEntity(HRT.Interfaces.Entities.User entity)
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

        protected HRT.Interfaces.Entities.User CreateTestEntity()
        {
            var entity = new HRT.Interfaces.Entities.User();
                          entity.Login = "Login 4c0573ba7e69490aba6f2f5d5c6098fd";
                            entity.FirstName = "FirstName 4c0573ba7e69490aba6f2f5d5c6098fd";
                            entity.LastName = "LastName 4c0573ba7e69490aba6f2f5d5c6098fd";
                            entity.Email = "Email 4c0573ba7e69490aba6f2f5d5c6098fd";
                            entity.Description = "Description 4c0573ba7e69490aba6f2f5d5c6098fd";
                            entity.PwdHash = "PwdHash 4c0573ba7e69490aba6f2f5d5c6098fd";
                            entity.Salt = "Salt 4c0573ba7e69490aba6f2f5d5c6098fd";
              
            return entity;
        }

        protected HRT.Interfaces.Entities.User AddTestEntity()
        {
            HRT.Interfaces.Entities.User result = null;

            var entity = CreateTestEntity();

            var dal = CreateDal();
            result = dal.Insert(entity);

            return result;
        }

        private HRT.Interfaces.IUserDal CreateDal()
        {
            var initParams = GetTestParams("DALInitParams");

            HRT.Interfaces.IUserDal dal = new HRT.DAL.MSSQL.UserDal();
            var dalInitParams = dal.CreateInitParams();
            dalInitParams.Parameters["ConnectionString"] = (string)initParams.Settings["ConnectionString"];
            dal.Init(dalInitParams);

            return dal;
        }
        #endregion
    }
}
