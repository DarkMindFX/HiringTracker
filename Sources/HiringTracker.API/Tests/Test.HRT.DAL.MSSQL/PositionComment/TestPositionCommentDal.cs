

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

            object objId = SetupCase(conn, caseName);
            long id = (long)objId;

            PositionComment entity = dal.Get(id);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.PositionID);
                        Assert.IsNotNull(entity.CommentID);
            
                          Assert.AreEqual(100009, entity.PositionID);
                            Assert.AreEqual(100005, entity.CommentID);
                      }

        [Test]
        public void PositionComment_GetDetails_InvalidId()
        {
            long id = Int32.MaxValue - 1;
            var dal = PreparePositionCommentDal("DALInitParams");

            PositionComment entity = dal.Get(id);

            Assert.IsNull(entity);
        }

        [TestCase("PositionComment\\010.Delete.Success")]
        public void PositionComment_Delete_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PreparePositionCommentDal("DALInitParams");

            object objId = SetupCase(conn, caseName);
            long id = (long)objId;

            bool removed = dal.Delete(id);

            TeardownCase(conn, caseName);

            Assert.IsTrue(removed);
        }

        [Test]
        public void PositionComment_Delete_InvalidId()
        {
            long positionId = Int32.MaxValue - 1;
            var dal = PreparePositionCommentDal("DALInitParams");

            bool removed = dal.Delete(positionId);
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
                            entity.CommentID = 100001;
                          
            entity = dal.Upsert(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.PositionID);
                        Assert.IsNotNull(entity.CommentID);
            
                          Assert.AreEqual(110142, entity.PositionID);
                            Assert.AreEqual(100001, entity.CommentID);
              
        }

        [TestCase("PositionComment\\030.Update.Success")]
        public void PositionComment_Update_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PreparePositionCommentDal("DALInitParams");

            object objId = SetupCase(conn, caseName);
            long id = (long)objId;

            var entity = dal.Get(id);
                          entity.PositionID = 100007;
                            entity.CommentID = 100007;
              
            entity = dal.Upsert(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.PositionID);
                        Assert.IsNotNull(entity.CommentID);
            
                          Assert.AreEqual(100007, entity.PositionID);
                            Assert.AreEqual(100007, entity.CommentID);
              
        }

        [Test]
        public void PositionComment_Update_InvalidId()
        {
            var dal = PreparePositionCommentDal("DALInitParams");

            var entity = new PositionComment();
            entity.ID = Int64.MaxValue - 1;
                          entity.PositionID = 100007;
                            entity.CommentID = 100007;
              
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
