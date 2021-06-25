

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
    public class TestPositionStatusDal : TestBase
    {
        [Test]
        public void DalInit_Success()
        {
            IConfiguration config = GetConfiguration();
            var initParams = config.GetSection("DALInitParams").Get<TestDalInitParams>();

            IPositionStatusDal dal = new PositionStatusDal();
            var dalInitParams = dal.CreateInitParams();
            dalInitParams.Parameters["ConnectionString"] = initParams.ConnectionString;
            dal.Init(dalInitParams);
        }

        [Test]
        public void PositionStatus_GetAll_Success()
        {
            var dal = PreparePositionStatusDal("DALInitParams");

            IList<PositionStatus> entities = dal.GetAll();

            Assert.IsNotNull(entities);
            Assert.IsNotEmpty(entities);
        }

        [TestCase("PositionStatus\\000.GetDetails.Success")]
        public void PositionStatus_GetDetails_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PreparePositionStatusDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
                var paramID = (System.Int64?)objIds[0];
            PositionStatus entity = dal.Get(paramID);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual("Name 2944b4bdcff74fb18aed50b0c4785ea8", entity.Name);
                      }

        [Test]
        public void PositionStatus_GetDetails_InvalidId()
        {
                var paramID = Int64.MaxValue - 1;
            var dal = PreparePositionStatusDal("DALInitParams");

            PositionStatus entity = dal.Get(paramID);

            Assert.IsNull(entity);
        }

        [TestCase("PositionStatus\\010.Delete.Success")]
        public void PositionStatus_Delete_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PreparePositionStatusDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
                var paramID = (System.Int64?)objIds[0];
            bool removed = dal.Delete(paramID);

            TeardownCase(conn, caseName);

            Assert.IsTrue(removed);
        }

        [Test]
        public void PositionStatus_Delete_InvalidId()
        {
            var dal = PreparePositionStatusDal("DALInitParams");
                var paramID = Int64.MaxValue - 1;
   
            bool removed = dal.Delete(paramID);
            Assert.IsFalse(removed);

        }

        [TestCase("PositionStatus\\020.Insert.Success")]
        public void PositionStatus_Insert_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            SetupCase(conn, caseName);

            var dal = PreparePositionStatusDal("DALInitParams");

            var entity = new PositionStatus();
                          entity.Name = "Name 2f34f6abdcec4dd38801e2fc4a3b6cf5";
                          
            entity = dal.Insert(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual("Name 2f34f6abdcec4dd38801e2fc4a3b6cf5", entity.Name);
              
        }

        [TestCase("PositionStatus\\030.Update.Success")]
        public void PositionStatus_Update_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PreparePositionStatusDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
                var paramID = (System.Int64?)objIds[0];
            PositionStatus entity = dal.Get(paramID);

                          entity.Name = "Name 2952041c57754af7b77d6bfca6522793";
              
            entity = dal.Update(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual("Name 2952041c57754af7b77d6bfca6522793", entity.Name);
              
        }

        [Test]
        public void PositionStatus_Update_InvalidId()
        {
            var dal = PreparePositionStatusDal("DALInitParams");

            var entity = new PositionStatus();
                          entity.Name = "Name 2952041c57754af7b77d6bfca6522793";
              
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

        protected IPositionStatusDal PreparePositionStatusDal(string configName)
        {
            IConfiguration config = GetConfiguration();
            var initParams = config.GetSection(configName).Get<TestDalInitParams>();

            IPositionStatusDal dal = new PositionStatusDal();
            var dalInitParams = dal.CreateInitParams();
            dalInitParams.Parameters["ConnectionString"] = initParams.ConnectionString;
            dal.Init(dalInitParams);

            return dal;
        }
    }
}
