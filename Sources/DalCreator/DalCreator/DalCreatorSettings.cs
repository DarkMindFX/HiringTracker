using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalCreator
{
    public class DalCreatorSettings
    {
        public string ConnectionString { get; set; }

        public string TemplateName { get; set; }

        public string TemplatesRoot { get; set; }

        public string OutputRoot { get; set; }

        public string DalNamespace { get; set; }

        public string DalImplNamespace { get; set; }

        public string DtoNamespace { get; set; }

        public string ApiDalNamespace { get; set; }
    }
}
