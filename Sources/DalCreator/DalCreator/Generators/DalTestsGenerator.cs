using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalCreator.Generators
{
    public class DalTestsGeneratorParams
    {
        public string TemplatesRoot { get; set; }

        public string TemplateName { get; set; }

        public string OutputRoot { get; set; }

        public DateTime Timestamp { get; set; }

        public string DalNamespace { get; set; }

        public string DalImplNamespace { get; set; }

        public string DalTestsNamespace { get; set; }

        public string ConnectionString
        {
            get; set;
        }
    }

    public class DalTestsGenerator : GeneratorBase
    {

        private readonly DalTestsGeneratorParams _genParams;

        public DalTestsGenerator(DalTestsGeneratorParams genParams)
        {
            _genParams = genParams;
        }

        public IList<string> GenerateScripts(IList<DataTable> tables)
        {
            IList<string> resultFiles = new List<string>();

            string templatesRoot = GetTemplatesFolder();
            

            foreach (var table in tables)
            {
                string getUUID = NewUUID();
                string deleteUUID = NewUUID();
                string insertUUID = NewUUID();
                string updateUUID = NewUUID();
                string updatedUUID = NewUUID();

                var pkColumns = GetPKColumns(table);

                IDictionary<string, object> testValsGet = GenerateTestValues(table, getUUID);
                IDictionary<string, object> testValsDelete = GenerateTestValues(table, deleteUUID);
                IDictionary<string, object> testValsInsert = GenerateTestValues(table, insertUUID);
                IDictionary<string, object> testValsUpdate = GenerateTestValues(table, updateUUID);
                IDictionary<string, object> testValsUpdated = GenerateTestValues(table, updatedUUID);

                string entityName = table.Name;
                string whereFieldsList = GenerateWhereFieldsList(table);
                string pksAssignmentList = GeneratePKsAssignmentList(pkColumns);
                string pksVarsList = GeneratePKsVarsList(pkColumns);
                string fieldsVariablesListGet = GenerateFieldsVariablesList(table, testValsGet);
                string fieldsVariablesListDelete = GenerateFieldsVariablesList(table, testValsDelete);
                string fieldsVariablesListInsert = GenerateFieldsVariablesList(table, testValsInsert);
                string fieldsVariablesListUpdate = GenerateFieldsVariablesList(table, testValsUpdate);
                string fieldsVariablesListUpdated = GenerateFieldsVariablesList(table, testValsUpdated);
                string insertFieldsNamesList = GenerateUpserInsertNamesList(table);
                string insertFieldsValuesList = GenerateUpserInsertValuesList(table);

                string testGetValidation = GenerateFieldsValidationList(table, testValsGet);
                string setInsertEntityValues = GenerateSetEntityFields(table, testValsInsert, "entity");
                string testInsertValidation = GenerateFieldsValidationList(table, testValsInsert);
                string setUpdateEntityValues = GenerateSetEntityFields(table, testValsUpdated, "entity");
                string testUpdateValidation = GenerateFieldsValidationList(table, testValsUpdated);

                IDictionary<string, string> replacements = new Dictionary<string, string>();
                replacements.Add("Entity", entityName);
                replacements.Add("DalTestsNamespace", _genParams.DalTestsNamespace);
                replacements.Add("DalImplNamespace", _genParams.DalImplNamespace);
                replacements.Add("DalNamespace", _genParams.DalNamespace);
                replacements.Add("WHERE_FIELDS_LIST", whereFieldsList);
                replacements.Add("PKS_ASSIGNMENT_LIST", pksAssignmentList);
                replacements.Add("PKS_VARS_LIST", pksVarsList);
                replacements.Add("INSERT_FIELDS_NAMES_LIST", insertFieldsNamesList);
                replacements.Add("INSERT_FIELDS_VALS_LIST", insertFieldsValuesList);
                replacements.Add("FIELDS_VARIABLES_LIST_GET", fieldsVariablesListGet);
                replacements.Add("FIELDS_VARIABLES_LIST_DELETE", fieldsVariablesListDelete);
                replacements.Add("FIELDS_VARIABLES_LIST_INSERT", fieldsVariablesListInsert);
                replacements.Add("FIELDS_VARIABLES_LIST_UPDATE", fieldsVariablesListUpdate);
                replacements.Add("FIELDS_VARIABLES_LIST_UPDATED", fieldsVariablesListUpdated);

                replacements.Add("TEST_GET_VALIDATION", testGetValidation);
                replacements.Add("SET_INSERT_ENTITY_VALUES", setInsertEntityValues);
                replacements.Add("TEST_INSERT_VALIDATION", testInsertValidation);
                replacements.Add("SET_UPDATE_ENTITY_VALUES", setUpdateEntityValues);
                replacements.Add("TEST_UPDATE_VALIDATION", testUpdateValidation);

                var files = Directory.GetFiles(templatesRoot, "*", SearchOption.AllDirectories).Select(s => s.Replace(templatesRoot, ""));
                string outRoot = GetOutputFolder(table.Name);
                foreach(var f in files)
                {
                    string resultFile = base.ComposeFile(outRoot, templatesRoot, f, replacements);
                    resultFiles.Add(resultFile);
                }
                
            }

            return resultFiles;
        }

        private string GenerateSetEntityFields(DataTable table, IDictionary<string, object> values, string varName)
        {
            StringBuilder result = new StringBuilder();
            foreach (var c in table.Columns)
            {
                Type columnType = GetColumnType(c);
                if (values.ContainsKey(c.Name))
                {
                    if (columnType == typeof(DateTime))
                    {
                        result.Append($"{varName}.{c.Name} = DateTime.Parse(\"{values[c.Name].ToString()}\");\r\n\t\t");
                    }
                    else if (columnType == typeof(string))
                    {
                        result.Append($"{varName}.{c.Name} = \"{values[c.Name].ToString()}\";\r\n\t\t");
                    }
                    else if(values[c.Name] != null)
                    {
                        result.Append($"{varName}.{c.Name} = {values[c.Name].ToString().ToLower()};\r\n\t\t");
                    }
                }

            }

            return result.ToString();
        }

        private string GenerateFieldsValidationList(DataTable table, IDictionary<string, object> values)
        {
            StringBuilder result = new StringBuilder();
            foreach(var c in table.Columns)
            {
                Type columnType = GetColumnType(c);
                if(c.IsIdentity)
                {
                    result.Append($"Assert.AreNotEqual(0, entity.{c.Name});\r\n\t\t");
                }
                else if(values.ContainsKey(c.Name))
                {
                    if(columnType == typeof(DateTime))
                    {
                        result.Append($"Assert.AreEqual(DateTime.Parse(\"{values[c.Name].ToString()}\"), entity.{c.Name});\r\n\t\t");
                    }
                    else if(columnType == typeof(string))
                    {
                        result.Append($"Assert.AreEqual(\"{values[c.Name].ToString()}\", entity.{c.Name});\r\n\t\t");
                    }
                    else if(values[c.Name] != null)
                    {
                        result.Append($"Assert.AreEqual({values[c.Name].ToString().ToLower()}, entity.{c.Name});\r\n\t\t");
                    }
                }
                
            }

            return result.ToString();
        }


        private IDictionary<string, object> GenerateTestValues(DataTable table, string uuid)
        {
            IDictionary<string, object> result = new Dictionary<string, object>();

            for (int i = 0; i < table.Columns.Count; ++i)
            {
                var c = table.Columns[i];
                if (!c.IsIdentity)
                {
                    result[c.Name] = GetRandomValue(c, uuid);
                }
            }

            return result;
        }

        private string GenerateFieldsVariablesList(DataTable table, IDictionary<string, object> values)
        {
            StringBuilder result = new StringBuilder();
            for (int i = 0; i < table.Columns.Count; ++i)
            {
                var c = table.Columns[i];
                string part = GenerateVariableDeclaration(c);
                result.Append(part);
                if (!c.IsIdentity && values.ContainsKey(c.Name))
                {
                    Type columnType = GetColumnType(c);
                    result.Append(" = ");
                    if (values[c.Name] != null)
                    {
                        string quote = string.Empty;
                        if(columnType == typeof(string) || columnType == typeof(DateTime))
                        {
                            quote = "'";
                        }
                        result.Append(quote + values[c.Name].ToString() + quote);
                    }
                    else
                    {
                        result.Append("NULL");
                    }
                }
                if(i+1 < table.Columns.Count)
                {
                    result.Append("\r\n");
                }
            }

            return result.ToString();
        }

        private object GetRandomValue(DataColumn c, string uuid)
        {
            object result = null;
            Type columnType = base.GetColumnType(c);

            if (!string.IsNullOrEmpty(c.FKRefTable) && !string.IsNullOrEmpty(c.FKRefColumn))
            {
                object randColValue = GetRandomTableColumnValue(c.FKRefTable, c.FKRefColumn);
                if (randColValue != null)
                {
                    result = (columnType == typeof(string)) ? $"\"{randColValue.ToString()}\"" : randColValue.ToString();
                }
            }
            else
            {
                var rnd = new Random(DateTime.Now.Millisecond);
                if (columnType == typeof(string))
                {
                    string value = $"{c.Name} {uuid}";
                    if (value.Length > c.CharMaxLength)
                    {
                        value = value.Substring(0, (int)c.CharMaxLength);
                    }
                    result = value;
                }
                else if (columnType == typeof(int) || columnType == typeof(long))
                {
                    result = rnd.Next(1, 1000 * (columnType == typeof(long) ? 1000 : 1));
                }
                else if (columnType == typeof(decimal) || columnType == typeof(float))
                {
                    result = rnd.NextDouble() * (float.MaxValue - 1);
                }
                else if (columnType == typeof(bool) )
                {
                    result = rnd.Next(1, 100) % 2 != 0;
                }
                else if(columnType == typeof(DateTime))
                {
                    result = DateTime.Now.AddDays(rnd.Next(-1000, 1000)).AddMinutes(rnd.Next(-1000, 1000));
                }
            }

            return result;
        }

        private object GetRandomTableColumnValue(string table, string column)
        {
            string sql = $"SELECT TOP 1 [{column}] FROM [dbo].[{table}] ORDER BY NEWID()";
            object result = null;

            using (var conn = new SqlConnection(_genParams.ConnectionString))
            {
                conn.Open();

                var cmd = new SqlCommand(sql);
                cmd.Connection = conn;

                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;

                da.Fill(ds);

                if(ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    result = ds.Tables[0].Rows[0][0];
                }
            }

            return result;
        }

        private string GeneratePKsVarsList(IList<DataColumn> columns)
        {
            StringBuilder result = new StringBuilder();
            for (int i = 0; i < columns.Count; ++i)
            {
                var c = columns[i];
                if (c.IsIdentity || c.IsPK)
                {
                    string part = $"@{c.Name}";
                    result.Append(part);
                    if (i + 1 < columns.Count)
                    {
                        result.Append(", ");
                    }
                }
            }

            return result.ToString();
        }

        private string GeneratePKsAssignmentList(IList<DataColumn> columns)
        {
            StringBuilder result = new StringBuilder();
            for (int i = 0; i < columns.Count; ++i)
            {
                var c = columns[i];
                if (c.IsIdentity || c.IsPK)
                {
                    string part = $"@{c.Name} = [{c.Name}]";
                    result.Append(part);
                    if (i + 1 < columns.Count)
                    {
                        result.Append(", ");
                    }
                }
            }

            return result.ToString();
        }

        private string GenerateWhereFieldsList(DataTable table)
        {
            StringBuilder result = new StringBuilder();
            for(int i = 0; i < table.Columns.Count; ++i)
            {
                var c = table.Columns[i];
                if(!c.IsIdentity)
                {
                    string part = $"(CASE WHEN @{c.Name} IS NOT NULL THEN (CASE WHEN [{c.Name}] = @{c.Name} THEN 1 ELSE 0 END) ELSE 1 END) = 1";
                    result.Append(part);
                    if(i + 1 < table.Columns.Count)
                    {
                        result.Append(" AND \r\n\t");
                    }
                }
            }

            return result.ToString();
        }

        protected string GetTemplatesFolder()
        {
            return Path.Combine(_genParams.TemplatesRoot, _genParams.TemplateName, "DalTests\\");
        }

        protected string GetOutputFolder(string entity)
        {
            string outFolder = Path.Combine(_genParams.OutputRoot, _genParams.TemplateName, _genParams.Timestamp.ToString("yyyy-MM-dd HH-mm-ss"), "DalTests", entity);
            if (!Directory.Exists(outFolder))
            {
                Directory.CreateDirectory(outFolder);
            }

            return outFolder;
        }
    }
}
