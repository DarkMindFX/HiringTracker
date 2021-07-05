

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
    public class TestProposalStatusDal : TestBase
    {
        [Test]
        public void DalInit_Success()
        {
            IConfiguration config = GetConfiguration();
            var initParams = config.GetSection("DALInitParams").Get<TestDalInitParams>();

            IProposalStatusDal dal = new ProposalStatusDal();
            var dalInitParams = dal.CreateInitParams();
            dalInitParams.Parameters["ConnectionString"] = initParams.ConnectionString;
            dal.Init(dalInitParams);
        }

        [Test]
        public void ProposalStatus_GetAll_Success()
        {
            var dal = PrepareProposalStatusDal("DALInitParams");

            IList<ProposalStatus> entities = dal.GetAll();

            Assert.IsNotNull(entities);
            Assert.IsNotEmpty(entities);
        }

        [TestCase("ProposalStatus\\000.GetDetails.Success")]
        public void ProposalStatus_GetDetails_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareProposalStatusDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
                var paramID = (System.Int64?)objIds[0];
            ProposalStatus entity = dal.Get(paramID);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual("Name 73cd2ce7e647449f9d4011be1bc53b92", entity.Name);
                      }

        [Test]
        public void ProposalStatus_GetDetails_InvalidId()
        {
                var paramID = Int64.MaxValue - 1;
            var dal = PrepareProposalStatusDal("DALInitParams");

            ProposalStatus entity = dal.Get(paramID);

            Assert.IsNull(entity);
        }

        [TestCase("ProposalStatus\\010.Delete.Success")]
        public void ProposalStatus_Delete_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareProposalStatusDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
                var paramID = (System.Int64?)objIds[0];
            bool removed = dal.Delete(paramID);

            TeardownCase(conn, caseName);

            Assert.IsTrue(removed);
        }

        [Test]
        public void ProposalStatus_Delete_InvalidId()
        {
            var dal = PrepareProposalStatusDal("DALInitParams");
                var paramID = Int64.MaxValue - 1;
   
            bool removed = dal.Delete(paramID);
            Assert.IsFalse(removed);

        }

        [TestCase("ProposalStatus\\020.Insert.Success")]
        public void ProposalStatus_Insert_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            SetupCase(conn, caseName);

            var dal = PrepareProposalStatusDal("DALInitParams");

            var entity = new ProposalStatus();
                          entity.Name = "Name 42b66d799e8a4a1588c63579625bd0a7";
                          
            entity = dal.Insert(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual("Name 42b66d799e8a4a1588c63579625bd0a7", entity.Name);
              
        }

        [TestCase("ProposalStatus\\030.Update.Success")]
        public void ProposalStatus_Update_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareProposalStatusDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
                var paramID = (System.Int64?)objIds[0];
            ProposalStatus entity = dal.Get(paramID);

                          entity.Name = "Name 4f21f0d918134d5eb10eebbb77ec6e01";
              
            entity = dal.Update(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual("Name 4f21f0d918134d5eb10eebbb77ec6e01", entity.Name);
              
        }

        [Test]
        public void ProposalStatus_Update_InvalidId()
        {
            var dal = PrepareProposalStatusDal("DALInitParams");

            var entity = new ProposalStatus();
                          entity.Name = "Name 4f21f0d918134d5eb10eebbb77ec6e01";
              
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

        protected IProposalStatusDal PrepareProposalStatusDal(string configName)
        {
            IConfiguration config = GetConfiguration();
            var initParams = config.GetSection(configName).Get<TestDalInitParams>();

            IProposalStatusDal dal = new ProposalStatusDal();
            var dalInitParams = dal.CreateInitParams();
            dalInitParams.Parameters["ConnectionString"] = initParams.ConnectionString;
            dal.Init(dalInitParams);

            return dal;
        }
    }
}
