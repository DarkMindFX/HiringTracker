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

        [TestCase("Candidate\\040.GetDetails.Success")]
        public void GetCandidate_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            object objId = SetupCase(conn, caseName);

            long id = (long)objId;
            var dal = PrepareCandidateDal("DALInitParams");

            Candidate entity = dal.Get(id);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
            Assert.AreNotEqual(0, entity.CandidateID);
            Assert.IsTrue(!string.IsNullOrEmpty(entity.FirstName));
            Assert.IsTrue(string.IsNullOrEmpty(entity.MiddleName));
            Assert.IsTrue(!string.IsNullOrEmpty(entity.LastName));
            Assert.IsTrue(!string.IsNullOrEmpty(entity.Email));
            Assert.IsTrue(!string.IsNullOrEmpty(entity.Phone));
            Assert.IsTrue(!string.IsNullOrEmpty(entity.CVLink));
            Assert.AreEqual(100001, entity.CreatedByID);
            Assert.AreNotEqual(DateTime.MinValue, entity.CreatedDate);
        }

        [Test]
        public void GetCandidate_InvalidId()
        {
            long candidateId = Int32.MaxValue - 1;
            var dal = PrepareCandidateDal("DALInitParams");

            Candidate entity = dal.Get(candidateId);

            Assert.IsNull(entity);
        }

        [TestCase("Candidate\\000.Delete.Success")]
        public void DeleteCandidate_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            SetupCase(conn, caseName);

            string uuid = "test_895A97819DEC4F7888EDD6939159D1A6@email.com";

            var dal = PrepareCandidateDal("DALInitParams");

            IList<Candidate> entities = dal.GetAll();

            Candidate entity = entities.FirstOrDefault(x => x.Email == uuid);

            if (entity != null)
            {
                bool removed = dal.Delete((long)entity.CandidateID);

                Assert.IsTrue(removed);
            }

            TeardownCase(conn, caseName);
        }

        [Test]
        public void DeleteCandidate_InvalidId()
        {
            long positionId = Int32.MaxValue - 1;
            var dal = PrepareCandidateDal("DALInitParams");

            bool removed = dal.Delete(positionId);
            Assert.IsFalse(removed);

        }

        [TestCase("Candidate\\010.Update.Success")]
        public void UpdateCandidate_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            SetupCase(conn, caseName);

            string uuid = "test_7DE3B9014362458C8709B8D1DD3D8EBA@email.com";
            long userId = 100001;

            string newFname = "[Test {7DE3B9014362458C8709B8D1DD3D8EBA}] First_U";
            string newMName = "[Test {7DE3B9014362458C8709B8D1DD3D8EBA}] Middle_U";
            string newLName = "[Test {7DE3B9014362458C8709B8D1DD3D8EBA}] Last_U";
            string newPhone = "+7DE3B9014362458C8709B8D1DD3D8EBA_U";
            string newCVLink = "http://www.dropbox.com/cvs/test_7DE3B9014362458C8709B8D1DD3D8EBA_U.pdf";

            var dal = PrepareCandidateDal("DALInitParams");

            IList<Candidate> entities = dal.GetAll();

            Candidate entity = entities.FirstOrDefault(x => x.Email == uuid);

            if (entity != null)
            {
                entity.FirstName = newFname;
                entity.MiddleName = newMName;
                entity.LastName = newLName;
                entity.CVLink = newCVLink;
                entity.Phone = newPhone;

                long? id = dal.Upsert(entity, userId);

                Assert.IsNull(id);
            }

            TeardownCase(conn, caseName);
        }

        [Test]
        public void UpdateCandidate_InvalidId()
        {
            long userId = 100001;
            long candidateId = Int64.MaxValue - 1;

            string newFname = "[Test {7DE3B9014362458C8709B8D1DD3D8EBA}] First";
            string newMName = "[Test {7DE3B9014362458C8709B8D1DD3D8EBA}] Middle";
            string newLName = "[Test {7DE3B9014362458C8709B8D1DD3D8EBA}] Last";
            string newPhone = "+7DE3B9014362458C8709B8D1DD3D8EBA_U";
            string newCVLink = "http://www.dropbox.com/cvs/test_7DE3B9014362458C8709B8D1DD3D8EBA.pdf";
            string newEmail = "test_7DE3B9014362458C8709B8D1DD3D8EBA@invalid.com";

            Candidate entity = new Candidate()
            {
                CandidateID = candidateId,
                FirstName = newFname,
                MiddleName = newMName,
                LastName = newLName,
                Email = newEmail,
                CVLink = newCVLink,
                Phone = newPhone
            };

            var dal = PrepareCandidateDal("DALInitParams");

            try
            {
                long? id = dal.Upsert(entity, userId);

                Assert.Fail("Test Failed - exception was not thrown");
            }
            catch (Exception ex)
            {
                if (ex.Message.ToLower().IndexOf("candidate with given id was not found") < 0)
                {
                    Assert.Fail($"Unexpected exception thrown: {ex}");
                }
            }
        }

        [TestCase("Candidate\\020.Insert.Success")]
        public void InsertCandidate_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            SetupCase(conn, caseName);
                        
            long userId = 100001;

            string newFname = "[Test {A340848A514F429EA5768209D6BED3E0}] First";
            string newMName = "[Test {A340848A514F429EA5768209D6BED3E0}] Middle";
            string newLName = "[Test {A340848A514F429EA5768209D6BED3E0}] Last";
            string newEmail = "test_A340848A514F429EA5768209D6BED3E0@email.com";
            string newPhone = "+A340848A514F429EA5768209D6BED3E0";
            string newCVLink = "http://www.dropbox.com/cvs/test_A340848A514F429EA5768209D6BED3E0.pdf";

            var dal = PrepareCandidateDal("DALInitParams");

            Candidate entity = new Candidate();

            entity.FirstName = newFname;
            entity.MiddleName = newMName;
            entity.LastName = newLName;
            entity.CVLink = newCVLink;
            entity.Phone = newPhone;
            entity.Email = newEmail;

            long? id = dal.Upsert(entity, userId);

            Assert.IsNotNull(id);
            Assert.Greater(id, 0);

            TeardownCase(conn, caseName);
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

        [TestCase("Candidate\\030.SetSkills.Success")]
        public void SetCandidateSkills_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            SetupCase(conn, caseName);

            string email = "test_C6E0BFF2A9584726ACA898DBF51A3AD9@email.com";

            var dal = PrepareCandidateDal("DALInitParams");

            IList<Candidate> positions = dal.GetAll();
            var position = positions.FirstOrDefault(x => x.Email.Equals(email)); 

            IList<CandidateSkill> skills = new List<CandidateSkill>();
            skills.Add(new CandidateSkill() { SkillID = 1, ProficiencyID = 2 });
            skills.Add(new CandidateSkill() { SkillID = 2, ProficiencyID = 3 });
            skills.Add(new CandidateSkill() { SkillID = 3, ProficiencyID = 4 });

            dal.SetSkills((long)position.CandidateID, skills);

            TeardownCase(conn, caseName);
        }
    }
}
