


using HRT.DTO;
using HRT.Utils.Convertors;
using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Collections.Generic;
using System.Net;
using Xunit;


namespace Test.E2E.HiringTracker.API.Controllers.V1
{
    public class TestPositionsController : E2ETestBase, IClassFixture<WebApplicationFactory<HRT.HiringTracker.API.Startup>>
    {
        public TestPositionsController(WebApplicationFactory<HRT.HiringTracker.API.Startup> factory) : base(factory)
        {
            _testParams = GetTestParams("PositionsControllerTestSettings");
        }

        [Fact]
        public void Position_GetAll_Success()
        {
            using (var client = _factory.CreateClient())
            {
                var respGetAll = client.GetAsync($"/api/v1/positions");

                Assert.Equal(HttpStatusCode.OK, respGetAll.Result.StatusCode);

                IList<Position> dtos = ExtractContentJson<List<Position>>(respGetAll.Result.Content);

                Assert.NotEmpty(dtos);
            }
        }

        [Fact]
        public void Position_Get_Success()
        {
            HRT.Interfaces.Entities.Position testEntity = AddTestEntity();
            using (var client = _factory.CreateClient())
            {
                try
                {
                    var paramID = testEntity.ID;
                    var respGet = client.GetAsync($"/api/v1/positions/{paramID}");

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
        public void Position_Get_InvalidID()
        {
            using (var client = _factory.CreateClient())
            {
                var paramID = Int64.MaxValue;

                var respGet = client.GetAsync($"/api/v1/positions/{paramID}");

                Assert.Equal(HttpStatusCode.NotFound, respGet.Result.StatusCode);
            }
        }

        [Fact]
        public void Position_Delete_Success()
        {
            var testEntity = AddTestEntity();
            using (var client = _factory.CreateClient())
            {
                try
                {
                    var paramID = testEntity.ID;

                    var respDel = client.DeleteAsync($"/api/v1/positions/{paramID}");

                    Assert.Equal(HttpStatusCode.OK, respDel.Result.StatusCode);
                }
                finally
                {
                    RemoveTestEntity(testEntity);
                }
            }
        }

        [Fact]
        public void Position_Delete_InvalidID()
        {
            using (var client = _factory.CreateClient())
            {
                var paramID = Int64.MaxValue;

                var respDel = client.DeleteAsync($"/api/v1/positions/{paramID}");

                Assert.Equal(HttpStatusCode.NotFound, respDel.Result.StatusCode);
            }
        }

        [Fact]
        public void Position_Insert_Success()
        {
            using (var client = _factory.CreateClient())
            {
                HRT.Interfaces.Entities.Position testEntity = CreateTestEntity();
                HRT.Interfaces.Entities.Position respEntity = null;
                try
                {
                    var reqDto = PositionConvertor.Convert(testEntity, null);

                    var content = CreateContentJson(reqDto);

                    var respInsert = client.PostAsync($"/api/v1/positions/", content);

                    Assert.Equal(HttpStatusCode.OK, respInsert.Result.StatusCode);

                    Position respDto = ExtractContentJson<Position>(respInsert.Result.Content);

                    Assert.NotNull(respDto.ID);
                    Assert.Equal(reqDto.DepartmentID, respDto.DepartmentID);
                    Assert.Equal(reqDto.Title, respDto.Title);
                    Assert.Equal(reqDto.ShortDesc, respDto.ShortDesc);
                    Assert.Equal(reqDto.Description, respDto.Description);
                    Assert.Equal(reqDto.StatusID, respDto.StatusID);
                    Assert.Equal(reqDto.CreatedDate, respDto.CreatedDate);
                    Assert.Equal(reqDto.CreatedByID, respDto.CreatedByID);
                    Assert.Equal(reqDto.ModifiedDate, respDto.ModifiedDate);
                    Assert.Equal(reqDto.ModifiedByID, respDto.ModifiedByID);

                    respEntity = PositionConvertor.Convert(respDto);
                }
                finally
                {
                    RemoveTestEntity(respEntity);
                }
            }
        }

        [Fact]
        public void Position_Update_Success()
        {
            using (var client = _factory.CreateClient())
            {
                HRT.Interfaces.Entities.Position testEntity = AddTestEntity();
                try
                {
                    testEntity.DepartmentID = 2;
                    testEntity.Title = "Title bb19ccb1d6854e22b844f397f512043b";
                    testEntity.ShortDesc = "ShortDesc bb19ccb1d6854e22b844f397f512043b";
                    testEntity.Description = "Description bb19ccb1d6854e22b844f397f512043b";
                    testEntity.StatusID = 2;
                    testEntity.CreatedDate = DateTime.Parse("3/21/2020 10:31:40 PM");
                    testEntity.CreatedByID = 100003;
                    testEntity.ModifiedDate = DateTime.Parse("1/31/2023 8:18:40 AM");
                    testEntity.ModifiedByID = 33020042;

                    var reqDto = PositionConvertor.Convert(testEntity, null);

                    var content = CreateContentJson(reqDto);

                    var respUpdate = client.PutAsync($"/api/v1/positions/", content);

                    Assert.Equal(HttpStatusCode.OK, respUpdate.Result.StatusCode);

                    Position respDto = ExtractContentJson<Position>(respUpdate.Result.Content);

                    Assert.NotNull(respDto.ID);
                    Assert.Equal(reqDto.DepartmentID, respDto.DepartmentID);
                    Assert.Equal(reqDto.Title, respDto.Title);
                    Assert.Equal(reqDto.ShortDesc, respDto.ShortDesc);
                    Assert.Equal(reqDto.Description, respDto.Description);
                    Assert.Equal(reqDto.StatusID, respDto.StatusID);
                    Assert.Equal(reqDto.CreatedDate, respDto.CreatedDate);
                    Assert.Equal(reqDto.CreatedByID, respDto.CreatedByID);
                    Assert.Equal(reqDto.ModifiedDate, respDto.ModifiedDate);
                    Assert.Equal(reqDto.ModifiedByID, respDto.ModifiedByID);

                }
                finally
                {
                    RemoveTestEntity(testEntity);
                }
            }
        }

        [Fact]
        public void Position_Update_InvalidID()
        {
            using (var client = _factory.CreateClient())
            {
                HRT.Interfaces.Entities.Position testEntity = CreateTestEntity();
                try
                {
                    testEntity.ID = Int64.MaxValue;
                    testEntity.DepartmentID = 2;
                    testEntity.Title = "Title bb19ccb1d6854e22b844f397f512043b";
                    testEntity.ShortDesc = "ShortDesc bb19ccb1d6854e22b844f397f512043b";
                    testEntity.Description = "Description bb19ccb1d6854e22b844f397f512043b";
                    testEntity.StatusID = 2;
                    testEntity.CreatedDate = DateTime.Parse("3/21/2020 10:31:40 PM");
                    testEntity.CreatedByID = 100003;
                    testEntity.ModifiedDate = DateTime.Parse("1/31/2023 8:18:40 AM");
                    testEntity.ModifiedByID = 33020042;

                    var reqDto = PositionConvertor.Convert(testEntity, null);

                    var content = CreateContentJson(reqDto);

                    var respUpdate = client.PutAsync($"/api/v1/positions/", content);

                    Assert.Equal(HttpStatusCode.NotFound, respUpdate.Result.StatusCode);
                }
                finally
                {
                    RemoveTestEntity(testEntity);
                }
            }
        }

        #region Support methods

        protected bool RemoveTestEntity(HRT.Interfaces.Entities.Position entity)
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

        protected HRT.Interfaces.Entities.Position CreateTestEntity()
        {
            var entity = new HRT.Interfaces.Entities.Position();
            entity.DepartmentID = 2;
            entity.Title = "Title 25fd4bbb003841d89edebcf6e9c9eeea";
            entity.ShortDesc = "ShortDesc 25fd4bbb003841d89edebcf6e9c9eeea";
            entity.Description = "Description 25fd4bbb003841d89edebcf6e9c9eeea";
            entity.StatusID = 2;
            entity.CreatedDate = DateTime.Parse("6/27/2019 6:30:40 AM");
            entity.CreatedByID = 100003;
            entity.ModifiedDate = DateTime.Parse("5/6/2022 6:57:40 AM");
            entity.ModifiedByID = 33020042;

            return entity;
        }

        protected HRT.Interfaces.Entities.Position AddTestEntity()
        {
            HRT.Interfaces.Entities.Position result = null;

            var entity = CreateTestEntity();

            var dal = CreateDal();
            result = dal.Insert(entity);

            return result;
        }

        private HRT.Interfaces.IPositionDal CreateDal()
        {
            var initParams = GetTestParams("DALInitParams");

            HRT.Interfaces.IPositionDal dal = new HRT.DAL.MSSQL.PositionDal();
            var dalInitParams = dal.CreateInitParams();
            dalInitParams.Parameters["ConnectionString"] = (string)initParams.Settings["ConnectionString"];
            dal.Init(dalInitParams);

            return dal;
        }
        #endregion
    }
}
