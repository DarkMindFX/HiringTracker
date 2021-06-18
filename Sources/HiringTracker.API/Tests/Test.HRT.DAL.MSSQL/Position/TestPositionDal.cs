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

        [TestCase("Position\\000.GetDetails.Success")]
        public void GetPosition_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PreparePositionDal("DALInitParams");

            object objId = SetupCase(conn, caseName);
            long id = (long)objId;

            Position entity = dal.Get(id);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
            Assert.AreNotEqual(0, entity.ID);
            Assert.AreEqual(727127, entity.DepartmentID);
            Assert.AreEqual("Title a5d9161f76324c3ca16553a638735e60", entity.Title);
            Assert.AreEqual("ShortDesc a5d9161f76324c3ca16553a638735e60", entity.ShortDesc);
            Assert.AreEqual("Description a5d9161f76324c3ca16553a638735e60", entity.Description);
            Assert.AreEqual(3, entity.StatusID);
            Assert.AreEqual(DateTime.Parse("7/8/2022 5:52:03 PM"), entity.CreatedDate);
            Assert.AreEqual(100001, entity.CreatedByID);
            Assert.AreEqual(DateTime.Parse("4/3/2019 3:16:03 AM"), entity.ModifiedDate);
            Assert.AreEqual(100003, entity.ModifiedByID);

        }

        [Test]
        public void GetPosition_InvalidId()
        {
            long id = Int32.MaxValue - 1;
            var dal = PreparePositionDal("DALInitParams");

            Position entity = dal.Get(id);

            Assert.IsNull(entity);
        }

        [TestCase("Position\\010.Delete.Success")]
        public void DeletePosition_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PreparePositionDal("DALInitParams");

            object objId = SetupCase(conn, caseName);
            long id = (long)objId;

            bool removed = dal.Delete(id);

            TeardownCase(conn, caseName);

            Assert.IsTrue(removed);
        }

        [Test]
        public void DeletePosition_InvalidId()
        {
            long positionId = Int32.MaxValue - 1;
            var dal = PreparePositionDal("DALInitParams");

            bool removed = dal.Delete(positionId);
            Assert.IsFalse(removed);

        }

        [TestCase("Position\\020.Insert.Success")]
        public void InsertPosition_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            SetupCase(conn, caseName);

            var dal = PreparePositionDal("DALInitParams");

            var entity = new Position();
            entity.DepartmentID = 909026;
            entity.Title = "Title 0d7bbd3eaa7646c0b95c47c567359b90";
            entity.ShortDesc = "ShortDesc 0d7bbd3eaa7646c0b95c47c567359b90";
            entity.Description = "Description 0d7bbd3eaa7646c0b95c47c567359b90";
            entity.StatusID = 2;
            entity.CreatedDate = DateTime.Parse("3/7/2019 12:16:03 AM");
            entity.CreatedByID = 100003;
            entity.ModifiedDate = DateTime.Parse("11/18/2021 3:27:03 PM");
            entity.ModifiedByID = 100002;


            entity = dal.Upsert(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
            Assert.AreNotEqual(0, entity.ID);
            Assert.AreEqual(909026, entity.DepartmentID);
            Assert.AreEqual("Title 0d7bbd3eaa7646c0b95c47c567359b90", entity.Title);
            Assert.AreEqual("ShortDesc 0d7bbd3eaa7646c0b95c47c567359b90", entity.ShortDesc);
            Assert.AreEqual("Description 0d7bbd3eaa7646c0b95c47c567359b90", entity.Description);
            Assert.AreEqual(2, entity.StatusID);
            Assert.AreEqual(DateTime.Parse("3/7/2019 12:16:03 AM"), entity.CreatedDate);
            Assert.AreEqual(100003, entity.CreatedByID);
            Assert.AreEqual(DateTime.Parse("11/18/2021 3:27:03 PM"), entity.ModifiedDate);
            Assert.AreEqual(100002, entity.ModifiedByID);

        }

        [TestCase("Position\\030.Update.Success")]
        public void UpdatePosition_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            var dal = PreparePositionDal("DALInitParams");

            object objId = SetupCase(conn, caseName);
            long id = (long)objId;

            var entity = dal.Get(id);
            entity.DepartmentID = 193912;
            entity.Title = "Title 85a2d7e0677a4f93a97ca2e97fe3762b";
            entity.ShortDesc = "ShortDesc 85a2d7e0677a4f93a97ca2e97fe3762b";
            entity.Description = "Description 85a2d7e0677a4f93a97ca2e97fe3762b";
            entity.StatusID = 2;
            entity.CreatedDate = DateTime.Parse("10/3/2019 9:41:03 PM");
            entity.CreatedByID = 100002;
            entity.ModifiedDate = DateTime.Parse("8/14/2022 7:28:03 AM");
            entity.ModifiedByID = 100003;


            entity = dal.Upsert(entity);

            TeardownCase(conn, caseName);

            Assert.IsNotNull(entity);
            Assert.AreNotEqual(0, entity.ID);
            Assert.AreEqual(193912, entity.DepartmentID);
            Assert.AreEqual("Title 85a2d7e0677a4f93a97ca2e97fe3762b", entity.Title);
            Assert.AreEqual("ShortDesc 85a2d7e0677a4f93a97ca2e97fe3762b", entity.ShortDesc);
            Assert.AreEqual("Description 85a2d7e0677a4f93a97ca2e97fe3762b", entity.Description);
            Assert.AreEqual(2, entity.StatusID);
            Assert.AreEqual(DateTime.Parse("10/3/2019 9:41:03 PM"), entity.CreatedDate);
            Assert.AreEqual(100002, entity.CreatedByID);
            Assert.AreEqual(DateTime.Parse("8/14/2022 7:28:03 AM"), entity.ModifiedDate);
            Assert.AreEqual(100003, entity.ModifiedByID);

        }

        [Test]
        public void UpdatePosition_InvalidId()
        {
            var dal = PreparePositionDal("DALInitParams");

            var entity = new Position();
            entity.ID = Int64.MaxValue - 1;
            entity.DepartmentID = 193912;
            entity.Title = "Title 85a2d7e0677a4f93a97ca2e97fe3762b";
            entity.ShortDesc = "ShortDesc 85a2d7e0677a4f93a97ca2e97fe3762b";
            entity.Description = "Description 85a2d7e0677a4f93a97ca2e97fe3762b";
            entity.StatusID = 2;
            entity.CreatedDate = DateTime.Parse("10/3/2019 9:41:03 PM");
            entity.CreatedByID = 100002;
            entity.ModifiedDate = DateTime.Parse("8/14/2022 7:28:03 AM");
            entity.ModifiedByID = 100003;


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

        [TestCase("Position\\050.SetSkills.Success")]
        public void SetPositionSkills_Success(string caseName)
        {
            SqlConnection conn = OpenConnection("DALInitParams");
            object objId = SetupCase(conn, caseName);
            long id = (long)objId;

            var dal = PreparePositionDal("DALInitParams");

            var candidate = dal.Get(id);

            IList<PositionSkill> skills = new List<PositionSkill>();
            skills.Add(new PositionSkill() { SkillID = 1, ProficiencyID = 2 });
            skills.Add(new PositionSkill() { SkillID = 2, ProficiencyID = 3 });
            skills.Add(new PositionSkill() { SkillID = 3, ProficiencyID = 4 });

            dal.SetSkills((long)candidate.ID, skills);

            TeardownCase(conn, caseName);
        }
    }
}
