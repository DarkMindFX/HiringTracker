using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalCreator.Generators
{
    public class DalImplsGeneratorParams
    {
        public string TemplatesRoot { get; set; }

        public string TemplateName { get; set; }

        public string OutputRoot { get; set; }

        public DateTime Timestamp { get; set; }

        public string DalImplNamespace { get; set; }
    }

    public class DalImplsGenerator : GeneratorBase
    {
        private readonly DalImplsGeneratorParams _genParams;

        public DalImplsGenerator(DalImplsGeneratorParams genParams)
        {
            _genParams = genParams;
        }

        public IList<string> GenerateScripts(IList<DataTable> tables)
        {
            IList<string> resultFiles = new List<string>();

            string templatesRoot = GetTemplatesFolder();
            string outRoot = GetOutputFolder();


            foreach (var table in tables)
            {
                var fileEntityDal = GenerateEntityDal(outRoot, templatesRoot, table);
                if (fileEntityDal != null)
                {
                    resultFiles.Add(fileEntityDal);
                }
            }

            return resultFiles;
        }

        private string GenerateEntityDal(string outRoot, string templatesRoot,  DataTable table)
        {
            string fileEntity = "EntityDal.cs";

            string rowToEntityList = GenerateRowToEntityList(table);
            string upsertParamsList = GenerateUpsertParamsList(table);

            Dictionary<string, string> replacements = new Dictionary<string, string>();
            replacements.Add("Entity", table.Name);
            replacements.Add("DalImplNamespace", _genParams.DalImplNamespace);
            replacements.Add("ROW_TO_ENTITY_LIST", rowToEntityList);
            replacements.Add("UPSERT_PARAMS_LIST", upsertParamsList);

            var outDal = ComposeFile(outRoot, templatesRoot, fileEntity, replacements);

            return outDal;
        }

        protected string GetTemplatesFolder()
        {
            return Path.Combine(_genParams.TemplatesRoot, _genParams.TemplateName, "DalImplementations");
        }

        protected string GetOutputFolder()
        {
            string outFolder = Path.Combine(_genParams.OutputRoot, _genParams.TemplateName, _genParams.Timestamp.ToString("yyyy-MM-dd HH-mm-ss"), "DalImplementations");
            if (!Directory.Exists(outFolder))
            {
                Directory.CreateDirectory(outFolder);
            }

            return outFolder;
        }

        protected string GenerateUpsertParamsList(DataTable table)
        {
            StringBuilder result = new StringBuilder();

            foreach (var c in table.Columns)
            {
                string line = $"   SqlParameter p{c.Name} = new SqlParameter(@\"{c.Name}\", " +
                              $"   SqlDbType.{DbTypeToSqlDbType(c)}, {(c.CharMaxLength != null ? c.CharMaxLength : 0)}, ParameterDirection.Input, {c.IsNullable.ToString().ToLower()}, {(c.Precision != null ? (int)c.Precision : 0)}, {(c.Scale != null ? (int)c.Scale : 0)}, \"{c.Name}\", DataRowVersion.Current, (object)entity.{c.Name} != null ? (object)entity.{c.Name} : DBNull.Value);" +
                              $"   cmd.Parameters.Add(p{c.Name}); ";

                result.AppendLine("\t\t" + line + "\n");
            }

            return result.ToString();
        }

        protected string GenerateRowToEntityList(DataTable table)
        {
            StringBuilder result = new StringBuilder();

            foreach(var c in table.Columns)
            {
                string type = DbTypeToType(c);
                string line = $"entity.{c.Name} = " + (c.IsNullable ? 
                                    $"!DBNull.Value.Equals(row[\"{c.Name}\"]) ?  ({type})row[\"{c.Name}\"] : null;"
                                    : $"({type})row[\"{c.Name}\"];");

                result.AppendLine("\t\t" + line);
            }

            return result.ToString();

        }
    }
}
