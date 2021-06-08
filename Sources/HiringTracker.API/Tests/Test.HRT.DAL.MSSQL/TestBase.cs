﻿using HRT.DAL.MSSQL;
using HRT.Interfaces;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;

namespace Test.HRT.DAL.MSSQL
{
    public class TestBase
    {
        public class TestDalInitParams
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
            var initParams = config.GetSection(configName).Get<TestDalInitParams>();

            ISkillDal dal = new SkillDal();
            var dalInitParams = dal.CreateInitParams();
            dalInitParams.Parameters["ConnectionString"] = initParams.ConnectionString;
            dal.Init(dalInitParams);

            return dal;
        }

        protected ISkillProficiencyDal PrepareSkillProficiencyDal(string configName)
        {
            IConfiguration config = GetConfiguration();
            var initParams = config.GetSection(configName).Get<TestDalInitParams>();

            ISkillProficiencyDal dal = new SkillProficiencyDal();
            var dalInitParams = dal.CreateInitParams();
            dalInitParams.Parameters["ConnectionString"] = initParams.ConnectionString;
            dal.Init(dalInitParams);

            return dal;
        }

        protected IPositionStatusDal PreparePositionStatusDal(string configName)
        {
            IConfiguration config = GetConfiguration();
            var initParams = config.GetSection(configName).Get<TestDalInitParams>();

            IPositionStatusDal dal = new PositionStatusDal();
            var dalInitParams = dal.CreateInitParams();
            dalInitParams.Parameters["ConnectionString"] = initParams.ConnectionString;
            dal.Init(dalInitParams);

            return dal;
        }

        protected IPositionCandidateDal PreparePositionCandidateDal(string configName)
        {
            IConfiguration config = GetConfiguration();
            var initParams = config.GetSection(configName).Get<TestDalInitParams>();

            IPositionCandidateDal dal = new PositionCandidateDal();
            var dalInitParams = dal.CreateInitParams();
            dalInitParams.Parameters["ConnectionString"] = initParams.ConnectionString;
            dal.Init(dalInitParams);

            return dal;
        }

        protected IPositionCandidateStepDal PreparePositionCandidateStepDal(string configName)
        {
            IConfiguration config = GetConfiguration();
            var initParams = config.GetSection(configName).Get<TestDalInitParams>();

            IPositionCandidateStepDal dal = new PositionCandidateStepDal();
            var dalInitParams = dal.CreateInitParams();
            dalInitParams.Parameters["ConnectionString"] = initParams.ConnectionString;
            dal.Init(dalInitParams);

            return dal;
        }

        protected IPositionCandidateStatusDal PreparePositionCandidateStatusDal(string configName)
        {
            IConfiguration config = GetConfiguration();
            var initParams = config.GetSection(configName).Get<TestDalInitParams>();

            IPositionCandidateStatusDal dal = new PositionCandidateStatusDal();
            var dalInitParams = dal.CreateInitParams();
            dalInitParams.Parameters["ConnectionString"] = initParams.ConnectionString;
            dal.Init(dalInitParams);

            return dal;
        }

        protected IDepartmentDal PrepareDepartmentDal(string configName)
        {
            IConfiguration config = GetConfiguration();
            var initParams = config.GetSection(configName).Get<TestDalInitParams>();

            IDepartmentDal dal = new DepartmentDal();
            var dalInitParams = dal.CreateInitParams();
            dalInitParams.Parameters["ConnectionString"] = initParams.ConnectionString;
            dal.Init(dalInitParams);

            return dal;
        }

        protected IPositionDal PreparePositionDal(string configName)
        {
            IConfiguration config = GetConfiguration();
            var initParams = config.GetSection(configName).Get<TestDalInitParams>();

            IPositionDal dal = new PositionDal();
            var dalInitParams = dal.CreateInitParams();
            dalInitParams.Parameters["ConnectionString"] = initParams.ConnectionString;
            dal.Init(dalInitParams);

            return dal;
        }

        protected ICandidateDal PrepareCandidateDal(string configName)
        {
            IConfiguration config = GetConfiguration();
            var initParams = config.GetSection(configName).Get<TestDalInitParams>();

            ICandidateDal dal = new CandidateDal();
            var dalInitParams = dal.CreateInitParams();
            dalInitParams.Parameters["ConnectionString"] = initParams.ConnectionString;
            dal.Init(dalInitParams);

            return dal;
        }

        protected ICandidateCommentDal PrepareCandidateCommentDal(string configName)
        {
            IConfiguration config = GetConfiguration();
            var initParams = config.GetSection(configName).Get<TestDalInitParams>();

            ICandidateCommentDal dal = new CandidateCommentDal();
            var dalInitParams = dal.CreateInitParams();
            dalInitParams.Parameters["ConnectionString"] = initParams.ConnectionString;
            dal.Init(dalInitParams);

            return dal;
        }

        protected ICommentDal PrepareCommentDal(string configName)
        {
            IConfiguration config = GetConfiguration();
            var initParams = config.GetSection(configName).Get<TestDalInitParams>();

            ICommentDal dal = new CommentDal();
            var dalInitParams = dal.CreateInitParams();
            dalInitParams.Parameters["ConnectionString"] = initParams.ConnectionString;
            dal.Init(dalInitParams);

            return dal;
        }

        protected object SetupCase(SqlConnection conn, string caseRoot)
        {
            object result = null;
            string fileName = "Setup.sql";
            string path = Path.Combine(TestBaseFolder, caseRoot, fileName);
            if (File.Exists(path))
            {
                result = RunScript(conn, path);
            }

            return result;
        }

        protected SqlConnection OpenConnection(string settingsName)
        {
            IConfiguration config = GetConfiguration();
            var initParams = config.GetSection(settingsName).Get<TestDalInitParams>();
            SqlConnection conn = new SqlConnection(initParams.ConnectionString);
            conn.Open();

            return conn;
        }

        protected void CloseConnection(SqlConnection conn)
        {
            if(conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
        }

        protected void TeardownCase(SqlConnection conn, string caseRoot)
        {
            string fileName = "Teardown.sql";
            string path = Path.Combine(TestBaseFolder, caseRoot, fileName);
            if (File.Exists(path))
            {
                RunScript(conn, path);
            }
        }

        protected object RunScript(SqlConnection conn, string filePath)
        {
            object result = null;
            string sql = File.ReadAllText(filePath);
            if (!string.IsNullOrEmpty(sql))
            {
                SqlCommand cmd = new SqlCommand(sql);
                cmd.Connection = conn;
                cmd.CommandType = CommandType.Text;
                result = cmd.ExecuteScalar();
            }

            return result;
        }

        protected string TestBaseFolder
        {
            get
            {
                return Path.Combine(TestContext.CurrentContext.TestDirectory, "..\\..\\..");
            }
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
