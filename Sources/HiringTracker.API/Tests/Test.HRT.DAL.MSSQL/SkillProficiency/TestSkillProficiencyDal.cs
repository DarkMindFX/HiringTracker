

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
    public class TestSkillProficiencyDal : TestBase
    {
        [Test]
        public void DalInit_Success()
        {
            IConfiguration config = GetConfiguration();
            var initParams = config.GetSection("DALInitParams").Get<TestDalInitParams>();

            ISkillProficiencyDal dal = new SkillProficiencyDal();
            var dalInitParams = dal.CreateInitParams();
            dalInitParams.Parameters["ConnectionString"] = initParams.ConnectionString;
            dal.Init(dalInitParams);
        }

        [Test]
        public void SkillProficiency_GetAll_Success()
        {
            var dal = PrepareSkillProficiencyDal("DALInitParams");

            IList<SkillProficiency> entities = dal.GetAll();

            Assert.IsNotNull(entities);
            Assert.IsNotEmpty(entities);
        }

        [TestCase("SkillProficiency\\000.GetDetails.Success")]
        public void SkillProficiency_GetDetails_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareSkillProficiencyDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
                var paramID = (System.Int64)objIds[0];
            SkillProficiency entity = dal.Get(paramID);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual(488704, entity.ID);
                            Assert.AreEqual("Name 332cef211218429ca496f7e286482d46", entity.Name);
                      }

        [Test]
        public void SkillProficiency_GetDetails_InvalidId()
        {
                var paramID = Int64.MaxValue - 1;
            var dal = PrepareSkillProficiencyDal("DALInitParams");

            SkillProficiency entity = dal.Get(paramID);

            Assert.IsNull(entity);
        }

        [TestCase("SkillProficiency\\010.Delete.Success")]
        public void SkillProficiency_Delete_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareSkillProficiencyDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
                var paramID = (System.Int64)objIds[0];
            bool removed = dal.Delete(paramID);

            TeardownCase(conn, caseName);

            Assert.IsTrue(removed);
        }

        [Test]
        public void SkillProficiency_Delete_InvalidId()
        {
            var dal = PrepareSkillProficiencyDal("DALInitParams");
                var paramID = Int64.MaxValue - 1;
   
            bool removed = dal.Delete(paramID);
            Assert.IsFalse(removed);

        }

        [TestCase("SkillProficiency\\020.Insert.Success")]
        public void SkillProficiency_Insert_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            SetupCase(conn, caseName);

            var dal = PrepareSkillProficiencyDal("DALInitParams");

            var entity = new SkillProficiency();
                          entity.ID = 488704;
                            entity.Name = "Name 17cdd39a123a46f9952230db4818e1e5";
                          
            entity = dal.Insert(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual(488704, entity.ID);
                            Assert.AreEqual("Name 17cdd39a123a46f9952230db4818e1e5", entity.Name);
              
        }

        [TestCase("SkillProficiency\\030.Update.Success")]
        public void SkillProficiency_Update_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareSkillProficiencyDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
                var paramID = (System.Int64)objIds[0];
            SkillProficiency entity = dal.Get(paramID);

                          entity.Name = "Name 0a73fd4353934ecb81a8f2dc0f505d4c";
              
            entity = dal.Update(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual(488704, entity.ID);
                            Assert.AreEqual("Name 0a73fd4353934ecb81a8f2dc0f505d4c", entity.Name);
              
        }

        [Test]
        public void SkillProficiency_Update_InvalidId()
        {
            var dal = PrepareSkillProficiencyDal("DALInitParams");

            var entity = new SkillProficiency();
                          entity.ID = 488704;
                            entity.Name = "Name 0a73fd4353934ecb81a8f2dc0f505d4c";
              
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

        protected ISkillProficiencyDal PrepareSkillProficiencyDal(string configName)
        {
            IConfiguration config = GetConfiguration();
            var initParams = config.GetSection(configName).Get<TestDalInitParams>();

            ISkillProficiencyDal dal = new SkillProficiencyDal();
            var dalInitParams = dal.CreateInitParams();
            dalInitParams.Parameters["ConnectionString"] = initParams.ConnectionString;
            dal.Init(dalInitParams);

            return dal;
        }
    }
}
