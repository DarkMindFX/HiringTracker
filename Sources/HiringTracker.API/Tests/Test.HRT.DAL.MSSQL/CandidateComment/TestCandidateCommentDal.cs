

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

            IList<object> objIds = SetupCase(conn, caseName);
                var paramCandidateID = (System.Int64)objIds[0];
                var paramCommentID = (System.Int64)objIds[1];
            CandidateComment entity = dal.Get(paramCandidateID,paramCommentID);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.CandidateID);
                        Assert.IsNotNull(entity.CommentID);
            
                          Assert.AreEqual(100009, entity.CandidateID);
                            Assert.AreEqual(100008, entity.CommentID);
                      }

        [Test]
        public void CandidateComment_GetDetails_InvalidId()
        {
                var paramCandidateID = Int64.MaxValue - 1;
                var paramCommentID = Int64.MaxValue - 1;
            var dal = PrepareCandidateCommentDal("DALInitParams");

            CandidateComment entity = dal.Get(paramCandidateID,paramCommentID);

            Assert.IsNull(entity);
        }

        [TestCase("CandidateComment\\010.Delete.Success")]
        public void CandidateComment_Delete_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareCandidateCommentDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
                var paramCandidateID = (System.Int64)objIds[0];
                var paramCommentID = (System.Int64)objIds[1];
            bool removed = dal.Delete(paramCandidateID,paramCommentID);

            TeardownCase(conn, caseName);

            Assert.IsTrue(removed);
        }

        [Test]
        public void CandidateComment_Delete_InvalidId()
        {
            var dal = PrepareCandidateCommentDal("DALInitParams");
                var paramCandidateID = Int64.MaxValue - 1;
                var paramCommentID = Int64.MaxValue - 1;
   
            bool removed = dal.Delete(paramCandidateID,paramCommentID);
            Assert.IsFalse(removed);

        }

        [TestCase("CandidateComment\\020.Insert.Success")]
        public void CandidateComment_Insert_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            SetupCase(conn, caseName);

            var dal = PrepareCandidateCommentDal("DALInitParams");

            var entity = new CandidateComment();
                          entity.CandidateID = 100008;
                            entity.CommentID = 100001;
                          
            entity = dal.Insert(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.CandidateID);
                        Assert.IsNotNull(entity.CommentID);
            
                          Assert.AreEqual(100008, entity.CandidateID);
                            Assert.AreEqual(100001, entity.CommentID);
              
        }

        [TestCase("CandidateComment\\030.Update.Success")]
        public void CandidateComment_Update_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareCandidateCommentDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
                var paramCandidateID = (System.Int64)objIds[0];
                var paramCommentID = (System.Int64)objIds[1];
            CandidateComment entity = dal.Get(paramCandidateID,paramCommentID);

            
            entity = dal.Update(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.CandidateID);
                        Assert.IsNotNull(entity.CommentID);
            
                          Assert.AreEqual(100004, entity.CandidateID);
                            Assert.AreEqual(100003, entity.CommentID);
              
        }

        [Test]
        public void CandidateComment_Update_InvalidId()
        {
            var dal = PrepareCandidateCommentDal("DALInitParams");

            var entity = new CandidateComment();
                          entity.CandidateID = 100004;
                            entity.CommentID = 100003;
              
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
