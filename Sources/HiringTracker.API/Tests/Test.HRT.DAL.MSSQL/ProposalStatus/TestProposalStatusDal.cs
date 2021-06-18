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
        public void GetProposalStatuss_Success()
        {
            var dal = PrepareProposalStatusDal("DALInitParams");

            IList<ProposalStatus> entities = dal.GetAll();

            Assert.IsNotNull(entities);
            Assert.IsNotEmpty(entities);
        }

        [TestCase("ProposalStatus\\000.GetDetails.Success")]
        public void GetProposalStatus_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareProposalStatusDal("DALInitParams");

            object objId = SetupCase(conn, caseName);
            long id = (long)objId;

            ProposalStatus entity = dal.Get(id);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
            Assert.AreNotEqual(0, entity.ID);
		Assert.AreEqual("Name b32caca28e0e46119081238b311a3543", entity.Name);
		
        }

        [Test]
        public void GetProposalStatus_InvalidId()
        {
            long id = Int32.MaxValue - 1;
            var dal = PrepareProposalStatusDal("DALInitParams");

            ProposalStatus entity = dal.Get(id);

            Assert.IsNull(entity);
        }

        [TestCase("ProposalStatus\\010.Delete.Success")]
        public void DeleteProposalStatus_Success(string caseName)
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
        public void DeleteProposalStatus_InvalidId()
        {
            long positionId = Int32.MaxValue - 1;
            var dal = PrepareProposalStatusDal("DALInitParams");

            bool removed = dal.Delete(positionId);
            Assert.IsFalse(removed);

        }

        [TestCase("ProposalStatus\\020.Insert.Success")]
        public void InsertProposalStatus_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            SetupCase(conn, caseName);

            var dal = PrepareProposalStatusDal("DALInitParams");

            var entity = new ProposalStatus();
            entity.Name = "Name a3a2e149ccd344aeace10123a01de62f";
		

            entity = dal.Upsert(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
            Assert.AreNotEqual(0, entity.ID);
		Assert.AreEqual("Name a3a2e149ccd344aeace10123a01de62f", entity.Name);
		
        }

        [TestCase("ProposalStatus\\030.Update.Success")]
        public void UpdateProposalStatus_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareProposalStatusDal("DALInitParams");

            object objId = SetupCase(conn, caseName);
            long id = (long)objId;

            var entity = dal.Get(id);
            entity.Name = "Name 826ec3bf4d5c4b28ad6d13ea4ab74a0e";
		

            entity = dal.Upsert(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
            Assert.AreNotEqual(0, entity.ID);
		Assert.AreEqual("Name 826ec3bf4d5c4b28ad6d13ea4ab74a0e", entity.Name);
		
        }

        [Test]
        public void UpdateProposalStatus_InvalidId()
        {
            var dal = PrepareProposalStatusDal("DALInitParams");

            var entity = new ProposalStatus();
            entity.ID = Int64.MaxValue - 1;
            entity.Name = "Name 826ec3bf4d5c4b28ad6d13ea4ab74a0e";
		

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
