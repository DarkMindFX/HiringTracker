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
    public class TestDepartmentDal : TestBase
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
            Assert.AreEqual("Name 592d8fc07d184399a7333dcb2f39b950", entity.Name);
            Assert.AreEqual("UUID 592d8fc07d184399a7333dcb2f39b950", entity.UUID);
            Assert.AreEqual(10, entity.ParentID);
            Assert.AreEqual(33000067, entity.ManagerID);

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

            var entity = new Department();
            entity.Name = "Name 6de9d6731940477b9c034dfae62e602b";
            entity.UUID = "UUID 6de9d6731940477b9c034dfae62e602b";
            entity.ParentID = 3;
            entity.ManagerID = 33000067;


            entity = dal.Upsert(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
            Assert.AreNotEqual(0, entity.ID);
            Assert.AreEqual("Name 6de9d6731940477b9c034dfae62e602b", entity.Name);
            Assert.AreEqual("UUID 6de9d6731940477b9c034dfae62e602b", entity.UUID);
            Assert.AreEqual(3, entity.ParentID);
            Assert.AreEqual(33000067, entity.ManagerID);

        }

        [TestCase("Department\\030.Update.Success")]
        public void UpdateDepartment_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareDepartmentDal("DALInitParams");

            object objId = SetupCase(conn, caseName);
            long id = (long)objId;

            var entity = dal.Get(id);
            entity.Name = "Name f7fd524bfc3345fcb2d05c2836a57ada";
            entity.UUID = "UUID f7fd524bfc3345fcb2d05c2836a57ada";
            entity.ParentID = 3;
            entity.ManagerID = 100001;


            entity = dal.Upsert(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
            Assert.AreNotEqual(0, entity.ID);
            Assert.AreEqual("Name f7fd524bfc3345fcb2d05c2836a57ada", entity.Name);
            Assert.AreEqual("UUID f7fd524bfc3345fcb2d05c2836a57ada", entity.UUID);
            Assert.AreEqual(3, entity.ParentID);
            Assert.AreEqual(100001, entity.ManagerID);

        }

        [Test]
        public void UpdateDepartment_InvalidId()
        {
            var dal = PrepareDepartmentDal("DALInitParams");

            var entity = new Department();
            entity.ID = Int64.MaxValue - 1;
            entity.Name = "Name f7fd524bfc3345fcb2d05c2836a57ada";
            entity.UUID = "UUID f7fd524bfc3345fcb2d05c2836a57ada";
            entity.ParentID = 3;
            entity.ManagerID = 100001;


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
