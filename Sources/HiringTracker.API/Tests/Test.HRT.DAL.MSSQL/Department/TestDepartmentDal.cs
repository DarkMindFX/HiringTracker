

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

            IList<object> objIds = SetupCase(conn, caseName);
                var paramID = (System.Int64?)objIds[0];
            Department entity = dal.Get(paramID);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual("Name 920424c250c54fb7a296edac15de7fcf", entity.Name);
                            Assert.AreEqual("UUID 920424c250c54fb7a296edac15de7fcf", entity.UUID);
                            Assert.AreEqual(10, entity.ParentID);
                            Assert.AreEqual(33000067, entity.ManagerID);
                      }

        [Test]
        public void Department_GetDetails_InvalidId()
        {
                var paramID = Int64.MaxValue - 1;
            var dal = PrepareDepartmentDal("DALInitParams");

            Department entity = dal.Get(paramID);

            Assert.IsNull(entity);
        }

        [TestCase("Department\\010.Delete.Success")]
        public void Department_Delete_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareDepartmentDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
                var paramID = (System.Int64?)objIds[0];
            bool removed = dal.Delete(paramID);

            TeardownCase(conn, caseName);

            Assert.IsTrue(removed);
        }

        [Test]
        public void Department_Delete_InvalidId()
        {
            var dal = PrepareDepartmentDal("DALInitParams");
                var paramID = Int64.MaxValue - 1;
   
            bool removed = dal.Delete(paramID);
            Assert.IsFalse(removed);

        }

        [TestCase("Department\\020.Insert.Success")]
        public void Department_Insert_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            SetupCase(conn, caseName);

            var dal = PrepareDepartmentDal("DALInitParams");

            var entity = new Department();
                          entity.Name = "Name 93fe3f52bdd74b128b9df16c8f34a4c1";
                            entity.UUID = "UUID 93fe3f52bdd74b128b9df16c8f34a4c1";
                            entity.ParentID = 3;
                            entity.ManagerID = 33020042;
                          
            entity = dal.Insert(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual("Name 93fe3f52bdd74b128b9df16c8f34a4c1", entity.Name);
                            Assert.AreEqual("UUID 93fe3f52bdd74b128b9df16c8f34a4c1", entity.UUID);
                            Assert.AreEqual(3, entity.ParentID);
                            Assert.AreEqual(33020042, entity.ManagerID);
              
        }

        [TestCase("Department\\030.Update.Success")]
        public void Department_Update_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareDepartmentDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
                var paramID = (System.Int64?)objIds[0];
            Department entity = dal.Get(paramID);

                          entity.Name = "Name f73b44dc9b8d44818752e9c422ec3cf9";
                            entity.UUID = "UUID f73b44dc9b8d44818752e9c422ec3cf9";
                            entity.ParentID = 3;
                            entity.ManagerID = 100002;
              
            entity = dal.Update(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual("Name f73b44dc9b8d44818752e9c422ec3cf9", entity.Name);
                            Assert.AreEqual("UUID f73b44dc9b8d44818752e9c422ec3cf9", entity.UUID);
                            Assert.AreEqual(3, entity.ParentID);
                            Assert.AreEqual(100002, entity.ManagerID);
              
        }

        [Test]
        public void Department_Update_InvalidId()
        {
            var dal = PrepareDepartmentDal("DALInitParams");

            var entity = new Department();
                          entity.Name = "Name f73b44dc9b8d44818752e9c422ec3cf9";
                            entity.UUID = "UUID f73b44dc9b8d44818752e9c422ec3cf9";
                            entity.ParentID = 3;
                            entity.ManagerID = 100002;
              
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
