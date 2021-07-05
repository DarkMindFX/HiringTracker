

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
    public class TestInterviewRoleDal : TestBase
    {
        [Test]
        public void DalInit_Success()
        {
            IConfiguration config = GetConfiguration();
            var initParams = config.GetSection("DALInitParams").Get<TestDalInitParams>();

            IInterviewRoleDal dal = new InterviewRoleDal();
            var dalInitParams = dal.CreateInitParams();
            dalInitParams.Parameters["ConnectionString"] = initParams.ConnectionString;
            dal.Init(dalInitParams);
        }

        [Test]
        public void InterviewRole_GetAll_Success()
        {
            var dal = PrepareInterviewRoleDal("DALInitParams");

            IList<InterviewRole> entities = dal.GetAll();

            Assert.IsNotNull(entities);
            Assert.IsNotEmpty(entities);
        }

        [TestCase("InterviewRole\\000.GetDetails.Success")]
        public void InterviewRole_GetDetails_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareInterviewRoleDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
                var paramInterviewID = (System.Int64)objIds[0];
                var paramUserID = (System.Int64)objIds[1];
            InterviewRole entity = dal.Get(paramInterviewID,paramUserID);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.InterviewID);
                        Assert.IsNotNull(entity.UserID);
            
                          Assert.AreEqual(100006, entity.InterviewID);
                            Assert.AreEqual(100005, entity.UserID);
                            Assert.AreEqual(2, entity.RoleID);
                      }

        [Test]
        public void InterviewRole_GetDetails_InvalidId()
        {
                var paramInterviewID = Int64.MaxValue - 1;
                var paramUserID = Int64.MaxValue - 1;
            var dal = PrepareInterviewRoleDal("DALInitParams");

            InterviewRole entity = dal.Get(paramInterviewID,paramUserID);

            Assert.IsNull(entity);
        }

        [TestCase("InterviewRole\\010.Delete.Success")]
        public void InterviewRole_Delete_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareInterviewRoleDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
                var paramInterviewID = (System.Int64)objIds[0];
                var paramUserID = (System.Int64)objIds[1];
            bool removed = dal.Delete(paramInterviewID,paramUserID);

            TeardownCase(conn, caseName);

            Assert.IsTrue(removed);
        }

        [Test]
        public void InterviewRole_Delete_InvalidId()
        {
            var dal = PrepareInterviewRoleDal("DALInitParams");
                var paramInterviewID = Int64.MaxValue - 1;
                var paramUserID = Int64.MaxValue - 1;
   
            bool removed = dal.Delete(paramInterviewID,paramUserID);
            Assert.IsFalse(removed);

        }

        [TestCase("InterviewRole\\020.Insert.Success")]
        public void InterviewRole_Insert_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            SetupCase(conn, caseName);

            var dal = PrepareInterviewRoleDal("DALInitParams");

            var entity = new InterviewRole();
                          entity.InterviewID = 100006;
                            entity.UserID = 100001;
                            entity.RoleID = 3;
                          
            entity = dal.Insert(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.InterviewID);
                        Assert.IsNotNull(entity.UserID);
            
                          Assert.AreEqual(100006, entity.InterviewID);
                            Assert.AreEqual(100001, entity.UserID);
                            Assert.AreEqual(3, entity.RoleID);
              
        }

        [TestCase("InterviewRole\\030.Update.Success")]
        public void InterviewRole_Update_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareInterviewRoleDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
                var paramInterviewID = (System.Int64)objIds[0];
                var paramUserID = (System.Int64)objIds[1];
            InterviewRole entity = dal.Get(paramInterviewID,paramUserID);

                          entity.RoleID = 1;
              
            entity = dal.Update(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.InterviewID);
                        Assert.IsNotNull(entity.UserID);
            
                          Assert.AreEqual(100006, entity.InterviewID);
                            Assert.AreEqual(100001, entity.UserID);
                            Assert.AreEqual(1, entity.RoleID);
              
        }

        [Test]
        public void InterviewRole_Update_InvalidId()
        {
            var dal = PrepareInterviewRoleDal("DALInitParams");

            var entity = new InterviewRole();
                          entity.InterviewID = 100006;
                            entity.UserID = 100001;
                            entity.RoleID = 1;
              
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

        protected IInterviewRoleDal PrepareInterviewRoleDal(string configName)
        {
            IConfiguration config = GetConfiguration();
            var initParams = config.GetSection(configName).Get<TestDalInitParams>();

            IInterviewRoleDal dal = new InterviewRoleDal();
            var dalInitParams = dal.CreateInitParams();
            dalInitParams.Parameters["ConnectionString"] = initParams.ConnectionString;
            dal.Init(dalInitParams);

            return dal;
        }
    }
}
