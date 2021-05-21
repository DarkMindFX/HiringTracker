using HRT.DAL.MSSQL;
using HRT.Interfaces;
using HRT.Interfaces.Entities;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using System.Collections.Generic;

namespace Test.HRT.DAL.MSSQL
{
    public class TestSkillDal : TestBase
    {
        [Test]
        public void DalInit_Success()
        {
            IConfiguration config = GetConfiguration();
            var skillDalInitParams = config.GetSection("DALInitParams").Get<TestDalInitParams>();

            ISkillDal dal = new SkillDal();
            var dalInitParams = dal.CreateInitParams();
            dalInitParams.Parameters["ConnectionString"] = skillDalInitParams.ConnectionString;
            dal.Init(dalInitParams);
        }

        [Test]
        public void GetSkills_Success()
        {
            var dal = PrepareSkillDal("DALInitParams");

            IList<Skill> skills = dal.GetAll();

            Assert.IsNotNull(skills);
            Assert.IsNotEmpty(skills);
        }
    }
}