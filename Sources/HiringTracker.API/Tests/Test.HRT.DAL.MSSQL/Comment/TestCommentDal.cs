

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
            
                          Assert.AreEqual("Text 01b337ac34df4e028f39fa4c5f5b5257", entity.Text);
                            Assert.AreEqual(DateTime.Parse("12/20/2021 5:16:33 PM"), entity.CreatedDate);
                            Assert.AreEqual(100004, entity.CreatedByID);
                            Assert.AreEqual(DateTime.Parse("10/30/2019 8:31:34 AM"), entity.ModifiedDate);
                            Assert.AreEqual(100004, entity.ModifiedByID);
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
                          entity.Text = "Text edbb9b1384a245e8b22746b645d804a1";
                            entity.CreatedDate = DateTime.Parse("9/21/2021 4:41:34 AM");
                            entity.CreatedByID = 100002;
                            entity.ModifiedDate = DateTime.Parse("6/11/2023 8:17:34 AM");
                            entity.ModifiedByID = 100004;
                          
            entity = dal.Insert(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual("Text edbb9b1384a245e8b22746b645d804a1", entity.Text);
                            Assert.AreEqual(DateTime.Parse("9/21/2021 4:41:34 AM"), entity.CreatedDate);
                            Assert.AreEqual(100002, entity.CreatedByID);
                            Assert.AreEqual(DateTime.Parse("6/11/2023 8:17:34 AM"), entity.ModifiedDate);
                            Assert.AreEqual(100004, entity.ModifiedByID);
              
        }

        [TestCase("Comment\\030.Update.Success")]
        public void Comment_Update_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareCommentDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
                var paramID = (System.Int64?)objIds[0];
            Comment entity = dal.Get(paramID);

                          entity.Text = "Text 8baf75ad35c24e14b919cce6c70a6cd7";
                            entity.CreatedDate = DateTime.Parse("3/12/2019 6:06:34 AM");
                            entity.CreatedByID = 100005;
                            entity.ModifiedDate = DateTime.Parse("3/4/2020 8:20:34 AM");
                            entity.ModifiedByID = 100003;
              
            entity = dal.Update(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ID);
            
                          Assert.AreEqual("Text 8baf75ad35c24e14b919cce6c70a6cd7", entity.Text);
                            Assert.AreEqual(DateTime.Parse("3/12/2019 6:06:34 AM"), entity.CreatedDate);
                            Assert.AreEqual(100005, entity.CreatedByID);
                            Assert.AreEqual(DateTime.Parse("3/4/2020 8:20:34 AM"), entity.ModifiedDate);
                            Assert.AreEqual(100003, entity.ModifiedByID);
              
        }

        [Test]
        public void Comment_Update_InvalidId()
        {
            var dal = PrepareCommentDal("DALInitParams");

            var entity = new Comment();
                          entity.Text = "Text 8baf75ad35c24e14b919cce6c70a6cd7";
                            entity.CreatedDate = DateTime.Parse("3/12/2019 6:06:34 AM");
                            entity.CreatedByID = 100005;
                            entity.ModifiedDate = DateTime.Parse("3/4/2020 8:20:34 AM");
                            entity.ModifiedByID = 100003;
              
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
