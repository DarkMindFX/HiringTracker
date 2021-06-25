

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
    public class TestCommentDal : TestBase
    {
        [Test]
        public void DalInit_Success()
        {
            IConfiguration config = GetConfiguration();
            var initParams = config.GetSection("DALInitParams").Get<TestDalInitParams>();

            ICommentDal dal = new CommentDal();
            var dalInitParams = dal.CreateInitParams();
            dalInitParams.Parameters["ConnectionString"] = initParams.ConnectionString;
            dal.Init(dalInitParams);
        }

        [Test]
        public void Comment_GetAll_Success()
        {
            var dal = PrepareCommentDal("DALInitParams");

            IList<Comment> entities = dal.GetAll();

            Assert.IsNotNull(entities);
            Assert.IsNotEmpty(entities);
        }

        [TestCase("Comment\\000.GetDetails.Success")]
        public void Comment_GetDetails_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareCommentDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
                var paramID = (System.Int64?)objIds[0];
            Comment entity = dal.Get(paramID);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual("Text 25defb2b9d344940992d4ff559d951ec", entity.Text);
                            Assert.AreEqual(DateTime.Parse("7/6/2019 6:19:12 AM"), entity.CreatedDate);
                            Assert.AreEqual(33020042, entity.CreatedByID);
                            Assert.AreEqual(DateTime.Parse("3/30/2020 10:20:12 PM"), entity.ModifiedDate);
                            Assert.AreEqual(33020042, entity.ModifiedByID);
                      }

        [Test]
        public void Comment_GetDetails_InvalidId()
        {
                var paramID = Int64.MaxValue - 1;
            var dal = PrepareCommentDal("DALInitParams");

            Comment entity = dal.Get(paramID);

            Assert.IsNull(entity);
        }

        [TestCase("Comment\\010.Delete.Success")]
        public void Comment_Delete_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareCommentDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
                var paramID = (System.Int64?)objIds[0];
            bool removed = dal.Delete(paramID);

            TeardownCase(conn, caseName);

            Assert.IsTrue(removed);
        }

        [Test]
        public void Comment_Delete_InvalidId()
        {
            var dal = PrepareCommentDal("DALInitParams");
                var paramID = Int64.MaxValue - 1;
   
            bool removed = dal.Delete(paramID);
            Assert.IsFalse(removed);

        }

        [TestCase("Comment\\020.Insert.Success")]
        public void Comment_Insert_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            SetupCase(conn, caseName);

            var dal = PrepareCommentDal("DALInitParams");

            var entity = new Comment();
                          entity.Text = "Text 25ad95c2177949629db48fddb3b90788";
                            entity.CreatedDate = DateTime.Parse("11/5/2023 12:07:12 AM");
                            entity.CreatedByID = 100002;
                            entity.ModifiedDate = DateTime.Parse("3/25/2021 9:54:12 AM");
                            entity.ModifiedByID = 33020042;
                          
            entity = dal.Insert(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual("Text 25ad95c2177949629db48fddb3b90788", entity.Text);
                            Assert.AreEqual(DateTime.Parse("11/5/2023 12:07:12 AM"), entity.CreatedDate);
                            Assert.AreEqual(100002, entity.CreatedByID);
                            Assert.AreEqual(DateTime.Parse("3/25/2021 9:54:12 AM"), entity.ModifiedDate);
                            Assert.AreEqual(33020042, entity.ModifiedByID);
              
        }

        [TestCase("Comment\\030.Update.Success")]
        public void Comment_Update_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareCommentDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
                var paramID = (System.Int64?)objIds[0];
            Comment entity = dal.Get(paramID);

                          entity.Text = "Text 80a074f257644a648ad0db9c4ca5b0ff";
                            entity.CreatedDate = DateTime.Parse("9/20/2021 6:22:12 AM");
                            entity.CreatedByID = 33020042;
                            entity.ModifiedDate = DateTime.Parse("2/7/2019 4:09:12 PM");
                            entity.ModifiedByID = 100001;
              
            entity = dal.Update(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual("Text 80a074f257644a648ad0db9c4ca5b0ff", entity.Text);
                            Assert.AreEqual(DateTime.Parse("9/20/2021 6:22:12 AM"), entity.CreatedDate);
                            Assert.AreEqual(33020042, entity.CreatedByID);
                            Assert.AreEqual(DateTime.Parse("2/7/2019 4:09:12 PM"), entity.ModifiedDate);
                            Assert.AreEqual(100001, entity.ModifiedByID);
              
        }

        [Test]
        public void Comment_Update_InvalidId()
        {
            var dal = PrepareCommentDal("DALInitParams");

            var entity = new Comment();
                          entity.Text = "Text 80a074f257644a648ad0db9c4ca5b0ff";
                            entity.CreatedDate = DateTime.Parse("9/20/2021 6:22:12 AM");
                            entity.CreatedByID = 33020042;
                            entity.ModifiedDate = DateTime.Parse("2/7/2019 4:09:12 PM");
                            entity.ModifiedByID = 100001;
              
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

        protected ICommentDal PrepareCommentDal(string configName)
        {
            IConfiguration config = GetConfiguration();
            var initParams = config.GetSection(configName).Get<TestDalInitParams>();

            ICommentDal dal = new CommentDal();
            var dalInitParams = dal.CreateInitParams();
            dalInitParams.Parameters["ConnectionString"] = initParams.ConnectionString;
            dal.Init(dalInitParams);

            return dal;
        }
    }
}
