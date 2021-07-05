

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

            IList<object> objIds = SetupCase(conn, caseName);
                var paramPositionID = (System.Int64)objIds[0];
                var paramUserID = (System.Int64)objIds[1];
            UserRolePosition entity = dal.Get(paramPositionID,paramUserID);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.PositionID);
                        Assert.IsNotNull(entity.UserID);
            
                          Assert.AreEqual(100003, entity.PositionID);
                            Assert.AreEqual(100004, entity.UserID);
                            Assert.AreEqual(6, entity.RoleID);
                      }

        [Test]
        public void UserRolePosition_GetDetails_InvalidId()
        {
                var paramPositionID = Int64.MaxValue - 1;
                var paramUserID = Int64.MaxValue - 1;
            var dal = PrepareUserRolePositionDal("DALInitParams");

            UserRolePosition entity = dal.Get(paramPositionID,paramUserID);

            Assert.IsNull(entity);
        }

        [TestCase("UserRolePosition\\010.Delete.Success")]
        public void UserRolePosition_Delete_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareUserRolePositionDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
                var paramPositionID = (System.Int64)objIds[0];
                var paramUserID = (System.Int64)objIds[1];
            bool removed = dal.Delete(paramPositionID,paramUserID);

            TeardownCase(conn, caseName);

            Assert.IsTrue(removed);
        }

        [Test]
        public void UserRolePosition_Delete_InvalidId()
        {
            var dal = PrepareUserRolePositionDal("DALInitParams");
                var paramPositionID = Int64.MaxValue - 1;
                var paramUserID = Int64.MaxValue - 1;
   
            bool removed = dal.Delete(paramPositionID,paramUserID);
            Assert.IsFalse(removed);

        }

        [TestCase("UserRolePosition\\020.Insert.Success")]
        public void UserRolePosition_Insert_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            SetupCase(conn, caseName);

            var dal = PrepareUserRolePositionDal("DALInitParams");

            var entity = new UserRolePosition();
                          entity.PositionID = 100002;
                            entity.UserID = 100004;
                            entity.RoleID = 6;
                          
            entity = dal.Insert(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.PositionID);
                        Assert.IsNotNull(entity.UserID);
            
                          Assert.AreEqual(100002, entity.PositionID);
                            Assert.AreEqual(100004, entity.UserID);
                            Assert.AreEqual(6, entity.RoleID);
              
        }

        [TestCase("UserRolePosition\\030.Update.Success")]
        public void UserRolePosition_Update_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareUserRolePositionDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
                var paramPositionID = (System.Int64)objIds[0];
                var paramUserID = (System.Int64)objIds[1];
            UserRolePosition entity = dal.Get(paramPositionID,paramUserID);

                          entity.RoleID = 9;
              
            entity = dal.Update(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.PositionID);
                        Assert.IsNotNull(entity.UserID);
            
                          Assert.AreEqual(100007, entity.PositionID);
                            Assert.AreEqual(100005, entity.UserID);
                            Assert.AreEqual(9, entity.RoleID);
              
        }

        [Test]
        public void UserRolePosition_Update_InvalidId()
        {
            var dal = PrepareUserRolePositionDal("DALInitParams");

            var entity = new UserRolePosition();
                          entity.PositionID = 100007;
                            entity.UserID = 100005;
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
