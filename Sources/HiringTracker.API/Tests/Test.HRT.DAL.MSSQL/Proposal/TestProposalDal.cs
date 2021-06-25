

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

            IList<object> objIds = SetupCase(conn, caseName);
                var paramID = (System.Int64?)objIds[0];
            Proposal entity = dal.Get(paramID);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual(100003, entity.PositionID);
                            Assert.AreEqual(100009, entity.CandidateID);
                            Assert.AreEqual(DateTime.Parse("3/6/2021 3:03:12 PM"), entity.Proposed);
                            Assert.AreEqual(10, entity.CurrentStepID);
                            Assert.AreEqual(DateTime.Parse("1/7/2021 8:26:12 PM"), entity.StepSetDate);
                            Assert.AreEqual(1, entity.NextStepID);
                            Assert.AreEqual(DateTime.Parse("5/21/2019 8:28:12 AM"), entity.DueDate);
                            Assert.AreEqual(4, entity.StatusID);
                            Assert.AreEqual(33000067, entity.CreatedByID);
                            Assert.AreEqual(DateTime.Parse("5/14/2020 8:02:12 PM"), entity.CreatedDate);
                            Assert.AreEqual(100002, entity.ModifiedByID);
                            Assert.AreEqual(DateTime.Parse("3/26/2023 5:49:12 AM"), entity.ModifiedDate);
                      }

        [Test]
        public void Proposal_GetDetails_InvalidId()
        {
                var paramID = Int64.MaxValue - 1;
            var dal = PrepareProposalDal("DALInitParams");

            Proposal entity = dal.Get(paramID);

            Assert.IsNull(entity);
        }

        [TestCase("Proposal\\010.Delete.Success")]
        public void Proposal_Delete_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareProposalDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
                var paramID = (System.Int64?)objIds[0];
            bool removed = dal.Delete(paramID);

            TeardownCase(conn, caseName);

            Assert.IsTrue(removed);
        }

        [Test]
        public void Proposal_Delete_InvalidId()
        {
            var dal = PrepareProposalDal("DALInitParams");
                var paramID = Int64.MaxValue - 1;
   
            bool removed = dal.Delete(paramID);
            Assert.IsFalse(removed);

        }

        [TestCase("Proposal\\020.Insert.Success")]
        public void Proposal_Insert_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            SetupCase(conn, caseName);

            var dal = PrepareProposalDal("DALInitParams");

            var entity = new Proposal();
                          entity.PositionID = 100009;
                            entity.CandidateID = 100001;
                            entity.Proposed = DateTime.Parse("9/22/2023 11:37:12 AM");
                            entity.CurrentStepID = 10;
                            entity.StepSetDate = DateTime.Parse("2/7/2021 12:03:12 PM");
                            entity.NextStepID = 10;
                            entity.DueDate = DateTime.Parse("2/7/2021 12:03:12 PM");
                            entity.StatusID = 3;
                            entity.CreatedByID = 100001;
                            entity.CreatedDate = DateTime.Parse("12/19/2023 9:50:12 PM");
                            entity.ModifiedByID = 100001;
                            entity.ModifiedDate = DateTime.Parse("5/9/2021 7:37:12 AM");
                          
            entity = dal.Insert(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual(100009, entity.PositionID);
                            Assert.AreEqual(100001, entity.CandidateID);
                            Assert.AreEqual(DateTime.Parse("9/22/2023 11:37:12 AM"), entity.Proposed);
                            Assert.AreEqual(10, entity.CurrentStepID);
                            Assert.AreEqual(DateTime.Parse("2/7/2021 12:03:12 PM"), entity.StepSetDate);
                            Assert.AreEqual(10, entity.NextStepID);
                            Assert.AreEqual(DateTime.Parse("2/7/2021 12:03:12 PM"), entity.DueDate);
                            Assert.AreEqual(3, entity.StatusID);
                            Assert.AreEqual(100001, entity.CreatedByID);
                            Assert.AreEqual(DateTime.Parse("12/19/2023 9:50:12 PM"), entity.CreatedDate);
                            Assert.AreEqual(100001, entity.ModifiedByID);
                            Assert.AreEqual(DateTime.Parse("5/9/2021 7:37:12 AM"), entity.ModifiedDate);
              
        }

        [TestCase("Proposal\\030.Update.Success")]
        public void Proposal_Update_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareProposalDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
                var paramID = (System.Int64?)objIds[0];
            Proposal entity = dal.Get(paramID);

                          entity.PositionID = 110118;
                            entity.CandidateID = 100002;
                            entity.Proposed = DateTime.Parse("2/1/2022 11:38:12 PM");
                            entity.CurrentStepID = 6;
                            entity.StepSetDate = DateTime.Parse("2/1/2022 11:38:12 PM");
                            entity.NextStepID = 2;
                            entity.DueDate = DateTime.Parse("6/23/2019 9:25:12 AM");
                            entity.StatusID = 4;
                            entity.CreatedByID = 33000067;
                            entity.CreatedDate = DateTime.Parse("5/2/2022 9:52:12 AM");
                            entity.ModifiedByID = 100001;
                            entity.ModifiedDate = DateTime.Parse("5/2/2022 9:52:12 AM");
              
            entity = dal.Update(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual(110118, entity.PositionID);
                            Assert.AreEqual(100002, entity.CandidateID);
                            Assert.AreEqual(DateTime.Parse("2/1/2022 11:38:12 PM"), entity.Proposed);
                            Assert.AreEqual(6, entity.CurrentStepID);
                            Assert.AreEqual(DateTime.Parse("2/1/2022 11:38:12 PM"), entity.StepSetDate);
                            Assert.AreEqual(2, entity.NextStepID);
                            Assert.AreEqual(DateTime.Parse("6/23/2019 9:25:12 AM"), entity.DueDate);
                            Assert.AreEqual(4, entity.StatusID);
                            Assert.AreEqual(33000067, entity.CreatedByID);
                            Assert.AreEqual(DateTime.Parse("5/2/2022 9:52:12 AM"), entity.CreatedDate);
                            Assert.AreEqual(100001, entity.ModifiedByID);
                            Assert.AreEqual(DateTime.Parse("5/2/2022 9:52:12 AM"), entity.ModifiedDate);
              
        }

        [Test]
        public void Proposal_Update_InvalidId()
        {
            var dal = PrepareProposalDal("DALInitParams");

            var entity = new Proposal();
                          entity.PositionID = 110118;
                            entity.CandidateID = 100002;
                            entity.Proposed = DateTime.Parse("2/1/2022 11:38:12 PM");
                            entity.CurrentStepID = 6;
                            entity.StepSetDate = DateTime.Parse("2/1/2022 11:38:12 PM");
                            entity.NextStepID = 2;
                            entity.DueDate = DateTime.Parse("6/23/2019 9:25:12 AM");
                            entity.StatusID = 4;
                            entity.CreatedByID = 33000067;
                            entity.CreatedDate = DateTime.Parse("5/2/2022 9:52:12 AM");
                            entity.ModifiedByID = 100001;
                            entity.ModifiedDate = DateTime.Parse("5/2/2022 9:52:12 AM");
              
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
