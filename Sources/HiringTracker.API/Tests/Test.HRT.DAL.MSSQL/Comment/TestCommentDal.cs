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
        public void GetComments_Success()
        {
            var dal = PrepareCommentDal("DALInitParams");

            IList<Comment> entities = dal.GetAll();

            Assert.IsNotNull(entities);
            Assert.IsNotEmpty(entities);
        }

        [TestCase("Comment\\000.GetDetails.Success")]
        public void GetComment_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareCommentDal("DALInitParams");

            object objId = SetupCase(conn, caseName);
            long id = (long)objId;

            Comment entity = dal.Get(id);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
            Assert.AreNotEqual(0, entity.ID);
		Assert.AreEqual("Text effd539ecd9e4367ac63bd677073b21d", entity.Text);
		Assert.AreEqual(DateTime.Parse("10/23/2019 4:33:03 PM"), entity.CreatedDate);
		Assert.AreEqual(100003, entity.CreatedByID);
		Assert.AreEqual(DateTime.Parse("12/25/2023 3:45:03 PM"), entity.ModifiedDate);
		Assert.AreEqual(33000067, entity.ModifiedByID);
		
        }

        [Test]
        public void GetComment_InvalidId()
        {
            long id = Int32.MaxValue - 1;
            var dal = PrepareCommentDal("DALInitParams");

            Comment entity = dal.Get(id);

            Assert.IsNull(entity);
        }

        [TestCase("Comment\\010.Delete.Success")]
        public void DeleteComment_Success(string caseName)
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
        public void DeleteComment_InvalidId()
        {
            long positionId = Int32.MaxValue - 1;
            var dal = PrepareCommentDal("DALInitParams");

            bool removed = dal.Delete(positionId);
            Assert.IsFalse(removed);

        }

        [TestCase("Comment\\020.Insert.Success")]
        public void InsertComment_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            SetupCase(conn, caseName);

            var dal = PrepareCommentDal("DALInitParams");

            var entity = new Comment();
            entity.Text = "Text 556f8522b133407ba6b9879908b8adda";
		entity.CreatedDate = DateTime.Parse("4/18/2021 10:33:03 PM");
		entity.CreatedByID = 33020024;
		entity.ModifiedDate = DateTime.Parse("2/26/2020 4:22:03 PM");
		entity.ModifiedByID = 100003;
		

            entity = dal.Upsert(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
            Assert.AreNotEqual(0, entity.ID);
		Assert.AreEqual("Text 556f8522b133407ba6b9879908b8adda", entity.Text);
		Assert.AreEqual(DateTime.Parse("4/18/2021 10:33:03 PM"), entity.CreatedDate);
		Assert.AreEqual(33020024, entity.CreatedByID);
		Assert.AreEqual(DateTime.Parse("2/26/2020 4:22:03 PM"), entity.ModifiedDate);
		Assert.AreEqual(100003, entity.ModifiedByID);
		
        }

        [TestCase("Comment\\030.Update.Success")]
        public void UpdateComment_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareCommentDal("DALInitParams");

            object objId = SetupCase(conn, caseName);
            long id = (long)objId;

            var entity = dal.Get(id);
            entity.Text = "Text e746b302de8743e79e74b123e76b4abb";
		entity.CreatedDate = DateTime.Parse("5/7/2023 3:59:03 AM");
		entity.CreatedByID = 100001;
		entity.ModifiedDate = DateTime.Parse("2/5/2019 1:48:03 AM");
		entity.ModifiedByID = 100003;
		

            entity = dal.Upsert(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
            Assert.AreNotEqual(0, entity.ID);
		Assert.AreEqual("Text e746b302de8743e79e74b123e76b4abb", entity.Text);
		Assert.AreEqual(DateTime.Parse("5/7/2023 3:59:03 AM"), entity.CreatedDate);
		Assert.AreEqual(100001, entity.CreatedByID);
		Assert.AreEqual(DateTime.Parse("2/5/2019 1:48:03 AM"), entity.ModifiedDate);
		Assert.AreEqual(100003, entity.ModifiedByID);
		
        }

        [Test]
        public void UpdateComment_InvalidId()
        {
            var dal = PrepareCommentDal("DALInitParams");

            var entity = new Comment();
            entity.ID = Int64.MaxValue - 1;
            entity.Text = "Text e746b302de8743e79e74b123e76b4abb";
		entity.CreatedDate = DateTime.Parse("5/7/2023 3:59:03 AM");
		entity.CreatedByID = 100001;
		entity.ModifiedDate = DateTime.Parse("2/5/2019 1:48:03 AM");
		entity.ModifiedByID = 100003;
		

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
