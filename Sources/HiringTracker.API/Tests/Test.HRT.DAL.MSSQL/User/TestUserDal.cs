using HRT.DAL.MSSQL;
using HRT.Interfaces;
using HRT.Interfaces.Entities;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

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
        public void GetUsers_Success()
        {
            var dal = PrepareUserDal("DALInitParams");

            IList<User> entities = dal.GetAll();

            Assert.IsNotNull(entities);
            Assert.IsNotEmpty(entities);
        }

        [TestCase("User\\000.GetDetails.Success")]
        public void GetUser_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareUserDal("DALInitParams");

            object objId = SetupCase(conn, caseName);
            long id = (long)objId;

            User entity = dal.Get(id);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
            Assert.AreNotEqual(0, entity.ID);
            Assert.AreEqual("Login b3b5133194954837a89314ccc9cc48f1", entity.Login);
            Assert.AreEqual("FirstName b3b5133194954837a89314ccc9cc48f1", entity.FirstName);
            Assert.AreEqual("LastName b3b5133194954837a89314ccc9cc48f1", entity.LastName);
            Assert.AreEqual("Email b3b5133194954837a89314ccc9cc48f1", entity.Email);
            Assert.AreEqual("Description b3b5133194954837a89314ccc9cc48f1", entity.Description);
            Assert.AreEqual("PwdHash b3b5133194954837a89314ccc9cc48f1", entity.PwdHash);
            Assert.AreEqual("Salt b3b5133194954837a89314ccc9cc48f1", entity.Salt);

        }

        [Test]
        public void GetUser_InvalidId()
        {
            long id = Int32.MaxValue - 1;
            var dal = PrepareUserDal("DALInitParams");

            User entity = dal.Get(id);

            Assert.IsNull(entity);
        }

        [TestCase("User\\010.Delete.Success")]
        public void DeleteUser_Success(string caseName)
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
        public void DeleteUser_InvalidId()
        {
            long positionId = Int32.MaxValue - 1;
            var dal = PrepareUserDal("DALInitParams");

            bool removed = dal.Delete(positionId);
            Assert.IsFalse(removed);

        }

        [TestCase("User\\020.Insert.Success")]
        public void InsertUser_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            SetupCase(conn, caseName);

            var dal = PrepareUserDal("DALInitParams");

            var entity = new User();
            entity.Login = "Login 13d49cd1beb944e3b84e0de55ff14d71";
            entity.FirstName = "FirstName 13d49cd1beb944e3b84e0de55ff14d71";
            entity.LastName = "LastName 13d49cd1beb944e3b84e0de55ff14d71";
            entity.Email = "Email 13d49cd1beb944e3b84e0de55ff14d71";
            entity.Description = "Description 13d49cd1beb944e3b84e0de55ff14d71";
            entity.PwdHash = "PwdHash 13d49cd1beb944e3b84e0de55ff14d71";
            entity.Salt = "Salt 13d49cd1beb944e3b84e0de55ff14d71";


            entity = dal.Upsert(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
            Assert.AreNotEqual(0, entity.ID);
            Assert.AreEqual("Login 13d49cd1beb944e3b84e0de55ff14d71", entity.Login);
            Assert.AreEqual("FirstName 13d49cd1beb944e3b84e0de55ff14d71", entity.FirstName);
            Assert.AreEqual("LastName 13d49cd1beb944e3b84e0de55ff14d71", entity.LastName);
            Assert.AreEqual("Email 13d49cd1beb944e3b84e0de55ff14d71", entity.Email);
            Assert.AreEqual("Description 13d49cd1beb944e3b84e0de55ff14d71", entity.Description);
            Assert.AreEqual("PwdHash 13d49cd1beb944e3b84e0de55ff14d71", entity.PwdHash);
            Assert.AreEqual("Salt 13d49cd1beb944e3b84e0de55ff14d71", entity.Salt);

        }

        [TestCase("User\\030.Update.Success")]
        public void UpdateUser_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareUserDal("DALInitParams");

            object objId = SetupCase(conn, caseName);
            long id = (long)objId;

            var entity = dal.Get(id);
            entity.Login = "Login 6381ee51cbec42d1bd78e44742b39605";
            entity.FirstName = "FirstName 6381ee51cbec42d1bd78e44742b39605";
            entity.LastName = "LastName 6381ee51cbec42d1bd78e44742b39605";
            entity.Email = "Email 6381ee51cbec42d1bd78e44742b39605";
            entity.Description = "Description 6381ee51cbec42d1bd78e44742b39605";
            entity.PwdHash = "PwdHash 6381ee51cbec42d1bd78e44742b39605";
            entity.Salt = "Salt 6381ee51cbec42d1bd78e44742b39605";


            entity = dal.Upsert(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
            Assert.AreNotEqual(0, entity.ID);
            Assert.AreEqual("Login 6381ee51cbec42d1bd78e44742b39605", entity.Login);
            Assert.AreEqual("FirstName 6381ee51cbec42d1bd78e44742b39605", entity.FirstName);
            Assert.AreEqual("LastName 6381ee51cbec42d1bd78e44742b39605", entity.LastName);
            Assert.AreEqual("Email 6381ee51cbec42d1bd78e44742b39605", entity.Email);
            Assert.AreEqual("Description 6381ee51cbec42d1bd78e44742b39605", entity.Description);
            Assert.AreEqual("PwdHash 6381ee51cbec42d1bd78e44742b39605", entity.PwdHash);
            Assert.AreEqual("Salt 6381ee51cbec42d1bd78e44742b39605", entity.Salt);

        }

        [Test]
        public void UpdateUser_InvalidId()
        {
            var dal = PrepareUserDal("DALInitParams");

            var entity = new User();
            entity.ID = Int64.MaxValue - 1;
            entity.Login = "Login 6381ee51cbec42d1bd78e44742b39605";
            entity.FirstName = "FirstName 6381ee51cbec42d1bd78e44742b39605";
            entity.LastName = "LastName 6381ee51cbec42d1bd78e44742b39605";
            entity.Email = "Email 6381ee51cbec42d1bd78e44742b39605";
            entity.Description = "Description 6381ee51cbec42d1bd78e44742b39605";
            entity.PwdHash = "PwdHash 6381ee51cbec42d1bd78e44742b39605";
            entity.Salt = "Salt 6381ee51cbec42d1bd78e44742b39605";


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
    }
}
