using HRT.DAL.MSSQL;
using HRT.Interfaces;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;


namespace Test.HRT.DAL.MSSQL
{
    public class TestBase
    {
        public class TestDALInitParams
        {
            [JsonProperty(PropertyName = "ConnectionString")]
            public string ConnectionString
            {
                get;
                set;
            }
        }
               
        protected ISkillDal PrepareSkillDal(string configName)
        {
            IConfiguration config = GetConfiguration();
            var userDALInitParams = config.GetSection(configName).Get<TestDALInitParams>();

            ISkillDal dal = new SkillDal();
            var dalInitParams = dal.CreateInitParams();
            dalInitParams.Parameters["ConnectionString"] = userDALInitParams.ConnectionString;
            dal.Init(dalInitParams);

            return dal;
        }

        protected ISkillProficiencyDal PrepareSkillProficiencyDal(string configName)
        {
            IConfiguration config = GetConfiguration();
            var userDALInitParams = config.GetSection(configName).Get<TestDALInitParams>();

            ISkillProficiencyDal dal = new SkillProficiencyDal();
            var dalInitParams = dal.CreateInitParams();
            dalInitParams.Parameters["ConnectionString"] = userDALInitParams.ConnectionString;
            dal.Init(dalInitParams);

            return dal;
        }

        protected IConfiguration GetConfiguration()
        {
            IConfiguration config = new ConfigurationBuilder()
                .AddJsonFile("appconfig.json", optional: false, reloadOnChange: true)
                .Build();

            return config;
        }
    }
}
