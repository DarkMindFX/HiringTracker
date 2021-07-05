

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
    public class TestInterviewStatusDal : TestBase
    {
        [Test]
        public void DalInit_Success()
        {
            IConfiguration config = GetConfiguration();
            var initParams = config.GetSection("DALInitParams").Get<TestDalInitParams>();

            IInterviewStatusDal dal = new InterviewStatusDal();
            var dalInitParams = dal.CreateInitParams();
            dalInitParams.Parameters["ConnectionString"] = initParams.ConnectionString;
            dal.Init(dalInitParams);
        }

        [Test]
        public void InterviewStatus_GetAll_Success()
        {
            var dal = PrepareInterviewStatusDal("DALInitParams");

            IList<InterviewStatus> entities = dal.GetAll();

            Assert.IsNotNull(entities);
            Assert.IsNotEmpty(entities);
        }

        [TestCase("InterviewStatus\\000.GetDetails.Success")]
        public void InterviewStatus_GetDetails_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareInterviewStatusDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
                var paramID = (System.Int64?)objIds[0];
            InterviewStatus entity = dal.Get(paramID);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual("Name 5eb1c149eefd47debeb7c479f138d881", entity.Name);
                      }

        [Test]
        public void InterviewStatus_GetDetails_InvalidId()
        {
                var paramID = Int64.MaxValue - 1;
            var dal = PrepareInterviewStatusDal("DALInitParams");

            InterviewStatus entity = dal.Get(paramID);

            Assert.IsNull(entity);
        }

        [TestCase("InterviewStatus\\010.Delete.Success")]
        public void InterviewStatus_Delete_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareInterviewStatusDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
                var paramID = (System.Int64?)objIds[0];
            bool removed = dal.Delete(paramID);

            TeardownCase(conn, caseName);

            Assert.IsTrue(removed);
        }

        [Test]
        public void InterviewStatus_Delete_InvalidId()
        {
            var dal = PrepareInterviewStatusDal("DALInitParams");
                var paramID = Int64.MaxValue - 1;
   
            bool removed = dal.Delete(paramID);
            Assert.IsFalse(removed);

        }

        [TestCase("InterviewStatus\\020.Insert.Success")]
        public void InterviewStatus_Insert_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            SetupCase(conn, caseName);

            var dal = PrepareInterviewStatusDal("DALInitParams");

            var entity = new InterviewStatus();
                          entity.Name = "Name 28e5da9df76b40ab9c248af1136e35ff";
                          
            entity = dal.Insert(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual("Name 28e5da9df76b40ab9c248af1136e35ff", entity.Name);
              
        }

        [TestCase("InterviewStatus\\030.Update.Success")]
        public void InterviewStatus_Update_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareInterviewStatusDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
                var paramID = (System.Int64?)objIds[0];
            InterviewStatus entity = dal.Get(paramID);

                          entity.Name = "Name 7e82b630cc75460d80cd25ce2cffbf8e";
              
            entity = dal.Update(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual("Name 7e82b630cc75460d80cd25ce2cffbf8e", entity.Name);
              
        }

        [Test]
        public void InterviewStatus_Update_InvalidId()
        {
            var dal = PrepareInterviewStatusDal("DALInitParams");

            var entity = new InterviewStatus();
                          entity.Name = "Name 7e82b630cc75460d80cd25ce2cffbf8e";
              
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

        protected IInterviewStatusDal PrepareInterviewStatusDal(string configName)
        {
            IConfiguration config = GetConfiguration();
            var initParams = config.GetSection(configName).Get<TestDalInitParams>();

            IInterviewStatusDal dal = new InterviewStatusDal();
            var dalInitParams = dal.CreateInitParams();
            dalInitParams.Parameters["ConnectionString"] = initParams.ConnectionString;
            dal.Init(dalInitParams);

            return dal;
        }
    }
}
