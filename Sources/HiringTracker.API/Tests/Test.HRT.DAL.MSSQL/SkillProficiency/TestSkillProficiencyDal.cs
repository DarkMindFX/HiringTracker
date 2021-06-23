

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

            object objId = SetupCase(conn, caseName);
            long id = (long)objId;

            SkillProficiency entity = dal.Get(id);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual(400693, entity.ID);
                            Assert.AreEqual("Name ef6b79fa3f3e4f8ab26affa754a16fb2", entity.Name);
                      }

        [Test]
        public void SkillProficiency_GetDetails_InvalidId()
        {
            long id = Int32.MaxValue - 1;
            var dal = PrepareSkillProficiencyDal("DALInitParams");

            SkillProficiency entity = dal.Get(id);

            Assert.IsNull(entity);
        }

        [TestCase("SkillProficiency\\010.Delete.Success")]
        public void SkillProficiency_Delete_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareSkillProficiencyDal("DALInitParams");

            object objId = SetupCase(conn, caseName);
            long id = (long)objId;

            bool removed = dal.Delete(id);

            TeardownCase(conn, caseName);

            Assert.IsTrue(removed);
        }

        [Test]
        public void SkillProficiency_Delete_InvalidId()
        {
            long positionId = Int32.MaxValue - 1;
            var dal = PrepareSkillProficiencyDal("DALInitParams");

            bool removed = dal.Delete(positionId);
            Assert.IsFalse(removed);

        }

        [TestCase("SkillProficiency\\020.Insert.Success")]
        public void SkillProficiency_Insert_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            SetupCase(conn, caseName);

            var dal = PrepareSkillProficiencyDal("DALInitParams");

            var entity = new SkillProficiency();
                          entity.ID = 400693;
                            entity.Name = "Name 60c858c33a604d87bb041b0d8a003211";
                          
            entity = dal.Upsert(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual(400693, entity.ID);
                            Assert.AreEqual("Name 60c858c33a604d87bb041b0d8a003211", entity.Name);
              
        }

        [TestCase("SkillProficiency\\030.Update.Success")]
        public void SkillProficiency_Update_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareSkillProficiencyDal("DALInitParams");

            object objId = SetupCase(conn, caseName);
            long id = (long)objId;

            var entity = dal.Get(id);
                          entity.ID = 400693;
                            entity.Name = "Name 5c6d5d8d20404ebea81f9447d37cc15c";
              
            entity = dal.Upsert(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual(400693, entity.ID);
                            Assert.AreEqual("Name 5c6d5d8d20404ebea81f9447d37cc15c", entity.Name);
              
        }

        [Test]
        public void SkillProficiency_Update_InvalidId()
        {
            var dal = PrepareSkillProficiencyDal("DALInitParams");

            var entity = new SkillProficiency();
            entity.ID = Int64.MaxValue - 1;
                          entity.ID = 400693;
                            entity.Name = "Name 5c6d5d8d20404ebea81f9447d37cc15c";
              
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
