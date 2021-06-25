

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

            Assert.AreEqual(100004, entity.CandidateID);
            Assert.AreEqual(13, entity.SkillID);
            Assert.AreEqual(1, entity.SkillProficiencyID);
        }

        [Test]
        public void CandidateSkill_GetDetails_InvalidId()
        {
            var paramCandidateID = Int64.MaxValue - 1;
            var paramSkillID = Int64.MaxValue - 1;
            var dal = PrepareCandidateSkillDal("DALInitParams");

            CandidateSkill entity = dal.Get(paramCandidateID, paramSkillID);

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
            var dal = PrepareCandidateSkillDal("DALInitParams");
            var paramCandidateID = Int64.MaxValue - 1;
            var paramSkillID = Int64.MaxValue - 1;

            bool removed = dal.Delete(paramCandidateID, paramSkillID);
            Assert.IsFalse(removed);

        }

        [TestCase("CandidateSkill\\020.Insert.Success")]
        public void CandidateSkill_Insert_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            SetupCase(conn, caseName);

            var dal = PrepareCandidateSkillDal("DALInitParams");

            var entity = new CandidateSkill();
            entity.CandidateID = 100006;
            entity.SkillID = 8;
            entity.SkillProficiencyID = 1;

            entity = dal.Insert(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
            Assert.IsNotNull(entity.CandidateID);
            Assert.IsNotNull(entity.SkillID);

            Assert.AreEqual(100006, entity.CandidateID);
            Assert.AreEqual(8, entity.SkillID);
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
            CandidateSkill entity = dal.Get(paramCandidateID, paramSkillID);

            entity.SkillProficiencyID = 1;

            entity = dal.Update(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
            Assert.IsNotNull(entity.CandidateID);
            Assert.IsNotNull(entity.SkillID);

            Assert.AreEqual(100005, entity.CandidateID);
            Assert.AreEqual(6, entity.SkillID);
            Assert.AreEqual(1, entity.SkillProficiencyID);

        }

        [Test]
        public void CandidateSkill_Update_InvalidId()
        {
            var dal = PrepareCandidateSkillDal("DALInitParams");

            var entity = new CandidateSkill();
            entity.CandidateID = 100005;
            entity.SkillID = 6;
            entity.SkillProficiencyID = 1;

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
