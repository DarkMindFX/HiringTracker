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
    public class TestProposalDal : TestBase
    {
        [Test]
        public void DalInit_Success()
        {
            IConfiguration config = GetConfiguration();
            var initParams = config.GetSection("DALInitParams").Get<TestDalInitParams>();

            IProposalDal dal = new ProposalDal();
            var dalInitParams = dal.CreateInitParams();
            dalInitParams.Parameters["ConnectionString"] = initParams.ConnectionString;
            dal.Init(dalInitParams);
        }

        [Test]
        public void GetProposals_Success()
        {
            var dal = PrepareProposalDal("DALInitParams");

            IList<Proposal> entities = dal.GetAll();

            Assert.IsNotNull(entities);
            Assert.IsNotEmpty(entities);
        }

        [TestCase("Proposal\\000.GetDetails.Success")]
        public void GetProposal_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareProposalDal("DALInitParams");

            object objId = SetupCase(conn, caseName);
            long id = (long)objId;

            Proposal entity = dal.Get(id);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
            Assert.AreNotEqual(0, entity.ID);
            Assert.AreEqual(100003, entity.PositionID);
            Assert.AreEqual(100008, entity.CandidateID);
            Assert.AreEqual(DateTime.Parse("3/11/2021 7:59:33 PM"), entity.Proposed);
            Assert.AreEqual(450536, entity.CurrentStepID);
            Assert.AreEqual(DateTime.Parse("3/11/2021 7:59:33 PM"), entity.StepSetDate);
            Assert.AreEqual(450536, entity.NextStepID);
            Assert.AreEqual(DateTime.Parse("3/11/2021 7:59:33 PM"), entity.DueDate);
            Assert.AreEqual(450536, entity.StatusID);
            Assert.AreEqual(450536, entity.CreatedByID);
            Assert.AreEqual(DateTime.Parse("3/11/2021 7:59:33 PM"), entity.CreatedDate);
            Assert.AreEqual(450536, entity.ModifiedByID);
            Assert.AreEqual(DateTime.Parse("3/11/2021 7:59:33 PM"), entity.ModifiedDate);

        }

        [Test]
        public void GetProposal_InvalidId()
        {
            long id = Int32.MaxValue - 1;
            var dal = PrepareProposalDal("DALInitParams");

            Proposal entity = dal.Get(id);

            Assert.IsNull(entity);
        }

        [Test]
        public void GetProposal_ByCandidate_Success()
        {
            long id = 100002;
            var dal = PrepareProposalDal("DALInitParams");

            IList<Proposal> entities = dal.GetByCandidate(id);

            Assert.IsNotNull(entities);
            Assert.IsNotEmpty(entities);
        }

        [Test]
        public void GetProposal_ByCandidate_InvalidId()
        {
            long id = Int64.MaxValue;
            var dal = PrepareProposalDal("DALInitParams");

            IList<Proposal> entities = dal.GetByCandidate(id);

            Assert.IsNull(entities);
        }

        [Test]
        public void GetProposal_ByPosition_Success()
        {
            long id = 100003;
            var dal = PrepareProposalDal("DALInitParams");

            IList<Proposal> entities = dal.GetByPosition(id);

            Assert.IsNotNull(entities);
            Assert.IsNotEmpty(entities);
        }

        [Test]
        public void GetProposal_ByPosition_InvalidId()
        {
            long id = Int64.MaxValue;
            var dal = PrepareProposalDal("DALInitParams");

            IList<Proposal> entities = dal.GetByPosition(id);

            Assert.IsNull(entities);
        }


        [TestCase("Proposal\\010.Delete.Success")]
        public void DeleteProposal_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareProposalDal("DALInitParams");

            object objId = SetupCase(conn, caseName);
            long id = (long)objId;

            bool removed = dal.Delete(id);

            TeardownCase(conn, caseName);

            Assert.IsTrue(removed);
        }

        [Test]
        public void DeleteProposal_InvalidId()
        {
            long positionId = Int32.MaxValue - 1;
            var dal = PrepareProposalDal("DALInitParams");

            bool removed = dal.Delete(positionId);
            Assert.IsFalse(removed);

        }

        [TestCase("Proposal\\020.Insert.Success")]
        public void InsertProposal_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            SetupCase(conn, caseName);

            var dal = PrepareProposalDal("DALInitParams");

            var entity = new Proposal();
            entity.PositionID = 110118;
            entity.CandidateID = 100001;
            entity.Proposed = DateTime.Parse("4/11/2021 11:36:33 AM");
            entity.CurrentStepID = 466318;
            entity.StepSetDate = DateTime.Parse("4/11/2021 11:36:33 AM");
            entity.NextStepID = 466318;
            entity.DueDate = DateTime.Parse("4/11/2021 11:36:33 AM");
            entity.StatusID = 466318;
            entity.CreatedByID = 466318;
            entity.CreatedDate = DateTime.Parse("4/11/2021 11:36:33 AM");
            entity.ModifiedByID = 466318;
            entity.ModifiedDate = DateTime.Parse("4/11/2021 11:36:33 AM");


            entity = dal.Upsert(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
            Assert.AreNotEqual(0, entity.ID);
            Assert.AreEqual(110118, entity.PositionID);
            Assert.AreEqual(100001, entity.CandidateID);
            Assert.AreEqual(DateTime.Parse("4/11/2021 11:36:33 AM"), entity.Proposed);
            Assert.AreEqual(466318, entity.CurrentStepID);
            Assert.AreEqual(DateTime.Parse("4/11/2021 11:36:33 AM"), entity.StepSetDate);
            Assert.AreEqual(466318, entity.NextStepID);
            Assert.AreEqual(DateTime.Parse("4/11/2021 11:36:33 AM"), entity.DueDate);
            Assert.AreEqual(466318, entity.StatusID);
            Assert.AreEqual(466318, entity.CreatedByID);
            Assert.AreEqual(DateTime.Parse("4/11/2021 11:36:33 AM"), entity.CreatedDate);
            Assert.AreEqual(466318, entity.ModifiedByID);
            Assert.AreEqual(DateTime.Parse("4/11/2021 11:36:33 AM"), entity.ModifiedDate);

        }

        [TestCase("Proposal\\030.Update.Success")]
        public void UpdateProposal_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareProposalDal("DALInitParams");

            object objId = SetupCase(conn, caseName);
            long id = (long)objId;

            var entity = dal.Get(id);
            entity.PositionID = 100002;
            entity.CandidateID = 100005;
            entity.Proposed = DateTime.Parse("11/28/2018 7:36:33 AM");
            entity.CurrentStepID = 33594;
            entity.StepSetDate = DateTime.Parse("11/28/2018 7:36:33 AM");
            entity.NextStepID = 33594;
            entity.DueDate = DateTime.Parse("11/28/2018 7:36:33 AM");
            entity.StatusID = 33594;
            entity.CreatedByID = 33594;
            entity.CreatedDate = DateTime.Parse("11/28/2018 7:36:33 AM");
            entity.ModifiedByID = 33594;
            entity.ModifiedDate = DateTime.Parse("11/28/2018 7:36:33 AM");


            entity = dal.Upsert(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
            Assert.AreNotEqual(0, entity.ID);
            Assert.AreEqual(100002, entity.PositionID);
            Assert.AreEqual(100005, entity.CandidateID);
            Assert.AreEqual(DateTime.Parse("11/28/2018 7:36:33 AM"), entity.Proposed);
            Assert.AreEqual(33594, entity.CurrentStepID);
            Assert.AreEqual(DateTime.Parse("11/28/2018 7:36:33 AM"), entity.StepSetDate);
            Assert.AreEqual(33594, entity.NextStepID);
            Assert.AreEqual(DateTime.Parse("11/28/2018 7:36:33 AM"), entity.DueDate);
            Assert.AreEqual(33594, entity.StatusID);
            Assert.AreEqual(33594, entity.CreatedByID);
            Assert.AreEqual(DateTime.Parse("11/28/2018 7:36:33 AM"), entity.CreatedDate);
            Assert.AreEqual(33594, entity.ModifiedByID);
            Assert.AreEqual(DateTime.Parse("11/28/2018 7:36:33 AM"), entity.ModifiedDate);

        }

        [Test]
        public void UpdateProposal_InvalidId()
        {
            var dal = PrepareProposalDal("DALInitParams");

            var entity = new Proposal();
            entity.ID = Int64.MaxValue - 1;
            entity.PositionID = 100002;
            entity.CandidateID = 100005;
            entity.Proposed = DateTime.Parse("11/28/2018 7:36:33 AM");
            entity.CurrentStepID = 33594;
            entity.StepSetDate = DateTime.Parse("11/28/2018 7:36:33 AM");
            entity.NextStepID = 33594;
            entity.DueDate = DateTime.Parse("11/28/2018 7:36:33 AM");
            entity.StatusID = 33594;
            entity.CreatedByID = 33594;
            entity.CreatedDate = DateTime.Parse("11/28/2018 7:36:33 AM");
            entity.ModifiedByID = 33594;
            entity.ModifiedDate = DateTime.Parse("11/28/2018 7:36:33 AM");


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
    }
}
