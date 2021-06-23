

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
    public class TestPositionSkillDal : TestBase
    {
        [Test]
        public void DalInit_Success()
        {
            IConfiguration config = GetConfiguration();
            var initParams = config.GetSection("DALInitParams").Get<TestDalInitParams>();

            IPositionSkillDal dal = new PositionSkillDal();
            var dalInitParams = dal.CreateInitParams();
            dalInitParams.Parameters["ConnectionString"] = initParams.ConnectionString;
            dal.Init(dalInitParams);
        }

        [Test]
        public void PositionSkill_GetAll_Success()
        {
            var dal = PreparePositionSkillDal("DALInitParams");

            IList<PositionSkill> entities = dal.GetAll();

            Assert.IsNotNull(entities);
            Assert.IsNotEmpty(entities);
        }

        [TestCase("PositionSkill\\000.GetDetails.Success")]
        public void PositionSkill_GetDetails_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PreparePositionSkillDal("DALInitParams");

            object objId = SetupCase(conn, caseName);
            long id = (long)objId;

            PositionSkill entity = dal.Get(id);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual(100002, entity.PositionID);
                            Assert.AreEqual(3, entity.SkillID);
                            Assert.AreEqual(false, entity.IsMandatory);
                            Assert.AreEqual(3, entity.SkillProficiencyID);
                      }

        [Test]
        public void PositionSkill_GetDetails_InvalidId()
        {
            long id = Int32.MaxValue - 1;
            var dal = PreparePositionSkillDal("DALInitParams");

            PositionSkill entity = dal.Get(id);

            Assert.IsNull(entity);
        }

        [TestCase("PositionSkill\\010.Delete.Success")]
        public void PositionSkill_Delete_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PreparePositionSkillDal("DALInitParams");

            object objId = SetupCase(conn, caseName);
            long id = (long)objId;

            bool removed = dal.Delete(id);

            TeardownCase(conn, caseName);

            Assert.IsTrue(removed);
        }

        [Test]
        public void PositionSkill_Delete_InvalidId()
        {
            long positionId = Int32.MaxValue - 1;
            var dal = PreparePositionSkillDal("DALInitParams");

            bool removed = dal.Delete(positionId);
            Assert.IsFalse(removed);

        }

        [TestCase("PositionSkill\\020.Insert.Success")]
        public void PositionSkill_Insert_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            SetupCase(conn, caseName);

            var dal = PreparePositionSkillDal("DALInitParams");

            var entity = new PositionSkill();
                          entity.PositionID = 100005;
                            entity.SkillID = 12;
                            entity.IsMandatory = true;              
                            entity.SkillProficiencyID = 2;
                          
            entity = dal.Upsert(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual(100005, entity.PositionID);
                            Assert.AreEqual(12, entity.SkillID);
                            Assert.AreEqual(false, entity.IsMandatory);
                            Assert.AreEqual(2, entity.SkillProficiencyID);
              
        }

        [TestCase("PositionSkill\\030.Update.Success")]
        public void PositionSkill_Update_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PreparePositionSkillDal("DALInitParams");

            object objId = SetupCase(conn, caseName);
            long id = (long)objId;

            var entity = dal.Get(id);
                          entity.PositionID = 100001;
                            entity.SkillID = 5;
                            entity.IsMandatory = true;              
                            entity.SkillProficiencyID = 4;
              
            entity = dal.Upsert(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual(100001, entity.PositionID);
                            Assert.AreEqual(5, entity.SkillID);
                            Assert.AreEqual(false, entity.IsMandatory);
                            Assert.AreEqual(4, entity.SkillProficiencyID);
              
        }

        [Test]
        public void PositionSkill_Update_InvalidId()
        {
            var dal = PreparePositionSkillDal("DALInitParams");

            var entity = new PositionSkill();
            entity.ID = Int64.MaxValue - 1;
                          entity.PositionID = 100001;
                            entity.SkillID = 5;
                            entity.IsMandatory = true;              
                            entity.SkillProficiencyID = 4;
              
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

        protected IPositionSkillDal PreparePositionSkillDal(string configName)
        {
            IConfiguration config = GetConfiguration();
            var initParams = config.GetSection(configName).Get<TestDalInitParams>();

            IPositionSkillDal dal = new PositionSkillDal();
            var dalInitParams = dal.CreateInitParams();
            dalInitParams.Parameters["ConnectionString"] = initParams.ConnectionString;
            dal.Init(dalInitParams);

            return dal;
        }
    }
}
