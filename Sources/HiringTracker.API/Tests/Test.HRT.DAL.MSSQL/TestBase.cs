using HRT.DAL.MSSQL;
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
