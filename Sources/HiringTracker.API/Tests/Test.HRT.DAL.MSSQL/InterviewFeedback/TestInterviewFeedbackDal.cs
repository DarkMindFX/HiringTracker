

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

            object objId = SetupCase(conn, caseName);
            long id = (long)objId;

            InterviewFeedback entity = dal.Get(id);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual(518657, entity.ID);
                            Assert.AreEqual("Comment 0dc3663238f24c0499148a6a205aed52", entity.Comment);
                            Assert.AreEqual(519, entity.Rating);
                            Assert.AreEqual(100002, entity.InterviewerID);
                            Assert.AreEqual(100003, entity.CreatedByID);
                            Assert.AreEqual(DateTime.Parse("4/20/2023 9:37:10 AM"), entity.CreatedDate);
                            Assert.AreEqual(33000067, entity.ModifiedByID);
                            Assert.AreEqual(DateTime.Parse("10/21/2018 11:52:10 AM"), entity.ModifiedDate);
                      }

        [Test]
        public void InterviewFeedback_GetDetails_InvalidId()
        {
            long id = Int32.MaxValue - 1;
            var dal = PrepareInterviewFeedbackDal("DALInitParams");

            InterviewFeedback entity = dal.Get(id);

            Assert.IsNull(entity);
        }

        [TestCase("InterviewFeedback\\010.Delete.Success")]
        public void InterviewFeedback_Delete_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareInterviewFeedbackDal("DALInitParams");

            object objId = SetupCase(conn, caseName);
            long id = (long)objId;

            bool removed = dal.Delete(id);

            TeardownCase(conn, caseName);

            Assert.IsTrue(removed);
        }

        [Test]
        public void InterviewFeedback_Delete_InvalidId()
        {
            long positionId = Int32.MaxValue - 1;
            var dal = PrepareInterviewFeedbackDal("DALInitParams");

            bool removed = dal.Delete(positionId);
            Assert.IsFalse(removed);

        }

        [TestCase("InterviewFeedback\\020.Insert.Success")]
        public void InterviewFeedback_Insert_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            SetupCase(conn, caseName);

            var dal = PrepareInterviewFeedbackDal("DALInitParams");

            var entity = new InterviewFeedback();
                          entity.ID = 236267;
                            entity.Comment = "Comment 5c614cb361cb45e580f04220e48641ed";
                            entity.Rating = 237;
                            entity.InterviewerID = 33000067;
                            entity.CreatedByID = 33000067;
                            entity.CreatedDate = DateTime.Parse("4/12/2020 5:14:10 AM");
                            entity.ModifiedByID = 100001;
                            entity.ModifiedDate = DateTime.Parse("4/12/2020 5:14:10 AM");
                          
            entity = dal.Upsert(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual(236267, entity.ID);
                            Assert.AreEqual("Comment 5c614cb361cb45e580f04220e48641ed", entity.Comment);
                            Assert.AreEqual(237, entity.Rating);
                            Assert.AreEqual(33000067, entity.InterviewerID);
                            Assert.AreEqual(33000067, entity.CreatedByID);
                            Assert.AreEqual(DateTime.Parse("4/12/2020 5:14:10 AM"), entity.CreatedDate);
                            Assert.AreEqual(100001, entity.ModifiedByID);
                            Assert.AreEqual(DateTime.Parse("4/12/2020 5:14:10 AM"), entity.ModifiedDate);
              
        }

        [TestCase("InterviewFeedback\\030.Update.Success")]
        public void InterviewFeedback_Update_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareInterviewFeedbackDal("DALInitParams");

            object objId = SetupCase(conn, caseName);
            long id = (long)objId;

            var entity = dal.Get(id);
                          entity.ID = 370819;
                            entity.Comment = "Comment 94e5e92fc76249019666af329e79e655";
                            entity.Rating = 371;
                            entity.InterviewerID = 33020042;
                            entity.CreatedByID = 100003;
                            entity.CreatedDate = DateTime.Parse("1/5/2021 9:15:10 PM");
                            entity.ModifiedByID = 100002;
                            entity.ModifiedDate = DateTime.Parse("1/5/2021 9:15:10 PM");
              
            entity = dal.Upsert(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual(370819, entity.ID);
                            Assert.AreEqual("Comment 94e5e92fc76249019666af329e79e655", entity.Comment);
                            Assert.AreEqual(371, entity.Rating);
                            Assert.AreEqual(33020042, entity.InterviewerID);
                            Assert.AreEqual(100003, entity.CreatedByID);
                            Assert.AreEqual(DateTime.Parse("1/5/2021 9:15:10 PM"), entity.CreatedDate);
                            Assert.AreEqual(100002, entity.ModifiedByID);
                            Assert.AreEqual(DateTime.Parse("1/5/2021 9:15:10 PM"), entity.ModifiedDate);
              
        }

        [Test]
        public void InterviewFeedback_Update_InvalidId()
        {
            var dal = PrepareInterviewFeedbackDal("DALInitParams");

            var entity = new InterviewFeedback();
            entity.ID = Int64.MaxValue - 1;
                          entity.ID = 370819;
                            entity.Comment = "Comment 94e5e92fc76249019666af329e79e655";
                            entity.Rating = 371;
                            entity.InterviewerID = 33020042;
                            entity.CreatedByID = 100003;
                            entity.CreatedDate = DateTime.Parse("1/5/2021 9:15:10 PM");
                            entity.ModifiedByID = 100002;
                            entity.ModifiedDate = DateTime.Parse("1/5/2021 9:15:10 PM");
              
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
