

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
    public class TestUserRoleCandidateDal : TestBase
    {
        [Test]
        public void DalInit_Success()
        {
            IConfiguration config = GetConfiguration();
            var initParams = config.GetSection("DALInitParams").Get<TestDalInitParams>();

            IUserRoleCandidateDal dal = new UserRoleCandidateDal();
            var dalInitParams = dal.CreateInitParams();
            dalInitParams.Parameters["ConnectionString"] = initParams.ConnectionString;
            dal.Init(dalInitParams);
        }

        [Test]
        public void UserRoleCandidate_GetAll_Success()
        {
            var dal = PrepareUserRoleCandidateDal("DALInitParams");

            IList<UserRoleCandidate> entities = dal.GetAll();

            Assert.IsNotNull(entities);
            Assert.IsNotEmpty(entities);
        }

        [TestCase("UserRoleCandidate\\000.GetDetails.Success")]
        public void UserRoleCandidate_GetDetails_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareUserRoleCandidateDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
                var paramCandidateID = (System.Int64)objIds[0];
                var paramUserID = (System.Int64)objIds[1];
            UserRoleCandidate entity = dal.Get(paramCandidateID,paramUserID);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.CandidateID);
                        Assert.IsNotNull(entity.UserID);
            
                          Assert.AreEqual(100009, entity.CandidateID);
                            Assert.AreEqual(100002, entity.UserID);
                            Assert.AreEqual(7, entity.RoleID);
                      }

        [Test]
        public void UserRoleCandidate_GetDetails_InvalidId()
        {
                var paramCandidateID = Int64.MaxValue - 1;
                var paramUserID = Int64.MaxValue - 1;
            var dal = PrepareUserRoleCandidateDal("DALInitParams");

            UserRoleCandidate entity = dal.Get(paramCandidateID,paramUserID);

            Assert.IsNull(entity);
        }

        [TestCase("UserRoleCandidate\\010.Delete.Success")]
        public void UserRoleCandidate_Delete_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareUserRoleCandidateDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
                var paramCandidateID = (System.Int64)objIds[0];
                var paramUserID = (System.Int64)objIds[1];
            bool removed = dal.Delete(paramCandidateID,paramUserID);

            TeardownCase(conn, caseName);

            Assert.IsTrue(removed);
        }

        [Test]
        public void UserRoleCandidate_Delete_InvalidId()
        {
            var dal = PrepareUserRoleCandidateDal("DALInitParams");
                var paramCandidateID = Int64.MaxValue - 1;
                var paramUserID = Int64.MaxValue - 1;
   
            bool removed = dal.Delete(paramCandidateID,paramUserID);
            Assert.IsFalse(removed);

        }

        [TestCase("UserRoleCandidate\\020.Insert.Success")]
        public void UserRoleCandidate_Insert_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            SetupCase(conn, caseName);

            var dal = PrepareUserRoleCandidateDal("DALInitParams");

            var entity = new UserRoleCandidate();
                          entity.CandidateID = 100002;
                            entity.UserID = 100004;
                            entity.RoleID = 9;
                          
            entity = dal.Insert(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.CandidateID);
                        Assert.IsNotNull(entity.UserID);
            
                          Assert.AreEqual(100002, entity.CandidateID);
                            Assert.AreEqual(100004, entity.UserID);
                            Assert.AreEqual(9, entity.RoleID);
              
        }

        [TestCase("UserRoleCandidate\\030.Update.Success")]
        public void UserRoleCandidate_Update_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareUserRoleCandidateDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
                var paramCandidateID = (System.Int64)objIds[0];
                var paramUserID = (System.Int64)objIds[1];
            UserRoleCandidate entity = dal.Get(paramCandidateID,paramUserID);

                          entity.RoleID = 2;
              
            entity = dal.Update(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.CandidateID);
                        Assert.IsNotNull(entity.UserID);
            
                          Assert.AreEqual(100006, entity.CandidateID);
                            Assert.AreEqual(100002, entity.UserID);
                            Assert.AreEqual(2, entity.RoleID);
              
        }

        [Test]
        public void UserRoleCandidate_Update_InvalidId()
        {
            var dal = PrepareUserRoleCandidateDal("DALInitParams");

            var entity = new UserRoleCandidate();
                          entity.CandidateID = 100006;
                            entity.UserID = 100002;
                            entity.RoleID = 2;
              
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

        protected IUserRoleCandidateDal PrepareUserRoleCandidateDal(string configName)
        {
            IConfiguration config = GetConfiguration();
            var initParams = config.GetSection(configName).Get<TestDalInitParams>();

            IUserRoleCandidateDal dal = new UserRoleCandidateDal();
            var dalInitParams = dal.CreateInitParams();
            dalInitParams.Parameters["ConnectionString"] = initParams.ConnectionString;
            dal.Init(dalInitParams);

            return dal;
        }
    }
}
