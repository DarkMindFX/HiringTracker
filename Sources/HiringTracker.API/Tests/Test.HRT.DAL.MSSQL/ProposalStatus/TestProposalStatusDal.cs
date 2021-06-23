

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

            object objId = SetupCase(conn, caseName);
            long id = (long)objId;

            ProposalStatus entity = dal.Get(id);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual("Name 23df314cee204d75970a5aa1b0e7c6c1", entity.Name);
                      }

        [Test]
        public void ProposalStatus_GetDetails_InvalidId()
        {
            long id = Int32.MaxValue - 1;
            var dal = PrepareProposalStatusDal("DALInitParams");

            ProposalStatus entity = dal.Get(id);

            Assert.IsNull(entity);
        }

        [TestCase("ProposalStatus\\010.Delete.Success")]
        public void ProposalStatus_Delete_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareProposalStatusDal("DALInitParams");

            object objId = SetupCase(conn, caseName);
            long id = (long)objId;

            bool removed = dal.Delete(id);

            TeardownCase(conn, caseName);

            Assert.IsTrue(removed);
        }

        [Test]
        public void ProposalStatus_Delete_InvalidId()
        {
            long positionId = Int32.MaxValue - 1;
            var dal = PrepareProposalStatusDal("DALInitParams");

            bool removed = dal.Delete(positionId);
            Assert.IsFalse(removed);

        }

        [TestCase("ProposalStatus\\020.Insert.Success")]
        public void ProposalStatus_Insert_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            SetupCase(conn, caseName);

            var dal = PrepareProposalStatusDal("DALInitParams");

            var entity = new ProposalStatus();
                          entity.Name = "Name c8fb74fc9ee84a2da8e4fcadcf967619";
                          
            entity = dal.Upsert(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual("Name c8fb74fc9ee84a2da8e4fcadcf967619", entity.Name);
              
        }

        [TestCase("ProposalStatus\\030.Update.Success")]
        public void ProposalStatus_Update_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareProposalStatusDal("DALInitParams");

            object objId = SetupCase(conn, caseName);
            long id = (long)objId;

            var entity = dal.Get(id);
                          entity.Name = "Name 6b75412678f14c76960e6248952b3b4e";
              
            entity = dal.Upsert(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual("Name 6b75412678f14c76960e6248952b3b4e", entity.Name);
              
        }

        [Test]
        public void ProposalStatus_Update_InvalidId()
        {
            var dal = PrepareProposalStatusDal("DALInitParams");

            var entity = new ProposalStatus();
            entity.ID = Int64.MaxValue - 1;
                          entity.Name = "Name 6b75412678f14c76960e6248952b3b4e";
              
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
