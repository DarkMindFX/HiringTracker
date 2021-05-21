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
    public class TestPositionStatusDal : TestBase
    {
        [Test]
        public void DalInit_Success()
        {
            IConfiguration config = GetConfiguration();
            var initParams = config.GetSection("DALInitParams").Get<TestDalInitParams>();

            IPositionStatusDal dal = new PositionStatusDal();
            var dalInitParams = dal.CreateInitParams();
            dalInitParams.Parameters["ConnectionString"] = initParams.ConnectionString;
            dal.Init(dalInitParams);
        }

        [Test]
        public void GetSkills_Success()
        {
            var dal = PreparePositionStatusDal("DALInitParams");

            IList<PositionStatus> statuses = dal.GetAll();

            Assert.IsNotNull(statuses);
            Assert.IsNotEmpty(statuses);
        }
    }
}
