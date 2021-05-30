using HRT.DAL.MSSQL;
using HRT.Interfaces;
using HRT.Interfaces.Entities;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Test.HRT.DAL.MSSQL
{
    public class TestPositionCandidateDal : TestBase
    {
        [Test]
        public void DalInit_Success()
        {
            IConfiguration config = GetConfiguration();
            var initParams = config.GetSection("DALInitParams").Get<TestDalInitParams>();

            IPositionCandidateDal dal = new PositionCandidateDal();
            var dalInitParams = dal.CreateInitParams();
            dalInitParams.Parameters["ConnectionString"] = initParams.ConnectionString;
            dal.Init(dalInitParams);
        }

        [Test]
        public void GetProposals_Success()
        {
            var dal = PreparePositionCandidateDal("DALInitParams");

            IList<PositionCandidate> entities = dal.GetAll();

            Assert.IsNotNull(entities);
            Assert.IsNotEmpty(entities);
        }

        [Test]
        public void GetProposal_Success()
        {
            long proposalId = 100001;
            var dal = PreparePositionCandidateDal("DALInitParams");

            PositionCandidate entity = dal.Get(proposalId);

            Assert.IsNotNull(entity);
            Assert.AreEqual(proposalId, entity.ProposalID);
            Assert.AreEqual(100001, entity.PositionID);
            Assert.AreEqual(100001, entity.CandidateID);
            Assert.AreEqual(1, entity.StatusID);
            Assert.AreEqual(1, entity.CurrentStepID);
            Assert.AreEqual(2, entity.NextStepID);
            Assert.AreNotEqual(DateTime.MinValue, entity.DueDate);
            Assert.AreNotEqual(DateTime.MinValue, entity.Proposed);

            Assert.AreEqual(100001, entity.CreatedByID);
            Assert.AreNotEqual(DateTime.MinValue, entity.CreatedDate);
        }

        [Test]
        public void GetCandidate_InvalidId()
        {
            long proposalId = Int32.MaxValue - 1;
            var dal = PreparePositionCandidateDal("DALInitParams");

            PositionCandidate entity = dal.Get(proposalId);

            Assert.IsNull(entity);
        }
    }
}
