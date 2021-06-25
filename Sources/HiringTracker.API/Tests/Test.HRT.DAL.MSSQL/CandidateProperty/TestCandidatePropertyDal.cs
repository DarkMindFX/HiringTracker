

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
            
                          Assert.AreEqual("Name ddd7213b9da947e4b3b56e06e17f314d", entity.Name);
                            Assert.AreEqual("Value ddd7213b9da947e4b3b56e06e17f314d", entity.Value);
                            Assert.AreEqual(110125, entity.CandidateID);
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
                          entity.Name = "Name d2d7fef8621544f597409d464f728ee5";
                            entity.Value = "Value d2d7fef8621544f597409d464f728ee5";
                            entity.CandidateID = 100006;
                          
            entity = dal.Insert(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual("Name d2d7fef8621544f597409d464f728ee5", entity.Name);
                            Assert.AreEqual("Value d2d7fef8621544f597409d464f728ee5", entity.Value);
                            Assert.AreEqual(100006, entity.CandidateID);
              
        }

        [TestCase("CandidateProperty\\030.Update.Success")]
        public void CandidateProperty_Update_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareCandidatePropertyDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
                var paramID = (System.Int64?)objIds[0];
            CandidateProperty entity = dal.Get(paramID);

                          entity.Name = "Name c7653d3c4725408f8065efa8b8f1a4e4";
                            entity.Value = "Value c7653d3c4725408f8065efa8b8f1a4e4";
                            entity.CandidateID = 100001;
              
            entity = dal.Update(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual("Name c7653d3c4725408f8065efa8b8f1a4e4", entity.Name);
                            Assert.AreEqual("Value c7653d3c4725408f8065efa8b8f1a4e4", entity.Value);
                            Assert.AreEqual(100001, entity.CandidateID);
              
        }

        [Test]
        public void CandidateProperty_Update_InvalidId()
        {
            var dal = PrepareCandidatePropertyDal("DALInitParams");

            var entity = new CandidateProperty();
                          entity.Name = "Name c7653d3c4725408f8065efa8b8f1a4e4";
                            entity.Value = "Value c7653d3c4725408f8065efa8b8f1a4e4";
                            entity.CandidateID = 100001;
              
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
