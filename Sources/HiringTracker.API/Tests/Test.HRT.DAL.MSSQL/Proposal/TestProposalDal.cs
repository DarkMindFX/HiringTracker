

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
            
                          Assert.AreEqual(100002, entity.PositionID);
                            Assert.AreEqual(100002, entity.CandidateID);
                            Assert.AreEqual(DateTime.Parse("12/21/2020 5:03:34 PM"), entity.Proposed);
                            Assert.AreEqual(11, entity.CurrentStepID);
                            Assert.AreEqual(DateTime.Parse("12/9/2022 6:52:34 AM"), entity.StepSetDate);
                            Assert.AreEqual(4, entity.NextStepID);
                            Assert.AreEqual(DateTime.Parse("10/19/2021 10:01:34 AM"), entity.DueDate);
                            Assert.AreEqual(4, entity.StatusID);
                            Assert.AreEqual(100004, entity.CreatedByID);
                            Assert.AreEqual(DateTime.Parse("1/16/2022 8:15:34 PM"), entity.CreatedDate);
                            Assert.AreEqual(100002, entity.ModifiedByID);
                            Assert.AreEqual(DateTime.Parse("6/6/2019 6:02:34 AM"), entity.ModifiedDate);
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
                          entity.PositionID = 100005;
                            entity.CandidateID = 100001;
                            entity.Proposed = DateTime.Parse("1/10/2023 7:50:34 AM");
                            entity.CurrentStepID = 2;
                            entity.StepSetDate = DateTime.Parse("1/10/2023 7:50:34 AM");
                            entity.NextStepID = 8;
                            entity.DueDate = DateTime.Parse("5/29/2020 8:17:34 AM");
                            entity.StatusID = 1;
                            entity.CreatedByID = 100001;
                            entity.CreatedDate = DateTime.Parse("4/9/2023 6:03:34 PM");
                            entity.ModifiedByID = 100001;
                            entity.ModifiedDate = DateTime.Parse("8/28/2020 3:50:34 AM");
                          
            entity = dal.Insert(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual(100005, entity.PositionID);
                            Assert.AreEqual(100001, entity.CandidateID);
                            Assert.AreEqual(DateTime.Parse("1/10/2023 7:50:34 AM"), entity.Proposed);
                            Assert.AreEqual(2, entity.CurrentStepID);
                            Assert.AreEqual(DateTime.Parse("1/10/2023 7:50:34 AM"), entity.StepSetDate);
                            Assert.AreEqual(8, entity.NextStepID);
                            Assert.AreEqual(DateTime.Parse("5/29/2020 8:17:34 AM"), entity.DueDate);
                            Assert.AreEqual(1, entity.StatusID);
                            Assert.AreEqual(100001, entity.CreatedByID);
                            Assert.AreEqual(DateTime.Parse("4/9/2023 6:03:34 PM"), entity.CreatedDate);
                            Assert.AreEqual(100001, entity.ModifiedByID);
                            Assert.AreEqual(DateTime.Parse("8/28/2020 3:50:34 AM"), entity.ModifiedDate);
              
        }

        [TestCase("Proposal\\030.Update.Success")]
        public void Proposal_Update_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareProposalDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
                var paramID = (System.Int64?)objIds[0];
            Proposal entity = dal.Get(paramID);

                          entity.PositionID = 100004;
                            entity.CandidateID = 100008;
                            entity.Proposed = DateTime.Parse("10/12/2018 5:38:34 AM");
                            entity.CurrentStepID = 4;
                            entity.StepSetDate = DateTime.Parse("8/21/2021 6:05:34 AM");
                            entity.NextStepID = 4;
                            entity.DueDate = DateTime.Parse("8/21/2021 6:05:34 AM");
                            entity.StatusID = 4;
                            entity.CreatedByID = 100002;
                            entity.CreatedDate = DateTime.Parse("1/9/2019 3:52:34 PM");
                            entity.ModifiedByID = 100003;
                            entity.ModifiedDate = DateTime.Parse("11/19/2021 1:39:34 AM");
              
            entity = dal.Update(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual(100004, entity.PositionID);
                            Assert.AreEqual(100008, entity.CandidateID);
                            Assert.AreEqual(DateTime.Parse("10/12/2018 5:38:34 AM"), entity.Proposed);
                            Assert.AreEqual(4, entity.CurrentStepID);
                            Assert.AreEqual(DateTime.Parse("8/21/2021 6:05:34 AM"), entity.StepSetDate);
                            Assert.AreEqual(4, entity.NextStepID);
                            Assert.AreEqual(DateTime.Parse("8/21/2021 6:05:34 AM"), entity.DueDate);
                            Assert.AreEqual(4, entity.StatusID);
                            Assert.AreEqual(100002, entity.CreatedByID);
                            Assert.AreEqual(DateTime.Parse("1/9/2019 3:52:34 PM"), entity.CreatedDate);
                            Assert.AreEqual(100003, entity.ModifiedByID);
                            Assert.AreEqual(DateTime.Parse("11/19/2021 1:39:34 AM"), entity.ModifiedDate);
              
        }

        [Test]
        public void Proposal_Update_InvalidId()
        {
            var dal = PrepareProposalDal("DALInitParams");

            var entity = new Proposal();
                          entity.PositionID = 100004;
                            entity.CandidateID = 100008;
                            entity.Proposed = DateTime.Parse("10/12/2018 5:38:34 AM");
                            entity.CurrentStepID = 4;
                            entity.StepSetDate = DateTime.Parse("8/21/2021 6:05:34 AM");
                            entity.NextStepID = 4;
                            entity.DueDate = DateTime.Parse("8/21/2021 6:05:34 AM");
                            entity.StatusID = 4;
                            entity.CreatedByID = 100002;
                            entity.CreatedDate = DateTime.Parse("1/9/2019 3:52:34 PM");
                            entity.ModifiedByID = 100003;
                            entity.ModifiedDate = DateTime.Parse("11/19/2021 1:39:34 AM");
              
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
