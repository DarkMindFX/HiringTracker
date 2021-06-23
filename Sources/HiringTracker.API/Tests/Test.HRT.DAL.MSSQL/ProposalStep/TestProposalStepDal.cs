

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
    public class TestProposalStepDal : TestBase
    {
        [Test]
        public void DalInit_Success()
        {
            IConfiguration config = GetConfiguration();
            var initParams = config.GetSection("DALInitParams").Get<TestDalInitParams>();

            IProposalStepDal dal = new ProposalStepDal();
            var dalInitParams = dal.CreateInitParams();
            dalInitParams.Parameters["ConnectionString"] = initParams.ConnectionString;
            dal.Init(dalInitParams);
        }

        [Test]
        public void ProposalStep_GetAll_Success()
        {
            var dal = PrepareProposalStepDal("DALInitParams");

            IList<ProposalStep> entities = dal.GetAll();

            Assert.IsNotNull(entities);
            Assert.IsNotEmpty(entities);
        }

        [TestCase("ProposalStep\\000.GetDetails.Success")]
        public void ProposalStep_GetDetails_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareProposalStepDal("DALInitParams");

            object objId = SetupCase(conn, caseName);
            long id = (long)objId;

            ProposalStep entity = dal.Get(id);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual("Name 8287fdb31762473daa8e4b60c4f2831f", entity.Name);
                            Assert.AreEqual(true, entity.ReqDueDate);
                            Assert.AreEqual(190, entity.RequiresRespInDays);
                      }

        [Test]
        public void ProposalStep_GetDetails_InvalidId()
        {
            long id = Int32.MaxValue - 1;
            var dal = PrepareProposalStepDal("DALInitParams");

            ProposalStep entity = dal.Get(id);

            Assert.IsNull(entity);
        }

        [TestCase("ProposalStep\\010.Delete.Success")]
        public void ProposalStep_Delete_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareProposalStepDal("DALInitParams");

            object objId = SetupCase(conn, caseName);
            long id = (long)objId;

            bool removed = dal.Delete(id);

            TeardownCase(conn, caseName);

            Assert.IsTrue(removed);
        }

        [Test]
        public void ProposalStep_Delete_InvalidId()
        {
            long positionId = Int32.MaxValue - 1;
            var dal = PrepareProposalStepDal("DALInitParams");

            bool removed = dal.Delete(positionId);
            Assert.IsFalse(removed);

        }

        [TestCase("ProposalStep\\020.Insert.Success")]
        public void ProposalStep_Insert_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            SetupCase(conn, caseName);

            var dal = PrepareProposalStepDal("DALInitParams");

            var entity = new ProposalStep();
                          entity.Name = "Name f41a2f4bd35e4cf6a3927b8835b92221";
                            entity.ReqDueDate = true;              
                            entity.RequiresRespInDays = 190;
                          
            entity = dal.Upsert(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual("Name f41a2f4bd35e4cf6a3927b8835b92221", entity.Name);
                            Assert.AreEqual(true, entity.ReqDueDate);
                            Assert.AreEqual(190, entity.RequiresRespInDays);
              
        }

        [TestCase("ProposalStep\\030.Update.Success")]
        public void ProposalStep_Update_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareProposalStepDal("DALInitParams");

            object objId = SetupCase(conn, caseName);
            long id = (long)objId;

            var entity = dal.Get(id);
                          entity.Name = "Name 71e90b29674b46ea95de3922703c6660";
                            entity.ReqDueDate = true;              
                            entity.RequiresRespInDays = 190;
              
            entity = dal.Upsert(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual("Name 71e90b29674b46ea95de3922703c6660", entity.Name);
                            Assert.AreEqual(true, entity.ReqDueDate);
                            Assert.AreEqual(190, entity.RequiresRespInDays);
              
        }

        [Test]
        public void ProposalStep_Update_InvalidId()
        {
            var dal = PrepareProposalStepDal("DALInitParams");

            var entity = new ProposalStep();
            entity.ID = Int64.MaxValue - 1;
                          entity.Name = "Name 71e90b29674b46ea95de3922703c6660";
                            entity.ReqDueDate = true;              
                            entity.RequiresRespInDays = 190;
              
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

        protected IProposalStepDal PrepareProposalStepDal(string configName)
        {
            IConfiguration config = GetConfiguration();
            var initParams = config.GetSection(configName).Get<TestDalInitParams>();

            IProposalStepDal dal = new ProposalStepDal();
            var dalInitParams = dal.CreateInitParams();
            dalInitParams.Parameters["ConnectionString"] = initParams.ConnectionString;
            dal.Init(dalInitParams);

            return dal;
        }
    }
}
