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
    public class TestSkillProficiencyDal : TestBase
    {
        [Test]
        public void DalInit_Success()
        {
            IConfiguration config = GetConfiguration();
            var initParams = config.GetSection("DALInitParams").Get<TestDalInitParams>();

            ISkillProficiencyDal dal = new SkillProficiencyDal();
            var dalInitParams = dal.CreateInitParams();
            dalInitParams.Parameters["ConnectionString"] = initParams.ConnectionString;
            dal.Init(dalInitParams);
        }

        [Test]
        public void GetSkillProficiencys_Success()
        {
            var dal = PrepareSkillProficiencyDal("DALInitParams");

            IList<SkillProficiency> entities = dal.GetAll();

            Assert.IsNotNull(entities);
            Assert.IsNotEmpty(entities);
        }

        [TestCase("SkillProficiency\\000.GetDetails.Success")]
        public void GetSkillProficiency_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareSkillProficiencyDal("DALInitParams");

            object objId = SetupCase(conn, caseName);
            long id = (long)objId;

            SkillProficiency entity = dal.Get(id);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
            Assert.AreEqual(361640, entity.ID);
            Assert.AreEqual("Name bdea0906de38438bac64d2ea6640ff81", entity.Name);

        }

        [Test]
        public void GetSkillProficiency_InvalidId()
        {
            long id = Int32.MaxValue - 1;
            var dal = PrepareSkillProficiencyDal("DALInitParams");

            SkillProficiency entity = dal.Get(id);

            Assert.IsNull(entity);
        }

        [TestCase("SkillProficiency\\010.Delete.Success")]
        public void DeleteSkillProficiency_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareSkillProficiencyDal("DALInitParams");

            object objId = SetupCase(conn, caseName);
            long id = (long)objId;

            bool removed = dal.Delete(id);

            TeardownCase(conn, caseName);

            Assert.IsTrue(removed);
        }

        [Test]
        public void DeleteSkillProficiency_InvalidId()
        {
            long positionId = Int32.MaxValue - 1;
            var dal = PrepareSkillProficiencyDal("DALInitParams");

            bool removed = dal.Delete(positionId);
            Assert.IsFalse(removed);

        }

        [TestCase("SkillProficiency\\020.Insert.Success")]
        public void InsertSkillProficiency_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            SetupCase(conn, caseName);

            var dal = PrepareSkillProficiencyDal("DALInitParams");

            var entity = new SkillProficiency();
            entity.ID = 361640;
            entity.Name = "Name 7532d2dc7aa649118b3ffb5355ba5dc6";


            entity = dal.Upsert(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
            Assert.AreEqual(361640, entity.ID);
            Assert.AreEqual("Name 7532d2dc7aa649118b3ffb5355ba5dc6", entity.Name);

        }

        [TestCase("SkillProficiency\\030.Update.Success")]
        public void UpdateSkillProficiency_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareSkillProficiencyDal("DALInitParams");

            object objId = SetupCase(conn, caseName);
            long id = (long)objId;

            var entity = dal.Get(id);
            entity.ID = 361640;
            entity.Name = "Name f363293791f9448694b222929dd04c70";


            entity = dal.Upsert(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
            Assert.AreEqual(361640, entity.ID);
            Assert.AreEqual("Name f363293791f9448694b222929dd04c70", entity.Name);

        }

        [Test]
        public void UpdateSkillProficiency_InvalidId()
        {
            var dal = PrepareSkillProficiencyDal("DALInitParams");

            var entity = new SkillProficiency();
            entity.ID = Int64.MaxValue - 1;
            entity.ID = 361640;
            entity.Name = "Name f363293791f9448694b222929dd04c70";


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
    }
}
