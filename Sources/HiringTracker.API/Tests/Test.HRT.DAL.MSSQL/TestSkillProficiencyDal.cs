using HRT.DAL.MSSQL;
using HRT.Interfaces;
using HRT.Interfaces.Entities;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Test.HRT.DAL.MSSQL
{
    class TestSkillProficiencyDal : TestBase
    {
        [Test]
        public void DalInit_Success()
        {
            IConfiguration config = GetConfiguration();
            var skillDalInitParams = config.GetSection("DALInitParams").Get<TestDALInitParams>();

            ISkillProficiencyDal dal = new SkillProficiencyDal();
            var dalInitParams = dal.CreateInitParams();
            dalInitParams.Parameters["ConnectionString"] = skillDalInitParams.ConnectionString;
            dal.Init(dalInitParams);
        }

        [Test]
        public void GetSkillProfs_Success()
        {
            var dal = PrepareSkillProficiencyDal("DALInitParams");

            IList<SkillProficiency> profs = dal.GetAll();

            Assert.IsNotNull(profs);
            Assert.IsNotEmpty(profs);
        }
    }
}
