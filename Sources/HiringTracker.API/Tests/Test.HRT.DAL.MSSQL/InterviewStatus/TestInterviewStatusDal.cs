

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

            object objId = SetupCase(conn, caseName);
            long id = (long)objId;

            InterviewStatus entity = dal.Get(id);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual("Name f2e23429f4904b22ba2fcc325ea37066", entity.Name);
                      }

        [Test]
        public void InterviewStatus_GetDetails_InvalidId()
        {
            long id = Int32.MaxValue - 1;
            var dal = PrepareInterviewStatusDal("DALInitParams");

            InterviewStatus entity = dal.Get(id);

            Assert.IsNull(entity);
        }

        [TestCase("InterviewStatus\\010.Delete.Success")]
        public void InterviewStatus_Delete_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareInterviewStatusDal("DALInitParams");

            object objId = SetupCase(conn, caseName);
            long id = (long)objId;

            bool removed = dal.Delete(id);

            TeardownCase(conn, caseName);

            Assert.IsTrue(removed);
        }

        [Test]
        public void InterviewStatus_Delete_InvalidId()
        {
            long positionId = Int32.MaxValue - 1;
            var dal = PrepareInterviewStatusDal("DALInitParams");

            bool removed = dal.Delete(positionId);
            Assert.IsFalse(removed);

        }

        [TestCase("InterviewStatus\\020.Insert.Success")]
        public void InterviewStatus_Insert_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            SetupCase(conn, caseName);

            var dal = PrepareInterviewStatusDal("DALInitParams");

            var entity = new InterviewStatus();
                          entity.Name = "Name 830c1a156beb4ab28577541b5dd31c90";
                          
            entity = dal.Upsert(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual("Name 830c1a156beb4ab28577541b5dd31c90", entity.Name);
              
        }

        [TestCase("InterviewStatus\\030.Update.Success")]
        public void InterviewStatus_Update_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareInterviewStatusDal("DALInitParams");

            object objId = SetupCase(conn, caseName);
            long id = (long)objId;

            var entity = dal.Get(id);
                          entity.Name = "Name 749d42d2ea8f497b8a4da0931c2bab63";
              
            entity = dal.Upsert(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual("Name 749d42d2ea8f497b8a4da0931c2bab63", entity.Name);
              
        }

        [Test]
        public void InterviewStatus_Update_InvalidId()
        {
            var dal = PrepareInterviewStatusDal("DALInitParams");

            var entity = new InterviewStatus();
            entity.ID = Int64.MaxValue - 1;
                          entity.Name = "Name 749d42d2ea8f497b8a4da0931c2bab63";
              
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
