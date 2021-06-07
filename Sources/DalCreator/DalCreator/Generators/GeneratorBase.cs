using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalCreator.Generators
{
    public abstract class GeneratorBase
    {
        protected string ComposeFile(string outRoot, string templatesRoot, string templateFile, IDictionary<string, string> replacements)
        {
            string resultFile = null;
            string templatePath = Path.Combine(templatesRoot, templateFile);
            if (File.Exists(templatePath))
            {
                string fileName = templateFile.Replace("Entity", replacements["Entity"]);
                resultFile = Path.Combine(outRoot, fileName);
                string resultDir = Path.GetDirectoryName(resultFile);
                if(!Directory.Exists(resultDir))
                {
                    Directory.CreateDirectory(resultDir);
                }
                string content = File.ReadAllText(templatePath);

                foreach (var k in replacements.Keys)
                {
                    string key = k;
                    string replacement = replacements[k];
                    content = content.Replace("{" + key + "}", replacement);
                }

                File.WriteAllText(resultFile, content);
            }

            return resultFile;
        }

        protected string DbTypeToSqlDbType(DataColumn c)
        {
            string type = null;

            IDictionary<string, SqlDbType> mapping = new Dictionary<string, SqlDbType>();
            mapping.Add("int", SqlDbType.Int);
            mapping.Add("date", SqlDbType.Date);
            mapping.Add("datetime", SqlDbType.DateTime2);
            mapping.Add("bit", SqlDbType.Bit);
            mapping.Add("bigint", SqlDbType.BigInt);
            mapping.Add("float", SqlDbType.Float);
            mapping.Add("money", SqlDbType.Money);
            mapping.Add("decimal", SqlDbType.Decimal);
            mapping.Add("char", SqlDbType.Char);
            mapping.Add("nchar", SqlDbType.NChar);
            mapping.Add("varchar", SqlDbType.VarChar);
            mapping.Add("nvarchar", SqlDbType.NVarChar);
            mapping.Add("text", SqlDbType.Text);
            mapping.Add("ntext", SqlDbType.NText);
            mapping.Add("xml", SqlDbType.Xml);

            type = mapping[c.SqlType].ToString();

            return type;
        }

        protected string DbTypeToType(DataColumn c)
        {
            string result = null;
            Type type = GetColumnType(c);

            result = type.ToString();

            if (c.IsIdentity || (c.IsNullable && !type.IsClass && Nullable.GetUnderlyingType(type) == null))
            {
                result += "?";
            }

            return result;
        }

        protected Type GetColumnType(DataColumn c)
        {
            Type type = null;

            switch (c.SqlType.ToLower())
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
                case "money":
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

            return type;
        }

        protected string GenerateUpserInsertNamesList(DataTable table)
        {
            StringBuilder result = new StringBuilder();
            for (int i = 0; i < table.Columns.Count; ++i)
            {
                var c = table.Columns[i];
                if (!c.IsIdentity)
                {
                    result.Append($"[{c.Name}]");
                    if (i + 1 < table.Columns.Count) result.Append(",");
                }
            }

            return result.ToString();
        }

        protected string GenerateUpserInsertValuesList(DataTable table)
        {
            StringBuilder result = new StringBuilder();
            for (int i = 0; i < table.Columns.Count; ++i)
            {
                var c = table.Columns[i];
                if (!c.IsIdentity)
                {
                    result.Append($"@{c.Name}");
                    if (i + 1 < table.Columns.Count) result.Append(",");
                }
            }

            return result.ToString();
        }

        protected string GenerateVariableDeclaration(DataColumn c)
        {
            StringBuilder result = new StringBuilder();

            result.Append($"DECLARE @{c.Name} {c.SqlType.ToUpper()}");
            if (c.CharMaxLength != null) result.Append($"({c.CharMaxLength})");
            if (c.Precision != null && c.Scale != null) result.Append($"({c.Precision}, {c.Scale})");

            return result.ToString();
        }

        protected IList<DataColumn> GetPKColumns(DataTable table)
        {
            return table.Columns.Where(c => c.IsPK).ToList();
        }

        protected string NewUUID()
        {
            return Guid.NewGuid().ToString().Replace("-", string.Empty).Replace("{", string.Empty).Replace("}", string.Empty);
        }

    }
}
