

using HRT.DAL.MSSQL;
using HRT.Interfaces;
using HRT.Interfaces.Entities;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;


namespace Test.HRT.DAL.MSSQL
{
    public class TestPositionDal : TestBase
    {
        [Test]
        public void DalInit_Success()
        {
            IConfiguration config = GetConfiguration();
            var initParams = config.GetSection("DALInitParams").Get<TestDalInitParams>();

            IPositionDal dal = new PositionDal();
            var dalInitParams = dal.CreateInitParams();
            dalInitParams.Parameters["ConnectionString"] = initParams.ConnectionString;
            dal.Init(dalInitParams);
        }

        [Test]
        public void Position_GetAll_Success()
        {
            var dal = PreparePositionDal("DALInitParams");

            IList<Position> entities = dal.GetAll();

            Assert.IsNotNull(entities);
            Assert.IsNotEmpty(entities);
        }

        [TestCase("Position\\000.GetDetails.Success")]
        public void Position_GetDetails_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PreparePositionDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
                var paramID = (System.Int64?)objIds[0];
            Position entity = dal.Get(paramID);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual(10, entity.DepartmentID);
                            Assert.AreEqual("Title 56c5cef7f2e64fc5b4c66e697101e256", entity.Title);
                            Assert.AreEqual("ShortDesc 56c5cef7f2e64fc5b4c66e697101e256", entity.ShortDesc);
                            Assert.AreEqual("Description 56c5cef7f2e64fc5b4c66e697101e256", entity.Description);
                            Assert.AreEqual(4, entity.StatusID);
                            Assert.AreEqual(DateTime.Parse("2/27/2020 1:34:12 PM"), entity.CreatedDate);
                            Assert.AreEqual(100001, entity.CreatedByID);
                            Assert.AreEqual(DateTime.Parse("5/21/2021 11:22:12 AM"), entity.ModifiedDate);
                            Assert.AreEqual(100001, entity.ModifiedByID);
                      }

        [Test]
        public void Position_GetDetails_InvalidId()
        {
                var paramID = Int64.MaxValue - 1;
            var dal = PreparePositionDal("DALInitParams");

            Position entity = dal.Get(paramID);

            Assert.IsNull(entity);
        }

        [TestCase("Position\\010.Delete.Success")]
        public void Position_Delete_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PreparePositionDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
                var paramID = (System.Int64?)objIds[0];
            bool removed = dal.Delete(paramID);

            TeardownCase(conn, caseName);

            Assert.IsTrue(removed);
        }

        [Test]
        public void Position_Delete_InvalidId()
        {
            var dal = PreparePositionDal("DALInitParams");
                var paramID = Int64.MaxValue - 1;
   
            bool removed = dal.Delete(paramID);
            Assert.IsFalse(removed);

        }

        [TestCase("Position\\020.Insert.Success")]
        public void Position_Insert_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            SetupCase(conn, caseName);

            var dal = PreparePositionDal("DALInitParams");

            var entity = new Position();
                          entity.DepartmentID = 10;
                            entity.Title = "Title c61812b2a18b4e60b7fc0c4136950496";
                            entity.ShortDesc = "ShortDesc c61812b2a18b4e60b7fc0c4136950496";
                            entity.Description = "Description c61812b2a18b4e60b7fc0c4136950496";
                            entity.StatusID = 2;
                            entity.CreatedDate = DateTime.Parse("3/3/2024 8:50:12 AM");
                            entity.CreatedByID = 33020042;
                            entity.ModifiedDate = DateTime.Parse("7/22/2021 6:37:12 PM");
                            entity.ModifiedByID = 100003;
                          
            entity = dal.Insert(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual(10, entity.DepartmentID);
                            Assert.AreEqual("Title c61812b2a18b4e60b7fc0c4136950496", entity.Title);
                            Assert.AreEqual("ShortDesc c61812b2a18b4e60b7fc0c4136950496", entity.ShortDesc);
                            Assert.AreEqual("Description c61812b2a18b4e60b7fc0c4136950496", entity.Description);
                            Assert.AreEqual(2, entity.StatusID);
                            Assert.AreEqual(DateTime.Parse("3/3/2024 8:50:12 AM"), entity.CreatedDate);
                            Assert.AreEqual(33020042, entity.CreatedByID);
                            Assert.AreEqual(DateTime.Parse("7/22/2021 6:37:12 PM"), entity.ModifiedDate);
                            Assert.AreEqual(100003, entity.ModifiedByID);
              
        }

        [TestCase("Position\\030.Update.Success")]
        public void Position_Update_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PreparePositionDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
                var paramID = (System.Int64?)objIds[0];
            Position entity = dal.Get(paramID);

                          entity.DepartmentID = 2;
                            entity.Title = "Title 52d3cbd93ac648099693dbc6284f7495";
                            entity.ShortDesc = "ShortDesc 52d3cbd93ac648099693dbc6284f7495";
                            entity.Description = "Description 52d3cbd93ac648099693dbc6284f7495";
                            entity.StatusID = 2;
                            entity.CreatedDate = DateTime.Parse("3/9/2019 2:37:12 PM");
                            entity.CreatedByID = 100001;
                            entity.ModifiedDate = DateTime.Parse("3/9/2019 2:37:12 PM");
                            entity.ModifiedByID = 33000067;
              
            entity = dal.Update(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual(2, entity.DepartmentID);
                            Assert.AreEqual("Title 52d3cbd93ac648099693dbc6284f7495", entity.Title);
                            Assert.AreEqual("ShortDesc 52d3cbd93ac648099693dbc6284f7495", entity.ShortDesc);
                            Assert.AreEqual("Description 52d3cbd93ac648099693dbc6284f7495", entity.Description);
                            Assert.AreEqual(2, entity.StatusID);
                            Assert.AreEqual(DateTime.Parse("3/9/2019 2:37:12 PM"), entity.CreatedDate);
                            Assert.AreEqual(100001, entity.CreatedByID);
                            Assert.AreEqual(DateTime.Parse("3/9/2019 2:37:12 PM"), entity.ModifiedDate);
                            Assert.AreEqual(33000067, entity.ModifiedByID);
              
        }

        [Test]
        public void Position_Update_InvalidId()
        {
            var dal = PreparePositionDal("DALInitParams");

            var entity = new Position();
                          entity.DepartmentID = 2;
                            entity.Title = "Title 52d3cbd93ac648099693dbc6284f7495";
                            entity.ShortDesc = "ShortDesc 52d3cbd93ac648099693dbc6284f7495";
                            entity.Description = "Description 52d3cbd93ac648099693dbc6284f7495";
                            entity.StatusID = 2;
                            entity.CreatedDate = DateTime.Parse("3/9/2019 2:37:12 PM");
                            entity.CreatedByID = 100001;
                            entity.ModifiedDate = DateTime.Parse("3/9/2019 2:37:12 PM");
                            entity.ModifiedByID = 33000067;
              
            try
            {
                entity = dal.Update(entity);

                Assert.Fail("Fail - exception was expected, but wasn't thrown.");
            }
            catch (Exception ex)
            {
                Assert.Pass("Success - exception thrown as expected");
            }
        }

        protected IPositionDal PreparePositionDal(string configName)
        {
            IConfiguration config = GetConfiguration();
            var initParams = config.GetSection(configName).Get<TestDalInitParams>();

            IPositionDal dal = new PositionDal();
            var dalInitParams = dal.CreateInitParams();
            dalInitParams.Parameters["ConnectionString"] = initParams.ConnectionString;
            dal.Init(dalInitParams);

            return dal;
        }
    }
}
