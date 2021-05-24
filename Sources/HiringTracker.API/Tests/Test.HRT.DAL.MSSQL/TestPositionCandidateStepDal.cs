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

    public class TestPositionCandidateStepDal : TestBase
    {
        [Test]
        public void DalInit_Success()
        {
            IConfiguration config = GetConfiguration();
            var initParams = config.GetSection("DALInitParams").Get<TestDalInitParams>();

            IPositionCandidateStepDal dal = new PositionCandidateStepDal();
            var dalInitParams = dal.CreateInitParams();
            dalInitParams.Parameters["ConnectionString"] = initParams.ConnectionString;
            dal.Init(dalInitParams);
        }

        [Test]
        public void GetSteps_Success()
        {
            var dal = PreparePositionCandidateStepDal("DALInitParams");

            IList<PositionCandidateStep> statuses = dal.GetAll();

            Assert.IsNotNull(statuses);
            Assert.IsNotEmpty(statuses);
        }
    }
}
