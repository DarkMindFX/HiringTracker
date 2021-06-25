

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
    public class TestPositionCommentDal : TestBase
    {
        [Test]
        public void DalInit_Success()
        {
            IConfiguration config = GetConfiguration();
            var initParams = config.GetSection("DALInitParams").Get<TestDalInitParams>();

            IPositionCommentDal dal = new PositionCommentDal();
            var dalInitParams = dal.CreateInitParams();
            dalInitParams.Parameters["ConnectionString"] = initParams.ConnectionString;
            dal.Init(dalInitParams);
        }

        [Test]
        public void PositionComment_GetAll_Success()
        {
            var dal = PreparePositionCommentDal("DALInitParams");

            IList<PositionComment> entities = dal.GetAll();

            Assert.IsNotNull(entities);
            Assert.IsNotEmpty(entities);
        }

        [TestCase("PositionComment\\000.GetDetails.Success")]
        public void PositionComment_GetDetails_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PreparePositionCommentDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
                var paramPositionID = (System.Int64)objIds[0];
                var paramCommentID = (System.Int64)objIds[1];
            PositionComment entity = dal.Get(paramPositionID,paramCommentID);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.PositionID);
                        Assert.IsNotNull(entity.CommentID);
            
                          Assert.AreEqual(100008, entity.PositionID);
                            Assert.AreEqual(100008, entity.CommentID);
                      }

        [Test]
        public void PositionComment_GetDetails_InvalidId()
        {
                var paramPositionID = Int64.MaxValue - 1;
                var paramCommentID = Int64.MaxValue - 1;
            var dal = PreparePositionCommentDal("DALInitParams");

            PositionComment entity = dal.Get(paramPositionID,paramCommentID);

            Assert.IsNull(entity);
        }

        [TestCase("PositionComment\\010.Delete.Success")]
        public void PositionComment_Delete_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PreparePositionCommentDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
                var paramPositionID = (System.Int64)objIds[0];
                var paramCommentID = (System.Int64)objIds[1];
            bool removed = dal.Delete(paramPositionID,paramCommentID);

            TeardownCase(conn, caseName);

            Assert.IsTrue(removed);
        }

        [Test]
        public void PositionComment_Delete_InvalidId()
        {
            var dal = PreparePositionCommentDal("DALInitParams");
                var paramPositionID = Int64.MaxValue - 1;
                var paramCommentID = Int64.MaxValue - 1;
   
            bool removed = dal.Delete(paramPositionID,paramCommentID);
            Assert.IsFalse(removed);

        }

        [TestCase("PositionComment\\020.Insert.Success")]
        public void PositionComment_Insert_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            SetupCase(conn, caseName);

            var dal = PreparePositionCommentDal("DALInitParams");

            var entity = new PositionComment();
                          entity.PositionID = 110142;
                            entity.CommentID = 100009;
                          
            entity = dal.Insert(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.PositionID);
                        Assert.IsNotNull(entity.CommentID);
            
                          Assert.AreEqual(110142, entity.PositionID);
                            Assert.AreEqual(100009, entity.CommentID);
              
        }

        [TestCase("PositionComment\\030.Update.Success")]
        public void PositionComment_Update_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PreparePositionCommentDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
                var paramPositionID = (System.Int64)objIds[0];
                var paramCommentID = (System.Int64)objIds[1];
            PositionComment entity = dal.Get(paramPositionID,paramCommentID);

            
            entity = dal.Update(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.PositionID);
                        Assert.IsNotNull(entity.CommentID);
            
                          Assert.AreEqual(110142, entity.PositionID);
                            Assert.AreEqual(100007, entity.CommentID);
              
        }

        [Test]
        public void PositionComment_Update_InvalidId()
        {
            var dal = PreparePositionCommentDal("DALInitParams");

            var entity = new PositionComment();
                          entity.PositionID = 110142;
                            entity.CommentID = 100007;
              
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

        protected IPositionCommentDal PreparePositionCommentDal(string configName)
        {
            IConfiguration config = GetConfiguration();
            var initParams = config.GetSection(configName).Get<TestDalInitParams>();

            IPositionCommentDal dal = new PositionCommentDal();
            var dalInitParams = dal.CreateInitParams();
            dalInitParams.Parameters["ConnectionString"] = initParams.ConnectionString;
            dal.Init(dalInitParams);

            return dal;
        }
    }
}
