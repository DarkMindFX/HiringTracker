

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

            object objId = SetupCase(conn, caseName);
            long id = (long)objId;

            CandidateSkill entity = dal.Get(id);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.CandidateID);
                        Assert.IsNotNull(entity.SkillID);
            
                          Assert.AreEqual(100006, entity.CandidateID);
                            Assert.AreEqual(3, entity.SkillID);
                            Assert.AreEqual(1, entity.SkillProficiencyID);
                      }

        [Test]
        public void CandidateSkill_GetDetails_InvalidId()
        {
            long id = Int32.MaxValue - 1;
            var dal = PrepareCandidateSkillDal("DALInitParams");

            CandidateSkill entity = dal.Get(id);

            Assert.IsNull(entity);
        }

        [TestCase("CandidateSkill\\010.Delete.Success")]
        public void CandidateSkill_Delete_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareCandidateSkillDal("DALInitParams");

            object objId = SetupCase(conn, caseName);
            long id = (long)objId;

            bool removed = dal.Delete(id);

            TeardownCase(conn, caseName);

            Assert.IsTrue(removed);
        }

        [Test]
        public void CandidateSkill_Delete_InvalidId()
        {
            long positionId = Int32.MaxValue - 1;
            var dal = PrepareCandidateSkillDal("DALInitParams");

            bool removed = dal.Delete(positionId);
            Assert.IsFalse(removed);

        }

        [TestCase("CandidateSkill\\020.Insert.Success")]
        public void CandidateSkill_Insert_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            SetupCase(conn, caseName);

            var dal = PrepareCandidateSkillDal("DALInitParams");

            var entity = new CandidateSkill();
                          entity.CandidateID = 100008;
                            entity.SkillID = 17;
                            entity.SkillProficiencyID = 3;
                          
            entity = dal.Upsert(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.CandidateID);
                        Assert.IsNotNull(entity.SkillID);
            
                          Assert.AreEqual(100008, entity.CandidateID);
                            Assert.AreEqual(17, entity.SkillID);
                            Assert.AreEqual(3, entity.SkillProficiencyID);
              
        }

        [TestCase("CandidateSkill\\030.Update.Success")]
        public void CandidateSkill_Update_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareCandidateSkillDal("DALInitParams");

            object objId = SetupCase(conn, caseName);
            long id = (long)objId;

            var entity = dal.Get(id);
                          entity.CandidateID = 100002;
                            entity.SkillID = 5;
                            entity.SkillProficiencyID = 1;
              
            entity = dal.Upsert(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.CandidateID);
                        Assert.IsNotNull(entity.SkillID);
            
                          Assert.AreEqual(100002, entity.CandidateID);
                            Assert.AreEqual(5, entity.SkillID);
                            Assert.AreEqual(1, entity.SkillProficiencyID);
              
        }

        [Test]
        public void CandidateSkill_Update_InvalidId()
        {
            var dal = PrepareCandidateSkillDal("DALInitParams");

            var entity = new CandidateSkill();
            entity.ID = Int64.MaxValue - 1;
                          entity.CandidateID = 100002;
                            entity.SkillID = 5;
                            entity.SkillProficiencyID = 1;
              
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
