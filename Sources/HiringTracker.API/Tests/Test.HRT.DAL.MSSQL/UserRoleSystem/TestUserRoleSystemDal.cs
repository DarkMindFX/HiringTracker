

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
    public class TestUserRoleSystemDal : TestBase
    {
        [Test]
        public void DalInit_Success()
        {
            IConfiguration config = GetConfiguration();
            var initParams = config.GetSection("DALInitParams").Get<TestDalInitParams>();

            IUserRoleSystemDal dal = new UserRoleSystemDal();
            var dalInitParams = dal.CreateInitParams();
            dalInitParams.Parameters["ConnectionString"] = initParams.ConnectionString;
            dal.Init(dalInitParams);
        }

        [Test]
        public void UserRoleSystem_GetAll_Success()
        {
            var dal = PrepareUserRoleSystemDal("DALInitParams");

            IList<UserRoleSystem> entities = dal.GetAll();

            Assert.IsNotNull(entities);
            Assert.IsNotEmpty(entities);
        }

        [TestCase("UserRoleSystem\\000.GetDetails.Success")]
        public void UserRoleSystem_GetDetails_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareUserRoleSystemDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
            var paramUserID = (System.Int64)objIds[0];
            var paramRoleID = (System.Int64)objIds[1];
            UserRoleSystem entity = dal.Get(paramUserID, paramRoleID);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
            Assert.IsNotNull(entity.UserID);
            Assert.IsNotNull(entity.RoleID);

            Assert.AreEqual(100004, entity.UserID);
            Assert.AreEqual(2, entity.RoleID);
        }

        [Test]
        public void UserRoleSystem_GetDetails_InvalidId()
        {
            var paramUserID = Int64.MaxValue - 1;
            var paramRoleID = Int64.MaxValue - 1;
            var dal = PrepareUserRoleSystemDal("DALInitParams");

            UserRoleSystem entity = dal.Get(paramUserID, paramRoleID);

            Assert.IsNull(entity);
        }

        [TestCase("UserRoleSystem\\010.Delete.Success")]
        public void UserRoleSystem_Delete_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareUserRoleSystemDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
            var paramUserID = (System.Int64)objIds[0];
            var paramRoleID = (System.Int64)objIds[1];
            bool removed = dal.Delete(paramUserID, paramRoleID);

            TeardownCase(conn, caseName);

            Assert.IsTrue(removed);
        }

        [Test]
        public void UserRoleSystem_Delete_InvalidId()
        {
            var dal = PrepareUserRoleSystemDal("DALInitParams");
            var paramUserID = Int64.MaxValue - 1;
            var paramRoleID = Int64.MaxValue - 1;

            bool removed = dal.Delete(paramUserID, paramRoleID);
            Assert.IsFalse(removed);

        }

        [TestCase("UserRoleSystem\\020.Insert.Success")]
        public void UserRoleSystem_Insert_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            SetupCase(conn, caseName);

            var dal = PrepareUserRoleSystemDal("DALInitParams");

            var entity = new UserRoleSystem();
            entity.UserID = 100001;
            entity.RoleID = 6;

            entity = dal.Insert(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
            Assert.IsNotNull(entity.UserID);
            Assert.IsNotNull(entity.RoleID);

            Assert.AreEqual(100001, entity.UserID);
            Assert.AreEqual(6, entity.RoleID);

        }

        [TestCase("UserRoleSystem\\030.Update.Success")]
        public void UserRoleSystem_Update_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareUserRoleSystemDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
            var paramUserID = (System.Int64)objIds[0];
            var paramRoleID = (System.Int64)objIds[1];
            UserRoleSystem entity = dal.Get(paramUserID, paramRoleID);
            entity.RoleID = 5;

            entity = dal.Update(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
            Assert.IsNotNull(entity.UserID);
            Assert.IsNotNull(entity.RoleID);

            Assert.AreEqual(100004, entity.UserID);
            Assert.AreEqual(5, entity.RoleID);

        }

        [Test]
        public void UserRoleSystem_Update_InvalidId()
        {
            var dal = PrepareUserRoleSystemDal("DALInitParams");

            var entity = new UserRoleSystem();
            entity.UserID = 100004;
            entity.RoleID = 9;

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

        protected IUserRoleSystemDal PrepareUserRoleSystemDal(string configName)
        {
            IConfiguration config = GetConfiguration();
            var initParams = config.GetSection(configName).Get<TestDalInitParams>();

            IUserRoleSystemDal dal = new UserRoleSystemDal();
            var dalInitParams = dal.CreateInitParams();
            dalInitParams.Parameters["ConnectionString"] = initParams.ConnectionString;
            dal.Init(dalInitParams);

            return dal;
        }
    }
}
