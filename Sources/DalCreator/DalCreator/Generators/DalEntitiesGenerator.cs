using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalCreator.Generators
{
    public class DalEntitiesGeneratorParams
    {
        public string TemplatesRoot { get; set; }

        public string TemplateName { get; set; }

        public string OutputRoot { get; set; }

        public DateTime Timestamp { get; set; }

        public string DalNamespace { get; set; }
    }

    public class DalEntitiesGenerator : GeneratorBase
    {
        private readonly DalEntitiesGeneratorParams _genParams;

        public DalEntitiesGenerator(DalEntitiesGeneratorParams genParams)
        {
            _genParams = genParams;
        }

        public IList<string> GenerateScripts(IList<DataTable> tables)
        {
            IList<string> resultFiles = new List<string>();

            string templatesRoot = GetTemplatesFolder();
            string outRootEntities = GetOutputFolder("Entities");
            string outRootInterfaces = GetOutputFolder("Interfaces");
            string fileEntity = "Entity.cs";
            string fileIEntityDal = "IEntityDal.cs";

            foreach (var table in tables)
            {
                IDictionary<string, string> replacements = new Dictionary<string, string>();
                replacements.Add("Entity", table.Name);
                replacements.Add("DalNamespace", _genParams.DalNamespace); ;
                replacements.Add("ENTITY_PROPERTIES_LIST", GenerateEntityProperties(table));

                var outEntity = ComposeFile(outRootEntities, templatesRoot, fileEntity, replacements);
                var outIEntityDal = ComposeFile(outRootInterfaces, templatesRoot, fileIEntityDal, replacements);

                if(outEntity != null)
                {
                    resultFiles.Add(outEntity);
                }
                if (outIEntityDal != null)
                {
                    resultFiles.Add(outIEntityDal);
                }
            }

            return resultFiles;
        }

        private string GenerateEntityProperties(DataTable table)
        {
            StringBuilder properties = new StringBuilder();

            foreach(var c in table.Columns)
            {
                string prop = GenerateClassProperty(c) + "\r\n";
                properties.Append(prop);
            }

            return properties.ToString();
        }

        

        private string GenerateClassProperty(DataColumn c)
        {
            StringBuilder result = new StringBuilder();
            result.Append("\t\tpublic ");
            result.Append(DbTypeToType(c));
            result.Append(" ");
            result.Append($"{c.Name}" + "{ get; set; }\r\n");

            return result.ToString();
        }
        

        

        protected string GetTemplatesFolder()
        {
            return Path.Combine(_genParams.TemplatesRoot, _genParams.TemplateName, "DalInterfaces");
        }

        protected string GetOutputFolder(string folder)
        {
            string outFolder = Path.Combine(_genParams.OutputRoot, _genParams.TemplateName, _genParams.Timestamp.ToString("yyyy-MM-dd HH-mm-ss"), "DalInterfaces", folder);
            if (!Directory.Exists(outFolder))
            {
                Directory.CreateDirectory(outFolder);
            }

            return outFolder;
        }
    }
}
