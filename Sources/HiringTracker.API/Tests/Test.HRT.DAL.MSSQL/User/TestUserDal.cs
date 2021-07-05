

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
            
                          Assert.AreEqual("Login f42f106480a3474e8839a9620e690435", entity.Login);
                            Assert.AreEqual("FirstName f42f106480a3474e8839a9620e690435", entity.FirstName);
                            Assert.AreEqual("LastName f42f106480a3474e8839a9620e690435", entity.LastName);
                            Assert.AreEqual("Email f42f106480a3474e8839a9620e690435", entity.Email);
                            Assert.AreEqual("Description f42f106480a3474e8839a9620e690435", entity.Description);
                            Assert.AreEqual("PwdHash f42f106480a3474e8839a9620e690435", entity.PwdHash);
                            Assert.AreEqual("Salt f42f106480a3474e8839a9620e690435", entity.Salt);
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
                          entity.Login = "Login 01a357002eea4fd1bbfb8fdcc4e1ab1a";
                            entity.FirstName = "FirstName 01a357002eea4fd1bbfb8fdcc4e1ab1a";
                            entity.LastName = "LastName 01a357002eea4fd1bbfb8fdcc4e1ab1a";
                            entity.Email = "Email 01a357002eea4fd1bbfb8fdcc4e1ab1a";
                            entity.Description = "Description 01a357002eea4fd1bbfb8fdcc4e1ab1a";
                            entity.PwdHash = "PwdHash 01a357002eea4fd1bbfb8fdcc4e1ab1a";
                            entity.Salt = "Salt 01a357002eea4fd1bbfb8fdcc4e1ab1a";
                          
            entity = dal.Insert(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual("Login 01a357002eea4fd1bbfb8fdcc4e1ab1a", entity.Login);
                            Assert.AreEqual("FirstName 01a357002eea4fd1bbfb8fdcc4e1ab1a", entity.FirstName);
                            Assert.AreEqual("LastName 01a357002eea4fd1bbfb8fdcc4e1ab1a", entity.LastName);
                            Assert.AreEqual("Email 01a357002eea4fd1bbfb8fdcc4e1ab1a", entity.Email);
                            Assert.AreEqual("Description 01a357002eea4fd1bbfb8fdcc4e1ab1a", entity.Description);
                            Assert.AreEqual("PwdHash 01a357002eea4fd1bbfb8fdcc4e1ab1a", entity.PwdHash);
                            Assert.AreEqual("Salt 01a357002eea4fd1bbfb8fdcc4e1ab1a", entity.Salt);
              
        }

        [TestCase("User\\030.Update.Success")]
        public void User_Update_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareUserDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
                var paramID = (System.Int64?)objIds[0];
            User entity = dal.Get(paramID);

                          entity.Login = "Login c5d64384bbbe46b3bb46c90435734afd";
                            entity.FirstName = "FirstName c5d64384bbbe46b3bb46c90435734afd";
                            entity.LastName = "LastName c5d64384bbbe46b3bb46c90435734afd";
                            entity.Email = "Email c5d64384bbbe46b3bb46c90435734afd";
                            entity.Description = "Description c5d64384bbbe46b3bb46c90435734afd";
                            entity.PwdHash = "PwdHash c5d64384bbbe46b3bb46c90435734afd";
                            entity.Salt = "Salt c5d64384bbbe46b3bb46c90435734afd";
              
            entity = dal.Update(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual("Login c5d64384bbbe46b3bb46c90435734afd", entity.Login);
                            Assert.AreEqual("FirstName c5d64384bbbe46b3bb46c90435734afd", entity.FirstName);
                            Assert.AreEqual("LastName c5d64384bbbe46b3bb46c90435734afd", entity.LastName);
                            Assert.AreEqual("Email c5d64384bbbe46b3bb46c90435734afd", entity.Email);
                            Assert.AreEqual("Description c5d64384bbbe46b3bb46c90435734afd", entity.Description);
                            Assert.AreEqual("PwdHash c5d64384bbbe46b3bb46c90435734afd", entity.PwdHash);
                            Assert.AreEqual("Salt c5d64384bbbe46b3bb46c90435734afd", entity.Salt);
              
        }

        [Test]
        public void User_Update_InvalidId()
        {
            var dal = PrepareUserDal("DALInitParams");

            var entity = new User();
                          entity.Login = "Login c5d64384bbbe46b3bb46c90435734afd";
                            entity.FirstName = "FirstName c5d64384bbbe46b3bb46c90435734afd";
                            entity.LastName = "LastName c5d64384bbbe46b3bb46c90435734afd";
                            entity.Email = "Email c5d64384bbbe46b3bb46c90435734afd";
                            entity.Description = "Description c5d64384bbbe46b3bb46c90435734afd";
                            entity.PwdHash = "PwdHash c5d64384bbbe46b3bb46c90435734afd";
                            entity.Salt = "Salt c5d64384bbbe46b3bb46c90435734afd";
              
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
