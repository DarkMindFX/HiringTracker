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

    public class DalEntitiesGenerator
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
            string outRoot = GetOutputFolder();
            string contentEntity = File.ReadAllText(Path.Combine(templatesRoot, "Entity.cs"));
            string contentIEntityDal = File.ReadAllText(Path.Combine(templatesRoot, "IEntityDal.cs"));

            foreach (var table in tables)
            {
                var fileEntity = GenerateEntity(outRoot, contentEntity, table);
                if (fileEntity != null)
                {
                    resultFiles.Add(fileEntity);
                }
                var fileIEntityDal = GenerateIEntityDal(outRoot, contentIEntityDal, table);
                if (fileIEntityDal != null)
                {
                    resultFiles.Add(fileIEntityDal);
                }
            }

            return resultFiles;
        }

        private string GenerateEntity(string outRoot, string contentEntity, DataTable table)
        {
            string outPath = Path.Combine(outRoot, table.Name + ".cs");
            StringBuilder properties = new StringBuilder();

            foreach(var c in table.Columns)
            {
                string prop = GenerateClassProperty(c) + "\r\n";
                properties.Append( prop);
            }

            contentEntity = contentEntity.Replace("{ENTITY_PROPERTIES_LIST}", properties.ToString());
            contentEntity = contentEntity.Replace("{Entity}", table.Name);
            contentEntity = contentEntity.Replace("{DaNamespace}", _genParams.DalNamespace);

            File.WriteAllText(outPath, contentEntity);

            return outPath;
        }

        private string GenerateIEntityDal(string outRoot, string contentIEntityDal, DataTable table)
        {
            string outPath = Path.Combine(outRoot, "I" + table.Name + "Dal.cs");

            contentIEntityDal = contentIEntityDal.Replace("{Entity}", table.Name);
            contentIEntityDal = contentIEntityDal.Replace("{DaNamespace}", _genParams.DalNamespace);

            File.WriteAllText(outPath, contentIEntityDal);

            return outPath;
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
        

        private string DbTypeToType(DataColumn c)
        {
            string result = null;
            Type type = null;

            switch(c.SqlType.ToLower())
            {
                case "int":
                    type = typeof(Int32);
                    break;
                case "date":
                case "datetime":
                case "datetime2":
                    type = typeof(DateTime);
                    break;
                case "bit":
                    type = typeof(bool);
                    break;
                case "bigint":
                    type = typeof(long);
                    break;
                case "float":
                    type = typeof(float);
                    break;
                case "numeric":
                case "decimal":
                    type = typeof(decimal);
                    break;
                case "char":
                case "nchar":
                case "varchar":
                case "nvarchar":
                case "text":
                case "ntext":
                case "xml":
                    type = typeof(string);
                    break;
            }

            result = type.ToString();

            if (c.IsIdentity || (c.IsNullable && Nullable.GetUnderlyingType(type) == null))
            {
                result += "?";
            }

            return result;
        }

        protected string GetTemplatesFolder()
        {
            return Path.Combine(_genParams.TemplatesRoot, _genParams.TemplateName, "DalInterfaces");
        }

        protected string GetOutputFolder()
        {
            string outFolder = Path.Combine(_genParams.OutputRoot, _genParams.TemplateName, _genParams.Timestamp.ToString("yyyy-MM-dd HH-mm-ss"), "DalInterfaces");
            if (!Directory.Exists(outFolder))
            {
                Directory.CreateDirectory(outFolder);
            }

            return outFolder;
        }
    }
}
