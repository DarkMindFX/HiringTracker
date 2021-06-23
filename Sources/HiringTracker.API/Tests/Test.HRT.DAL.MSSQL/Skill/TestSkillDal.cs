

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
    public class TestSkillDal : TestBase
    {
        [Test]
        public void DalInit_Success()
        {
            IConfiguration config = GetConfiguration();
            var initParams = config.GetSection("DALInitParams").Get<TestDalInitParams>();

            ISkillDal dal = new SkillDal();
            var dalInitParams = dal.CreateInitParams();
            dalInitParams.Parameters["ConnectionString"] = initParams.ConnectionString;
            dal.Init(dalInitParams);
        }

        [Test]
        public void Skill_GetAll_Success()
        {
            var dal = PrepareSkillDal("DALInitParams");

            IList<Skill> entities = dal.GetAll();

            Assert.IsNotNull(entities);
            Assert.IsNotEmpty(entities);
        }

        [TestCase("Skill\\000.GetDetails.Success")]
        public void Skill_GetDetails_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareSkillDal("DALInitParams");

            object objId = SetupCase(conn, caseName);
            long id = (long)objId;

            Skill entity = dal.Get(id);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual("Name 0be48494d3d94f13987483bd59477426", entity.Name);
                      }

        [Test]
        public void Skill_GetDetails_InvalidId()
        {
            long id = Int32.MaxValue - 1;
            var dal = PrepareSkillDal("DALInitParams");

            Skill entity = dal.Get(id);

            Assert.IsNull(entity);
        }

        [TestCase("Skill\\010.Delete.Success")]
        public void Skill_Delete_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareSkillDal("DALInitParams");

            object objId = SetupCase(conn, caseName);
            long id = (long)objId;

            bool removed = dal.Delete(id);

            TeardownCase(conn, caseName);

            Assert.IsTrue(removed);
        }

        [Test]
        public void Skill_Delete_InvalidId()
        {
            long positionId = Int32.MaxValue - 1;
            var dal = PrepareSkillDal("DALInitParams");

            bool removed = dal.Delete(positionId);
            Assert.IsFalse(removed);

        }

        [TestCase("Skill\\020.Insert.Success")]
        public void Skill_Insert_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            SetupCase(conn, caseName);

            var dal = PrepareSkillDal("DALInitParams");

            var entity = new Skill();
                          entity.Name = "Name e503e2d6efa04ecc9e656548c852fc0b";
                          
            entity = dal.Upsert(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual("Name e503e2d6efa04ecc9e656548c852fc0b", entity.Name);
              
        }

        [TestCase("Skill\\030.Update.Success")]
        public void Skill_Update_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareSkillDal("DALInitParams");

            object objId = SetupCase(conn, caseName);
            long id = (long)objId;

            var entity = dal.Get(id);
                          entity.Name = "Name ed81ccbcc90a4d029ca20f86dd44ae9a";
              
            entity = dal.Upsert(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual("Name ed81ccbcc90a4d029ca20f86dd44ae9a", entity.Name);
              
        }

        [Test]
        public void Skill_Update_InvalidId()
        {
            var dal = PrepareSkillDal("DALInitParams");

            var entity = new Skill();
            entity.ID = Int64.MaxValue - 1;
                          entity.Name = "Name ed81ccbcc90a4d029ca20f86dd44ae9a";
              
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

        protected ISkillDal PrepareSkillDal(string configName)
        {
            IConfiguration config = GetConfiguration();
            var initParams = config.GetSection(configName).Get<TestDalInitParams>();

            ISkillDal dal = new SkillDal();
            var dalInitParams = dal.CreateInitParams();
            dalInitParams.Parameters["ConnectionString"] = initParams.ConnectionString;
            dal.Init(dalInitParams);

            return dal;
        }
    }
}
