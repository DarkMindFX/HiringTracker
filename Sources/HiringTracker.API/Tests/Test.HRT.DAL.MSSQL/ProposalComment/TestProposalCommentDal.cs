

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

            object objId = SetupCase(conn, caseName);
            long id = (long)objId;

            ProposalComment entity = dal.Get(id);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ProposalID);
                        Assert.IsNotNull(entity.CommentID);
            
                          Assert.AreEqual(100004, entity.ProposalID);
                            Assert.AreEqual(100001, entity.CommentID);
                      }

        [Test]
        public void ProposalComment_GetDetails_InvalidId()
        {
            long id = Int32.MaxValue - 1;
            var dal = PrepareProposalCommentDal("DALInitParams");

            ProposalComment entity = dal.Get(id);

            Assert.IsNull(entity);
        }

        [TestCase("ProposalComment\\010.Delete.Success")]
        public void ProposalComment_Delete_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareProposalCommentDal("DALInitParams");

            object objId = SetupCase(conn, caseName);
            long id = (long)objId;

            bool removed = dal.Delete(id);

            TeardownCase(conn, caseName);

            Assert.IsTrue(removed);
        }

        [Test]
        public void ProposalComment_Delete_InvalidId()
        {
            long positionId = Int32.MaxValue - 1;
            var dal = PrepareProposalCommentDal("DALInitParams");

            bool removed = dal.Delete(positionId);
            Assert.IsFalse(removed);

        }

        [TestCase("ProposalComment\\020.Insert.Success")]
        public void ProposalComment_Insert_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            SetupCase(conn, caseName);

            var dal = PrepareProposalCommentDal("DALInitParams");

            var entity = new ProposalComment();
                          entity.ProposalID = 100001;
                            entity.CommentID = 100006;
                          
            entity = dal.Upsert(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ProposalID);
                        Assert.IsNotNull(entity.CommentID);
            
                          Assert.AreEqual(100001, entity.ProposalID);
                            Assert.AreEqual(100006, entity.CommentID);
              
        }

        [TestCase("ProposalComment\\030.Update.Success")]
        public void ProposalComment_Update_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareProposalCommentDal("DALInitParams");

            object objId = SetupCase(conn, caseName);
            long id = (long)objId;

            var entity = dal.Get(id);
                          entity.ProposalID = 100005;
                            entity.CommentID = 100009;
              
            entity = dal.Upsert(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
                        Assert.IsNotNull(entity.ProposalID);
                        Assert.IsNotNull(entity.CommentID);
            
                          Assert.AreEqual(100005, entity.ProposalID);
                            Assert.AreEqual(100009, entity.CommentID);
              
        }

        [Test]
        public void ProposalComment_Update_InvalidId()
        {
            var dal = PrepareProposalCommentDal("DALInitParams");

            var entity = new ProposalComment();
            entity.ID = Int64.MaxValue - 1;
                          entity.ProposalID = 100005;
                            entity.CommentID = 100009;
              
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
