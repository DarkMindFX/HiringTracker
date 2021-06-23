

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

            object objId = SetupCase(conn, caseName);
            long id = (long)objId;

            Candidate entity = dal.Get(id);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual("FirstName 9022f43bcc424fe6a8bc313d88f13ebe", entity.FirstName);
                            Assert.AreEqual("MiddleName 9022f43bcc424fe6a8bc313d88f13ebe", entity.MiddleName);
                            Assert.AreEqual("LastName 9022f43bcc424fe6a8bc313d88f13ebe", entity.LastName);
                            Assert.AreEqual("Email 9022f43bcc424fe6a8bc313d88f13ebe", entity.Email);
                            Assert.AreEqual("Phone 9022f43bcc424fe6a8bc313d88f13ebe", entity.Phone);
                            Assert.AreEqual("CVLink 9022f43bcc424fe6a8bc313d88f13ebe", entity.CVLink);
                            Assert.AreEqual(33000067, entity.CreatedByID);
                            Assert.AreEqual(DateTime.Parse("2/12/2020 8:17:10 AM"), entity.CreatedDate);
                            Assert.AreEqual(100002, entity.ModifiedByID);
                            Assert.AreEqual(DateTime.Parse("8/9/2020 2:04:10 PM"), entity.ModifiedDate);
                      }

        [Test]
        public void Candidate_GetDetails_InvalidId()
        {
            long id = Int32.MaxValue - 1;
            var dal = PrepareCandidateDal("DALInitParams");

            Candidate entity = dal.Get(id);

            Assert.IsNull(entity);
        }

        [TestCase("Candidate\\010.Delete.Success")]
        public void Candidate_Delete_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareCandidateDal("DALInitParams");

            object objId = SetupCase(conn, caseName);
            long id = (long)objId;

            bool removed = dal.Delete(id);

            TeardownCase(conn, caseName);

            Assert.IsTrue(removed);
        }

        [Test]
        public void Candidate_Delete_InvalidId()
        {
            long positionId = Int32.MaxValue - 1;
            var dal = PrepareCandidateDal("DALInitParams");

            bool removed = dal.Delete(positionId);
            Assert.IsFalse(removed);

        }

        [TestCase("Candidate\\020.Insert.Success")]
        public void Candidate_Insert_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            SetupCase(conn, caseName);

            var dal = PrepareCandidateDal("DALInitParams");

            var entity = new Candidate();
                          entity.FirstName = "FirstName ff91ce044a71401ba6de0bdef33ffc42";
                            entity.MiddleName = "MiddleName ff91ce044a71401ba6de0bdef33ffc42";
                            entity.LastName = "LastName ff91ce044a71401ba6de0bdef33ffc42";
                            entity.Email = "Email ff91ce044a71401ba6de0bdef33ffc42";
                            entity.Phone = "Phone ff91ce044a71401ba6de0bdef33ffc42";
                            entity.CVLink = "CVLink ff91ce044a71401ba6de0bdef33ffc42";
                            entity.CreatedByID = 100003;
                            entity.CreatedDate = DateTime.Parse("12/16/2019 1:41:10 PM");
                            entity.ModifiedByID = 33020042;
                            entity.ModifiedDate = DateTime.Parse("6/11/2020 10:08:10 AM");
                          
            entity = dal.Upsert(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual("FirstName ff91ce044a71401ba6de0bdef33ffc42", entity.FirstName);
                            Assert.AreEqual("MiddleName ff91ce044a71401ba6de0bdef33ffc42", entity.MiddleName);
                            Assert.AreEqual("LastName ff91ce044a71401ba6de0bdef33ffc42", entity.LastName);
                            Assert.AreEqual("Email ff91ce044a71401ba6de0bdef33ffc42", entity.Email);
                            Assert.AreEqual("Phone ff91ce044a71401ba6de0bdef33ffc42", entity.Phone);
                            Assert.AreEqual("CVLink ff91ce044a71401ba6de0bdef33ffc42", entity.CVLink);
                            Assert.AreEqual(100003, entity.CreatedByID);
                            Assert.AreEqual(DateTime.Parse("12/16/2019 1:41:10 PM"), entity.CreatedDate);
                            Assert.AreEqual(33020042, entity.ModifiedByID);
                            Assert.AreEqual(DateTime.Parse("6/11/2020 10:08:10 AM"), entity.ModifiedDate);
              
        }

        [TestCase("Candidate\\030.Update.Success")]
        public void Candidate_Update_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareCandidateDal("DALInitParams");

            object objId = SetupCase(conn, caseName);
            long id = (long)objId;

            var entity = dal.Get(id);
                          entity.FirstName = "FirstName 7e42a2d06757489da13465990cd1e3f4";
                            entity.MiddleName = "MiddleName 7e42a2d06757489da13465990cd1e3f4";
                            entity.LastName = "LastName 7e42a2d06757489da13465990cd1e3f4";
                            entity.Email = "Email 7e42a2d06757489da13465990cd1e3f4";
                            entity.Phone = "Phone 7e42a2d06757489da13465990cd1e3f4";
                            entity.CVLink = "CVLink 7e42a2d06757489da13465990cd1e3f4";
                            entity.CreatedByID = 100003;
                            entity.CreatedDate = DateTime.Parse("3/1/2022 1:44:10 PM");
                            entity.ModifiedByID = 33020042;
                            entity.ModifiedDate = DateTime.Parse("2/24/2023 1:18:10 AM");
              
            entity = dal.Upsert(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual("FirstName 7e42a2d06757489da13465990cd1e3f4", entity.FirstName);
                            Assert.AreEqual("MiddleName 7e42a2d06757489da13465990cd1e3f4", entity.MiddleName);
                            Assert.AreEqual("LastName 7e42a2d06757489da13465990cd1e3f4", entity.LastName);
                            Assert.AreEqual("Email 7e42a2d06757489da13465990cd1e3f4", entity.Email);
                            Assert.AreEqual("Phone 7e42a2d06757489da13465990cd1e3f4", entity.Phone);
                            Assert.AreEqual("CVLink 7e42a2d06757489da13465990cd1e3f4", entity.CVLink);
                            Assert.AreEqual(100003, entity.CreatedByID);
                            Assert.AreEqual(DateTime.Parse("3/1/2022 1:44:10 PM"), entity.CreatedDate);
                            Assert.AreEqual(33020042, entity.ModifiedByID);
                            Assert.AreEqual(DateTime.Parse("2/24/2023 1:18:10 AM"), entity.ModifiedDate);
              
        }

        [Test]
        public void Candidate_Update_InvalidId()
        {
            var dal = PrepareCandidateDal("DALInitParams");

            var entity = new Candidate();
            entity.ID = Int64.MaxValue - 1;
                          entity.FirstName = "FirstName 7e42a2d06757489da13465990cd1e3f4";
                            entity.MiddleName = "MiddleName 7e42a2d06757489da13465990cd1e3f4";
                            entity.LastName = "LastName 7e42a2d06757489da13465990cd1e3f4";
                            entity.Email = "Email 7e42a2d06757489da13465990cd1e3f4";
                            entity.Phone = "Phone 7e42a2d06757489da13465990cd1e3f4";
                            entity.CVLink = "CVLink 7e42a2d06757489da13465990cd1e3f4";
                            entity.CreatedByID = 100003;
                            entity.CreatedDate = DateTime.Parse("3/1/2022 1:44:10 PM");
                            entity.ModifiedByID = 33020042;
                            entity.ModifiedDate = DateTime.Parse("2/24/2023 1:18:10 AM");
              
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
