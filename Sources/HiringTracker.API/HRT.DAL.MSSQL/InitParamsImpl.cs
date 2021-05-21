using HRT.Interfaces;
using SM.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace HRT.DAL.MSSQL
{
    public class InitParamsImpl : IInitParams
    {
        public InitParamsImpl()
        {
            Parameters = new Dictionary<string, string>();
            Parameters["ConnectionString"] = string.Empty;
        }

        public Dictionary<string, string> Parameters
        {
            get;
            set;
        }
    }
}
