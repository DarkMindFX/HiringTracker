

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
    public class TestCandidatePropertyDal : TestBase
    {
        [Test]
        public void DalInit_Success()
        {
            IConfiguration config = GetConfiguration();
            var initParams = config.GetSection("DALInitParams").Get<TestDalInitParams>();

            ICandidatePropertyDal dal = new CandidatePropertyDal();
            var dalInitParams = dal.CreateInitParams();
            dalInitParams.Parameters["ConnectionString"] = initParams.ConnectionString;
            dal.Init(dalInitParams);
        }

        [Test]
        public void CandidateProperty_GetAll_Success()
        {
            var dal = PrepareCandidatePropertyDal("DALInitParams");

            IList<CandidateProperty> entities = dal.GetAll();

            Assert.IsNotNull(entities);
            Assert.IsNotEmpty(entities);
        }

        [TestCase("CandidateProperty\\000.GetDetails.Success")]
        public void CandidateProperty_GetDetails_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareCandidatePropertyDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
                var paramID = (System.Int64?)objIds[0];
            CandidateProperty entity = dal.Get(paramID);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual("Name 3ecfa5fab0a148c29bdc2e3384c0eb56", entity.Name);
                            Assert.AreEqual("Value 3ecfa5fab0a148c29bdc2e3384c0eb56", entity.Value);
                            Assert.AreEqual(100009, entity.CandidateID);
                      }

        [Test]
        public void CandidateProperty_GetDetails_InvalidId()
        {
                var paramID = Int64.MaxValue - 1;
            var dal = PrepareCandidatePropertyDal("DALInitParams");

            CandidateProperty entity = dal.Get(paramID);

            Assert.IsNull(entity);
        }

        [TestCase("CandidateProperty\\010.Delete.Success")]
        public void CandidateProperty_Delete_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareCandidatePropertyDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
                var paramID = (System.Int64?)objIds[0];
            bool removed = dal.Delete(paramID);

            TeardownCase(conn, caseName);

            Assert.IsTrue(removed);
        }

        [Test]
        public void CandidateProperty_Delete_InvalidId()
        {
            var dal = PrepareCandidatePropertyDal("DALInitParams");
                var paramID = Int64.MaxValue - 1;
   
            bool removed = dal.Delete(paramID);
            Assert.IsFalse(removed);

        }

        [TestCase("CandidateProperty\\020.Insert.Success")]
        public void CandidateProperty_Insert_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            SetupCase(conn, caseName);

            var dal = PrepareCandidatePropertyDal("DALInitParams");

            var entity = new CandidateProperty();
                          entity.Name = "Name 9adfdf84550a4945aeaaca8b894fd36d";
                            entity.Value = "Value 9adfdf84550a4945aeaaca8b894fd36d";
                            entity.CandidateID = 100005;
                          
            entity = dal.Insert(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual("Name 9adfdf84550a4945aeaaca8b894fd36d", entity.Name);
                            Assert.AreEqual("Value 9adfdf84550a4945aeaaca8b894fd36d", entity.Value);
                            Assert.AreEqual(100005, entity.CandidateID);
              
        }

        [TestCase("CandidateProperty\\030.Update.Success")]
        public void CandidateProperty_Update_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareCandidatePropertyDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
                var paramID = (System.Int64?)objIds[0];
            CandidateProperty entity = dal.Get(paramID);

                          entity.Name = "Name afd740f712c34083bbd38a38a4d2ba16";
                            entity.Value = "Value afd740f712c34083bbd38a38a4d2ba16";
                            entity.CandidateID = 100007;
              
            entity = dal.Update(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual("Name afd740f712c34083bbd38a38a4d2ba16", entity.Name);
                            Assert.AreEqual("Value afd740f712c34083bbd38a38a4d2ba16", entity.Value);
                            Assert.AreEqual(100007, entity.CandidateID);
              
        }

        [Test]
        public void CandidateProperty_Update_InvalidId()
        {
            var dal = PrepareCandidatePropertyDal("DALInitParams");

            var entity = new CandidateProperty();
                          entity.Name = "Name afd740f712c34083bbd38a38a4d2ba16";
                            entity.Value = "Value afd740f712c34083bbd38a38a4d2ba16";
                            entity.CandidateID = 100007;
              
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

        protected ICandidatePropertyDal PrepareCandidatePropertyDal(string configName)
        {
            IConfiguration config = GetConfiguration();
            var initParams = config.GetSection(configName).Get<TestDalInitParams>();

            ICandidatePropertyDal dal = new CandidatePropertyDal();
            var dalInitParams = dal.CreateInitParams();
            dalInitParams.Parameters["ConnectionString"] = initParams.ConnectionString;
            dal.Init(dalInitParams);

            return dal;
        }
    }
}
