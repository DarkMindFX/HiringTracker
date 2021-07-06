

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
    public class TestInterviewFeedbackDal : TestBase
    {
        [Test]
        public void DalInit_Success()
        {
            IConfiguration config = GetConfiguration();
            var initParams = config.GetSection("DALInitParams").Get<TestDalInitParams>();

            IInterviewFeedbackDal dal = new InterviewFeedbackDal();
            var dalInitParams = dal.CreateInitParams();
            dalInitParams.Parameters["ConnectionString"] = initParams.ConnectionString;
            dal.Init(dalInitParams);
        }

        [Test]
        public void InterviewFeedback_GetAll_Success()
        {
            var dal = PrepareInterviewFeedbackDal("DALInitParams");

            IList<InterviewFeedback> entities = dal.GetAll();

            Assert.IsNotNull(entities);
            Assert.IsNotEmpty(entities);
        }

        [TestCase("InterviewFeedback\\000.GetDetails.Success")]
        public void InterviewFeedback_GetDetails_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareInterviewFeedbackDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
            var paramID = (System.Int64?)objIds[0];
            InterviewFeedback entity = dal.Get(paramID);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
            Assert.IsNotNull(entity.ID);

            Assert.AreEqual("Comment 60243b2d2fd8489393b81146b4cd0b6f", entity.Comment);
            Assert.AreEqual(509, entity.Rating);
            Assert.AreEqual(100007, entity.InterviewID);
            Assert.AreEqual(100002, entity.InterviewerID);
            Assert.AreEqual(100002, entity.CreatedByID);
            Assert.AreEqual(DateTime.Parse("4/5/2020 9:58:25 AM"), entity.CreatedDate);
            Assert.AreEqual(100001, entity.ModifiedByID);
            Assert.AreEqual(DateTime.Parse("4/5/2020 9:58:25 AM"), entity.ModifiedDate);
        }

        [Test]
        public void InterviewFeedback_GetDetails_InvalidId()
        {
            var paramID = Int64.MaxValue - 1;
            var dal = PrepareInterviewFeedbackDal("DALInitParams");

            InterviewFeedback entity = dal.Get(paramID);

            Assert.IsNull(entity);
        }

        [TestCase("InterviewFeedback\\010.Delete.Success")]
        public void InterviewFeedback_Delete_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareInterviewFeedbackDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
            var paramID = (System.Int64?)objIds[0];
            bool removed = dal.Delete(paramID);

            TeardownCase(conn, caseName);

            Assert.IsTrue(removed);
        }

        [Test]
        public void InterviewFeedback_Delete_InvalidId()
        {
            var dal = PrepareInterviewFeedbackDal("DALInitParams");
            var paramID = Int64.MaxValue - 1;

            bool removed = dal.Delete(paramID);
            Assert.IsFalse(removed);

        }

        [TestCase("InterviewFeedback\\020.Insert.Success")]
        public void InterviewFeedback_Insert_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            SetupCase(conn, caseName);

            var dal = PrepareInterviewFeedbackDal("DALInitParams");

            var entity = new InterviewFeedback();
            entity.Comment = "Comment c08e7247b206418c8b4c262404b20bae";
            entity.Rating = 317;
            entity.InterviewID = 100006;
            entity.InterviewerID = 100002;
            entity.CreatedByID = 100005;
            entity.CreatedDate = DateTime.Parse("10/1/2020 6:25:25 AM");
            entity.ModifiedByID = 100004;
            entity.ModifiedDate = DateTime.Parse("8/12/2023 4:12:25 PM");

            entity = dal.Insert(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
            Assert.IsNotNull(entity.ID);

            Assert.AreEqual("Comment c08e7247b206418c8b4c262404b20bae", entity.Comment);
            Assert.AreEqual(317, entity.Rating);
            Assert.AreEqual(100006, entity.InterviewID);
            Assert.AreEqual(100002, entity.InterviewerID);
            Assert.AreEqual(100005, entity.CreatedByID);
            Assert.AreEqual(DateTime.Parse("10/1/2020 6:25:25 AM"), entity.CreatedDate);
            Assert.AreEqual(100004, entity.ModifiedByID);
            Assert.AreEqual(DateTime.Parse("8/12/2023 4:12:25 PM"), entity.ModifiedDate);

        }

        [TestCase("InterviewFeedback\\030.Update.Success")]
        public void InterviewFeedback_Update_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareInterviewFeedbackDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
            var paramID = (System.Int64?)objIds[0];
            InterviewFeedback entity = dal.Get(paramID);

            entity.Comment = "Comment 06075afbffe1452d85f11fb3d55fff67";
            entity.Rating = 451;
            entity.InterviewID = 100006;
            entity.InterviewerID = 100002;
            entity.CreatedByID = 100005;
            entity.CreatedDate = DateTime.Parse("6/28/2021 7:46:25 AM");
            entity.ModifiedByID = 100001;
            entity.ModifiedDate = DateTime.Parse("6/28/2021 7:46:25 AM");

            entity = dal.Update(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
            Assert.IsNotNull(entity.ID);

            Assert.AreEqual("Comment 06075afbffe1452d85f11fb3d55fff67", entity.Comment);
            Assert.AreEqual(451, entity.Rating);
            Assert.AreEqual(100006, entity.InterviewID);
            Assert.AreEqual(100002, entity.InterviewerID);
            Assert.AreEqual(100005, entity.CreatedByID);
            Assert.AreEqual(DateTime.Parse("6/28/2021 7:46:25 AM"), entity.CreatedDate);
            Assert.AreEqual(100001, entity.ModifiedByID);
            Assert.AreEqual(DateTime.Parse("6/28/2021 7:46:25 AM"), entity.ModifiedDate);

        }

        [Test]
        public void InterviewFeedback_Update_InvalidId()
        {
            var dal = PrepareInterviewFeedbackDal("DALInitParams");

            var entity = new InterviewFeedback();
            entity.Comment = "Comment 06075afbffe1452d85f11fb3d55fff67";
            entity.Rating = 451;
            entity.InterviewID = 100006;
            entity.InterviewerID = 100002;
            entity.CreatedByID = 100005;
            entity.CreatedDate = DateTime.Parse("6/28/2021 7:46:25 AM");
            entity.ModifiedByID = 100001;
            entity.ModifiedDate = DateTime.Parse("6/28/2021 7:46:25 AM");

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

        protected IInterviewFeedbackDal PrepareInterviewFeedbackDal(string configName)
        {
            IConfiguration config = GetConfiguration();
            var initParams = config.GetSection(configName).Get<TestDalInitParams>();

            IInterviewFeedbackDal dal = new InterviewFeedbackDal();
            var dalInitParams = dal.CreateInitParams();
            dalInitParams.Parameters["ConnectionString"] = initParams.ConnectionString;
            dal.Init(dalInitParams);

            return dal;
        }
    }
}
