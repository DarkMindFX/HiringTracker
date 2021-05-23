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
    public class TestPositionDal : TestBase
    {
        [Test]
        public void DalInit_Success()
        {
            IConfiguration config = GetConfiguration();
            var initParams = config.GetSection("DALInitParams").Get<TestDalInitParams>();

            IPositionDal dal = new PositionDal();
            var dalInitParams = dal.CreateInitParams();
            dalInitParams.Parameters["ConnectionString"] = initParams.ConnectionString;
            dal.Init(dalInitParams);
        }

        [Test]
        public void GetPositions_Success()
        {
            var dal = PreparePositionDal("DALInitParams");

            IList<Position> entities = dal.GetAll();

            Assert.IsNotNull(entities);
            Assert.IsNotEmpty(entities);
        }

        [Test]
        public void GetPosition_Success()
        {
            long positionId = 100001;
            var dal = PreparePositionDal("DALInitParams");

            Position entity = dal.Get(positionId);

            Assert.IsNotNull(entity);
            Assert.AreNotEqual(0, entity.PositionID);
            Assert.IsTrue(!string.IsNullOrEmpty(entity.Title));
            Assert.IsTrue(!string.IsNullOrEmpty(entity.ShortDesc));
            Assert.IsTrue(!string.IsNullOrEmpty(entity.Description));
            Assert.AreEqual(1, entity.StatusID);
            Assert.AreEqual(100001, entity.CreatedByID);
            Assert.AreNotEqual(DateTime.MinValue, entity.CreatedDate);

        }

        [Test]
        public void GetPosition_InvalidId()
        {
            long positionId = Int32.MaxValue - 1;
            var dal = PreparePositionDal("DALInitParams");

            Position entity = dal.Get(positionId);

            Assert.IsNull(entity);
        }

        [TestCase("Position\\000.Delete.Success")]
        public void DeletePosition_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            SetupCase(conn, caseName);

            string uuid = "[Test {16C8B6BE19D34FD5B97DB0F31C0ECFF3}] Title";

            var dal = PreparePositionDal("DALInitParams");

            IList<Position> entities = dal.GetAll();

            Position entity = entities.FirstOrDefault(x => x.Title == uuid);

            if (entity != null)
            {
                bool removed = dal.Delete((long)entity.PositionID);

                Assert.IsTrue(removed);
            }

            TeardownCase(conn, caseName);
        }

        [Test]
        public void DeletePosition_InvalidId()
        {
            long positionId = Int32.MaxValue - 1;
            var dal = PreparePositionDal("DALInitParams");

            bool removed = dal.Delete(positionId);
            Assert.IsFalse(removed);

        }

        [TestCase("Position\\010.Update.Success")]
        public void UpdatePosition_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            SetupCase(conn, caseName);

            string uuid = "[Test {A3841D7B15D74E9EAA9E0F81FA77A64B}] Title";
            long userId = 100001;

            string newTitle = "[Test {A3841D7B15D74E9EAA9E0F81FA77A64B}] Updated";
            string newShortDesc = "[Test {A3841D7B15D74E9EAA9E0F81FA77A64B}] Updated";
            string newDesc = "[Test {A3841D7B15D74E9EAA9E0F81FA77A64B}] Desc Updated";

            var dal = PreparePositionDal("DALInitParams");

            IList<Position> entities = dal.GetAll();

            Position entity = entities.FirstOrDefault(x => x.Title == uuid);

            if (entity != null)
            {
                entity.Title = newTitle;
                entity.ShortDesc = newShortDesc;
                entity.Description = newDesc;
                long? id = dal.Upsert(entity, userId);

                Assert.IsNull(id);
            }

            TeardownCase(conn, caseName);
        }

        [Test]
        public void UpdatePosition_InvalidId()
        {
            long userId = 100001;
            long positionId = Int32.MaxValue - 1;

            string newTitle = "[Test {9A78D71DEC6B4296AA362CD1F95F2541}] Updated";
            string newShortDesc = "[Test {9A78D71DEC6B4296AA362CD1F95F2541}] Updated";
            string newDesc = "[Test {9A78D71DEC6B4296AA362CD1F95F2541}] Desc Updated";

            Position entity = new Position()
            {
                PositionID = positionId,
                StatusID = 1,
                Title = newTitle,
                ShortDesc = newShortDesc,
                Description = newDesc
            };

            var dal = PreparePositionDal("DALInitParams");

            try
            {
                long? id = dal.Upsert(entity, userId);

                Assert.Fail("Test Failed - exception was not thrown");
            }
            catch (Exception ex)
            {
                if (ex.Message.IndexOf("Position with given id not found") < 0)
                {
                    Assert.Fail($"Unexpected exception thrown: {ex}");
                }
            }
        }

        [TestCase("Position\\020.Insert.Success")]
        public void AddPosition_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            SetupCase(conn, caseName);

            long userId = 100001;

            string newTitle = "[Test {BFFBD7DA0279431FB799E7EBB0E82926}] New";
            string newShortDesc = "[Test {BFFBD7DA0279431FB799E7EBB0E82926}] ShortDesc";
            string newDesc = "[Test {BFFBD7DA0279431FB799E7EBB0E82926}] Desc";

            Position entity = new Position()
            {
                PositionID = null,
                StatusID = 1,
                Title = newTitle,
                ShortDesc = newShortDesc,
                Description = newDesc
            };

            var dal = PreparePositionDal("DALInitParams");

            long? id = dal.Upsert(entity, userId);

            Assert.IsNotNull(id);
            Assert.Greater(id, 0);

            TeardownCase(conn, caseName);
        }

        [Test]
        public void GetPositionSkills_Success()
        {
            long positionId = 100003;
            var dal = PreparePositionDal("DALInitParams");

            IList<PositionSkill> skills = dal.GetSkills(positionId);

            Assert.NotNull(skills);
            Assert.IsNotEmpty(skills);
        }

        [Test]
        public void GetPositionSkills_InvalidPositionId()
        {
            long positionId = Int64.MaxValue - 1;
            var dal = PreparePositionDal("DALInitParams");

            IList<PositionSkill> skills = dal.GetSkills(positionId);

            Assert.IsNull(skills);
        }

        [TestCase("Position\\030.SetSkills.Success")]
        public void SetPositionSkills_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            SetupCase(conn, caseName);

            string title = "[Test {E82969A94FA5417986C4F70040CBF6F5}] Skills";

            var dal = PreparePositionDal("DALInitParams");

            IList<Position> positions = dal.GetAll();
            var position = positions.FirstOrDefault( x => x.Title.Equals(title));

            IList<PositionSkill> skills = new List<PositionSkill>();
            skills.Add(new PositionSkill() { SkillID = 1, IsMandatory = false, ProficiencyID = 2 });
            skills.Add(new PositionSkill() { SkillID = 2, IsMandatory = false, ProficiencyID = 3 });
            skills.Add(new PositionSkill() { SkillID = 3, IsMandatory = false, ProficiencyID = 4 });

            dal.SetSkills((long)position.PositionID, skills);

            TeardownCase(conn, caseName);
        }
    }
}
