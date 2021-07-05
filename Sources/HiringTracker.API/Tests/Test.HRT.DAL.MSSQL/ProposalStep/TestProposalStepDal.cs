

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

            IList<object> objIds = SetupCase(conn, caseName);
                var paramID = (System.Int64?)objIds[0];
            ProposalStep entity = dal.Get(paramID);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual("Name cec36ff822884c6ab8b3435c1e765507", entity.Name);
                            Assert.AreEqual(false, entity.ReqDueDate);
                            Assert.AreEqual(504, entity.RequiresRespInDays);
                      }

        [Test]
        public void ProposalStep_GetDetails_InvalidId()
        {
                var paramID = Int64.MaxValue - 1;
            var dal = PrepareProposalStepDal("DALInitParams");

            ProposalStep entity = dal.Get(paramID);

            Assert.IsNull(entity);
        }

        [TestCase("ProposalStep\\010.Delete.Success")]
        public void ProposalStep_Delete_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareProposalStepDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
                var paramID = (System.Int64?)objIds[0];
            bool removed = dal.Delete(paramID);

            TeardownCase(conn, caseName);

            Assert.IsTrue(removed);
        }

        [Test]
        public void ProposalStep_Delete_InvalidId()
        {
            var dal = PrepareProposalStepDal("DALInitParams");
                var paramID = Int64.MaxValue - 1;
   
            bool removed = dal.Delete(paramID);
            Assert.IsFalse(removed);

        }

        [TestCase("ProposalStep\\020.Insert.Success")]
        public void ProposalStep_Insert_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            SetupCase(conn, caseName);

            var dal = PrepareProposalStepDal("DALInitParams");

            var entity = new ProposalStep();
                          entity.Name = "Name 68fdcfe45dc5487f9a4149725bddd83f";
                            entity.ReqDueDate = false;              
                            entity.RequiresRespInDays = 504;
                          
            entity = dal.Insert(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual("Name 68fdcfe45dc5487f9a4149725bddd83f", entity.Name);
                            Assert.AreEqual(false, entity.ReqDueDate);
                            Assert.AreEqual(504, entity.RequiresRespInDays);
              
        }

        [TestCase("ProposalStep\\030.Update.Success")]
        public void ProposalStep_Update_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareProposalStepDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
                var paramID = (System.Int64?)objIds[0];
            ProposalStep entity = dal.Get(paramID);

                          entity.Name = "Name bdf696d8afbc4e39b2334acb9fd65a0b";
                            entity.ReqDueDate = false;              
                            entity.RequiresRespInDays = 504;
              
            entity = dal.Update(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual("Name bdf696d8afbc4e39b2334acb9fd65a0b", entity.Name);
                            Assert.AreEqual(false, entity.ReqDueDate);
                            Assert.AreEqual(504, entity.RequiresRespInDays);
              
        }

        [Test]
        public void ProposalStep_Update_InvalidId()
        {
            var dal = PrepareProposalStepDal("DALInitParams");

            var entity = new ProposalStep();
                          entity.Name = "Name bdf696d8afbc4e39b2334acb9fd65a0b";
                            entity.ReqDueDate = false;              
                            entity.RequiresRespInDays = 504;
              
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
