

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

            object objId = SetupCase(conn, caseName);
            long id = (long)objId;

            Comment entity = dal.Get(id);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual("Text 66525186721c4f368089a510431f69de", entity.Text);
                            Assert.AreEqual(DateTime.Parse("11/1/2018 3:37:10 PM"), entity.CreatedDate);
                            Assert.AreEqual(33000067, entity.CreatedByID);
                            Assert.AreEqual(DateTime.Parse("4/23/2020 8:59:10 AM"), entity.ModifiedDate);
                            Assert.AreEqual(100002, entity.ModifiedByID);
                      }

        [Test]
        public void Comment_GetDetails_InvalidId()
        {
            long id = Int32.MaxValue - 1;
            var dal = PrepareCommentDal("DALInitParams");

            Comment entity = dal.Get(id);

            Assert.IsNull(entity);
        }

        [TestCase("Comment\\010.Delete.Success")]
        public void Comment_Delete_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareCommentDal("DALInitParams");

            object objId = SetupCase(conn, caseName);
            long id = (long)objId;

            bool removed = dal.Delete(id);

            TeardownCase(conn, caseName);

            Assert.IsTrue(removed);
        }

        [Test]
        public void Comment_Delete_InvalidId()
        {
            long positionId = Int32.MaxValue - 1;
            var dal = PrepareCommentDal("DALInitParams");

            bool removed = dal.Delete(positionId);
            Assert.IsFalse(removed);

        }

        [TestCase("Comment\\020.Insert.Success")]
        public void Comment_Insert_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            SetupCase(conn, caseName);

            var dal = PrepareCommentDal("DALInitParams");

            var entity = new Comment();
                          entity.Text = "Text 9cc5c68a0b794a8081441e7a28ed86c6";
                            entity.CreatedDate = DateTime.Parse("7/21/2020 7:13:10 PM");
                            entity.CreatedByID = 100001;
                            entity.ModifiedDate = DateTime.Parse("7/21/2020 7:13:10 PM");
                            entity.ModifiedByID = 100002;
                          
            entity = dal.Upsert(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual("Text 9cc5c68a0b794a8081441e7a28ed86c6", entity.Text);
                            Assert.AreEqual(DateTime.Parse("7/21/2020 7:13:10 PM"), entity.CreatedDate);
                            Assert.AreEqual(100001, entity.CreatedByID);
                            Assert.AreEqual(DateTime.Parse("7/21/2020 7:13:10 PM"), entity.ModifiedDate);
                            Assert.AreEqual(100002, entity.ModifiedByID);
              
        }

        [TestCase("Comment\\030.Update.Success")]
        public void Comment_Update_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareCommentDal("DALInitParams");

            object objId = SetupCase(conn, caseName);
            long id = (long)objId;

            var entity = dal.Get(id);
                          entity.Text = "Text 7f6a8f5a0ea547a09ce6ecac90d9d16a";
                            entity.CreatedDate = DateTime.Parse("10/20/2020 2:46:10 PM");
                            entity.CreatedByID = 100001;
                            entity.ModifiedDate = DateTime.Parse("8/30/2023 3:13:10 PM");
                            entity.ModifiedByID = 100001;
              
            entity = dal.Upsert(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual("Text 7f6a8f5a0ea547a09ce6ecac90d9d16a", entity.Text);
                            Assert.AreEqual(DateTime.Parse("10/20/2020 2:46:10 PM"), entity.CreatedDate);
                            Assert.AreEqual(100001, entity.CreatedByID);
                            Assert.AreEqual(DateTime.Parse("8/30/2023 3:13:10 PM"), entity.ModifiedDate);
                            Assert.AreEqual(100001, entity.ModifiedByID);
              
        }

        [Test]
        public void Comment_Update_InvalidId()
        {
            var dal = PrepareCommentDal("DALInitParams");

            var entity = new Comment();
            entity.ID = Int64.MaxValue - 1;
                          entity.Text = "Text 7f6a8f5a0ea547a09ce6ecac90d9d16a";
                            entity.CreatedDate = DateTime.Parse("10/20/2020 2:46:10 PM");
                            entity.CreatedByID = 100001;
                            entity.ModifiedDate = DateTime.Parse("8/30/2023 3:13:10 PM");
                            entity.ModifiedByID = 100001;
              
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
