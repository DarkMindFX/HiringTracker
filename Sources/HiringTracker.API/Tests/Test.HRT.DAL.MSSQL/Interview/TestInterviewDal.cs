

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
    public class TestInterviewDal : TestBase
    {
        [Test]
        public void DalInit_Success()
        {
            IConfiguration config = GetConfiguration();
            var initParams = config.GetSection("DALInitParams").Get<TestDalInitParams>();

            IInterviewDal dal = new InterviewDal();
            var dalInitParams = dal.CreateInitParams();
            dalInitParams.Parameters["ConnectionString"] = initParams.ConnectionString;
            dal.Init(dalInitParams);
        }

        [Test]
        public void Interview_GetAll_Success()
        {
            var dal = PrepareInterviewDal("DALInitParams");

            IList<Interview> entities = dal.GetAll();

            Assert.IsNotNull(entities);
            Assert.IsNotEmpty(entities);
        }

        [TestCase("Interview\\000.GetDetails.Success")]
        public void Interview_GetDetails_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareInterviewDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
                var paramID = (System.Int64?)objIds[0];
            Interview entity = dal.Get(paramID);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual(100004, entity.ProposalID);
                            Assert.AreEqual(2, entity.InterviewTypeID);
                            Assert.AreEqual(DateTime.Parse("3/17/2023 12:22:12 PM"), entity.StartTime);
                            Assert.AreEqual(DateTime.Parse("3/17/2023 12:22:12 PM"), entity.EndTime);
                            Assert.AreEqual(4, entity.InterviewStatusID);
                            Assert.AreEqual(33000067, entity.CreatedByID);
                            Assert.AreEqual(DateTime.Parse("3/10/2024 11:57:12 PM"), entity.CretedDate);
                            Assert.AreEqual(100001, entity.ModifiedByID);
                            Assert.AreEqual(DateTime.Parse("10/21/2022 7:32:12 AM"), entity.ModifiedDate);
                      }

        [Test]
        public void Interview_GetDetails_InvalidId()
        {
                var paramID = Int64.MaxValue - 1;
            var dal = PrepareInterviewDal("DALInitParams");

            Interview entity = dal.Get(paramID);

            Assert.IsNull(entity);
        }

        [TestCase("Interview\\010.Delete.Success")]
        public void Interview_Delete_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareInterviewDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
                var paramID = (System.Int64?)objIds[0];
            bool removed = dal.Delete(paramID);

            TeardownCase(conn, caseName);

            Assert.IsTrue(removed);
        }

        [Test]
        public void Interview_Delete_InvalidId()
        {
            var dal = PrepareInterviewDal("DALInitParams");
                var paramID = Int64.MaxValue - 1;
   
            bool removed = dal.Delete(paramID);
            Assert.IsFalse(removed);

        }

        [TestCase("Interview\\020.Insert.Success")]
        public void Interview_Insert_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            SetupCase(conn, caseName);

            var dal = PrepareInterviewDal("DALInitParams");

            var entity = new Interview();
                          entity.ProposalID = 100002;
                            entity.InterviewTypeID = 2;
                            entity.StartTime = DateTime.Parse("1/16/2024 5:58:12 PM");
                            entity.EndTime = DateTime.Parse("1/16/2024 5:58:12 PM");
                            entity.InterviewStatusID = 2;
                            entity.CreatedByID = 33000067;
                            entity.CretedDate = DateTime.Parse("6/6/2021 3:45:12 AM");
                            entity.ModifiedByID = 100003;
                            entity.ModifiedDate = DateTime.Parse("6/6/2021 3:45:12 AM");
                          
            entity = dal.Insert(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual(100002, entity.ProposalID);
                            Assert.AreEqual(2, entity.InterviewTypeID);
                            Assert.AreEqual(DateTime.Parse("1/16/2024 5:58:12 PM"), entity.StartTime);
                            Assert.AreEqual(DateTime.Parse("1/16/2024 5:58:12 PM"), entity.EndTime);
                            Assert.AreEqual(2, entity.InterviewStatusID);
                            Assert.AreEqual(33000067, entity.CreatedByID);
                            Assert.AreEqual(DateTime.Parse("6/6/2021 3:45:12 AM"), entity.CretedDate);
                            Assert.AreEqual(100003, entity.ModifiedByID);
                            Assert.AreEqual(DateTime.Parse("6/6/2021 3:45:12 AM"), entity.ModifiedDate);
              
        }

        [TestCase("Interview\\030.Update.Success")]
        public void Interview_Update_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareInterviewDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
                var paramID = (System.Int64?)objIds[0];
            Interview entity = dal.Get(paramID);

                          entity.ProposalID = 100006;
                            entity.InterviewTypeID = 4;
                            entity.StartTime = DateTime.Parse("12/3/2021 9:32:12 AM");
                            entity.EndTime = DateTime.Parse("12/3/2021 9:32:12 AM");
                            entity.InterviewStatusID = 1;
                            entity.CreatedByID = 100001;
                            entity.CretedDate = DateTime.Parse("7/21/2019 5:33:12 AM");
                            entity.ModifiedByID = 33020042;
                            entity.ModifiedDate = DateTime.Parse("5/30/2022 6:00:12 AM");
              
            entity = dal.Update(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual(100006, entity.ProposalID);
                            Assert.AreEqual(4, entity.InterviewTypeID);
                            Assert.AreEqual(DateTime.Parse("12/3/2021 9:32:12 AM"), entity.StartTime);
                            Assert.AreEqual(DateTime.Parse("12/3/2021 9:32:12 AM"), entity.EndTime);
                            Assert.AreEqual(1, entity.InterviewStatusID);
                            Assert.AreEqual(100001, entity.CreatedByID);
                            Assert.AreEqual(DateTime.Parse("7/21/2019 5:33:12 AM"), entity.CretedDate);
                            Assert.AreEqual(33020042, entity.ModifiedByID);
                            Assert.AreEqual(DateTime.Parse("5/30/2022 6:00:12 AM"), entity.ModifiedDate);
              
        }

        [Test]
        public void Interview_Update_InvalidId()
        {
            var dal = PrepareInterviewDal("DALInitParams");

            var entity = new Interview();
                          entity.ProposalID = 100006;
                            entity.InterviewTypeID = 4;
                            entity.StartTime = DateTime.Parse("12/3/2021 9:32:12 AM");
                            entity.EndTime = DateTime.Parse("12/3/2021 9:32:12 AM");
                            entity.InterviewStatusID = 1;
                            entity.CreatedByID = 100001;
                            entity.CretedDate = DateTime.Parse("7/21/2019 5:33:12 AM");
                            entity.ModifiedByID = 33020042;
                            entity.ModifiedDate = DateTime.Parse("5/30/2022 6:00:12 AM");
              
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

        protected IInterviewDal PrepareInterviewDal(string configName)
        {
            IConfiguration config = GetConfiguration();
            var initParams = config.GetSection(configName).Get<TestDalInitParams>();

            IInterviewDal dal = new InterviewDal();
            var dalInitParams = dal.CreateInitParams();
            dalInitParams.Parameters["ConnectionString"] = initParams.ConnectionString;
            dal.Init(dalInitParams);

            return dal;
        }
    }
}
