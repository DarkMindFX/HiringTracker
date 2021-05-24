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

    public class TestPositionCandidateStatusDal : TestBase
    {
        [Test]
        public void DalInit_Success()
        {
            IConfiguration config = GetConfiguration();
            var initParams = config.GetSection("DALInitParams").Get<TestDalInitParams>();

            IPositionCandidateStatusDal dal = new PositionCandidateStatusDal();
            var dalInitParams = dal.CreateInitParams();
            dalInitParams.Parameters["ConnectionString"] = initParams.ConnectionString;
            dal.Init(dalInitParams);
        }

        [Test]
        public void GetStatuses_Success()
        {
            var dal = PreparePositionCandidateStatusDal("DALInitParams");

            IList<PositionCandidateStatus> statuses = dal.GetAll();

            Assert.IsNotNull(statuses);
            Assert.IsNotEmpty(statuses);
        }
    }
}
