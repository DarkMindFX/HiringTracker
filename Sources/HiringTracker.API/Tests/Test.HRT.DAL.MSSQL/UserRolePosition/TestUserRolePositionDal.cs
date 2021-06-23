

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
    public class TestUserRolePositionDal : TestBase
    {
        [Test]
        public void DalInit_Success()
        {
            IConfiguration config = GetConfiguration();
            var initParams = config.GetSection("DALInitParams").Get<TestDalInitParams>();

            IUserRolePositionDal dal = new UserRolePositionDal();
            var dalInitParams = dal.CreateInitParams();
            dalInitParams.Parameters["ConnectionString"] = initParams.ConnectionString;
            dal.Init(dalInitParams);
        }

        [Test]
        public void UserRolePosition_GetAll_Success()
        {
            var dal = PrepareUserRolePositionDal("DALInitParams");

            IList<UserRolePosition> entities = dal.GetAll();

            Assert.IsNotNull(entities);
            Assert.IsNotEmpty(entities);
        }

        [TestCase("UserRolePosition\\000.GetDetails.Success")]
        public void UserRolePosition_GetDetails_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareUserRolePositionDal("DALInitParams");

            object objId = SetupCase(conn, caseName);
            long id = (long)objId;

            UserRolePosition entity = dal.Get(id);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.PositionID);
                        Assert.IsNotNull(entity.UserID);
            
                          Assert.AreEqual(100001, entity.PositionID);
                            Assert.AreEqual(100003, entity.UserID);
                            Assert.AreEqual(5, entity.RoleID);
                      }

        [Test]
        public void UserRolePosition_GetDetails_InvalidId()
        {
            long id = Int32.MaxValue - 1;
            var dal = PrepareUserRolePositionDal("DALInitParams");

            UserRolePosition entity = dal.Get(id);

            Assert.IsNull(entity);
        }

        [TestCase("UserRolePosition\\010.Delete.Success")]
        public void UserRolePosition_Delete_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareUserRolePositionDal("DALInitParams");

            object objId = SetupCase(conn, caseName);
            long id = (long)objId;

            bool removed = dal.Delete(id);

            TeardownCase(conn, caseName);

            Assert.IsTrue(removed);
        }

        [Test]
        public void UserRolePosition_Delete_InvalidId()
        {
            long positionId = Int32.MaxValue - 1;
            var dal = PrepareUserRolePositionDal("DALInitParams");

            bool removed = dal.Delete(positionId);
            Assert.IsFalse(removed);

        }

        [TestCase("UserRolePosition\\020.Insert.Success")]
        public void UserRolePosition_Insert_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            SetupCase(conn, caseName);

            var dal = PrepareUserRolePositionDal("DALInitParams");

            var entity = new UserRolePosition();
                          entity.PositionID = 100007;
                            entity.UserID = 33000067;
                            entity.RoleID = 5;
                          
            entity = dal.Upsert(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.PositionID);
                        Assert.IsNotNull(entity.UserID);
            
                          Assert.AreEqual(100007, entity.PositionID);
                            Assert.AreEqual(33000067, entity.UserID);
                            Assert.AreEqual(5, entity.RoleID);
              
        }

        [TestCase("UserRolePosition\\030.Update.Success")]
        public void UserRolePosition_Update_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareUserRolePositionDal("DALInitParams");

            object objId = SetupCase(conn, caseName);
            long id = (long)objId;

            var entity = dal.Get(id);
                          entity.PositionID = 110118;
                            entity.UserID = 33020042;
                            entity.RoleID = 1;
              
            entity = dal.Upsert(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.PositionID);
                        Assert.IsNotNull(entity.UserID);
            
                          Assert.AreEqual(110118, entity.PositionID);
                            Assert.AreEqual(33020042, entity.UserID);
                            Assert.AreEqual(1, entity.RoleID);
              
        }

        [Test]
        public void UserRolePosition_Update_InvalidId()
        {
            var dal = PrepareUserRolePositionDal("DALInitParams");

            var entity = new UserRolePosition();
            entity.ID = Int64.MaxValue - 1;
                          entity.PositionID = 110118;
                            entity.UserID = 33020042;
                            entity.RoleID = 1;
              
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

        protected IUserRolePositionDal PrepareUserRolePositionDal(string configName)
        {
            IConfiguration config = GetConfiguration();
            var initParams = config.GetSection(configName).Get<TestDalInitParams>();

            IUserRolePositionDal dal = new UserRolePositionDal();
            var dalInitParams = dal.CreateInitParams();
            dalInitParams.Parameters["ConnectionString"] = initParams.ConnectionString;
            dal.Init(dalInitParams);

            return dal;
        }
    }
}
