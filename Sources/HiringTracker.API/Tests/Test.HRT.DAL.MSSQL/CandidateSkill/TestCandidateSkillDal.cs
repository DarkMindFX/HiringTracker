

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
    public class TestCandidateSkillDal : TestBase
    {
        [Test]
        public void DalInit_Success()
        {
            IConfiguration config = GetConfiguration();
            var initParams = config.GetSection("DALInitParams").Get<TestDalInitParams>();

            ICandidateSkillDal dal = new CandidateSkillDal();
            var dalInitParams = dal.CreateInitParams();
            dalInitParams.Parameters["ConnectionString"] = initParams.ConnectionString;
            dal.Init(dalInitParams);
        }

        [Test]
        public void CandidateSkill_GetAll_Success()
        {
            var dal = PrepareCandidateSkillDal("DALInitParams");

            IList<CandidateSkill> entities = dal.GetAll();

            Assert.IsNotNull(entities);
            Assert.IsNotEmpty(entities);
        }

        [TestCase("CandidateSkill\\000.GetDetails.Success")]
        public void CandidateSkill_GetDetails_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareCandidateSkillDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
            var paramCandidateID = (System.Int64)objIds[0];
            var paramSkillID = (System.Int64)objIds[1];
            CandidateSkill entity = dal.Get(paramCandidateID, paramSkillID);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
            Assert.IsNotNull(entity.CandidateID);
            Assert.IsNotNull(entity.SkillID);

            Assert.AreEqual(110127, entity.CandidateID);
            Assert.AreEqual(18, entity.SkillID);
            Assert.AreEqual(4, entity.SkillProficiencyID);
        }

        [Test]
        public void CandidateSkill_GetDetails_InvalidId()
        {
            long id = Int32.MaxValue - 1;
            var dal = PrepareCandidateSkillDal("DALInitParams");

            CandidateSkill entity = dal.Get(id, id);

            Assert.IsNull(entity);
        }

        [TestCase("CandidateSkill\\010.Delete.Success")]
        public void CandidateSkill_Delete_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareCandidateSkillDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
            var paramCandidateID = (System.Int64)objIds[0];
            var paramSkillID = (System.Int64)objIds[1];

            bool removed = dal.Delete(paramCandidateID, paramSkillID);

            TeardownCase(conn, caseName);

            Assert.IsTrue(removed);
        }

        [Test]
        public void CandidateSkill_Delete_InvalidId()
        {
            long id = Int32.MaxValue - 1;
            var dal = PrepareCandidateSkillDal("DALInitParams");

            bool removed = dal.Delete(id, id);
            Assert.IsFalse(removed);

        }

        [TestCase("CandidateSkill\\020.Insert.Success")]
        public void CandidateSkill_Insert_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            SetupCase(conn, caseName);

            var dal = PrepareCandidateSkillDal("DALInitParams");

            var entity = new CandidateSkill();
            entity.CandidateID = 110125;
            entity.SkillID = 16;
            entity.SkillProficiencyID = 1;

            entity = dal.Upsert(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
            Assert.IsNotNull(entity.CandidateID);
            Assert.IsNotNull(entity.SkillID);

            Assert.AreEqual(110125, entity.CandidateID);
            Assert.AreEqual(16, entity.SkillID);
            Assert.AreEqual(1, entity.SkillProficiencyID);

        }

        [TestCase("CandidateSkill\\030.Update.Success")]
        public void CandidateSkill_Update_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareCandidateSkillDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
            var paramCandidateID = (System.Int64)objIds[0];
            var paramSkillID = (System.Int64)objIds[1];


            var entity = dal.Get(paramCandidateID, paramSkillID);
            entity.CandidateID = 100006;
            entity.SkillID = 14;
            entity.SkillProficiencyID = 3;

            entity = dal.Upsert(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
            Assert.IsNotNull(entity.CandidateID);
            Assert.IsNotNull(entity.SkillID);

            Assert.AreEqual(100006, entity.CandidateID);
            Assert.AreEqual(14, entity.SkillID);
            Assert.AreEqual(3, entity.SkillProficiencyID);

        }

        [Test]
        public void CandidateSkill_Update_InvalidId()
        {
            var dal = PrepareCandidateSkillDal("DALInitParams");

            var entity = new CandidateSkill();
            entity.CandidateID = Int64.MaxValue - 1;
            entity.SkillID = 14;
            entity.SkillProficiencyID = 3;

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

        protected ICandidateSkillDal PrepareCandidateSkillDal(string configName)
        {
            IConfiguration config = GetConfiguration();
            var initParams = config.GetSection(configName).Get<TestDalInitParams>();

            ICandidateSkillDal dal = new CandidateSkillDal();
            var dalInitParams = dal.CreateInitParams();
            dalInitParams.Parameters["ConnectionString"] = initParams.ConnectionString;
            dal.Init(dalInitParams);

            return dal;
        }
    }
}
