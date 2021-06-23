

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

            object objId = SetupCase(conn, caseName);
            long id = (long)objId;

            InterviewType entity = dal.Get(id);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual(956372, entity.ID);
                            Assert.AreEqual("Name 5300b0279af04ce6b7b90ceb1505c765", entity.Name);
                      }

        [Test]
        public void InterviewType_GetDetails_InvalidId()
        {
            long id = Int32.MaxValue - 1;
            var dal = PrepareInterviewTypeDal("DALInitParams");

            InterviewType entity = dal.Get(id);

            Assert.IsNull(entity);
        }

        [TestCase("InterviewType\\010.Delete.Success")]
        public void InterviewType_Delete_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareInterviewTypeDal("DALInitParams");

            object objId = SetupCase(conn, caseName);
            long id = (long)objId;

            bool removed = dal.Delete(id);

            TeardownCase(conn, caseName);

            Assert.IsTrue(removed);
        }

        [Test]
        public void InterviewType_Delete_InvalidId()
        {
            long positionId = Int32.MaxValue - 1;
            var dal = PrepareInterviewTypeDal("DALInitParams");

            bool removed = dal.Delete(positionId);
            Assert.IsFalse(removed);

        }

        [TestCase("InterviewType\\020.Insert.Success")]
        public void InterviewType_Insert_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            SetupCase(conn, caseName);

            var dal = PrepareInterviewTypeDal("DALInitParams");

            var entity = new InterviewType();
                          entity.ID = 956372;
                            entity.Name = "Name dcfba075144d44ed86a8262c6caf8b2b";
                          
            entity = dal.Upsert(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual(956372, entity.ID);
                            Assert.AreEqual("Name dcfba075144d44ed86a8262c6caf8b2b", entity.Name);
              
        }

        [TestCase("InterviewType\\030.Update.Success")]
        public void InterviewType_Update_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareInterviewTypeDal("DALInitParams");

            object objId = SetupCase(conn, caseName);
            long id = (long)objId;

            var entity = dal.Get(id);
                          entity.ID = 956372;
                            entity.Name = "Name 56adea82142a475ab0ce362e55ca7753";
              
            entity = dal.Upsert(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual(956372, entity.ID);
                            Assert.AreEqual("Name 56adea82142a475ab0ce362e55ca7753", entity.Name);
              
        }

        [Test]
        public void InterviewType_Update_InvalidId()
        {
            var dal = PrepareInterviewTypeDal("DALInitParams");

            var entity = new InterviewType();
            entity.ID = Int64.MaxValue - 1;
                          entity.ID = 956372;
                            entity.Name = "Name 56adea82142a475ab0ce362e55ca7753";
              
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
