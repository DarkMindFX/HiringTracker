

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
    public class TestProposalCommentDal : TestBase
    {
        [Test]
        public void DalInit_Success()
        {
            IConfiguration config = GetConfiguration();
            var initParams = config.GetSection("DALInitParams").Get<TestDalInitParams>();

            IProposalCommentDal dal = new ProposalCommentDal();
            var dalInitParams = dal.CreateInitParams();
            dalInitParams.Parameters["ConnectionString"] = initParams.ConnectionString;
            dal.Init(dalInitParams);
        }

        [Test]
        public void ProposalComment_GetAll_Success()
        {
            var dal = PrepareProposalCommentDal("DALInitParams");

            IList<ProposalComment> entities = dal.GetAll();

            Assert.IsNotNull(entities);
            Assert.IsNotEmpty(entities);
        }

        [TestCase("ProposalComment\\000.GetDetails.Success")]
        public void ProposalComment_GetDetails_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareProposalCommentDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
                var paramProposalID = (System.Int64)objIds[0];
                var paramCommentID = (System.Int64)objIds[1];
            ProposalComment entity = dal.Get(paramProposalID,paramCommentID);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ProposalID);
                        Assert.IsNotNull(entity.CommentID);
            
                          Assert.AreEqual(100006, entity.ProposalID);
                            Assert.AreEqual(100004, entity.CommentID);
                      }

        [Test]
        public void ProposalComment_GetDetails_InvalidId()
        {
                var paramProposalID = Int64.MaxValue - 1;
                var paramCommentID = Int64.MaxValue - 1;
            var dal = PrepareProposalCommentDal("DALInitParams");

            ProposalComment entity = dal.Get(paramProposalID,paramCommentID);

            Assert.IsNull(entity);
        }

        [TestCase("ProposalComment\\010.Delete.Success")]
        public void ProposalComment_Delete_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareProposalCommentDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
                var paramProposalID = (System.Int64)objIds[0];
                var paramCommentID = (System.Int64)objIds[1];
            bool removed = dal.Delete(paramProposalID,paramCommentID);

            TeardownCase(conn, caseName);

            Assert.IsTrue(removed);
        }

        [Test]
        public void ProposalComment_Delete_InvalidId()
        {
            var dal = PrepareProposalCommentDal("DALInitParams");
                var paramProposalID = Int64.MaxValue - 1;
                var paramCommentID = Int64.MaxValue - 1;
   
            bool removed = dal.Delete(paramProposalID,paramCommentID);
            Assert.IsFalse(removed);

        }

        [TestCase("ProposalComment\\020.Insert.Success")]
        public void ProposalComment_Insert_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            SetupCase(conn, caseName);

            var dal = PrepareProposalCommentDal("DALInitParams");

            var entity = new ProposalComment();
                          entity.ProposalID = 100003;
                            entity.CommentID = 100009;
                          
            entity = dal.Insert(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ProposalID);
                        Assert.IsNotNull(entity.CommentID);
            
                          Assert.AreEqual(100003, entity.ProposalID);
                            Assert.AreEqual(100009, entity.CommentID);
              
        }

        [TestCase("ProposalComment\\030.Update.Success")]
        public void ProposalComment_Update_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareProposalCommentDal("DALInitParams");

            IList<object> objIds = SetupCase(conn, caseName);
                var paramProposalID = (System.Int64)objIds[0];
                var paramCommentID = (System.Int64)objIds[1];
            ProposalComment entity = dal.Get(paramProposalID,paramCommentID);

            
            entity = dal.Update(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ProposalID);
                        Assert.IsNotNull(entity.CommentID);
            
                          Assert.AreEqual(100007, entity.ProposalID);
                            Assert.AreEqual(100007, entity.CommentID);
              
        }

        [Test]
        public void ProposalComment_Update_InvalidId()
        {
            var dal = PrepareProposalCommentDal("DALInitParams");

            var entity = new ProposalComment();
                          entity.ProposalID = 100007;
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

        protected IProposalCommentDal PrepareProposalCommentDal(string configName)
        {
            IConfiguration config = GetConfiguration();
            var initParams = config.GetSection(configName).Get<TestDalInitParams>();

            IProposalCommentDal dal = new ProposalCommentDal();
            var dalInitParams = dal.CreateInitParams();
            dalInitParams.Parameters["ConnectionString"] = initParams.ConnectionString;
            dal.Init(dalInitParams);

            return dal;
        }
    }
}
