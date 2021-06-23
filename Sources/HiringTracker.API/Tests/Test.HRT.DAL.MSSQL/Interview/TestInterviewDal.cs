

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

            object objId = SetupCase(conn, caseName);
            long id = (long)objId;

            Interview entity = dal.Get(id);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual(100001, entity.ProposalID);
                            Assert.AreEqual(1, entity.InterviewTypeID);
                            Assert.AreEqual(DateTime.Parse("8/7/2019 6:14:10 PM"), entity.StartTime);
                            Assert.AreEqual(DateTime.Parse("8/7/2019 6:14:10 PM"), entity.EndTime);
                            Assert.AreEqual(3, entity.InterviewStatusID);
                            Assert.AreEqual(100003, entity.CreatedByID);
                            Assert.AreEqual(DateTime.Parse("10/29/2020 4:03:10 PM"), entity.CretedDate);
                            Assert.AreEqual(100002, entity.ModifiedByID);
                            Assert.AreEqual(DateTime.Parse("9/10/2023 1:49:10 AM"), entity.ModifiedDate);
                      }

        [Test]
        public void Interview_GetDetails_InvalidId()
        {
            long id = Int32.MaxValue - 1;
            var dal = PrepareInterviewDal("DALInitParams");

            Interview entity = dal.Get(id);

            Assert.IsNull(entity);
        }

        [TestCase("Interview\\010.Delete.Success")]
        public void Interview_Delete_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareInterviewDal("DALInitParams");

            object objId = SetupCase(conn, caseName);
            long id = (long)objId;

            bool removed = dal.Delete(id);

            TeardownCase(conn, caseName);

            Assert.IsTrue(removed);
        }

        [Test]
        public void Interview_Delete_InvalidId()
        {
            long positionId = Int32.MaxValue - 1;
            var dal = PrepareInterviewDal("DALInitParams");

            bool removed = dal.Delete(positionId);
            Assert.IsFalse(removed);

        }

        [TestCase("Interview\\020.Insert.Success")]
        public void Interview_Insert_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            SetupCase(conn, caseName);

            var dal = PrepareInterviewDal("DALInitParams");

            var entity = new Interview();
                          entity.ProposalID = 100005;
                            entity.InterviewTypeID = 1;
                            entity.StartTime = DateTime.Parse("7/13/2023 7:13:10 AM");
                            entity.EndTime = DateTime.Parse("7/13/2023 7:13:10 AM");
                            entity.InterviewStatusID = 1;
                            entity.CreatedByID = 33000067;
                            entity.CretedDate = DateTime.Parse("5/28/2021 1:27:10 PM");
                            entity.ModifiedByID = 100002;
                            entity.ModifiedDate = DateTime.Parse("5/28/2021 1:27:10 PM");
                          
            entity = dal.Upsert(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual(100005, entity.ProposalID);
                            Assert.AreEqual(1, entity.InterviewTypeID);
                            Assert.AreEqual(DateTime.Parse("7/13/2023 7:13:10 AM"), entity.StartTime);
                            Assert.AreEqual(DateTime.Parse("7/13/2023 7:13:10 AM"), entity.EndTime);
                            Assert.AreEqual(1, entity.InterviewStatusID);
                            Assert.AreEqual(33000067, entity.CreatedByID);
                            Assert.AreEqual(DateTime.Parse("5/28/2021 1:27:10 PM"), entity.CretedDate);
                            Assert.AreEqual(100002, entity.ModifiedByID);
                            Assert.AreEqual(DateTime.Parse("5/28/2021 1:27:10 PM"), entity.ModifiedDate);
              
        }

        [TestCase("Interview\\030.Update.Success")]
        public void Interview_Update_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareInterviewDal("DALInitParams");

            object objId = SetupCase(conn, caseName);
            long id = (long)objId;

            var entity = dal.Get(id);
                          entity.ProposalID = 100004;
                            entity.InterviewTypeID = 5;
                            entity.StartTime = DateTime.Parse("8/13/2023 10:50:10 PM");
                            entity.EndTime = DateTime.Parse("8/13/2023 10:50:10 PM");
                            entity.InterviewStatusID = 2;
                            entity.CreatedByID = 100002;
                            entity.CretedDate = DateTime.Parse("11/16/2018 2:51:10 PM");
                            entity.ModifiedByID = 100002;
                            entity.ModifiedDate = DateTime.Parse("11/16/2018 2:51:10 PM");
              
            entity = dal.Upsert(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual(100004, entity.ProposalID);
                            Assert.AreEqual(5, entity.InterviewTypeID);
                            Assert.AreEqual(DateTime.Parse("8/13/2023 10:50:10 PM"), entity.StartTime);
                            Assert.AreEqual(DateTime.Parse("8/13/2023 10:50:10 PM"), entity.EndTime);
                            Assert.AreEqual(2, entity.InterviewStatusID);
                            Assert.AreEqual(100002, entity.CreatedByID);
                            Assert.AreEqual(DateTime.Parse("11/16/2018 2:51:10 PM"), entity.CretedDate);
                            Assert.AreEqual(100002, entity.ModifiedByID);
                            Assert.AreEqual(DateTime.Parse("11/16/2018 2:51:10 PM"), entity.ModifiedDate);
              
        }

        [Test]
        public void Interview_Update_InvalidId()
        {
            var dal = PrepareInterviewDal("DALInitParams");

            var entity = new Interview();
            entity.ID = Int64.MaxValue - 1;
                          entity.ProposalID = 100004;
                            entity.InterviewTypeID = 5;
                            entity.StartTime = DateTime.Parse("8/13/2023 10:50:10 PM");
                            entity.EndTime = DateTime.Parse("8/13/2023 10:50:10 PM");
                            entity.InterviewStatusID = 2;
                            entity.CreatedByID = 100002;
                            entity.CretedDate = DateTime.Parse("11/16/2018 2:51:10 PM");
                            entity.ModifiedByID = 100002;
                            entity.ModifiedDate = DateTime.Parse("11/16/2018 2:51:10 PM");
              
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
