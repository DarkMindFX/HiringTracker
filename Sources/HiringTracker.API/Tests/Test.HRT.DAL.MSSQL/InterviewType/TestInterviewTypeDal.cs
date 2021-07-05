

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
    public class TestInterviewTypeDal : TestBase
    {
        [Test]
        public void DalInit_Success()
        {
            IConfiguration config = GetConfiguration();
            var initParams = config.GetSection("DALInitParams").Get<TestDalInitParams>();

            IInterviewTypeDal dal = new InterviewTypeDal();
            var dalInitParams = dal.CreateInitParams();
            dalInitParams.Parameters["ConnectionString"] = initParams.ConnectionString;
            dal.Init(dalInitParams);
        }

        [Test]
        public void InterviewType_GetAll_Success()
        {
            var dal = PrepareInterviewTypeDal("DALInitParams");

            IList<InterviewType> entities = dal.GetAll();

            Assert.IsNotNull(entities);
            Assert.IsNotEmpty(entities);
        }

        [TestCase("InterviewType\\000.GetDetails.Success")]
        public void InterviewType_GetDetails_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareInterviewTypeDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
                var paramID = (System.Int64)objIds[0];
            InterviewType entity = dal.Get(paramID);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual(326774, entity.ID);
                            Assert.AreEqual("Name bd963d0cf52f49099b589a34cdf0d41a", entity.Name);
                      }

        [Test]
        public void InterviewType_GetDetails_InvalidId()
        {
                var paramID = Int64.MaxValue - 1;
            var dal = PrepareInterviewTypeDal("DALInitParams");

            InterviewType entity = dal.Get(paramID);

            Assert.IsNull(entity);
        }

        [TestCase("InterviewType\\010.Delete.Success")]
        public void InterviewType_Delete_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareInterviewTypeDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
                var paramID = (System.Int64)objIds[0];
            bool removed = dal.Delete(paramID);

            TeardownCase(conn, caseName);

            Assert.IsTrue(removed);
        }

        [Test]
        public void InterviewType_Delete_InvalidId()
        {
            var dal = PrepareInterviewTypeDal("DALInitParams");
                var paramID = Int64.MaxValue - 1;
   
            bool removed = dal.Delete(paramID);
            Assert.IsFalse(removed);

        }

        [TestCase("InterviewType\\020.Insert.Success")]
        public void InterviewType_Insert_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            SetupCase(conn, caseName);

            var dal = PrepareInterviewTypeDal("DALInitParams");

            var entity = new InterviewType();
                          entity.ID = 326774;
                            entity.Name = "Name 37f02d6fc0294f759e2f161db54c55c0";
                          
            entity = dal.Insert(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual(326774, entity.ID);
                            Assert.AreEqual("Name 37f02d6fc0294f759e2f161db54c55c0", entity.Name);
              
        }

        [TestCase("InterviewType\\030.Update.Success")]
        public void InterviewType_Update_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareInterviewTypeDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
                var paramID = (System.Int64)objIds[0];
            InterviewType entity = dal.Get(paramID);

                          entity.Name = "Name bc22105fdb624279811c0e58bd8d6b09";
              
            entity = dal.Update(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual(326774, entity.ID);
                            Assert.AreEqual("Name bc22105fdb624279811c0e58bd8d6b09", entity.Name);
              
        }

        [Test]
        public void InterviewType_Update_InvalidId()
        {
            var dal = PrepareInterviewTypeDal("DALInitParams");

            var entity = new InterviewType();
                          entity.ID = 326774;
                            entity.Name = "Name bc22105fdb624279811c0e58bd8d6b09";
              
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

        protected IInterviewTypeDal PrepareInterviewTypeDal(string configName)
        {
            IConfiguration config = GetConfiguration();
            var initParams = config.GetSection(configName).Get<TestDalInitParams>();

            IInterviewTypeDal dal = new InterviewTypeDal();
            var dalInitParams = dal.CreateInitParams();
            dalInitParams.Parameters["ConnectionString"] = initParams.ConnectionString;
            dal.Init(dalInitParams);

            return dal;
        }
    }
}
