

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

            object objId = SetupCase(conn, caseName);
            long id = (long)objId;

            UserRoleCandidate entity = dal.Get(id);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.CandidateID);
                        Assert.IsNotNull(entity.UserID);
            
                          Assert.AreEqual(110127, entity.CandidateID);
                            Assert.AreEqual(100002, entity.UserID);
                            Assert.AreEqual(6, entity.RoleID);
                      }

        [Test]
        public void UserRoleCandidate_GetDetails_InvalidId()
        {
            long id = Int32.MaxValue - 1;
            var dal = PrepareUserRoleCandidateDal("DALInitParams");

            UserRoleCandidate entity = dal.Get(id);

            Assert.IsNull(entity);
        }

        [TestCase("UserRoleCandidate\\010.Delete.Success")]
        public void UserRoleCandidate_Delete_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareUserRoleCandidateDal("DALInitParams");

            object objId = SetupCase(conn, caseName);
            long id = (long)objId;

            bool removed = dal.Delete(id);

            TeardownCase(conn, caseName);

            Assert.IsTrue(removed);
        }

        [Test]
        public void UserRoleCandidate_Delete_InvalidId()
        {
            long positionId = Int32.MaxValue - 1;
            var dal = PrepareUserRoleCandidateDal("DALInitParams");

            bool removed = dal.Delete(positionId);
            Assert.IsFalse(removed);

        }

        [TestCase("UserRoleCandidate\\020.Insert.Success")]
        public void UserRoleCandidate_Insert_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            SetupCase(conn, caseName);

            var dal = PrepareUserRoleCandidateDal("DALInitParams");

            var entity = new UserRoleCandidate();
                          entity.CandidateID = 100009;
                            entity.UserID = 100003;
                            entity.RoleID = 4;
                          
            entity = dal.Upsert(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.CandidateID);
                        Assert.IsNotNull(entity.UserID);
            
                          Assert.AreEqual(100009, entity.CandidateID);
                            Assert.AreEqual(100003, entity.UserID);
                            Assert.AreEqual(4, entity.RoleID);
              
        }

        [TestCase("UserRoleCandidate\\030.Update.Success")]
        public void UserRoleCandidate_Update_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareUserRoleCandidateDal("DALInitParams");

            object objId = SetupCase(conn, caseName);
            long id = (long)objId;

            var entity = dal.Get(id);
                          entity.CandidateID = 100006;
                            entity.UserID = 33020042;
                            entity.RoleID = 1;
              
            entity = dal.Upsert(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.CandidateID);
                        Assert.IsNotNull(entity.UserID);
            
                          Assert.AreEqual(100006, entity.CandidateID);
                            Assert.AreEqual(33020042, entity.UserID);
                            Assert.AreEqual(1, entity.RoleID);
              
        }

        [Test]
        public void UserRoleCandidate_Update_InvalidId()
        {
            var dal = PrepareUserRoleCandidateDal("DALInitParams");

            var entity = new UserRoleCandidate();
            entity.ID = Int64.MaxValue - 1;
                          entity.CandidateID = 100006;
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
