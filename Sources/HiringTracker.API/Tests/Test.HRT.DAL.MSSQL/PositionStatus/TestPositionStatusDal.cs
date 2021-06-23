

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
    public class TestPositionStatusDal : TestBase
    {
        [Test]
        public void DalInit_Success()
        {
            IConfiguration config = GetConfiguration();
            var initParams = config.GetSection("DALInitParams").Get<TestDalInitParams>();

            IPositionStatusDal dal = new PositionStatusDal();
            var dalInitParams = dal.CreateInitParams();
            dalInitParams.Parameters["ConnectionString"] = initParams.ConnectionString;
            dal.Init(dalInitParams);
        }

        [Test]
        public void PositionStatus_GetAll_Success()
        {
            var dal = PreparePositionStatusDal("DALInitParams");

            IList<PositionStatus> entities = dal.GetAll();

            Assert.IsNotNull(entities);
            Assert.IsNotEmpty(entities);
        }

        [TestCase("PositionStatus\\000.GetDetails.Success")]
        public void PositionStatus_GetDetails_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PreparePositionStatusDal("DALInitParams");

            object objId = SetupCase(conn, caseName);
            long id = (long)objId;

            PositionStatus entity = dal.Get(id);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual("Name 61bbb193c1fd46cca7f3ffa3d373b850", entity.Name);
                      }

        [Test]
        public void PositionStatus_GetDetails_InvalidId()
        {
            long id = Int32.MaxValue - 1;
            var dal = PreparePositionStatusDal("DALInitParams");

            PositionStatus entity = dal.Get(id);

            Assert.IsNull(entity);
        }

        [TestCase("PositionStatus\\010.Delete.Success")]
        public void PositionStatus_Delete_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PreparePositionStatusDal("DALInitParams");

            object objId = SetupCase(conn, caseName);
            long id = (long)objId;

            bool removed = dal.Delete(id);

            TeardownCase(conn, caseName);

            Assert.IsTrue(removed);
        }

        [Test]
        public void PositionStatus_Delete_InvalidId()
        {
            long positionId = Int32.MaxValue - 1;
            var dal = PreparePositionStatusDal("DALInitParams");

            bool removed = dal.Delete(positionId);
            Assert.IsFalse(removed);

        }

        [TestCase("PositionStatus\\020.Insert.Success")]
        public void PositionStatus_Insert_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            SetupCase(conn, caseName);

            var dal = PreparePositionStatusDal("DALInitParams");

            var entity = new PositionStatus();
                          entity.Name = "Name d4a8a410a2f04f92ba6ef610811c5fa0";
                          
            entity = dal.Upsert(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual("Name d4a8a410a2f04f92ba6ef610811c5fa0", entity.Name);
              
        }

        [TestCase("PositionStatus\\030.Update.Success")]
        public void PositionStatus_Update_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PreparePositionStatusDal("DALInitParams");

            object objId = SetupCase(conn, caseName);
            long id = (long)objId;

            var entity = dal.Get(id);
                          entity.Name = "Name 7fe280be078f4639b33852466bc90a66";
              
            entity = dal.Upsert(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual("Name 7fe280be078f4639b33852466bc90a66", entity.Name);
              
        }

        [Test]
        public void PositionStatus_Update_InvalidId()
        {
            var dal = PreparePositionStatusDal("DALInitParams");

            var entity = new PositionStatus();
            entity.ID = Int64.MaxValue - 1;
                          entity.Name = "Name 7fe280be078f4639b33852466bc90a66";
              
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

        protected IPositionStatusDal PreparePositionStatusDal(string configName)
        {
            IConfiguration config = GetConfiguration();
            var initParams = config.GetSection(configName).Get<TestDalInitParams>();

            IPositionStatusDal dal = new PositionStatusDal();
            var dalInitParams = dal.CreateInitParams();
            dalInitParams.Parameters["ConnectionString"] = initParams.ConnectionString;
            dal.Init(dalInitParams);

            return dal;
        }
    }
}
