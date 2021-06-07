using {DalImplNamespace};
using {DalNamespace};
using {DalNamespace}.Entities;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace {DalTestsNamespace}
{
    public class Test{Entity}Dal : TestBase
    {
        [Test]
        public void DalInit_Success()
        {
            IConfiguration config = GetConfiguration();
            var initParams = config.GetSection("DALInitParams").Get<TestDalInitParams>();

            I{Entity}Dal dal = new {Entity}Dal();
            var dalInitParams = dal.CreateInitParams();
            dalInitParams.Parameters["ConnectionString"] = initParams.ConnectionString;
            dal.Init(dalInitParams);
        }

        [Test]
        public void Get{Entity}s_Success()
        {
            var dal = Prepare{Entity}Dal("DALInitParams");

            IList<{Entity}> entities = dal.GetAll();

            Assert.IsNotNull(entities);
            Assert.IsNotEmpty(entities);
        }

        [TestCase("{Entity}\\000.GetDetails.Success")]
        public void Get{Entity}_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = Prepare{Entity}Dal("DALInitParams");

            object objId = SetupCase(conn, caseName);
            long id = (long)objId;

            {Entity} entity = dal.Get(id);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
            {TEST_GET_VALIDATION}
        }

        [Test]
        public void Get{Entity}_InvalidId()
        {
            long id = Int32.MaxValue - 1;
            var dal = Prepare{Entity}Dal("DALInitParams");

            {Entity} entity = dal.Get(id);

            Assert.IsNull(entity);
        }

        [TestCase("{Entity}\\010.Delete.Success")]
        public void Delete{Entity}_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = Prepare{Entity}Dal("DALInitParams");

            object objId = SetupCase(conn, caseName);
            long id = (long)objId;

            bool removed = dal.Delete(id);

            TeardownCase(conn, caseName);

            Assert.IsTrue(removed);
        }

        [Test]
        public void Delete{Entity}_InvalidId()
        {
            long positionId = Int32.MaxValue - 1;
            var dal = Prepare{Entity}Dal("DALInitParams");

            bool removed = dal.Delete(positionId);
            Assert.IsFalse(removed);

        }

        [TestCase("{Entity}\\020.Insert.Success")]
        public void Insert{Entity}_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            SetupCase(conn, caseName);

            var dal = Prepare{Entity}Dal("DALInitParams");

            var entity = new {Entity}();
            {SET_INSERT_ENTITY_VALUES}

            entity = dal.Upsert(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
            {TEST_INSERT_VALIDATION}
        }

        [TestCase("{Entity}\\030.Update.Success")]
        public void Update{Entity}_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = Prepare{Entity}Dal("DALInitParams");

            object objId = SetupCase(conn, caseName);
            long id = (long)objId;

            var entity = dal.Get(id);
            {SET_UPDATE_ENTITY_VALUES}

            entity = dal.Upsert(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
            {TEST_UPDATE_VALIDATION}
        }

        [Test]
        public void Update{Entity}_InvalidId()
        {
            var dal = Prepare{Entity}Dal("DALInitParams");

            var entity = new {Entity}();
            entity.ID = Int64.MaxValue - 1;
            {SET_UPDATE_ENTITY_VALUES}

            try
            {
                entity = dal.Upsert(entity);

                Assert.Fail("Fail - exception was expected, but wasn't thrown.");
            }
            catch(Exception ex)
            {
                Assert.Pass("Success - exception thrown as expected");
            }
        }
    }
}
