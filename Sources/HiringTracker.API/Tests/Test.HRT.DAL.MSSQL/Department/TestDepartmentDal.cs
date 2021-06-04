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
    public class TestDepartmenteDal : TestBase
    {
        [Test]
        public void DalInit_Success()
        {
            IConfiguration config = GetConfiguration();
            var initParams = config.GetSection("DALInitParams").Get<TestDalInitParams>();

            IDepartmentDal dal = new DepartmentDal();
            var dalInitParams = dal.CreateInitParams();
            dalInitParams.Parameters["ConnectionString"] = initParams.ConnectionString;
            dal.Init(dalInitParams);
        }

        [Test]
        public void GetDepartments_Success()
        {
            var dal = PrepareDepartmentDal("DALInitParams");

            IList<Department> entities = dal.GetAll();

            Assert.IsNotNull(entities);
            Assert.IsNotEmpty(entities);
        }

        [TestCase("Department\\000.GetDetails.Success")]
        public void GetDepartment_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareDepartmentDal("DALInitParams");

            object objId = SetupCase(conn, caseName);
            long id = (long)objId;

            Department entity = dal.Get(id);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
            Assert.AreNotEqual(0, entity.ID);
            Assert.IsTrue(!string.IsNullOrEmpty(entity.Name));
            Assert.IsTrue(!string.IsNullOrEmpty(entity.UUID));
        }

        [Test]
        public void GetDepartment_InvalidId()
        {
            long id = Int32.MaxValue - 1;
            var dal = PrepareDepartmentDal("DALInitParams");

            Department entity = dal.Get(id);

            Assert.IsNull(entity);
        }

        [TestCase("Department\\010.Delete.Success")]
        public void DeleteDepartment_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareDepartmentDal("DALInitParams");

            object objId = SetupCase(conn, caseName);
            long id = (long)objId;

            bool removed = dal.Delete(id);

            TeardownCase(conn, caseName);

            Assert.IsTrue(removed);
        }

        [Test]
        public void DeleteDepartment_InvalidId()
        {
            long positionId = Int32.MaxValue - 1;
            var dal = PrepareDepartmentDal("DALInitParams");

            bool removed = dal.Delete(positionId);
            Assert.IsFalse(removed);

        }

        [TestCase("Department\\020.Insert.Success")]
        public void InsertDepartment_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            SetupCase(conn, caseName);

            var dal = PrepareDepartmentDal("DALInitParams");

            var newEntity = new Department();
            newEntity.Name = "[Name 629AB76A4712415AA4DD03B137634B3F]";
            newEntity.UUID = "[UUID 629AB76A4712415AA4DD03B137634B3F]";
            newEntity.ParentID = null;

            var entity = dal.Upsert(newEntity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity.ID);
            Assert.AreNotEqual(0, entity.ID);
            Assert.AreEqual(newEntity.Name, entity.Name);
            Assert.AreEqual(newEntity.UUID, entity.UUID);
            Assert.AreEqual(newEntity.ParentID, entity.ParentID);


        }

        [TestCase("Department\\030.Update.Success")]
        public void UpdateDepartment_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareDepartmentDal("DALInitParams");

            object objId = SetupCase(conn, caseName);
            long id = (long)objId;

            var entity = dal.Get(id);
            entity.Name = "[Name 5BBC53BD43DA43E8817F3E9713895428]_UPD";
            entity.UUID = "[UUID 5BBC53BD43DA43E8817F3E9713895428]_UPD";
            entity.ParentID = PrepareDepartmentDal("DALInitParams").GetAll().First().ID;

            var updatedEntity = dal.Upsert(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(updatedEntity);
            Assert.AreEqual(entity.ID, updatedEntity.ID);
            Assert.AreEqual(entity.Name, updatedEntity.Name);
            Assert.AreEqual(entity.UUID, updatedEntity.UUID);
            Assert.AreEqual(entity.ParentID, updatedEntity.ParentID);
        }

        [Test]
        public void UpdateDepartment_InvalidId()
        {
            var dal = PrepareDepartmentDal("DALInitParams");

            var newEntity = new Department();
            newEntity.ID = Int64.MaxValue - 1;
            newEntity.Name = "[Name AB9AB76A4712415AQQQDD03B137634B3F]_UPD";
            newEntity.UUID = "[UUID AB9AB76A4712415AQQQDD03B137634B3F]_UPD";
            newEntity.ParentID = null;

            try
            {
                var entity = dal.Upsert(newEntity);

                Assert.Fail("Fail - exception was expected, but wasn't thrown.");
            }
            catch(Exception ex)
            {
                Assert.Pass("Success - exception thrown as expected");
            }
        }

        
        
    }
}
