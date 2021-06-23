

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

            object objId = SetupCase(conn, caseName);
            long id = (long)objId;

            InterviewRole entity = dal.Get(id);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.InterviewID);
                        Assert.IsNotNull(entity.UserID);
            
                          Assert.AreEqual(100002, entity.UserID);
                            Assert.AreEqual(4, entity.RoleID);
                      }

        [Test]
        public void InterviewRole_GetDetails_InvalidId()
        {
            long id = Int32.MaxValue - 1;
            var dal = PrepareInterviewRoleDal("DALInitParams");

            InterviewRole entity = dal.Get(id);

            Assert.IsNull(entity);
        }

        [TestCase("InterviewRole\\010.Delete.Success")]
        public void InterviewRole_Delete_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareInterviewRoleDal("DALInitParams");

            object objId = SetupCase(conn, caseName);
            long id = (long)objId;

            bool removed = dal.Delete(id);

            TeardownCase(conn, caseName);

            Assert.IsTrue(removed);
        }

        [Test]
        public void InterviewRole_Delete_InvalidId()
        {
            long positionId = Int32.MaxValue - 1;
            var dal = PrepareInterviewRoleDal("DALInitParams");

            bool removed = dal.Delete(positionId);
            Assert.IsFalse(removed);

        }

        [TestCase("InterviewRole\\020.Insert.Success")]
        public void InterviewRole_Insert_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            SetupCase(conn, caseName);

            var dal = PrepareInterviewRoleDal("DALInitParams");

            var entity = new InterviewRole();
                          entity.UserID = 33000067;
                            entity.RoleID = 1;
                          
            entity = dal.Upsert(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.InterviewID);
                        Assert.IsNotNull(entity.UserID);
            
                          Assert.AreEqual(33000067, entity.UserID);
                            Assert.AreEqual(1, entity.RoleID);
              
        }

        [TestCase("InterviewRole\\030.Update.Success")]
        public void InterviewRole_Update_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareInterviewRoleDal("DALInitParams");

            object objId = SetupCase(conn, caseName);
            long id = (long)objId;

            var entity = dal.Get(id);
                          entity.UserID = 100003;
                            entity.RoleID = 4;
              
            entity = dal.Upsert(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.InterviewID);
                        Assert.IsNotNull(entity.UserID);
            
                          Assert.AreEqual(100003, entity.UserID);
                            Assert.AreEqual(4, entity.RoleID);
              
        }

        [Test]
        public void InterviewRole_Update_InvalidId()
        {
            var dal = PrepareInterviewRoleDal("DALInitParams");

            var entity = new InterviewRole();
            entity.ID = Int64.MaxValue - 1;
                          entity.UserID = 100003;
                            entity.RoleID = 4;
              
            try
            {
                entity = dal.Upsert(entity);

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
