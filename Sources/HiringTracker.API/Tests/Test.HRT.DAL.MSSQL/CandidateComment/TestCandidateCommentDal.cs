

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
    public class TestCandidateCommentDal : TestBase
    {
        [Test]
        public void DalInit_Success()
        {
            IConfiguration config = GetConfiguration();
            var initParams = config.GetSection("DALInitParams").Get<TestDalInitParams>();

            ICandidateCommentDal dal = new CandidateCommentDal();
            var dalInitParams = dal.CreateInitParams();
            dalInitParams.Parameters["ConnectionString"] = initParams.ConnectionString;
            dal.Init(dalInitParams);
        }

        [Test]
        public void CandidateComment_GetAll_Success()
        {
            var dal = PrepareCandidateCommentDal("DALInitParams");

            IList<CandidateComment> entities = dal.GetAll();

            Assert.IsNotNull(entities);
            Assert.IsNotEmpty(entities);
        }

        [TestCase("CandidateComment\\000.GetDetails.Success")]
        public void CandidateComment_GetDetails_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareCandidateCommentDal("DALInitParams");

            object objId = SetupCase(conn, caseName);
            long id = (long)objId;

            CandidateComment entity = dal.Get(id);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.CandidateID);
                        Assert.IsNotNull(entity.CommentID);
            
                          Assert.AreEqual(100007, entity.CandidateID);
                            Assert.AreEqual(100003, entity.CommentID);
                      }

        [Test]
        public void CandidateComment_GetDetails_InvalidId()
        {
            long id = Int32.MaxValue - 1;
            var dal = PrepareCandidateCommentDal("DALInitParams");

            CandidateComment entity = dal.Get(id);

            Assert.IsNull(entity);
        }

        [TestCase("CandidateComment\\010.Delete.Success")]
        public void CandidateComment_Delete_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareCandidateCommentDal("DALInitParams");

            object objId = SetupCase(conn, caseName);
            long id = (long)objId;

            bool removed = dal.Delete(id);

            TeardownCase(conn, caseName);

            Assert.IsTrue(removed);
        }

        [Test]
        public void CandidateComment_Delete_InvalidId()
        {
            long positionId = Int32.MaxValue - 1;
            var dal = PrepareCandidateCommentDal("DALInitParams");

            bool removed = dal.Delete(positionId);
            Assert.IsFalse(removed);

        }

        [TestCase("CandidateComment\\020.Insert.Success")]
        public void CandidateComment_Insert_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            SetupCase(conn, caseName);

            var dal = PrepareCandidateCommentDal("DALInitParams");

            var entity = new CandidateComment();
                          entity.CandidateID = 110118;
                            entity.CommentID = 100001;
                          
            entity = dal.Upsert(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.CandidateID);
                        Assert.IsNotNull(entity.CommentID);
            
                          Assert.AreEqual(110118, entity.CandidateID);
                            Assert.AreEqual(100001, entity.CommentID);
              
        }

        [TestCase("CandidateComment\\030.Update.Success")]
        public void CandidateComment_Update_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareCandidateCommentDal("DALInitParams");

            object objId = SetupCase(conn, caseName);
            long id = (long)objId;

            var entity = dal.Get(id);
                          entity.CandidateID = 100005;
                            entity.CommentID = 100004;
              
            entity = dal.Upsert(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.CandidateID);
                        Assert.IsNotNull(entity.CommentID);
            
                          Assert.AreEqual(100005, entity.CandidateID);
                            Assert.AreEqual(100004, entity.CommentID);
              
        }

        [Test]
        public void CandidateComment_Update_InvalidId()
        {
            var dal = PrepareCandidateCommentDal("DALInitParams");

            var entity = new CandidateComment();
            entity.ID = Int64.MaxValue - 1;
                          entity.CandidateID = 100005;
                            entity.CommentID = 100004;
              
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

        protected ICandidateCommentDal PrepareCandidateCommentDal(string configName)
        {
            IConfiguration config = GetConfiguration();
            var initParams = config.GetSection(configName).Get<TestDalInitParams>();

            ICandidateCommentDal dal = new CandidateCommentDal();
            var dalInitParams = dal.CreateInitParams();
            dalInitParams.Parameters["ConnectionString"] = initParams.ConnectionString;
            dal.Init(dalInitParams);

            return dal;
        }
    }
}
