using HRT.DAL.MSSQL;
using HRT.Interfaces;
using HRT.Interfaces.Entities;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

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
        public void GetPositionStatuss_Success()
        {
            var dal = PreparePositionStatusDal("DALInitParams");

            IList<PositionStatus> entities = dal.GetAll();

            Assert.IsNotNull(entities);
            Assert.IsNotEmpty(entities);
        }

        [TestCase("PositionStatus\\000.GetDetails.Success")]
        public void GetPositionStatus_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PreparePositionStatusDal("DALInitParams");

            object objId = SetupCase(conn, caseName);
            long id = (long)objId;

            PositionStatus entity = dal.Get(id);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
            Assert.AreNotEqual(0, entity.ID);
            Assert.AreEqual("Name 456040d680694451879c2a07a67832b4", entity.Name);

        }

        [Test]
        public void GetPositionStatus_InvalidId()
        {
            long id = Int32.MaxValue - 1;
            var dal = PreparePositionStatusDal("DALInitParams");

            PositionStatus entity = dal.Get(id);

            Assert.IsNull(entity);
        }

        [TestCase("PositionStatus\\010.Delete.Success")]
        public void DeletePositionStatus_Success(string caseName)
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
        public void DeletePositionStatus_InvalidId()
        {
            long positionId = Int32.MaxValue - 1;
            var dal = PreparePositionStatusDal("DALInitParams");

            bool removed = dal.Delete(positionId);
            Assert.IsFalse(removed);

        }

        [TestCase("PositionStatus\\020.Insert.Success")]
        public void InsertPositionStatus_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            SetupCase(conn, caseName);

            var dal = PreparePositionStatusDal("DALInitParams");

            var entity = new PositionStatus();
            entity.Name = "Name 49cad6d4e14544d6ad014fcdd1628769";


            entity = dal.Upsert(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
            Assert.AreNotEqual(0, entity.ID);
            Assert.AreEqual("Name 49cad6d4e14544d6ad014fcdd1628769", entity.Name);

        }

        [TestCase("PositionStatus\\030.Update.Success")]
        public void UpdatePositionStatus_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PreparePositionStatusDal("DALInitParams");

            object objId = SetupCase(conn, caseName);
            long id = (long)objId;

            var entity = dal.Get(id);
            entity.Name = "Name 0fbae53dd2784151b5446f9023aca330";


            entity = dal.Upsert(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
            Assert.AreNotEqual(0, entity.ID);
            Assert.AreEqual("Name 0fbae53dd2784151b5446f9023aca330", entity.Name);

        }

        [Test]
        public void UpdatePositionStatus_InvalidId()
        {
            var dal = PreparePositionStatusDal("DALInitParams");

            var entity = new PositionStatus();
            entity.ID = Int64.MaxValue - 1;
            entity.Name = "Name 0fbae53dd2784151b5446f9023aca330";


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
    }
}
