using HRT.DAL.MSSQL;
using HRT.DTO;
using HRT.HiringTracker.API.Dal;
using HRT.Utils.Convertors;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
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
            using (var client = _factory.CreateClient())
            {
                long ID = 100002;

                var respGet = client.GetAsync($"/api/v1/positions/{ID}");

                Assert.Equal(HttpStatusCode.OK, respGet.Result.StatusCode);

                Position dto = ExtractContentJson<Position>(respGet.Result.Content);

                Assert.NotNull(dto);
                Assert.NotNull(dto.Links);
            }
        }

        [Fact]
        public void Position_Get_InvalidID()
        {
            using (var client = _factory.CreateClient())
            {
                long ID = Int64.MaxValue;

                var respGet = client.GetAsync($"/api/v1/positions/{ID}");

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
                    long? ID = testEntity.ID;

                    var respDel = client.DeleteAsync($"/api/v1/positions/{ID}");

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
                long? ID = Int64.MaxValue;

                var respDel = client.DeleteAsync($"/api/v1/positions/{ID}");

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
                    Assert.Equal(reqDto.StatusID, respDto.StatusID);

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
                    testEntity.Title = "Title 5FB8E6CCF3614DB4A693090F7C946159_UPD";
                    testEntity.ShortDesc = "ShortDesc 5FB8E6CCF3614DB4A693090F7C946159_UPD";
                    testEntity.StatusID = 2;
                    testEntity.ModifiedByID = 100002;
                    testEntity.ModifiedDate = DateTime.Parse("2021/01/01 13:00");
                    testEntity.Description = "Description 5FB8E6CCF3614DB4A693090F7C946159_UPD";
                    testEntity.DepartmentID = null;
                    testEntity.CreatedByID = 100003;
                    testEntity.CreatedDate = DateTime.Parse("2020/12/31 13:00");

                    var reqDto = PositionConvertor.Convert(testEntity, null);

                    var content = CreateContentJson(reqDto);

                    var respUpdate = client.PutAsync($"/api/v1/positions/", content);

                    Assert.Equal(HttpStatusCode.OK, respUpdate.Result.StatusCode);

                    Position respDto = ExtractContentJson<Position>(respUpdate.Result.Content);

                    Assert.NotNull(respDto.ID);
                    Assert.Equal(reqDto.StatusID, respDto.StatusID);

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
                    testEntity.Title = "Title 5FB8E6CCF3614DB4A693090F7C946159_UPD";
                    testEntity.ShortDesc = "ShortDesc 5FB8E6CCF3614DB4A693090F7C946159_UPD";
                    testEntity.StatusID = 2;
                    testEntity.ModifiedByID = 100002;
                    testEntity.ModifiedDate = DateTime.Parse("2021/01/01 13:00");
                    testEntity.Description = "Description 5FB8E6CCF3614DB4A693090F7C946159_UPD";
                    testEntity.DepartmentID = null;
                    testEntity.CreatedByID = 100003;
                    testEntity.CreatedDate = DateTime.Parse("2020/12/31 13:00");

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

                return dal.Delete(entity.ID);
            }
            else
            {
                return false;
            }
        }

        protected HRT.Interfaces.Entities.Position CreateTestEntity()
        {
            var entity = new HRT.Interfaces.Entities.Position();
            entity.ID = null;
            entity.Title = "Title 5FB8E6CCF3614DB4A693090F7C946159";
            entity.ShortDesc = "ShortDesc 5FB8E6CCF3614DB4A693090F7C946159";
            entity.StatusID = 1;
            entity.ModifiedByID = 100001;
            entity.ModifiedDate = DateTime.Parse("2021/01/01 12:00");
            entity.Description = "Description 5FB8E6CCF3614DB4A693090F7C946159";
            entity.DepartmentID = null;
            entity.CreatedByID = 100002;
            entity.CreatedDate = DateTime.Parse("2020/12/31 12:00");

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
