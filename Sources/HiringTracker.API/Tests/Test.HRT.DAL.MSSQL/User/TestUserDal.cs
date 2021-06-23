

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

            object objId = SetupCase(conn, caseName);
            long id = (long)objId;

            User entity = dal.Get(id);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual("Login e8291e684a4845bd867e8911e7d1f817", entity.Login);
                            Assert.AreEqual("FirstName e8291e684a4845bd867e8911e7d1f817", entity.FirstName);
                            Assert.AreEqual("LastName e8291e684a4845bd867e8911e7d1f817", entity.LastName);
                            Assert.AreEqual("Email e8291e684a4845bd867e8911e7d1f817", entity.Email);
                            Assert.AreEqual("Description e8291e684a4845bd867e8911e7d1f817", entity.Description);
                            Assert.AreEqual("PwdHash e8291e684a4845bd867e8911e7d1f817", entity.PwdHash);
                            Assert.AreEqual("Salt e8291e684a4845bd867e8911e7d1f817", entity.Salt);
                      }

        [Test]
        public void User_GetDetails_InvalidId()
        {
            long id = Int32.MaxValue - 1;
            var dal = PrepareUserDal("DALInitParams");

            User entity = dal.Get(id);

            Assert.IsNull(entity);
        }

        [TestCase("User\\010.Delete.Success")]
        public void User_Delete_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareUserDal("DALInitParams");

            object objId = SetupCase(conn, caseName);
            long id = (long)objId;

            bool removed = dal.Delete(id);

            TeardownCase(conn, caseName);

            Assert.IsTrue(removed);
        }

        [Test]
        public void User_Delete_InvalidId()
        {
            long positionId = Int32.MaxValue - 1;
            var dal = PrepareUserDal("DALInitParams");

            bool removed = dal.Delete(positionId);
            Assert.IsFalse(removed);

        }

        [TestCase("User\\020.Insert.Success")]
        public void User_Insert_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            SetupCase(conn, caseName);

            var dal = PrepareUserDal("DALInitParams");

            var entity = new User();
                          entity.Login = "Login a92b17a6e7fd4a92892847c7b84aaeef";
                            entity.FirstName = "FirstName a92b17a6e7fd4a92892847c7b84aaeef";
                            entity.LastName = "LastName a92b17a6e7fd4a92892847c7b84aaeef";
                            entity.Email = "Email a92b17a6e7fd4a92892847c7b84aaeef";
                            entity.Description = "Description a92b17a6e7fd4a92892847c7b84aaeef";
                            entity.PwdHash = "PwdHash a92b17a6e7fd4a92892847c7b84aaeef";
                            entity.Salt = "Salt a92b17a6e7fd4a92892847c7b84aaeef";
                          
            entity = dal.Upsert(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual("Login a92b17a6e7fd4a92892847c7b84aaeef", entity.Login);
                            Assert.AreEqual("FirstName a92b17a6e7fd4a92892847c7b84aaeef", entity.FirstName);
                            Assert.AreEqual("LastName a92b17a6e7fd4a92892847c7b84aaeef", entity.LastName);
                            Assert.AreEqual("Email a92b17a6e7fd4a92892847c7b84aaeef", entity.Email);
                            Assert.AreEqual("Description a92b17a6e7fd4a92892847c7b84aaeef", entity.Description);
                            Assert.AreEqual("PwdHash a92b17a6e7fd4a92892847c7b84aaeef", entity.PwdHash);
                            Assert.AreEqual("Salt a92b17a6e7fd4a92892847c7b84aaeef", entity.Salt);
              
        }

        [TestCase("User\\030.Update.Success")]
        public void User_Update_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareUserDal("DALInitParams");

            object objId = SetupCase(conn, caseName);
            long id = (long)objId;

            var entity = dal.Get(id);
                          entity.Login = "Login f54bc868ab8b47ddb7f6ea4e354299a6";
                            entity.FirstName = "FirstName f54bc868ab8b47ddb7f6ea4e354299a6";
                            entity.LastName = "LastName f54bc868ab8b47ddb7f6ea4e354299a6";
                            entity.Email = "Email f54bc868ab8b47ddb7f6ea4e354299a6";
                            entity.Description = "Description f54bc868ab8b47ddb7f6ea4e354299a6";
                            entity.PwdHash = "PwdHash f54bc868ab8b47ddb7f6ea4e354299a6";
                            entity.Salt = "Salt f54bc868ab8b47ddb7f6ea4e354299a6";
              
            entity = dal.Upsert(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual("Login f54bc868ab8b47ddb7f6ea4e354299a6", entity.Login);
                            Assert.AreEqual("FirstName f54bc868ab8b47ddb7f6ea4e354299a6", entity.FirstName);
                            Assert.AreEqual("LastName f54bc868ab8b47ddb7f6ea4e354299a6", entity.LastName);
                            Assert.AreEqual("Email f54bc868ab8b47ddb7f6ea4e354299a6", entity.Email);
                            Assert.AreEqual("Description f54bc868ab8b47ddb7f6ea4e354299a6", entity.Description);
                            Assert.AreEqual("PwdHash f54bc868ab8b47ddb7f6ea4e354299a6", entity.PwdHash);
                            Assert.AreEqual("Salt f54bc868ab8b47ddb7f6ea4e354299a6", entity.Salt);
              
        }

        [Test]
        public void User_Update_InvalidId()
        {
            var dal = PrepareUserDal("DALInitParams");

            var entity = new User();
            entity.ID = Int64.MaxValue - 1;
                          entity.Login = "Login f54bc868ab8b47ddb7f6ea4e354299a6";
                            entity.FirstName = "FirstName f54bc868ab8b47ddb7f6ea4e354299a6";
                            entity.LastName = "LastName f54bc868ab8b47ddb7f6ea4e354299a6";
                            entity.Email = "Email f54bc868ab8b47ddb7f6ea4e354299a6";
                            entity.Description = "Description f54bc868ab8b47ddb7f6ea4e354299a6";
                            entity.PwdHash = "PwdHash f54bc868ab8b47ddb7f6ea4e354299a6";
                            entity.Salt = "Salt f54bc868ab8b47ddb7f6ea4e354299a6";
              
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
