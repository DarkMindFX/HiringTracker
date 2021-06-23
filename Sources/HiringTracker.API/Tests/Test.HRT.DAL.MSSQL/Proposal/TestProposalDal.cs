

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
        public void Proposal_GetAll_Success()
        {
            var dal = PrepareProposalDal("DALInitParams");

            IList<Proposal> entities = dal.GetAll();

            Assert.IsNotNull(entities);
            Assert.IsNotEmpty(entities);
        }

        [TestCase("Proposal\\000.GetDetails.Success")]
        public void Proposal_GetDetails_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareProposalDal("DALInitParams");

            object objId = SetupCase(conn, caseName);
            long id = (long)objId;

            Proposal entity = dal.Get(id);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual(100001, entity.PositionID);
                            Assert.AreEqual(100002, entity.CandidateID);
                            Assert.AreEqual(DateTime.Parse("3/16/2022 12:58:11 PM"), entity.Proposed);
                            Assert.AreEqual(7, entity.CurrentStepID);
                            Assert.AreEqual(DateTime.Parse("6/7/2023 10:46:11 AM"), entity.StepSetDate);
                            Assert.AreEqual(9, entity.NextStepID);
                            Assert.AreEqual(DateTime.Parse("7/21/2021 12:34:11 PM"), entity.DueDate);
                            Assert.AreEqual(4, entity.StatusID);
                            Assert.AreEqual(100003, entity.CreatedByID);
                            Assert.AreEqual(DateTime.Parse("4/18/2022 1:55:11 PM"), entity.CreatedDate);
                            Assert.AreEqual(33020042, entity.ModifiedByID);
                            Assert.AreEqual(DateTime.Parse("9/4/2019 2:22:11 PM"), entity.ModifiedDate);
                      }

        [Test]
        public void Proposal_GetDetails_InvalidId()
        {
            long id = Int32.MaxValue - 1;
            var dal = PrepareProposalDal("DALInitParams");

            Proposal entity = dal.Get(id);

            Assert.IsNull(entity);
        }

        [TestCase("Proposal\\010.Delete.Success")]
        public void Proposal_Delete_Success(string caseName)
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
        public void Proposal_Delete_InvalidId()
        {
            long positionId = Int32.MaxValue - 1;
            var dal = PrepareProposalDal("DALInitParams");

            bool removed = dal.Delete(positionId);
            Assert.IsFalse(removed);

        }

        [TestCase("Proposal\\020.Insert.Success")]
        public void Proposal_Insert_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            SetupCase(conn, caseName);

            var dal = PrepareProposalDal("DALInitParams");

            var entity = new Proposal();
                          entity.PositionID = 100008;
                            entity.CandidateID = 100004;
                            entity.Proposed = DateTime.Parse("1/10/2019 1:58:11 PM");
                            entity.CurrentStepID = 6;
                            entity.StepSetDate = DateTime.Parse("1/10/2019 1:58:11 PM");
                            entity.NextStepID = 1;
                            entity.DueDate = DateTime.Parse("11/20/2021 11:45:11 PM");
                            entity.StatusID = 3;
                            entity.CreatedByID = 100002;
                            entity.CreatedDate = DateTime.Parse("4/11/2019 9:32:11 AM");
                            entity.ModifiedByID = 100001;
                            entity.ModifiedDate = DateTime.Parse("4/11/2019 9:32:11 AM");
                          
            entity = dal.Upsert(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual(100008, entity.PositionID);
                            Assert.AreEqual(100004, entity.CandidateID);
                            Assert.AreEqual(DateTime.Parse("1/10/2019 1:58:11 PM"), entity.Proposed);
                            Assert.AreEqual(6, entity.CurrentStepID);
                            Assert.AreEqual(DateTime.Parse("1/10/2019 1:58:11 PM"), entity.StepSetDate);
                            Assert.AreEqual(1, entity.NextStepID);
                            Assert.AreEqual(DateTime.Parse("11/20/2021 11:45:11 PM"), entity.DueDate);
                            Assert.AreEqual(3, entity.StatusID);
                            Assert.AreEqual(100002, entity.CreatedByID);
                            Assert.AreEqual(DateTime.Parse("4/11/2019 9:32:11 AM"), entity.CreatedDate);
                            Assert.AreEqual(100001, entity.ModifiedByID);
                            Assert.AreEqual(DateTime.Parse("4/11/2019 9:32:11 AM"), entity.ModifiedDate);
              
        }

        [TestCase("Proposal\\030.Update.Success")]
        public void Proposal_Update_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareProposalDal("DALInitParams");

            object objId = SetupCase(conn, caseName);
            long id = (long)objId;

            var entity = dal.Get(id);
                          entity.PositionID = 100005;
                            entity.CandidateID = 100002;
                            entity.Proposed = DateTime.Parse("5/19/2022 5:33:11 AM");
                            entity.CurrentStepID = 1;
                            entity.StepSetDate = DateTime.Parse("10/7/2019 3:19:11 PM");
                            entity.NextStepID = 5;
                            entity.DueDate = DateTime.Parse("10/7/2019 3:19:11 PM");
                            entity.StatusID = 4;
                            entity.CreatedByID = 100002;
                            entity.CreatedDate = DateTime.Parse("8/16/2022 3:46:11 PM");
                            entity.ModifiedByID = 100002;
                            entity.ModifiedDate = DateTime.Parse("1/5/2020 1:33:11 AM");
              
            entity = dal.Upsert(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual(100005, entity.PositionID);
                            Assert.AreEqual(100002, entity.CandidateID);
                            Assert.AreEqual(DateTime.Parse("5/19/2022 5:33:11 AM"), entity.Proposed);
                            Assert.AreEqual(1, entity.CurrentStepID);
                            Assert.AreEqual(DateTime.Parse("10/7/2019 3:19:11 PM"), entity.StepSetDate);
                            Assert.AreEqual(5, entity.NextStepID);
                            Assert.AreEqual(DateTime.Parse("10/7/2019 3:19:11 PM"), entity.DueDate);
                            Assert.AreEqual(4, entity.StatusID);
                            Assert.AreEqual(100002, entity.CreatedByID);
                            Assert.AreEqual(DateTime.Parse("8/16/2022 3:46:11 PM"), entity.CreatedDate);
                            Assert.AreEqual(100002, entity.ModifiedByID);
                            Assert.AreEqual(DateTime.Parse("1/5/2020 1:33:11 AM"), entity.ModifiedDate);
              
        }

        [Test]
        public void Proposal_Update_InvalidId()
        {
            var dal = PrepareProposalDal("DALInitParams");

            var entity = new Proposal();
            entity.ID = Int64.MaxValue - 1;
                          entity.PositionID = 100005;
                            entity.CandidateID = 100002;
                            entity.Proposed = DateTime.Parse("5/19/2022 5:33:11 AM");
                            entity.CurrentStepID = 1;
                            entity.StepSetDate = DateTime.Parse("10/7/2019 3:19:11 PM");
                            entity.NextStepID = 5;
                            entity.DueDate = DateTime.Parse("10/7/2019 3:19:11 PM");
                            entity.StatusID = 4;
                            entity.CreatedByID = 100002;
                            entity.CreatedDate = DateTime.Parse("8/16/2022 3:46:11 PM");
                            entity.ModifiedByID = 100002;
                            entity.ModifiedDate = DateTime.Parse("1/5/2020 1:33:11 AM");
              
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

        protected IProposalDal PrepareProposalDal(string configName)
        {
            IConfiguration config = GetConfiguration();
            var initParams = config.GetSection(configName).Get<TestDalInitParams>();

            IProposalDal dal = new ProposalDal();
            var dalInitParams = dal.CreateInitParams();
            dalInitParams.Parameters["ConnectionString"] = initParams.ConnectionString;
            dal.Init(dalInitParams);

            return dal;
        }
    }
}
