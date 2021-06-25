

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
            
                          Assert.AreEqual("FirstName 650cf135595f4253b0db2b1677150d4b", entity.FirstName);
                            Assert.AreEqual("MiddleName 650cf135595f4253b0db2b1677150d4b", entity.MiddleName);
                            Assert.AreEqual("LastName 650cf135595f4253b0db2b1677150d4b", entity.LastName);
                            Assert.AreEqual("Email 650cf135595f4253b0db2b1677150d4b", entity.Email);
                            Assert.AreEqual("Phone 650cf135595f4253b0db2b1677150d4b", entity.Phone);
                            Assert.AreEqual("CVLink 650cf135595f4253b0db2b1677150d4b", entity.CVLink);
                            Assert.AreEqual(100002, entity.CreatedByID);
                            Assert.AreEqual(DateTime.Parse("11/26/2020 6:13:11 PM"), entity.CreatedDate);
                            Assert.AreEqual(100002, entity.ModifiedByID);
                            Assert.AreEqual(DateTime.Parse("8/16/2022 9:49:11 PM"), entity.ModifiedDate);
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
                          entity.FirstName = "FirstName bdbedca22f9d4aebb1680b9ff061aee8";
                            entity.MiddleName = "MiddleName bdbedca22f9d4aebb1680b9ff061aee8";
                            entity.LastName = "LastName bdbedca22f9d4aebb1680b9ff061aee8";
                            entity.Email = "Email bdbedca22f9d4aebb1680b9ff061aee8";
                            entity.Phone = "Phone bdbedca22f9d4aebb1680b9ff061aee8";
                            entity.CVLink = "CVLink bdbedca22f9d4aebb1680b9ff061aee8";
                            entity.CreatedByID = 33000067;
                            entity.CreatedDate = DateTime.Parse("6/6/2020 10:24:11 AM");
                            entity.ModifiedByID = 100003;
                            entity.ModifiedDate = DateTime.Parse("1/11/2024 12:11:11 PM");
                          
            entity = dal.Insert(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual("FirstName bdbedca22f9d4aebb1680b9ff061aee8", entity.FirstName);
                            Assert.AreEqual("MiddleName bdbedca22f9d4aebb1680b9ff061aee8", entity.MiddleName);
                            Assert.AreEqual("LastName bdbedca22f9d4aebb1680b9ff061aee8", entity.LastName);
                            Assert.AreEqual("Email bdbedca22f9d4aebb1680b9ff061aee8", entity.Email);
                            Assert.AreEqual("Phone bdbedca22f9d4aebb1680b9ff061aee8", entity.Phone);
                            Assert.AreEqual("CVLink bdbedca22f9d4aebb1680b9ff061aee8", entity.CVLink);
                            Assert.AreEqual(33000067, entity.CreatedByID);
                            Assert.AreEqual(DateTime.Parse("6/6/2020 10:24:11 AM"), entity.CreatedDate);
                            Assert.AreEqual(100003, entity.ModifiedByID);
                            Assert.AreEqual(DateTime.Parse("1/11/2024 12:11:11 PM"), entity.ModifiedDate);
              
        }

        [TestCase("Candidate\\030.Update.Success")]
        public void Candidate_Update_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareCandidateDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
                var paramID = (System.Int64?)objIds[0];
            Candidate entity = dal.Get(paramID);

                          entity.FirstName = "FirstName 93d527c0eb04498cac8e3907077beac9";
                            entity.MiddleName = "MiddleName 93d527c0eb04498cac8e3907077beac9";
                            entity.LastName = "LastName 93d527c0eb04498cac8e3907077beac9";
                            entity.Email = "Email 93d527c0eb04498cac8e3907077beac9";
                            entity.Phone = "Phone 93d527c0eb04498cac8e3907077beac9";
                            entity.CVLink = "CVLink 93d527c0eb04498cac8e3907077beac9";
                            entity.CreatedByID = 100001;
                            entity.CreatedDate = DateTime.Parse("3/20/2023 7:51:11 AM");
                            entity.ModifiedByID = 100003;
                            entity.ModifiedDate = DateTime.Parse("12/15/2023 9:12:11 AM");
              
            entity = dal.Update(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual("FirstName 93d527c0eb04498cac8e3907077beac9", entity.FirstName);
                            Assert.AreEqual("MiddleName 93d527c0eb04498cac8e3907077beac9", entity.MiddleName);
                            Assert.AreEqual("LastName 93d527c0eb04498cac8e3907077beac9", entity.LastName);
                            Assert.AreEqual("Email 93d527c0eb04498cac8e3907077beac9", entity.Email);
                            Assert.AreEqual("Phone 93d527c0eb04498cac8e3907077beac9", entity.Phone);
                            Assert.AreEqual("CVLink 93d527c0eb04498cac8e3907077beac9", entity.CVLink);
                            Assert.AreEqual(100001, entity.CreatedByID);
                            Assert.AreEqual(DateTime.Parse("3/20/2023 7:51:11 AM"), entity.CreatedDate);
                            Assert.AreEqual(100003, entity.ModifiedByID);
                            Assert.AreEqual(DateTime.Parse("12/15/2023 9:12:11 AM"), entity.ModifiedDate);
              
        }

        [Test]
        public void Candidate_Update_InvalidId()
        {
            var dal = PrepareCandidateDal("DALInitParams");

            var entity = new Candidate();
                          entity.FirstName = "FirstName 93d527c0eb04498cac8e3907077beac9";
                            entity.MiddleName = "MiddleName 93d527c0eb04498cac8e3907077beac9";
                            entity.LastName = "LastName 93d527c0eb04498cac8e3907077beac9";
                            entity.Email = "Email 93d527c0eb04498cac8e3907077beac9";
                            entity.Phone = "Phone 93d527c0eb04498cac8e3907077beac9";
                            entity.CVLink = "CVLink 93d527c0eb04498cac8e3907077beac9";
                            entity.CreatedByID = 100001;
                            entity.CreatedDate = DateTime.Parse("3/20/2023 7:51:11 AM");
                            entity.ModifiedByID = 100003;
                            entity.ModifiedDate = DateTime.Parse("12/15/2023 9:12:11 AM");
              
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
