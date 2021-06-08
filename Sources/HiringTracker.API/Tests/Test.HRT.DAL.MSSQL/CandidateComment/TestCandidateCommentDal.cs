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
        public void GetCandidateComments_Success()
        {
            var dal = PrepareCandidateCommentDal("DALInitParams");

            IList<CandidateComment> entities = dal.GetAll();

            Assert.IsNotNull(entities);
            Assert.IsNotEmpty(entities);
        }

        [TestCase("CandidateComment\\000.GetByCandidate.Success")]
        public void GetCandidateCommentsByCandidate_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareCandidateCommentDal("DALInitParams");

            object objId = SetupCase(conn, caseName);
            long id = (long)objId;

            IList<CandidateComment> entities = dal.GetByCandidate(id);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entities);
            Assert.IsNotEmpty(entities);
		
        }

        [Test]
        public void GetCandidateCommentByCandidate_InvalidId()
        {
            long id = Int32.MaxValue - 1;
            var dal = PrepareCandidateCommentDal("DALInitParams");

            IList<CandidateComment> entities = dal.GetByCandidate(id);

            Assert.IsNull(entities);
        }

        [TestCase("CandidateComment\\010.Delete.Success")]
        public void DeleteCandidateComment_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareCandidateCommentDal("DALInitParams");

            object objId = SetupCase(conn, caseName);
            long candidateId = 100002;
            long commentId = 100004;

            bool removed = dal.Delete(candidateId, commentId);

            TeardownCase(conn, caseName);

            Assert.IsTrue(removed);
        }

        [Test]
        public void DeleteCandidateComment_InvalidId()
        {
            long candidateId = Int32.MaxValue - 1;
            long commentId = Int32.MaxValue - 1;
            var dal = PrepareCandidateCommentDal("DALInitParams");

            bool removed = dal.Delete(candidateId, commentId);
            Assert.IsFalse(removed);

        }

        [TestCase("CandidateComment\\020.Insert.Success")]
        public void InsertCandidateComment_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            SetupCase(conn, caseName);

            var dal = PrepareCandidateCommentDal("DALInitParams");

            var entity = new CandidateComment();
            entity.CandidateID = 100009;
            entity.CommentID = 100004;


            entity = dal.Upsert(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
            Assert.AreEqual(100009, entity.CandidateID);
		
        }

        [TestCase("CandidateComment\\030.Update.Success")]
        public void UpdateCandidateComment_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareCandidateCommentDal("DALInitParams");

            object objId = SetupCase(conn, caseName);
            long id = (long)objId;

            var entity = new CandidateComment();
            entity.CandidateID = 100002;
            entity.CommentID = 100008;

            entity = dal.Upsert(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
            Assert.AreEqual(100002, entity.CandidateID);
            Assert.AreEqual(100008, entity.CommentID);

        }

        [Test]
        public void UpdateCandidateComment_InvalidId()
        {
            var dal = PrepareCandidateCommentDal("DALInitParams");

            var entity = new CandidateComment();
            entity.CommentID = Int64.MaxValue - 1;
            entity.CandidateID = 100002;
		

            try
            {
                entity = dal.Upsert(entity);

                Assert.Fail("Fail - exception was expected, but wasn't thrown.");
            }
            catch(Exception ex)
            {
                Assert.Pass("Success - exception thrown as expected");
            }
        }
    }
}
