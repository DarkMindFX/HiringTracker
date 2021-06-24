

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
            
                          Assert.AreEqual("Login cf4ebefc22dd42709f65a0d80f7879e7", entity.Login);
                            Assert.AreEqual("FirstName cf4ebefc22dd42709f65a0d80f7879e7", entity.FirstName);
                            Assert.AreEqual("LastName cf4ebefc22dd42709f65a0d80f7879e7", entity.LastName);
                            Assert.AreEqual("Email cf4ebefc22dd42709f65a0d80f7879e7", entity.Email);
                            Assert.AreEqual("Description cf4ebefc22dd42709f65a0d80f7879e7", entity.Description);
                            Assert.AreEqual("PwdHash cf4ebefc22dd42709f65a0d80f7879e7", entity.PwdHash);
                            Assert.AreEqual("Salt cf4ebefc22dd42709f65a0d80f7879e7", entity.Salt);
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
                          entity.Login = "Login 0e0f9e12ee4349d293dc68533103f56e";
                            entity.FirstName = "FirstName 0e0f9e12ee4349d293dc68533103f56e";
                            entity.LastName = "LastName 0e0f9e12ee4349d293dc68533103f56e";
                            entity.Email = "Email 0e0f9e12ee4349d293dc68533103f56e";
                            entity.Description = "Description 0e0f9e12ee4349d293dc68533103f56e";
                            entity.PwdHash = "PwdHash 0e0f9e12ee4349d293dc68533103f56e";
                            entity.Salt = "Salt 0e0f9e12ee4349d293dc68533103f56e";
                          
            entity = dal.Upsert(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual("Login 0e0f9e12ee4349d293dc68533103f56e", entity.Login);
                            Assert.AreEqual("FirstName 0e0f9e12ee4349d293dc68533103f56e", entity.FirstName);
                            Assert.AreEqual("LastName 0e0f9e12ee4349d293dc68533103f56e", entity.LastName);
                            Assert.AreEqual("Email 0e0f9e12ee4349d293dc68533103f56e", entity.Email);
                            Assert.AreEqual("Description 0e0f9e12ee4349d293dc68533103f56e", entity.Description);
                            Assert.AreEqual("PwdHash 0e0f9e12ee4349d293dc68533103f56e", entity.PwdHash);
                            Assert.AreEqual("Salt 0e0f9e12ee4349d293dc68533103f56e", entity.Salt);
              
        }

        [TestCase("User\\030.Update.Success")]
        public void User_Update_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareUserDal("DALInitParams");

            object objId = SetupCase(conn, caseName);
            long id = (long)objId;

            var entity = dal.Get(id);
                          entity.Login = "Login de9c1d6947c745208c4287f69d5de879";
                            entity.FirstName = "FirstName de9c1d6947c745208c4287f69d5de879";
                            entity.LastName = "LastName de9c1d6947c745208c4287f69d5de879";
                            entity.Email = "Email de9c1d6947c745208c4287f69d5de879";
                            entity.Description = "Description de9c1d6947c745208c4287f69d5de879";
                            entity.PwdHash = "PwdHash de9c1d6947c745208c4287f69d5de879";
                            entity.Salt = "Salt de9c1d6947c745208c4287f69d5de879";
              
            entity = dal.Upsert(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual("Login de9c1d6947c745208c4287f69d5de879", entity.Login);
                            Assert.AreEqual("FirstName de9c1d6947c745208c4287f69d5de879", entity.FirstName);
                            Assert.AreEqual("LastName de9c1d6947c745208c4287f69d5de879", entity.LastName);
                            Assert.AreEqual("Email de9c1d6947c745208c4287f69d5de879", entity.Email);
                            Assert.AreEqual("Description de9c1d6947c745208c4287f69d5de879", entity.Description);
                            Assert.AreEqual("PwdHash de9c1d6947c745208c4287f69d5de879", entity.PwdHash);
                            Assert.AreEqual("Salt de9c1d6947c745208c4287f69d5de879", entity.Salt);
              
        }

        [Test]
        public void User_Update_InvalidId()
        {
            var dal = PrepareUserDal("DALInitParams");

            var entity = new User();
            entity.ID = Int64.MaxValue - 1;
                          entity.Login = "Login de9c1d6947c745208c4287f69d5de879";
                            entity.FirstName = "FirstName de9c1d6947c745208c4287f69d5de879";
                            entity.LastName = "LastName de9c1d6947c745208c4287f69d5de879";
                            entity.Email = "Email de9c1d6947c745208c4287f69d5de879";
                            entity.Description = "Description de9c1d6947c745208c4287f69d5de879";
                            entity.PwdHash = "PwdHash de9c1d6947c745208c4287f69d5de879";
                            entity.Salt = "Salt de9c1d6947c745208c4287f69d5de879";
              
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
