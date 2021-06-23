

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

            object objId = SetupCase(conn, caseName);
            long id = (long)objId;

            CandidateProperty entity = dal.Get(id);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual("Name 0fb8b7da0cee45568dbaaf9c4bb7d211", entity.Name);
                            Assert.AreEqual("Value 0fb8b7da0cee45568dbaaf9c4bb7d211", entity.Value);
                            Assert.AreEqual(100001, entity.CandidateID);
                      }

        [Test]
        public void CandidateProperty_GetDetails_InvalidId()
        {
            long id = Int32.MaxValue - 1;
            var dal = PrepareCandidatePropertyDal("DALInitParams");

            CandidateProperty entity = dal.Get(id);

            Assert.IsNull(entity);
        }

        [TestCase("CandidateProperty\\010.Delete.Success")]
        public void CandidateProperty_Delete_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareCandidatePropertyDal("DALInitParams");

            object objId = SetupCase(conn, caseName);
            long id = (long)objId;

            bool removed = dal.Delete(id);

            TeardownCase(conn, caseName);

            Assert.IsTrue(removed);
        }

        [Test]
        public void CandidateProperty_Delete_InvalidId()
        {
            long positionId = Int32.MaxValue - 1;
            var dal = PrepareCandidatePropertyDal("DALInitParams");

            bool removed = dal.Delete(positionId);
            Assert.IsFalse(removed);

        }

        [TestCase("CandidateProperty\\020.Insert.Success")]
        public void CandidateProperty_Insert_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            SetupCase(conn, caseName);

            var dal = PrepareCandidatePropertyDal("DALInitParams");

            var entity = new CandidateProperty();
                          entity.Name = "Name 2bf9c9a06ebe44e8995bb5376bd04e2f";
                            entity.Value = "Value 2bf9c9a06ebe44e8995bb5376bd04e2f";
                            entity.CandidateID = 110125;
                          
            entity = dal.Upsert(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual("Name 2bf9c9a06ebe44e8995bb5376bd04e2f", entity.Name);
                            Assert.AreEqual("Value 2bf9c9a06ebe44e8995bb5376bd04e2f", entity.Value);
                            Assert.AreEqual(110125, entity.CandidateID);
              
        }

        [TestCase("CandidateProperty\\030.Update.Success")]
        public void CandidateProperty_Update_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareCandidatePropertyDal("DALInitParams");

            object objId = SetupCase(conn, caseName);
            long id = (long)objId;

            var entity = dal.Get(id);
                          entity.Name = "Name 046a2ed31cb04722a362518c5fac27c8";
                            entity.Value = "Value 046a2ed31cb04722a362518c5fac27c8";
                            entity.CandidateID = 100007;
              
            entity = dal.Upsert(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual("Name 046a2ed31cb04722a362518c5fac27c8", entity.Name);
                            Assert.AreEqual("Value 046a2ed31cb04722a362518c5fac27c8", entity.Value);
                            Assert.AreEqual(100007, entity.CandidateID);
              
        }

        [Test]
        public void CandidateProperty_Update_InvalidId()
        {
            var dal = PrepareCandidatePropertyDal("DALInitParams");

            var entity = new CandidateProperty();
            entity.ID = Int64.MaxValue - 1;
                          entity.Name = "Name 046a2ed31cb04722a362518c5fac27c8";
                            entity.Value = "Value 046a2ed31cb04722a362518c5fac27c8";
                            entity.CandidateID = 100007;
              
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
