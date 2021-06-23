

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

            object objId = SetupCase(conn, caseName);
            long id = (long)objId;

            UserRoleSystem entity = dal.Get(id);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.UserID);
                        Assert.IsNotNull(entity.RoleID);
            
                          Assert.AreEqual(100002, entity.UserID);
                            Assert.AreEqual(2, entity.RoleID);
                      }

        [Test]
        public void UserRoleSystem_GetDetails_InvalidId()
        {
            long id = Int32.MaxValue - 1;
            var dal = PrepareUserRoleSystemDal("DALInitParams");

            UserRoleSystem entity = dal.Get(id);

            Assert.IsNull(entity);
        }

        [TestCase("UserRoleSystem\\010.Delete.Success")]
        public void UserRoleSystem_Delete_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareUserRoleSystemDal("DALInitParams");

            object objId = SetupCase(conn, caseName);
            long id = (long)objId;

            bool removed = dal.Delete(id);

            TeardownCase(conn, caseName);

            Assert.IsTrue(removed);
        }

        [Test]
        public void UserRoleSystem_Delete_InvalidId()
        {
            long positionId = Int32.MaxValue - 1;
            var dal = PrepareUserRoleSystemDal("DALInitParams");

            bool removed = dal.Delete(positionId);
            Assert.IsFalse(removed);

        }

        [TestCase("UserRoleSystem\\020.Insert.Success")]
        public void UserRoleSystem_Insert_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            SetupCase(conn, caseName);

            var dal = PrepareUserRoleSystemDal("DALInitParams");

            var entity = new UserRoleSystem();
                          entity.UserID = 33000067;
                            entity.RoleID = 1;
                          
            entity = dal.Upsert(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.UserID);
                        Assert.IsNotNull(entity.RoleID);
            
                          Assert.AreEqual(33000067, entity.UserID);
                            Assert.AreEqual(1, entity.RoleID);
              
        }

        [TestCase("UserRoleSystem\\030.Update.Success")]
        public void UserRoleSystem_Update_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareUserRoleSystemDal("DALInitParams");

            object objId = SetupCase(conn, caseName);
            long id = (long)objId;

            var entity = dal.Get(id);
                          entity.UserID = 33000067;
                            entity.RoleID = 4;
              
            entity = dal.Upsert(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.UserID);
                        Assert.IsNotNull(entity.RoleID);
            
                          Assert.AreEqual(33000067, entity.UserID);
                            Assert.AreEqual(4, entity.RoleID);
              
        }

        [Test]
        public void UserRoleSystem_Update_InvalidId()
        {
            var dal = PrepareUserRoleSystemDal("DALInitParams");

            var entity = new UserRoleSystem();
            entity.ID = Int64.MaxValue - 1;
                          entity.UserID = 33000067;
                            entity.RoleID = 4;
              
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
