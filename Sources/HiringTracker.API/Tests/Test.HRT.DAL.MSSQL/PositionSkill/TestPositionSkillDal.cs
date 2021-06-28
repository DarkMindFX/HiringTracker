

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

            IList<object> objIds = SetupCase(conn, caseName);
                var paramPositionID = (System.Int64)objIds[0];
                var paramSkillID = (System.Int64)objIds[1];
            PositionSkill entity = dal.Get(paramPositionID,paramSkillID);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.PositionID);
                        Assert.IsNotNull(entity.SkillID);
            
                          Assert.AreEqual(100009, entity.PositionID);
                            Assert.AreEqual(13, entity.SkillID);
                            Assert.AreEqual(true, entity.IsMandatory);
                            Assert.AreEqual(801046, entity.SkillProficiencyID);
                      }

        [Test]
        public void PositionSkill_GetDetails_InvalidId()
        {
                var paramPositionID = Int64.MaxValue - 1;
                var paramSkillID = Int64.MaxValue - 1;
            var dal = PreparePositionSkillDal("DALInitParams");

            PositionSkill entity = dal.Get(paramPositionID,paramSkillID);

            Assert.IsNull(entity);
        }

        [TestCase("PositionSkill\\010.Delete.Success")]
        public void PositionSkill_Delete_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PreparePositionSkillDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
                var paramPositionID = (System.Int64)objIds[0];
                var paramSkillID = (System.Int64)objIds[1];
            bool removed = dal.Delete(paramPositionID,paramSkillID);

            TeardownCase(conn, caseName);

            Assert.IsTrue(removed);
        }

        [Test]
        public void PositionSkill_Delete_InvalidId()
        {
            var dal = PreparePositionSkillDal("DALInitParams");
                var paramPositionID = Int64.MaxValue - 1;
                var paramSkillID = Int64.MaxValue - 1;
   
            bool removed = dal.Delete(paramPositionID,paramSkillID);
            Assert.IsFalse(removed);

        }

        [TestCase("PositionSkill\\020.Insert.Success")]
        public void PositionSkill_Insert_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            SetupCase(conn, caseName);

            var dal = PreparePositionSkillDal("DALInitParams");

            var entity = new PositionSkill();
                          entity.PositionID = 100008;
                            entity.SkillID = 1;
                            entity.IsMandatory = true;              
                            entity.SkillProficiencyID = 801046;
                          
            entity = dal.Insert(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.PositionID);
                        Assert.IsNotNull(entity.SkillID);
            
                          Assert.AreEqual(100008, entity.PositionID);
                            Assert.AreEqual(1, entity.SkillID);
                            Assert.AreEqual(true, entity.IsMandatory);
                            Assert.AreEqual(801046, entity.SkillProficiencyID);
              
        }

        [TestCase("PositionSkill\\030.Update.Success")]
        public void PositionSkill_Update_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PreparePositionSkillDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
                var paramPositionID = (System.Int64)objIds[0];
                var paramSkillID = (System.Int64)objIds[1];
            PositionSkill entity = dal.Get(paramPositionID,paramSkillID);

                          entity.IsMandatory = true;              
                            entity.SkillProficiencyID = 3;
              
            entity = dal.Update(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.PositionID);
                        Assert.IsNotNull(entity.SkillID);
            
                          Assert.AreEqual(100003, entity.PositionID);
                            Assert.AreEqual(1, entity.SkillID);
                            Assert.AreEqual(true, entity.IsMandatory);
                            Assert.AreEqual(3, entity.SkillProficiencyID);
              
        }

        [Test]
        public void PositionSkill_Update_InvalidId()
        {
            var dal = PreparePositionSkillDal("DALInitParams");

            var entity = new PositionSkill();
                          entity.PositionID = 100003;
                            entity.SkillID = 1;
                            entity.IsMandatory = true;              
                            entity.SkillProficiencyID = 3;
              
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
