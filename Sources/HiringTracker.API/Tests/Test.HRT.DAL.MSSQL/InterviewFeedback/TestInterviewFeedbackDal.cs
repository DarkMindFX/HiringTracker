

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
                var paramID = (System.Int64)objIds[0];
            InterviewFeedback entity = dal.Get(paramID);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual(461326, entity.ID);
                            Assert.AreEqual("Comment 3c0f667406db4d21a5ff61ec0dfb89e3", entity.Comment);
                            Assert.AreEqual(461, entity.Rating);
                            Assert.AreEqual(33020042, entity.InterviewerID);
                            Assert.AreEqual(100002, entity.CreatedByID);
                            Assert.AreEqual(DateTime.Parse("11/19/2019 7:24:12 AM"), entity.CreatedDate);
                            Assert.AreEqual(100003, entity.ModifiedByID);
                            Assert.AreEqual(DateTime.Parse("3/27/2023 10:58:12 PM"), entity.ModifiedDate);
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
                var paramID = (System.Int64)objIds[0];
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
                          entity.ID = 611660;
                            entity.Comment = "Comment 7281f9683887404a9fc613378986e788";
                            entity.Rating = 612;
                            entity.InterviewerID = 33000067;
                            entity.CreatedByID = 100003;
                            entity.CreatedDate = DateTime.Parse("9/21/2019 12:47:12 PM");
                            entity.ModifiedByID = 33020042;
                            entity.ModifiedDate = DateTime.Parse("8/1/2022 10:34:12 PM");
                          
            entity = dal.Insert(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual(611660, entity.ID);
                            Assert.AreEqual("Comment 7281f9683887404a9fc613378986e788", entity.Comment);
                            Assert.AreEqual(612, entity.Rating);
                            Assert.AreEqual(33000067, entity.InterviewerID);
                            Assert.AreEqual(100003, entity.CreatedByID);
                            Assert.AreEqual(DateTime.Parse("9/21/2019 12:47:12 PM"), entity.CreatedDate);
                            Assert.AreEqual(33020042, entity.ModifiedByID);
                            Assert.AreEqual(DateTime.Parse("8/1/2022 10:34:12 PM"), entity.ModifiedDate);
              
        }

        [TestCase("InterviewFeedback\\030.Update.Success")]
        public void InterviewFeedback_Update_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareInterviewFeedbackDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
                var paramID = (System.Int64)objIds[0];
            InterviewFeedback entity = dal.Get(paramID);

                          entity.Comment = "Comment 76f0915144fe4c099ee495d102aae0e8";
                            entity.Rating = 746;
                            entity.InterviewerID = 100001;
                            entity.CreatedByID = 33000067;
                            entity.CreatedDate = DateTime.Parse("1/29/2023 4:22:12 AM");
                            entity.ModifiedByID = 33020042;
                            entity.ModifiedDate = DateTime.Parse("6/17/2020 2:08:12 PM");
              
            entity = dal.Update(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual(701361, entity.ID);
                            Assert.AreEqual("Comment 76f0915144fe4c099ee495d102aae0e8", entity.Comment);
                            Assert.AreEqual(746, entity.Rating);
                            Assert.AreEqual(100001, entity.InterviewerID);
                            Assert.AreEqual(33000067, entity.CreatedByID);
                            Assert.AreEqual(DateTime.Parse("1/29/2023 4:22:12 AM"), entity.CreatedDate);
                            Assert.AreEqual(33020042, entity.ModifiedByID);
                            Assert.AreEqual(DateTime.Parse("6/17/2020 2:08:12 PM"), entity.ModifiedDate);
              
        }

        [Test]
        public void InterviewFeedback_Update_InvalidId()
        {
            var dal = PrepareInterviewFeedbackDal("DALInitParams");

            var entity = new InterviewFeedback();
                          entity.ID = 701361;
                            entity.Comment = "Comment 76f0915144fe4c099ee495d102aae0e8";
                            entity.Rating = 746;
                            entity.InterviewerID = 100001;
                            entity.CreatedByID = 33000067;
                            entity.CreatedDate = DateTime.Parse("1/29/2023 4:22:12 AM");
                            entity.ModifiedByID = 33020042;
                            entity.ModifiedDate = DateTime.Parse("6/17/2020 2:08:12 PM");
              
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
