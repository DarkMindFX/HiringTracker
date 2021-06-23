

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
        public void Department_GetAll_Success()
        {
            var dal = PrepareDepartmentDal("DALInitParams");

            IList<Department> entities = dal.GetAll();

            Assert.IsNotNull(entities);
            Assert.IsNotEmpty(entities);
        }

        [TestCase("Department\\000.GetDetails.Success")]
        public void Department_GetDetails_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareDepartmentDal("DALInitParams");

            object objId = SetupCase(conn, caseName);
            long id = (long)objId;

            Department entity = dal.Get(id);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual("Name 3c34d9a895104a7c9a93267c1fc5de33", entity.Name);
                            Assert.AreEqual("UUID 3c34d9a895104a7c9a93267c1fc5de33", entity.UUID);
                            Assert.AreEqual(2, entity.ParentID);
                            Assert.AreEqual(100001, entity.ManagerID);
                      }

        [Test]
        public void Department_GetDetails_InvalidId()
        {
            long id = Int32.MaxValue - 1;
            var dal = PrepareDepartmentDal("DALInitParams");

            Department entity = dal.Get(id);

            Assert.IsNull(entity);
        }

        [TestCase("Department\\010.Delete.Success")]
        public void Department_Delete_Success(string caseName)
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
        public void Department_Delete_InvalidId()
        {
            long positionId = Int32.MaxValue - 1;
            var dal = PrepareDepartmentDal("DALInitParams");

            bool removed = dal.Delete(positionId);
            Assert.IsFalse(removed);

        }

        [TestCase("Department\\020.Insert.Success")]
        public void Department_Insert_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            SetupCase(conn, caseName);

            var dal = PrepareDepartmentDal("DALInitParams");

            var entity = new Department();
                          entity.Name = "Name 48acf4d067e041519a5b59485c7add59";
                            entity.UUID = "UUID 48acf4d067e041519a5b59485c7add59";
                            entity.ParentID = 3;
                            entity.ManagerID = 33000067;
                          
            entity = dal.Upsert(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual("Name 48acf4d067e041519a5b59485c7add59", entity.Name);
                            Assert.AreEqual("UUID 48acf4d067e041519a5b59485c7add59", entity.UUID);
                            Assert.AreEqual(3, entity.ParentID);
                            Assert.AreEqual(33000067, entity.ManagerID);
              
        }

        [TestCase("Department\\030.Update.Success")]
        public void Department_Update_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareDepartmentDal("DALInitParams");

            object objId = SetupCase(conn, caseName);
            long id = (long)objId;

            var entity = dal.Get(id);
                          entity.Name = "Name 72b4b3e87cab47ccb389fb9d1b995d31";
                            entity.UUID = "UUID 72b4b3e87cab47ccb389fb9d1b995d31";
                            entity.ParentID = 3;
                            entity.ManagerID = 100002;
              
            entity = dal.Upsert(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual("Name 72b4b3e87cab47ccb389fb9d1b995d31", entity.Name);
                            Assert.AreEqual("UUID 72b4b3e87cab47ccb389fb9d1b995d31", entity.UUID);
                            Assert.AreEqual(3, entity.ParentID);
                            Assert.AreEqual(100002, entity.ManagerID);
              
        }

        [Test]
        public void Department_Update_InvalidId()
        {
            var dal = PrepareDepartmentDal("DALInitParams");

            var entity = new Department();
            entity.ID = Int64.MaxValue - 1;
                          entity.Name = "Name 72b4b3e87cab47ccb389fb9d1b995d31";
                            entity.UUID = "UUID 72b4b3e87cab47ccb389fb9d1b995d31";
                            entity.ParentID = 3;
                            entity.ManagerID = 100002;
              
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

        protected IDepartmentDal PrepareDepartmentDal(string configName)
        {
            IConfiguration config = GetConfiguration();
            var initParams = config.GetSection(configName).Get<TestDalInitParams>();

            IDepartmentDal dal = new DepartmentDal();
            var dalInitParams = dal.CreateInitParams();
            dalInitParams.Parameters["ConnectionString"] = initParams.ConnectionString;
            dal.Init(dalInitParams);

            return dal;
        }
    }
}
