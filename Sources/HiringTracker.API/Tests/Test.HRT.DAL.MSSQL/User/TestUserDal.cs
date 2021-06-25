

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
    public class TestUserDal : TestBase
    {
        [Test]
        public void DalInit_Success()
        {
            IConfiguration config = GetConfiguration();
            var initParams = config.GetSection("DALInitParams").Get<TestDalInitParams>();

            IUserDal dal = new UserDal();
            var dalInitParams = dal.CreateInitParams();
            dalInitParams.Parameters["ConnectionString"] = initParams.ConnectionString;
            dal.Init(dalInitParams);
        }

        [Test]
        public void User_GetAll_Success()
        {
            var dal = PrepareUserDal("DALInitParams");

            IList<User> entities = dal.GetAll();

            Assert.IsNotNull(entities);
            Assert.IsNotEmpty(entities);
        }

        [TestCase("User\\000.GetDetails.Success")]
        public void User_GetDetails_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareUserDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
                var paramID = (System.Int64?)objIds[0];
            User entity = dal.Get(paramID);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual("Login abe0149d86f24826891ed5bd16de76a5", entity.Login);
                            Assert.AreEqual("FirstName abe0149d86f24826891ed5bd16de76a5", entity.FirstName);
                            Assert.AreEqual("LastName abe0149d86f24826891ed5bd16de76a5", entity.LastName);
                            Assert.AreEqual("Email abe0149d86f24826891ed5bd16de76a5", entity.Email);
                            Assert.AreEqual("Description abe0149d86f24826891ed5bd16de76a5", entity.Description);
                            Assert.AreEqual("PwdHash abe0149d86f24826891ed5bd16de76a5", entity.PwdHash);
                            Assert.AreEqual("Salt abe0149d86f24826891ed5bd16de76a5", entity.Salt);
                      }

        [Test]
        public void User_GetDetails_InvalidId()
        {
                var paramID = Int64.MaxValue - 1;
            var dal = PrepareUserDal("DALInitParams");

            User entity = dal.Get(paramID);

            Assert.IsNull(entity);
        }

        [TestCase("User\\010.Delete.Success")]
        public void User_Delete_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareUserDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
                var paramID = (System.Int64?)objIds[0];
            bool removed = dal.Delete(paramID);

            TeardownCase(conn, caseName);

            Assert.IsTrue(removed);
        }

        [Test]
        public void User_Delete_InvalidId()
        {
            var dal = PrepareUserDal("DALInitParams");
                var paramID = Int64.MaxValue - 1;
   
            bool removed = dal.Delete(paramID);
            Assert.IsFalse(removed);

        }

        [TestCase("User\\020.Insert.Success")]
        public void User_Insert_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            SetupCase(conn, caseName);

            var dal = PrepareUserDal("DALInitParams");

            var entity = new User();
                          entity.Login = "Login f9496bc9199e4e589225af9b032c1536";
                            entity.FirstName = "FirstName f9496bc9199e4e589225af9b032c1536";
                            entity.LastName = "LastName f9496bc9199e4e589225af9b032c1536";
                            entity.Email = "Email f9496bc9199e4e589225af9b032c1536";
                            entity.Description = "Description f9496bc9199e4e589225af9b032c1536";
                            entity.PwdHash = "PwdHash f9496bc9199e4e589225af9b032c1536";
                            entity.Salt = "Salt f9496bc9199e4e589225af9b032c1536";
                          
            entity = dal.Insert(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual("Login f9496bc9199e4e589225af9b032c1536", entity.Login);
                            Assert.AreEqual("FirstName f9496bc9199e4e589225af9b032c1536", entity.FirstName);
                            Assert.AreEqual("LastName f9496bc9199e4e589225af9b032c1536", entity.LastName);
                            Assert.AreEqual("Email f9496bc9199e4e589225af9b032c1536", entity.Email);
                            Assert.AreEqual("Description f9496bc9199e4e589225af9b032c1536", entity.Description);
                            Assert.AreEqual("PwdHash f9496bc9199e4e589225af9b032c1536", entity.PwdHash);
                            Assert.AreEqual("Salt f9496bc9199e4e589225af9b032c1536", entity.Salt);
              
        }

        [TestCase("User\\030.Update.Success")]
        public void User_Update_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareUserDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
                var paramID = (System.Int64?)objIds[0];
            User entity = dal.Get(paramID);

                          entity.Login = "Login 5fc74323937e43478ce6587e04b5814d";
                            entity.FirstName = "FirstName 5fc74323937e43478ce6587e04b5814d";
                            entity.LastName = "LastName 5fc74323937e43478ce6587e04b5814d";
                            entity.Email = "Email 5fc74323937e43478ce6587e04b5814d";
                            entity.Description = "Description 5fc74323937e43478ce6587e04b5814d";
                            entity.PwdHash = "PwdHash 5fc74323937e43478ce6587e04b5814d";
                            entity.Salt = "Salt 5fc74323937e43478ce6587e04b5814d";
              
            entity = dal.Update(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual("Login 5fc74323937e43478ce6587e04b5814d", entity.Login);
                            Assert.AreEqual("FirstName 5fc74323937e43478ce6587e04b5814d", entity.FirstName);
                            Assert.AreEqual("LastName 5fc74323937e43478ce6587e04b5814d", entity.LastName);
                            Assert.AreEqual("Email 5fc74323937e43478ce6587e04b5814d", entity.Email);
                            Assert.AreEqual("Description 5fc74323937e43478ce6587e04b5814d", entity.Description);
                            Assert.AreEqual("PwdHash 5fc74323937e43478ce6587e04b5814d", entity.PwdHash);
                            Assert.AreEqual("Salt 5fc74323937e43478ce6587e04b5814d", entity.Salt);
              
        }

        [Test]
        public void User_Update_InvalidId()
        {
            var dal = PrepareUserDal("DALInitParams");

            var entity = new User();
                          entity.Login = "Login 5fc74323937e43478ce6587e04b5814d";
                            entity.FirstName = "FirstName 5fc74323937e43478ce6587e04b5814d";
                            entity.LastName = "LastName 5fc74323937e43478ce6587e04b5814d";
                            entity.Email = "Email 5fc74323937e43478ce6587e04b5814d";
                            entity.Description = "Description 5fc74323937e43478ce6587e04b5814d";
                            entity.PwdHash = "PwdHash 5fc74323937e43478ce6587e04b5814d";
                            entity.Salt = "Salt 5fc74323937e43478ce6587e04b5814d";
              
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

        protected IUserDal PrepareUserDal(string configName)
        {
            IConfiguration config = GetConfiguration();
            var initParams = config.GetSection(configName).Get<TestDalInitParams>();

            IUserDal dal = new UserDal();
            var dalInitParams = dal.CreateInitParams();
            dalInitParams.Parameters["ConnectionString"] = initParams.ConnectionString;
            dal.Init(dalInitParams);

            return dal;
        }
    }
}
