using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace T4DalGenerator
{
    public class DalCreatorSettings
    {
        public string ProjectName { get; set; }
        public string ConnectionString { get; set; }

        public string OutputRoot { get; set; }

        public Dictionary<string, string> OutputFolders { get; set; }

    }
}
