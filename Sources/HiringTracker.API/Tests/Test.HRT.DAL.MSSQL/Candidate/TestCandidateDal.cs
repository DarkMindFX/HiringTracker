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
    public class TestCandidateDal : TestBase
    {
        [Test]
        public void DalInit_Success()
        {
            IConfiguration config = GetConfiguration();
            var initParams = config.GetSection("DALInitParams").Get<TestDalInitParams>();

            ICandidateDal dal = new CandidateDal();
            var dalInitParams = dal.CreateInitParams();
            dalInitParams.Parameters["ConnectionString"] = initParams.ConnectionString;
            dal.Init(dalInitParams);
        }

        [Test]
        public void GetCandidates_Success()
        {
            var dal = PrepareCandidateDal("DALInitParams");

            IList<Candidate> entities = dal.GetAll();

            Assert.IsNotNull(entities);
            Assert.IsNotEmpty(entities);
        }

        [TestCase("Candidate\\000.GetDetails.Success")]
        public void GetCandidate_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareCandidateDal("DALInitParams");

            object objId = SetupCase(conn, caseName);
            long id = (long)objId;

            Candidate entity = dal.Get(id);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
            Assert.AreNotEqual(0, entity.ID);
            Assert.AreEqual("FirstName 1c3c960501264186988bb3af92cce7cd", entity.FirstName);
            Assert.AreEqual("MiddleName 1c3c960501264186988bb3af92cce7cd", entity.MiddleName);
            Assert.AreEqual("LastName 1c3c960501264186988bb3af92cce7cd", entity.LastName);
            Assert.AreEqual("Email 1c3c960501264186988bb3af92cce7cd", entity.Email);
            Assert.AreEqual("Phone 1c3c960501264186988bb3af92cce7cd", entity.Phone);
            Assert.AreEqual("CVLink 1c3c960501264186988bb3af92cce7cd", entity.CVLink);
            Assert.AreEqual(33000067, entity.CreatedByID);
            Assert.AreEqual(DateTime.Parse("10/29/2020 5:03:18 PM"), entity.CreatedDate);
            Assert.AreEqual(33020024, entity.ModifiedByID);
            Assert.AreEqual(DateTime.Parse("9/16/2018 8:37:18 AM"), entity.ModifiedDate);

        }

        [Test]
        public void GetCandidate_InvalidId()
        {
            long id = Int32.MaxValue - 1;
            var dal = PrepareCandidateDal("DALInitParams");

            Candidate entity = dal.Get(id);

            Assert.IsNull(entity);
        }

        [TestCase("Candidate\\010.Delete.Success")]
        public void DeleteCandidate_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareCandidateDal("DALInitParams");

            object objId = SetupCase(conn, caseName);
            long id = (long)objId;

            bool removed = dal.Delete(id);

            TeardownCase(conn, caseName);

            Assert.IsTrue(removed);
        }

        [Test]
        public void DeleteCandidate_InvalidId()
        {
            long positionId = Int32.MaxValue - 1;
            var dal = PrepareCandidateDal("DALInitParams");

            bool removed = dal.Delete(positionId);
            Assert.IsFalse(removed);

        }

        [TestCase("Candidate\\020.Insert.Success")]
        public void InsertCandidate_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            SetupCase(conn, caseName);

            var dal = PrepareCandidateDal("DALInitParams");

            var entity = new Candidate();
            entity.FirstName = "FirstName b3cd1a4b6009409db1146959c51df419";
            entity.MiddleName = "MiddleName b3cd1a4b6009409db1146959c51df419";
            entity.LastName = "LastName b3cd1a4b6009409db1146959c51df419";
            entity.Email = "Email b3cd1a4b6009409db1146959c51df419";
            entity.Phone = "Phone b3cd1a4b6009409db1146959c51df419";
            entity.CVLink = "CVLink b3cd1a4b6009409db1146959c51df419";
            entity.CreatedByID = 33020024;
            entity.CreatedDate = DateTime.Parse("12/1/2020 6:00:18 PM");
            entity.ModifiedByID = 100003;
            entity.ModifiedDate = DateTime.Parse("8/27/2021 10:01:18 AM");


            entity = dal.Upsert(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
            Assert.AreNotEqual(0, entity.ID);
            Assert.AreEqual("FirstName b3cd1a4b6009409db1146959c51df419", entity.FirstName);
            Assert.AreEqual("MiddleName b3cd1a4b6009409db1146959c51df419", entity.MiddleName);
            Assert.AreEqual("LastName b3cd1a4b6009409db1146959c51df419", entity.LastName);
            Assert.AreEqual("Email b3cd1a4b6009409db1146959c51df419", entity.Email);
            Assert.AreEqual("Phone b3cd1a4b6009409db1146959c51df419", entity.Phone);
            Assert.AreEqual("CVLink b3cd1a4b6009409db1146959c51df419", entity.CVLink);
            Assert.AreEqual(33020024, entity.CreatedByID);
            Assert.AreEqual(DateTime.Parse("12/1/2020 6:00:18 PM"), entity.CreatedDate);
            Assert.AreEqual(100003, entity.ModifiedByID);
            Assert.AreEqual(DateTime.Parse("8/27/2021 10:01:18 AM"), entity.ModifiedDate);

        }

        [TestCase("Candidate\\030.Update.Success")]
        public void UpdateCandidate_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PrepareCandidateDal("DALInitParams");

            object objId = SetupCase(conn, caseName);
            long id = (long)objId;

            var entity = dal.Get(id);
            entity.FirstName = "FirstName 3d513ab68a7b491db22f727769b2b91d";
            entity.MiddleName = "MiddleName 3d513ab68a7b491db22f727769b2b91d";
            entity.LastName = "LastName 3d513ab68a7b491db22f727769b2b91d";
            entity.Email = "Email 3d513ab68a7b491db22f727769b2b91d";
            entity.Phone = "Phone 3d513ab68a7b491db22f727769b2b91d";
            entity.CVLink = "CVLink 3d513ab68a7b491db22f727769b2b91d";
            entity.CreatedByID = 100003;
            entity.CreatedDate = DateTime.Parse("8/14/2023 11:50:18 PM");
            entity.ModifiedByID = 100001;
            entity.ModifiedDate = DateTime.Parse("12/26/2021 9:12:18 PM");


            entity = dal.Upsert(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
            Assert.AreNotEqual(0, entity.ID);
            Assert.AreEqual("FirstName 3d513ab68a7b491db22f727769b2b91d", entity.FirstName);
            Assert.AreEqual("MiddleName 3d513ab68a7b491db22f727769b2b91d", entity.MiddleName);
            Assert.AreEqual("LastName 3d513ab68a7b491db22f727769b2b91d", entity.LastName);
            Assert.AreEqual("Email 3d513ab68a7b491db22f727769b2b91d", entity.Email);
            Assert.AreEqual("Phone 3d513ab68a7b491db22f727769b2b91d", entity.Phone);
            Assert.AreEqual("CVLink 3d513ab68a7b491db22f727769b2b91d", entity.CVLink);
            Assert.AreEqual(100003, entity.CreatedByID);
            Assert.AreEqual(DateTime.Parse("8/14/2023 11:50:18 PM"), entity.CreatedDate);
            Assert.AreEqual(100001, entity.ModifiedByID);
            Assert.AreEqual(DateTime.Parse("12/26/2021 9:12:18 PM"), entity.ModifiedDate);

        }

        [Test]
        public void UpdateCandidate_InvalidId()
        {
            var dal = PrepareCandidateDal("DALInitParams");

            var entity = new Candidate();
            entity.ID = Int64.MaxValue - 1;
            entity.FirstName = "FirstName 3d513ab68a7b491db22f727769b2b91d";
            entity.MiddleName = "MiddleName 3d513ab68a7b491db22f727769b2b91d";
            entity.LastName = "LastName 3d513ab68a7b491db22f727769b2b91d";
            entity.Email = "Email 3d513ab68a7b491db22f727769b2b91d";
            entity.Phone = "Phone 3d513ab68a7b491db22f727769b2b91d";
            entity.CVLink = "CVLink 3d513ab68a7b491db22f727769b2b91d";
            entity.CreatedByID = 100003;
            entity.CreatedDate = DateTime.Parse("8/14/2023 11:50:18 PM");
            entity.ModifiedByID = 100001;
            entity.ModifiedDate = DateTime.Parse("12/26/2021 9:12:18 PM");


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

        [Test]
        public void GetCandidateSkills_Success()
        {
            long positionId = 100003;
            var dal = PrepareCandidateDal("DALInitParams");

            IList<CandidateSkill> skills = dal.GetSkills(positionId);

            Assert.NotNull(skills);
            Assert.IsNotEmpty(skills);
        }

        [Test]
        public void GetCandidateSkills_InvalidCandidateId()
        {
            long positionId = Int64.MaxValue - 1;
            var dal = PrepareCandidateDal("DALInitParams");

            IList<CandidateSkill> skills = dal.GetSkills(positionId);

            Assert.IsNull(skills);
        }

        [TestCase("Candidate\\050.SetSkills.Success")]
        public void SetCandidateSkills_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            object objId = SetupCase(conn, caseName);
            long id = (long)objId;

            var dal = PrepareCandidateDal("DALInitParams");

            var candidate = dal.Get(id);

            IList<CandidateSkill> skills = new List<CandidateSkill>();
            skills.Add(new CandidateSkill() { SkillID = 1, ProficiencyID = 2 });
            skills.Add(new CandidateSkill() { SkillID = 2, ProficiencyID = 3 });
            skills.Add(new CandidateSkill() { SkillID = 3, ProficiencyID = 4 });

            dal.SetSkills((long)candidate.ID, skills);

            TeardownCase(conn, caseName);
        }
    }
}
