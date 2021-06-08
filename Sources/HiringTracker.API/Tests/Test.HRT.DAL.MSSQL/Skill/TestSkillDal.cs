using HRT.DAL.MSSQL;
using HRT.Interfaces;
using HRT.Interfaces.Entities;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

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
        public void GetSkills_Success()
        {
            var dal = PrepareSkillDal("DALInitParams");

            IList<Skill> entities = dal.GetAll();

            Assert.IsNotNull(entities);
            Assert.IsNotEmpty(entities);
        }

        [TestCase("Skill\\000.GetDetails.Success")]
        public void GetSkill_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareSkillDal("DALInitParams");

            object objId = SetupCase(conn, caseName);
            long id = (long)objId;

            Skill entity = dal.Get(id);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
            Assert.AreNotEqual(0, entity.ID);
		Assert.AreEqual("Name 7b2c1b14765242e88f0a738970ae8287", entity.Name);
		
        }

        [Test]
        public void GetSkill_InvalidId()
        {
            long id = Int32.MaxValue - 1;
            var dal = PrepareSkillDal("DALInitParams");

            Skill entity = dal.Get(id);

            Assert.IsNull(entity);
        }

        [TestCase("Skill\\010.Delete.Success")]
        public void DeleteSkill_Success(string caseName)
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
        public void DeleteSkill_InvalidId()
        {
            long positionId = Int32.MaxValue - 1;
            var dal = PrepareSkillDal("DALInitParams");

            bool removed = dal.Delete(positionId);
            Assert.IsFalse(removed);

        }

        [TestCase("Skill\\020.Insert.Success")]
        public void InsertSkill_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            SetupCase(conn, caseName);

            var dal = PrepareSkillDal("DALInitParams");

            var entity = new Skill();
            entity.Name = "Name a22ab4c536d64db5b04688c3392a73e8";
		

            entity = dal.Upsert(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
            Assert.AreNotEqual(0, entity.ID);
		Assert.AreEqual("Name a22ab4c536d64db5b04688c3392a73e8", entity.Name);
		
        }

        [TestCase("Skill\\030.Update.Success")]
        public void UpdateSkill_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareSkillDal("DALInitParams");

            object objId = SetupCase(conn, caseName);
            long id = (long)objId;

            var entity = dal.Get(id);
            entity.Name = "Name 3eace487dea8457b90004271cbc4c3b2";
		

            entity = dal.Upsert(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
            Assert.AreNotEqual(0, entity.ID);
		Assert.AreEqual("Name 3eace487dea8457b90004271cbc4c3b2", entity.Name);
		
        }

        [Test]
        public void UpdateSkill_InvalidId()
        {
            var dal = PrepareSkillDal("DALInitParams");

            var entity = new Skill();
            entity.ID = Int64.MaxValue - 1;
            entity.Name = "Name 3eace487dea8457b90004271cbc4c3b2";
		

            try
            {
                entity = dal.Upsert(entity);

                Assert.Fail("Fail - exception was expected, but wasn't thrown.");
            }
            catch(Exception ex)
            {
                Assert.Pass("Success - exception thrown as expected");
            }
        }
    }
}
