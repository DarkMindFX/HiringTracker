

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
            
                          Assert.AreEqual("Title 0f2cccdbb2dc4b8bb1527608550db423", entity.Title);
                            Assert.AreEqual("ShortDesc 0f2cccdbb2dc4b8bb1527608550db423", entity.ShortDesc);
                            Assert.AreEqual("Description 0f2cccdbb2dc4b8bb1527608550db423", entity.Description);
                            Assert.AreEqual(1, entity.StatusID);
                            Assert.AreEqual(DateTime.Parse("9/7/2023 1:22:34 AM"), entity.CreatedDate);
                            Assert.AreEqual(100004, entity.CreatedByID);
                            Assert.AreEqual(DateTime.Parse("8/9/2019 6:25:34 AM"), entity.ModifiedDate);
                            Assert.AreEqual(100004, entity.ModifiedByID);
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
                          entity.Title = "Title 5d492528cac149f0bed22d62eac46c79";
                            entity.ShortDesc = "ShortDesc 5d492528cac149f0bed22d62eac46c79";
                            entity.Description = "Description 5d492528cac149f0bed22d62eac46c79";
                            entity.StatusID = 2;
                            entity.CreatedDate = DateTime.Parse("4/2/2021 7:02:34 AM");
                            entity.CreatedByID = 100005;
                            entity.ModifiedDate = DateTime.Parse("11/19/2018 12:22:34 PM");
                            entity.ModifiedByID = 100004;
                          
            entity = dal.Insert(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual("Title 5d492528cac149f0bed22d62eac46c79", entity.Title);
                            Assert.AreEqual("ShortDesc 5d492528cac149f0bed22d62eac46c79", entity.ShortDesc);
                            Assert.AreEqual("Description 5d492528cac149f0bed22d62eac46c79", entity.Description);
                            Assert.AreEqual(2, entity.StatusID);
                            Assert.AreEqual(DateTime.Parse("4/2/2021 7:02:34 AM"), entity.CreatedDate);
                            Assert.AreEqual(100005, entity.CreatedByID);
                            Assert.AreEqual(DateTime.Parse("11/19/2018 12:22:34 PM"), entity.ModifiedDate);
                            Assert.AreEqual(100004, entity.ModifiedByID);
              
        }

        [TestCase("Position\\030.Update.Success")]
        public void Position_Update_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PreparePositionDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
                var paramID = (System.Int64?)objIds[0];
            Position entity = dal.Get(paramID);

                          entity.Title = "Title 1ebfe71c84df476995524cc630436869";
                            entity.ShortDesc = "ShortDesc 1ebfe71c84df476995524cc630436869";
                            entity.Description = "Description 1ebfe71c84df476995524cc630436869";
                            entity.StatusID = 5;
                            entity.CreatedDate = DateTime.Parse("9/23/2022 12:24:34 AM");
                            entity.CreatedByID = 100003;
                            entity.ModifiedDate = DateTime.Parse("9/23/2022 12:24:34 AM");
                            entity.ModifiedByID = 100001;
              
            entity = dal.Update(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual("Title 1ebfe71c84df476995524cc630436869", entity.Title);
                            Assert.AreEqual("ShortDesc 1ebfe71c84df476995524cc630436869", entity.ShortDesc);
                            Assert.AreEqual("Description 1ebfe71c84df476995524cc630436869", entity.Description);
                            Assert.AreEqual(5, entity.StatusID);
                            Assert.AreEqual(DateTime.Parse("9/23/2022 12:24:34 AM"), entity.CreatedDate);
                            Assert.AreEqual(100003, entity.CreatedByID);
                            Assert.AreEqual(DateTime.Parse("9/23/2022 12:24:34 AM"), entity.ModifiedDate);
                            Assert.AreEqual(100001, entity.ModifiedByID);
              
        }

        [Test]
        public void Position_Update_InvalidId()
        {
            var dal = PreparePositionDal("DALInitParams");

            var entity = new Position();
                          entity.Title = "Title 1ebfe71c84df476995524cc630436869";
                            entity.ShortDesc = "ShortDesc 1ebfe71c84df476995524cc630436869";
                            entity.Description = "Description 1ebfe71c84df476995524cc630436869";
                            entity.StatusID = 5;
                            entity.CreatedDate = DateTime.Parse("9/23/2022 12:24:34 AM");
                            entity.CreatedByID = 100003;
                            entity.ModifiedDate = DateTime.Parse("9/23/2022 12:24:34 AM");
                            entity.ModifiedByID = 100001;
              
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
