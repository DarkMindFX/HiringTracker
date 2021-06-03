using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalCreator.Generators
{
    public class SqlScriptsGeneratorParams
    {
        public string TemplatesRoot { get; set; }

        public string TemplateName { get; set; }

        public string OutputRoot { get; set; }

        public DateTime Timestamp { get; set; }
    }

    public class SqlScriptsGenerator
    {
        private readonly SqlScriptsGeneratorParams _genParams;

        public SqlScriptsGenerator(SqlScriptsGeneratorParams genParams)
        {
            _genParams = genParams;
        }

        public IList<string> GenerateScripts(IList<DataTable> tables)
        {
            IList<string> resultFiles = new List<string>();

            string templatesRoot = GetTemplatesFolder();
            string outRoot = GetOutputFolder();

            foreach(var table in tables)
            {
                var pkColumns = GetPKColumns(table);
                string entityName = table.Name;
                string paramsPKList = GenerateParamsPKList(table, pkColumns);
                string wherePKList = GenerateWherePKList(table, pkColumns);
                string paramsUpsertList = GenerateParamsUpsertList(table);
                string upsertInsertValuesList = GenerateUpserInsertValuesList(table);
                string upsertUpdateValuesList = GenerateUpserUpdateValuesList(table);
                string whereUpsertList = GenerateWhereUpsertList(table);

                IDictionary<string, string> replacements = new Dictionary<string, string>();
                replacements.Add("Entity", entityName);
                replacements.Add("PARAMS_PK_LIST", paramsPKList);
                replacements.Add("WHERE_PK_LIST", wherePKList);
                replacements.Add("PARAMS_UPSERT_LIST", paramsUpsertList);
                replacements.Add("UPSERT_INSERT_VALUES_LIST", upsertInsertValuesList);
                replacements.Add("UPSERT_UPDATE_VALUES_LIST", upsertUpdateValuesList);
                replacements.Add("WHERE_UPSERT_LIST", whereUpsertList);

                string[] templateFiles = Directory.GetFiles(templatesRoot);
                foreach(var tf in templateFiles)
                {
                    string outFile = ComposeFile(outRoot, templatesRoot, Path.GetFileName(tf), replacements);
                    if(!string.IsNullOrEmpty(outFile))
                    {
                        resultFiles.Add(outFile);
                    }
                }
            }

            return resultFiles;
        }

        private string ComposeFile(string outRoot, string templatesRoot, string templateFile, IDictionary<string, string> replacements)
        {
            string resultFile = null;
            string templatePath = Path.Combine(templatesRoot, templateFile);
            if (File.Exists(templatePath))
            {
                string fileName = templateFile.Replace("Entity", replacements["Entity"]);
                resultFile = Path.Combine(outRoot, fileName);
                string content = File.ReadAllText(templatePath);

                foreach(var k in replacements.Keys)
                {
                    content = content.Replace("{"+k+"}", replacements[k]);
                }

                File.WriteAllText(resultFile, content);
            }

            return resultFile;
        }


        private string GenerateUpserInsertValuesList(DataTable table)
        {
            StringBuilder result = new StringBuilder();
            for (int i = 0; i < table.Columns.Count; ++i)
            {
                var c = table.Columns[i];
                if (!c.IsIdentity)
                {
                    result.Append($"@{c.Name}");
                    if (i + 1 < table.Columns.Count) result.Append(",\n\t\t");
                }
            }

            return result.ToString();
        }

        private string GenerateUpserUpdateValuesList(DataTable table)
        {
            StringBuilder result = new StringBuilder();
            for (int i = 0; i < table.Columns.Count; ++i)
            {
                var c = table.Columns[i];
                if (!c.IsIdentity)
                {
                    result.Append($"[{c.Name}] = IIF( @{c.Name} IS NOT NULL, @{c.Name}, [{c.Name}] )");
                    if (i + 1 < table.Columns.Count) result.Append(",\n\t\t");
                }
            }

            return result.ToString();
        }

        private string GenerateWhereUpsertList(DataTable table)
        {
            StringBuilder result = new StringBuilder();
            for (int i = 0; i < table.Columns.Count; ++i)
            {
                var c = table.Columns[i];
                if (!c.IsIdentity)
                {
                    result.Append($"[{c.Name}] = IIF( @{c.Name} IS NOT NULL, @{c.Name}, [{c.Name}] )");
                    if (i + 1 < table.Columns.Count) result.Append(" AND \r\t\t");
                }
            }

            return result.ToString();
        }

        private string GenerateParamsUpsertList(DataTable table)
        {
            StringBuilder result = new StringBuilder();
            for (int i = 0; i < table.Columns.Count; ++i)
            {
                var c = table.Columns[i];
                result.Append($"@{c.Name} {c.SqlType.ToUpper()}");
                if (c.CharMaxLength != null) result.Append($"({c.CharMaxLength})");
                if (c.Precision != null && c.Scale != null) result.Append($"({c.Precision}, {c.Scale})");
                if (i + 1 < table.Columns.Count) result.Append(",\n\t");
            }

            return result.ToString();
        }

        private string GenerateParamsPKList(DataTable table, IList<DataColumn> pkColumns)
        {
            StringBuilder result = new StringBuilder();
            for(int i = 0; i < pkColumns.Count; ++i)
            {
                var c = table.Columns[i];
                result.Append( $"@{c.Name} {c.SqlType.ToUpper()}");
                if (c.CharMaxLength != null) result.Append($"({c.CharMaxLength})");
                if (c.Precision != null && c.Scale != null) result.Append($"({c.Precision}, {c.Scale})");
                if (i + 1 < pkColumns.Count) result.Append(",\n\t");
            }

            return result.ToString();
        }

        private string GenerateWherePKList(DataTable table, IList<DataColumn> pkColumns)
        {
            StringBuilder result = new StringBuilder();
            for (int i = 0; i < pkColumns.Count; ++i)
            {
                var c = table.Columns[i];
                result.Append($"[{c.Name}] = @{c.Name}");
                if (i + 1 < pkColumns.Count) result.Append(" AND ");
            }

            return result.ToString();
        }

        private IList<DataColumn> GetPKColumns(DataTable table)
        {
            return table.Columns.Where(c => c.IsPK).ToList();
        }

        protected string GetTemplatesFolder()
        {
            return Path.Combine(_genParams.TemplatesRoot, _genParams.TemplateName, "Storprocs");
        }

        protected string GetOutputFolder()
        {
            string  outFolder = Path.Combine(_genParams.OutputRoot, _genParams.TemplateName, _genParams.Timestamp.ToString("yyyy-MM-dd HH-mm-ss"), "Storprocs");
            if(!Directory.Exists(outFolder))
            {
                Directory.CreateDirectory(outFolder);
            }

            return outFolder;
        }
    }
}
