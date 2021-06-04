using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalCreator.Generators
{
    public class DtosGeneratorParams
    {
        public string TemplatesRoot { get; set; }

        public string TemplateName { get; set; }

        public string OutputRoot { get; set; }

        public DateTime Timestamp { get; set; }

        public string DtoNamespace { get; set; }

        public string ApiDalNamespace { get; set; }
    }

    public class DtosGenerator : GeneratorBase
    {
        private readonly DtosGeneratorParams _genParams;

        public DtosGenerator(DtosGeneratorParams genParams)
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
                replacements.Add("DtoNamespace", _genParams.DtoNamespace);
                replacements.Add("ApiDalNamespace", _genParams.ApiDalNamespace);
                replacements.Add("ENTITY_PROPERTIES_LIST", GenerateEntityProperties(table));

                var outEntity = ComposeFile(outRootEntities, templatesRoot, fileEntity, replacements);
                var outIEntityDal = ComposeFile(outRootInterfaces, templatesRoot, fileIEntityDal, replacements);

                if (outEntity != null)
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

            foreach (var c in table.Columns)
            {
                string prop = GenerateClassProperty(c) + "\r\n";
                properties.Append(prop);
            }

            return properties.ToString();
        }

        private string GenerateClassProperty(DataColumn c)
        {
            StringBuilder result = new StringBuilder();
            result.Append($"\t\t[JsonPropertyName(\"{c.Name}\")]\r\n");
            result.Append("\t\tpublic ");
            if (string.IsNullOrEmpty(c.FKRefTable))
            {
                result.Append(DbTypeToType(c));
            }
            else
            {
                result.Append(c.FKRefTable);
            }
            result.Append(" ");
            result.Append($"{c.Name}" + " { get; set; }\r\n");

            return result.ToString();
        }

        protected string GetTemplatesFolder()
        {
            return Path.Combine(_genParams.TemplatesRoot, _genParams.TemplateName, "DTOs");
        }

        protected string GetOutputFolder(string folder)
        {
            string outFolder = Path.Combine(_genParams.OutputRoot, _genParams.TemplateName, _genParams.Timestamp.ToString("yyyy-MM-dd HH-mm-ss"), "DTOs", folder);
            if (!Directory.Exists(outFolder))
            {
                Directory.CreateDirectory(outFolder);
            }

            return outFolder;
        }
    }
}
