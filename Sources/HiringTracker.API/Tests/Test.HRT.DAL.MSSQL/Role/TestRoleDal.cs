

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
    public class TestRoleDal : TestBase
    {
        [Test]
        public void DalInit_Success()
        {
            IConfiguration config = GetConfiguration();
            var initParams = config.GetSection("DALInitParams").Get<TestDalInitParams>();

            IRoleDal dal = new RoleDal();
            var dalInitParams = dal.CreateInitParams();
            dalInitParams.Parameters["ConnectionString"] = initParams.ConnectionString;
            dal.Init(dalInitParams);
        }

        [Test]
        public void Role_GetAll_Success()
        {
            var dal = PrepareRoleDal("DALInitParams");

            IList<Role> entities = dal.GetAll();

            Assert.IsNotNull(entities);
            Assert.IsNotEmpty(entities);
        }

        [TestCase("Role\\000.GetDetails.Success")]
        public void Role_GetDetails_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareRoleDal("DALInitParams");

            object objId = SetupCase(conn, caseName);
            long id = (long)objId;

            Role entity = dal.Get(id);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual("Name 8e42f2ba8df5469b9cbe7b824ba7c0f0", entity.Name);
                      }

        [Test]
        public void Role_GetDetails_InvalidId()
        {
            long id = Int32.MaxValue - 1;
            var dal = PrepareRoleDal("DALInitParams");

            Role entity = dal.Get(id);

            Assert.IsNull(entity);
        }

        [TestCase("Role\\010.Delete.Success")]
        public void Role_Delete_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareRoleDal("DALInitParams");

            object objId = SetupCase(conn, caseName);
            long id = (long)objId;

            bool removed = dal.Delete(id);

            TeardownCase(conn, caseName);

            Assert.IsTrue(removed);
        }

        [Test]
        public void Role_Delete_InvalidId()
        {
            long positionId = Int32.MaxValue - 1;
            var dal = PrepareRoleDal("DALInitParams");

            bool removed = dal.Delete(positionId);
            Assert.IsFalse(removed);

        }

        [TestCase("Role\\020.Insert.Success")]
        public void Role_Insert_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            SetupCase(conn, caseName);

            var dal = PrepareRoleDal("DALInitParams");

            var entity = new Role();
                          entity.Name = "Name 25e33d9d41e44ae488e7676ab6d95fbe";
                          
            entity = dal.Upsert(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual("Name 25e33d9d41e44ae488e7676ab6d95fbe", entity.Name);
              
        }

        [TestCase("Role\\030.Update.Success")]
        public void Role_Update_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareRoleDal("DALInitParams");

            object objId = SetupCase(conn, caseName);
            long id = (long)objId;

            var entity = dal.Get(id);
                          entity.Name = "Name c7d442d127e9439e80fbdba6ea1ae6c0";
              
            entity = dal.Upsert(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual("Name c7d442d127e9439e80fbdba6ea1ae6c0", entity.Name);
              
        }

        [Test]
        public void Role_Update_InvalidId()
        {
            var dal = PrepareRoleDal("DALInitParams");

            var entity = new Role();
            entity.ID = Int64.MaxValue - 1;
                          entity.Name = "Name c7d442d127e9439e80fbdba6ea1ae6c0";
              
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

        protected IRoleDal PrepareRoleDal(string configName)
        {
            IConfiguration config = GetConfiguration();
            var initParams = config.GetSection(configName).Get<TestDalInitParams>();

            IRoleDal dal = new RoleDal();
            var dalInitParams = dal.CreateInitParams();
            dalInitParams.Parameters["ConnectionString"] = initParams.ConnectionString;
            dal.Init(dalInitParams);

            return dal;
        }
    }
}
