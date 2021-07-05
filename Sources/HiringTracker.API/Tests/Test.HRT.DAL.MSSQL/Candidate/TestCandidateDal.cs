

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
    public class TestCandidateDal : TestBase
    {
        [Test]
        public void DalInit_Success()
        {
            IConfiguration config = GetConfiguration();
            var initParams = config.GetSection("DALInitParams").Get<TestDalInitParams>();

            ICandidateDal dal = new CandidateDal();
            var dalInitParams = dal.CreateInitParams();
            dalInitParams.Parameters["ConnectionString"] = initParams.ConnectionString;
            dal.Init(dalInitParams);
        }

        [Test]
        public void Candidate_GetAll_Success()
        {
            var dal = PrepareCandidateDal("DALInitParams");

            IList<Candidate> entities = dal.GetAll();

            Assert.IsNotNull(entities);
            Assert.IsNotEmpty(entities);
        }

        [TestCase("Candidate\\000.GetDetails.Success")]
        public void Candidate_GetDetails_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareCandidateDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
                var paramID = (System.Int64?)objIds[0];
            Candidate entity = dal.Get(paramID);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual("FirstName b536006398f14919b98887b9a7761a33", entity.FirstName);
                            Assert.AreEqual("MiddleName b536006398f14919b98887b9a7761a33", entity.MiddleName);
                            Assert.AreEqual("LastName b536006398f14919b98887b9a7761a33", entity.LastName);
                            Assert.AreEqual("Email b536006398f14919b98887b9a7761a33", entity.Email);
                            Assert.AreEqual("Phone b536006398f14919b98887b9a7761a33", entity.Phone);
                            Assert.AreEqual("CVLink b536006398f14919b98887b9a7761a33", entity.CVLink);
                            Assert.AreEqual(100002, entity.CreatedByID);
                            Assert.AreEqual(DateTime.Parse("3/21/2023 6:11:32 AM"), entity.CreatedDate);
                            Assert.AreEqual(100005, entity.ModifiedByID);
                            Assert.AreEqual(DateTime.Parse("2/2/2021 12:25:32 PM"), entity.ModifiedDate);
                      }

        [Test]
        public void Candidate_GetDetails_InvalidId()
        {
                var paramID = Int64.MaxValue - 1;
            var dal = PrepareCandidateDal("DALInitParams");

            Candidate entity = dal.Get(paramID);

            Assert.IsNull(entity);
        }

        [TestCase("Candidate\\010.Delete.Success")]
        public void Candidate_Delete_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareCandidateDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
                var paramID = (System.Int64?)objIds[0];
            bool removed = dal.Delete(paramID);

            TeardownCase(conn, caseName);

            Assert.IsTrue(removed);
        }

        [Test]
        public void Candidate_Delete_InvalidId()
        {
            var dal = PrepareCandidateDal("DALInitParams");
                var paramID = Int64.MaxValue - 1;
   
            bool removed = dal.Delete(paramID);
            Assert.IsFalse(removed);

        }

        [TestCase("Candidate\\020.Insert.Success")]
        public void Candidate_Insert_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            SetupCase(conn, caseName);

            var dal = PrepareCandidateDal("DALInitParams");

            var entity = new Candidate();
                          entity.FirstName = "FirstName a2c578eb2e164e91b3599edce3eb165f";
                            entity.MiddleName = "MiddleName a2c578eb2e164e91b3599edce3eb165f";
                            entity.LastName = "LastName a2c578eb2e164e91b3599edce3eb165f";
                            entity.Email = "Email a2c578eb2e164e91b3599edce3eb165f";
                            entity.Phone = "Phone a2c578eb2e164e91b3599edce3eb165f";
                            entity.CVLink = "CVLink a2c578eb2e164e91b3599edce3eb165f";
                            entity.CreatedByID = 100005;
                            entity.CreatedDate = DateTime.Parse("2/23/2023 3:12:32 AM");
                            entity.ModifiedByID = 100004;
                            entity.ModifiedDate = DateTime.Parse("10/9/2020 11:12:32 PM");
                          
            entity = dal.Insert(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual("FirstName a2c578eb2e164e91b3599edce3eb165f", entity.FirstName);
                            Assert.AreEqual("MiddleName a2c578eb2e164e91b3599edce3eb165f", entity.MiddleName);
                            Assert.AreEqual("LastName a2c578eb2e164e91b3599edce3eb165f", entity.LastName);
                            Assert.AreEqual("Email a2c578eb2e164e91b3599edce3eb165f", entity.Email);
                            Assert.AreEqual("Phone a2c578eb2e164e91b3599edce3eb165f", entity.Phone);
                            Assert.AreEqual("CVLink a2c578eb2e164e91b3599edce3eb165f", entity.CVLink);
                            Assert.AreEqual(100005, entity.CreatedByID);
                            Assert.AreEqual(DateTime.Parse("2/23/2023 3:12:32 AM"), entity.CreatedDate);
                            Assert.AreEqual(100004, entity.ModifiedByID);
                            Assert.AreEqual(DateTime.Parse("10/9/2020 11:12:32 PM"), entity.ModifiedDate);
              
        }

        [TestCase("Candidate\\030.Update.Success")]
        public void Candidate_Update_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareCandidateDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
                var paramID = (System.Int64?)objIds[0];
            Candidate entity = dal.Get(paramID);

                          entity.FirstName = "FirstName c0133fc9adce44d695bb73c682b17142";
                            entity.MiddleName = "MiddleName c0133fc9adce44d695bb73c682b17142";
                            entity.LastName = "LastName c0133fc9adce44d695bb73c682b17142";
                            entity.Email = "Email c0133fc9adce44d695bb73c682b17142";
                            entity.Phone = "Phone c0133fc9adce44d695bb73c682b17142";
                            entity.CVLink = "CVLink c0133fc9adce44d695bb73c682b17142";
                            entity.CreatedByID = 100004;
                            entity.CreatedDate = DateTime.Parse("10/8/2021 11:25:32 PM");
                            entity.ModifiedByID = 100003;
                            entity.ModifiedDate = DateTime.Parse("4/6/2022 5:13:32 AM");
              
            entity = dal.Update(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual("FirstName c0133fc9adce44d695bb73c682b17142", entity.FirstName);
                            Assert.AreEqual("MiddleName c0133fc9adce44d695bb73c682b17142", entity.MiddleName);
                            Assert.AreEqual("LastName c0133fc9adce44d695bb73c682b17142", entity.LastName);
                            Assert.AreEqual("Email c0133fc9adce44d695bb73c682b17142", entity.Email);
                            Assert.AreEqual("Phone c0133fc9adce44d695bb73c682b17142", entity.Phone);
                            Assert.AreEqual("CVLink c0133fc9adce44d695bb73c682b17142", entity.CVLink);
                            Assert.AreEqual(100004, entity.CreatedByID);
                            Assert.AreEqual(DateTime.Parse("10/8/2021 11:25:32 PM"), entity.CreatedDate);
                            Assert.AreEqual(100003, entity.ModifiedByID);
                            Assert.AreEqual(DateTime.Parse("4/6/2022 5:13:32 AM"), entity.ModifiedDate);
              
        }

        [Test]
        public void Candidate_Update_InvalidId()
        {
            var dal = PrepareCandidateDal("DALInitParams");

            var entity = new Candidate();
                          entity.FirstName = "FirstName c0133fc9adce44d695bb73c682b17142";
                            entity.MiddleName = "MiddleName c0133fc9adce44d695bb73c682b17142";
                            entity.LastName = "LastName c0133fc9adce44d695bb73c682b17142";
                            entity.Email = "Email c0133fc9adce44d695bb73c682b17142";
                            entity.Phone = "Phone c0133fc9adce44d695bb73c682b17142";
                            entity.CVLink = "CVLink c0133fc9adce44d695bb73c682b17142";
                            entity.CreatedByID = 100004;
                            entity.CreatedDate = DateTime.Parse("10/8/2021 11:25:32 PM");
                            entity.ModifiedByID = 100003;
                            entity.ModifiedDate = DateTime.Parse("4/6/2022 5:13:32 AM");
              
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

        protected ICandidateDal PrepareCandidateDal(string configName)
        {
            IConfiguration config = GetConfiguration();
            var initParams = config.GetSection(configName).Get<TestDalInitParams>();

            ICandidateDal dal = new CandidateDal();
            var dalInitParams = dal.CreateInitParams();
            dalInitParams.Parameters["ConnectionString"] = initParams.ConnectionString;
            dal.Init(dalInitParams);

            return dal;
        }
    }
}
