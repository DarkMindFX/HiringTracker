

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
            
                          Assert.AreEqual("Login c9a81e2e61cb4d21bc310181b1ea58ee", entity.Login);
                            Assert.AreEqual("FirstName c9a81e2e61cb4d21bc310181b1ea58ee", entity.FirstName);
                            Assert.AreEqual("LastName c9a81e2e61cb4d21bc310181b1ea58ee", entity.LastName);
                            Assert.AreEqual("Email c9a81e2e61cb4d21bc310181b1ea58ee", entity.Email);
                            Assert.AreEqual("Description c9a81e2e61cb4d21bc310181b1ea58ee", entity.Description);
                            Assert.AreEqual("PwdHash c9a81e2e61cb4d21bc310181b1ea58ee", entity.PwdHash);
                            Assert.AreEqual("Salt c9a81e2e61cb4d21bc310181b1ea58ee", entity.Salt);
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
                          entity.Login = "Login 34c65704b0b84294890028fe366bc27a";
                            entity.FirstName = "FirstName 34c65704b0b84294890028fe366bc27a";
                            entity.LastName = "LastName 34c65704b0b84294890028fe366bc27a";
                            entity.Email = "Email 34c65704b0b84294890028fe366bc27a";
                            entity.Description = "Description 34c65704b0b84294890028fe366bc27a";
                            entity.PwdHash = "PwdHash 34c65704b0b84294890028fe366bc27a";
                            entity.Salt = "Salt 34c65704b0b84294890028fe366bc27a";
                          
            entity = dal.Insert(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual("Login 34c65704b0b84294890028fe366bc27a", entity.Login);
                            Assert.AreEqual("FirstName 34c65704b0b84294890028fe366bc27a", entity.FirstName);
                            Assert.AreEqual("LastName 34c65704b0b84294890028fe366bc27a", entity.LastName);
                            Assert.AreEqual("Email 34c65704b0b84294890028fe366bc27a", entity.Email);
                            Assert.AreEqual("Description 34c65704b0b84294890028fe366bc27a", entity.Description);
                            Assert.AreEqual("PwdHash 34c65704b0b84294890028fe366bc27a", entity.PwdHash);
                            Assert.AreEqual("Salt 34c65704b0b84294890028fe366bc27a", entity.Salt);
              
        }

        [TestCase("User\\030.Update.Success")]
        public void User_Update_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareUserDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
                var paramID = (System.Int64?)objIds[0];
            User entity = dal.Get(paramID);

                          entity.Login = "Login a0888688f59248cc9507ea24fa0995d9";
                            entity.FirstName = "FirstName a0888688f59248cc9507ea24fa0995d9";
                            entity.LastName = "LastName a0888688f59248cc9507ea24fa0995d9";
                            entity.Email = "Email a0888688f59248cc9507ea24fa0995d9";
                            entity.Description = "Description a0888688f59248cc9507ea24fa0995d9";
                            entity.PwdHash = "PwdHash a0888688f59248cc9507ea24fa0995d9";
                            entity.Salt = "Salt a0888688f59248cc9507ea24fa0995d9";
              
            entity = dal.Update(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual("Login a0888688f59248cc9507ea24fa0995d9", entity.Login);
                            Assert.AreEqual("FirstName a0888688f59248cc9507ea24fa0995d9", entity.FirstName);
                            Assert.AreEqual("LastName a0888688f59248cc9507ea24fa0995d9", entity.LastName);
                            Assert.AreEqual("Email a0888688f59248cc9507ea24fa0995d9", entity.Email);
                            Assert.AreEqual("Description a0888688f59248cc9507ea24fa0995d9", entity.Description);
                            Assert.AreEqual("PwdHash a0888688f59248cc9507ea24fa0995d9", entity.PwdHash);
                            Assert.AreEqual("Salt a0888688f59248cc9507ea24fa0995d9", entity.Salt);
              
        }

        [Test]
        public void User_Update_InvalidId()
        {
            var dal = PrepareUserDal("DALInitParams");

            var entity = new User();
                          entity.Login = "Login a0888688f59248cc9507ea24fa0995d9";
                            entity.FirstName = "FirstName a0888688f59248cc9507ea24fa0995d9";
                            entity.LastName = "LastName a0888688f59248cc9507ea24fa0995d9";
                            entity.Email = "Email a0888688f59248cc9507ea24fa0995d9";
                            entity.Description = "Description a0888688f59248cc9507ea24fa0995d9";
                            entity.PwdHash = "PwdHash a0888688f59248cc9507ea24fa0995d9";
                            entity.Salt = "Salt a0888688f59248cc9507ea24fa0995d9";
              
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
