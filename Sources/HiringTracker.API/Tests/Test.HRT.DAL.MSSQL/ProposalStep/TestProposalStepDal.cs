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
        public void GetProposalSteps_Success()
        {
            var dal = PrepareProposalStepDal("DALInitParams");

            IList<ProposalStep> entities = dal.GetAll();

            Assert.IsNotNull(entities);
            Assert.IsNotEmpty(entities);
        }

        [TestCase("ProposalStep\\000.GetDetails.Success")]
        public void GetProposalStep_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareProposalStepDal("DALInitParams");

            object objId = SetupCase(conn, caseName);
            long id = (long)objId;

            ProposalStep entity = dal.Get(id);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
            Assert.AreNotEqual(0, entity.ID);
		Assert.AreEqual("Name 8664d44acb47416fa2a3b208b908d748", entity.Name);
		Assert.AreEqual(true, entity.ReqDueDate);
		Assert.AreEqual(648, entity.RequiresRespInDays);
		
        }

        [Test]
        public void GetProposalStep_InvalidId()
        {
            long id = Int32.MaxValue - 1;
            var dal = PrepareProposalStepDal("DALInitParams");

            ProposalStep entity = dal.Get(id);

            Assert.IsNull(entity);
        }

        [TestCase("ProposalStep\\010.Delete.Success")]
        public void DeleteProposalStep_Success(string caseName)
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
        public void DeleteProposalStep_InvalidId()
        {
            long positionId = Int32.MaxValue - 1;
            var dal = PrepareProposalStepDal("DALInitParams");

            bool removed = dal.Delete(positionId);
            Assert.IsFalse(removed);

        }

        [TestCase("ProposalStep\\020.Insert.Success")]
        public void InsertProposalStep_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            SetupCase(conn, caseName);

            var dal = PrepareProposalStepDal("DALInitParams");

            var entity = new ProposalStep();
            entity.Name = "Name 9f2e83d49da44b4aa247494426d77b06";
		entity.ReqDueDate = true;
		entity.RequiresRespInDays = 648;
		

            entity = dal.Upsert(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
            Assert.AreNotEqual(0, entity.ID);
		Assert.AreEqual("Name 9f2e83d49da44b4aa247494426d77b06", entity.Name);
		Assert.AreEqual(true, entity.ReqDueDate);
		Assert.AreEqual(648, entity.RequiresRespInDays);
		
        }

        [TestCase("ProposalStep\\030.Update.Success")]
        public void UpdateProposalStep_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareProposalStepDal("DALInitParams");

            object objId = SetupCase(conn, caseName);
            long id = (long)objId;

            var entity = dal.Get(id);
            entity.Name = "Name 62c6fba1c2fc41589738434d7ab63e04";
		entity.ReqDueDate = true;
		entity.RequiresRespInDays = 648;
		

            entity = dal.Upsert(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
            Assert.AreNotEqual(0, entity.ID);
		Assert.AreEqual("Name 62c6fba1c2fc41589738434d7ab63e04", entity.Name);
		Assert.AreEqual(true, entity.ReqDueDate);
		Assert.AreEqual(648, entity.RequiresRespInDays);
		
        }

        [Test]
        public void UpdateProposalStep_InvalidId()
        {
            var dal = PrepareProposalStepDal("DALInitParams");

            var entity = new ProposalStep();
            entity.ID = Int64.MaxValue - 1;
            entity.Name = "Name 62c6fba1c2fc41589738434d7ab63e04";
		entity.ReqDueDate = true;
		entity.RequiresRespInDays = 648;
		

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
