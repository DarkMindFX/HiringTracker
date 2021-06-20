﻿// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 16.0.0.0
//  
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------
namespace T4DalGenerator.Templates
{
    using System.Linq;
    using System.Text;
    using System.Collections.Generic;
    using System;
    
    /// <summary>
    /// Class to produce the template output
    /// </summary>
    
    #line 1 "D:\Projects\Test Projects\HiringTracker\Sources\DalCreator\T4DalGenerator\Templates\SQLDalImplTemplate.tt"
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.TextTemplating", "16.0.0.0")]
    public partial class SQLDalImplTemplate : SQLDalImplTemplateBase
    {
#line hidden
        /// <summary>
        /// Create the template output
        /// </summary>
        public virtual string TransformText()
        {
            this.Write("\r\n");
            this.Write(@"
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Data;
using System.Data.SqlClient;
using HRT.Common;
using HRT.DAL.MSSQL;
using HRT.Interfaces;
using HRT.Interfaces.Entities;

namespace HRT.DAL.MSSQL 
{
    class ");
            
            #line 23 "D:\Projects\Test Projects\HiringTracker\Sources\DalCreator\T4DalGenerator\Templates\SQLDalImplTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(table.Name));
            
            #line default
            #line hidden
            this.Write("DalInitParams : InitParamsImpl\r\n    {\r\n    }\r\n\r\n    [Export(\"MSSQL\", typeof(I");
            
            #line 27 "D:\Projects\Test Projects\HiringTracker\Sources\DalCreator\T4DalGenerator\Templates\SQLDalImplTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(table.Name));
            
            #line default
            #line hidden
            this.Write("Dal))]\r\n    public class ");
            
            #line 28 "D:\Projects\Test Projects\HiringTracker\Sources\DalCreator\T4DalGenerator\Templates\SQLDalImplTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(table.Name));
            
            #line default
            #line hidden
            this.Write("Dal: SQLDal, I");
            
            #line 28 "D:\Projects\Test Projects\HiringTracker\Sources\DalCreator\T4DalGenerator\Templates\SQLDalImplTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(table.Name));
            
            #line default
            #line hidden
            this.Write("Dal\r\n    {\r\n        public IInitParams CreateInitParams()\r\n        {\r\n           " +
                    " return new ");
            
            #line 32 "D:\Projects\Test Projects\HiringTracker\Sources\DalCreator\T4DalGenerator\Templates\SQLDalImplTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(table.Name));
            
            #line default
            #line hidden
            this.Write(@"DalInitParams();
        }

        public void Init(IInitParams initParams)
        {
            InitDbConnection(initParams.Parameters[""ConnectionString""]);
        }

        public bool Delete(long id)
        {
            bool removed = base.Delete<");
            
            #line 42 "D:\Projects\Test Projects\HiringTracker\Sources\DalCreator\T4DalGenerator\Templates\SQLDalImplTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(table.Name));
            
            #line default
            #line hidden
            this.Write(">(\"p_");
            
            #line 42 "D:\Projects\Test Projects\HiringTracker\Sources\DalCreator\T4DalGenerator\Templates\SQLDalImplTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(table.Name));
            
            #line default
            #line hidden
            this.Write("_Delete\", id, \"@ID\");\r\n\r\n            return removed;\r\n        }\r\n\r\n        public" +
                    " ");
            
            #line 47 "D:\Projects\Test Projects\HiringTracker\Sources\DalCreator\T4DalGenerator\Templates\SQLDalImplTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(table.Name));
            
            #line default
            #line hidden
            this.Write(" Get(long id)\r\n        {\r\n            ");
            
            #line 49 "D:\Projects\Test Projects\HiringTracker\Sources\DalCreator\T4DalGenerator\Templates\SQLDalImplTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(table.Name));
            
            #line default
            #line hidden
            this.Write(" entityOut = base.Get<");
            
            #line 49 "D:\Projects\Test Projects\HiringTracker\Sources\DalCreator\T4DalGenerator\Templates\SQLDalImplTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(table.Name));
            
            #line default
            #line hidden
            this.Write(">(\"p_");
            
            #line 49 "D:\Projects\Test Projects\HiringTracker\Sources\DalCreator\T4DalGenerator\Templates\SQLDalImplTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(table.Name));
            
            #line default
            #line hidden
            this.Write("_GetDetails\", id, \"@ID\", ");
            
            #line 49 "D:\Projects\Test Projects\HiringTracker\Sources\DalCreator\T4DalGenerator\Templates\SQLDalImplTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(table.Name));
            
            #line default
            #line hidden
            this.Write("FromRow);\r\n\r\n            return entityOut;\r\n        }\r\n\r\n        ");
            
            #line 54 "D:\Projects\Test Projects\HiringTracker\Sources\DalCreator\T4DalGenerator\Templates\SQLDalImplTemplate.tt"

        foreach(var c in table.Columns)
        {
                if(!string.IsNullOrEmpty(c.FKRefTable))
                {
        
            
            #line default
            #line hidden
            this.Write("        public IList<");
            
            #line 60 "D:\Projects\Test Projects\HiringTracker\Sources\DalCreator\T4DalGenerator\Templates\SQLDalImplTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(table.Name));
            
            #line default
            #line hidden
            this.Write("> GetBy");
            
            #line 60 "D:\Projects\Test Projects\HiringTracker\Sources\DalCreator\T4DalGenerator\Templates\SQLDalImplTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(c.Name));
            
            #line default
            #line hidden
            this.Write("(");
            
            #line 60 "D:\Projects\Test Projects\HiringTracker\Sources\DalCreator\T4DalGenerator\Templates\SQLDalImplTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(generator.DbTypeToType(c)));
            
            #line default
            #line hidden
            this.Write(" ");
            
            #line 60 "D:\Projects\Test Projects\HiringTracker\Sources\DalCreator\T4DalGenerator\Templates\SQLDalImplTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(c.Name));
            
            #line default
            #line hidden
            this.Write(")\r\n        {\r\n            var entitiesOut = base.GetBy<");
            
            #line 62 "D:\Projects\Test Projects\HiringTracker\Sources\DalCreator\T4DalGenerator\Templates\SQLDalImplTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(table.Name));
            
            #line default
            #line hidden
            this.Write(", long>(\"p_");
            
            #line 62 "D:\Projects\Test Projects\HiringTracker\Sources\DalCreator\T4DalGenerator\Templates\SQLDalImplTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(table.Name));
            
            #line default
            #line hidden
            this.Write("_GetBy");
            
            #line 62 "D:\Projects\Test Projects\HiringTracker\Sources\DalCreator\T4DalGenerator\Templates\SQLDalImplTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(c.Name));
            
            #line default
            #line hidden
            this.Write("\", ");
            
            #line 62 "D:\Projects\Test Projects\HiringTracker\Sources\DalCreator\T4DalGenerator\Templates\SQLDalImplTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(c.Name));
            
            #line default
            #line hidden
            this.Write(", \"@");
            
            #line 62 "D:\Projects\Test Projects\HiringTracker\Sources\DalCreator\T4DalGenerator\Templates\SQLDalImplTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(c.Name));
            
            #line default
            #line hidden
            this.Write("\", SqlDbType.BigInt, 0, ");
            
            #line 62 "D:\Projects\Test Projects\HiringTracker\Sources\DalCreator\T4DalGenerator\Templates\SQLDalImplTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(table.Name));
            
            #line default
            #line hidden
            this.Write("FromRow);\r\n\r\n            return entitiesOut;\r\n        }\r\n        ");
            
            #line 66 "D:\Projects\Test Projects\HiringTracker\Sources\DalCreator\T4DalGenerator\Templates\SQLDalImplTemplate.tt"

                }
        }
        
            
            #line default
            #line hidden
            this.Write("\r\n        public IList<");
            
            #line 71 "D:\Projects\Test Projects\HiringTracker\Sources\DalCreator\T4DalGenerator\Templates\SQLDalImplTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(table.Name));
            
            #line default
            #line hidden
            this.Write("> GetAll()\r\n        {\r\n            IList<");
            
            #line 73 "D:\Projects\Test Projects\HiringTracker\Sources\DalCreator\T4DalGenerator\Templates\SQLDalImplTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(table.Name));
            
            #line default
            #line hidden
            this.Write("> result = base.GetAll<");
            
            #line 73 "D:\Projects\Test Projects\HiringTracker\Sources\DalCreator\T4DalGenerator\Templates\SQLDalImplTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(table.Name));
            
            #line default
            #line hidden
            this.Write(">(\"p_");
            
            #line 73 "D:\Projects\Test Projects\HiringTracker\Sources\DalCreator\T4DalGenerator\Templates\SQLDalImplTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(table.Name));
            
            #line default
            #line hidden
            this.Write("_GetAll\", ");
            
            #line 73 "D:\Projects\Test Projects\HiringTracker\Sources\DalCreator\T4DalGenerator\Templates\SQLDalImplTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(table.Name));
            
            #line default
            #line hidden
            this.Write("FromRow);\r\n\r\n            return result;\r\n        }\r\n\r\n        public ");
            
            #line 78 "D:\Projects\Test Projects\HiringTracker\Sources\DalCreator\T4DalGenerator\Templates\SQLDalImplTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(table.Name));
            
            #line default
            #line hidden
            this.Write(" Upsert(");
            
            #line 78 "D:\Projects\Test Projects\HiringTracker\Sources\DalCreator\T4DalGenerator\Templates\SQLDalImplTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(table.Name));
            
            #line default
            #line hidden
            this.Write(" entity) \r\n        {\r\n            ");
            
            #line 80 "D:\Projects\Test Projects\HiringTracker\Sources\DalCreator\T4DalGenerator\Templates\SQLDalImplTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(table.Name));
            
            #line default
            #line hidden
            this.Write(" entityOut = base.Upsert<");
            
            #line 80 "D:\Projects\Test Projects\HiringTracker\Sources\DalCreator\T4DalGenerator\Templates\SQLDalImplTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(table.Name));
            
            #line default
            #line hidden
            this.Write(">(\"p_");
            
            #line 80 "D:\Projects\Test Projects\HiringTracker\Sources\DalCreator\T4DalGenerator\Templates\SQLDalImplTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(table.Name));
            
            #line default
            #line hidden
            this.Write("_Upsert\", entity, AddUpsertParameters, ");
            
            #line 80 "D:\Projects\Test Projects\HiringTracker\Sources\DalCreator\T4DalGenerator\Templates\SQLDalImplTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(table.Name));
            
            #line default
            #line hidden
            this.Write("FromRow);\r\n\r\n            return entityOut;\r\n        }\r\n\r\n        protected SqlCom" +
                    "mand AddUpsertParameters(SqlCommand cmd, ");
            
            #line 85 "D:\Projects\Test Projects\HiringTracker\Sources\DalCreator\T4DalGenerator\Templates\SQLDalImplTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(table.Name));
            
            #line default
            #line hidden
            this.Write(@" entity)
        {
            SqlParameter pID = new SqlParameter(@""ID"",    SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, ""ID"", DataRowVersion.Current, (object)entity.ID != null ? (object)entity.ID : DBNull.Value);   cmd.Parameters.Add(pID); 

            SqlParameter pName = new SqlParameter(@""Name"",    SqlDbType.NVarChar, 50, ParameterDirection.Input, false, 0, 0, ""Name"", DataRowVersion.Current, (object)entity.Name != null ? (object)entity.Name : DBNull.Value);   cmd.Parameters.Add(pName); 

            return cmd;
        }

        protected ");
            
            #line 94 "D:\Projects\Test Projects\HiringTracker\Sources\DalCreator\T4DalGenerator\Templates\SQLDalImplTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(table.Name));
            
            #line default
            #line hidden
            this.Write(" ");
            
            #line 94 "D:\Projects\Test Projects\HiringTracker\Sources\DalCreator\T4DalGenerator\Templates\SQLDalImplTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(table.Name));
            
            #line default
            #line hidden
            this.Write("FromRow(DataRow row)\r\n        {\r\n            var entity = new ");
            
            #line 96 "D:\Projects\Test Projects\HiringTracker\Sources\DalCreator\T4DalGenerator\Templates\SQLDalImplTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(table.Name));
            
            #line default
            #line hidden
            this.Write("();\r\n\r\n            entity.ID = (System.Int64?)row[\"ID\"];\r\n\t\t    entity.Name = (Sy" +
                    "stem.String)row[\"Name\"];\r\n\r\n            return entity;\r\n        }\r\n        \r\n   " +
                    " }\r\n}\r\n");
            return this.GenerationEnvironment.ToString();
        }
        
        #line 1 "D:\Projects\Test Projects\HiringTracker\Sources\DalCreator\T4DalGenerator\Templates\SQLDalImplTemplate.tt"

private global::DataModel.DataTable _tableField;

/// <summary>
/// Access the table parameter of the template.
/// </summary>
private global::DataModel.DataTable table
{
    get
    {
        return this._tableField;
    }
}

private global::T4DalGenerator.ModelHelper _modelHelperField;

/// <summary>
/// Access the modelHelper parameter of the template.
/// </summary>
private global::T4DalGenerator.ModelHelper modelHelper
{
    get
    {
        return this._modelHelperField;
    }
}

private global::T4DalGenerator.Generators.GeneratorBase _generatorField;

/// <summary>
/// Access the generator parameter of the template.
/// </summary>
private global::T4DalGenerator.Generators.GeneratorBase generator
{
    get
    {
        return this._generatorField;
    }
}


/// <summary>
/// Initialize the template
/// </summary>
public virtual void Initialize()
{
    if ((this.Errors.HasErrors == false))
    {
bool tableValueAcquired = false;
if (this.Session.ContainsKey("table"))
{
    this._tableField = ((global::DataModel.DataTable)(this.Session["table"]));
    tableValueAcquired = true;
}
if ((tableValueAcquired == false))
{
    object data = global::System.Runtime.Remoting.Messaging.CallContext.LogicalGetData("table");
    if ((data != null))
    {
        this._tableField = ((global::DataModel.DataTable)(data));
    }
}
bool modelHelperValueAcquired = false;
if (this.Session.ContainsKey("modelHelper"))
{
    this._modelHelperField = ((global::T4DalGenerator.ModelHelper)(this.Session["modelHelper"]));
    modelHelperValueAcquired = true;
}
if ((modelHelperValueAcquired == false))
{
    object data = global::System.Runtime.Remoting.Messaging.CallContext.LogicalGetData("modelHelper");
    if ((data != null))
    {
        this._modelHelperField = ((global::T4DalGenerator.ModelHelper)(data));
    }
}
bool generatorValueAcquired = false;
if (this.Session.ContainsKey("generator"))
{
    this._generatorField = ((global::T4DalGenerator.Generators.GeneratorBase)(this.Session["generator"]));
    generatorValueAcquired = true;
}
if ((generatorValueAcquired == false))
{
    object data = global::System.Runtime.Remoting.Messaging.CallContext.LogicalGetData("generator");
    if ((data != null))
    {
        this._generatorField = ((global::T4DalGenerator.Generators.GeneratorBase)(data));
    }
}


    }
}


        
        #line default
        #line hidden
    }
    
    #line default
    #line hidden
    #region Base class
    /// <summary>
    /// Base class for this transformation
    /// </summary>
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.TextTemplating", "16.0.0.0")]
    public class SQLDalImplTemplateBase
    {
        #region Fields
        private global::System.Text.StringBuilder generationEnvironmentField;
        private global::System.CodeDom.Compiler.CompilerErrorCollection errorsField;
        private global::System.Collections.Generic.List<int> indentLengthsField;
        private string currentIndentField = "";
        private bool endsWithNewline;
        private global::System.Collections.Generic.IDictionary<string, object> sessionField;
        #endregion
        #region Properties
        /// <summary>
        /// The string builder that generation-time code is using to assemble generated output
        /// </summary>
        protected System.Text.StringBuilder GenerationEnvironment
        {
            get
            {
                if ((this.generationEnvironmentField == null))
                {
                    this.generationEnvironmentField = new global::System.Text.StringBuilder();
                }
                return this.generationEnvironmentField;
            }
            set
            {
                this.generationEnvironmentField = value;
            }
        }
        /// <summary>
        /// The error collection for the generation process
        /// </summary>
        public System.CodeDom.Compiler.CompilerErrorCollection Errors
        {
            get
            {
                if ((this.errorsField == null))
                {
                    this.errorsField = new global::System.CodeDom.Compiler.CompilerErrorCollection();
                }
                return this.errorsField;
            }
        }
        /// <summary>
        /// A list of the lengths of each indent that was added with PushIndent
        /// </summary>
        private System.Collections.Generic.List<int> indentLengths
        {
            get
            {
                if ((this.indentLengthsField == null))
                {
                    this.indentLengthsField = new global::System.Collections.Generic.List<int>();
                }
                return this.indentLengthsField;
            }
        }
        /// <summary>
        /// Gets the current indent we use when adding lines to the output
        /// </summary>
        public string CurrentIndent
        {
            get
            {
                return this.currentIndentField;
            }
        }
        /// <summary>
        /// Current transformation session
        /// </summary>
        public virtual global::System.Collections.Generic.IDictionary<string, object> Session
        {
            get
            {
                return this.sessionField;
            }
            set
            {
                this.sessionField = value;
            }
        }
        #endregion
        #region Transform-time helpers
        /// <summary>
        /// Write text directly into the generated output
        /// </summary>
        public void Write(string textToAppend)
        {
            if (string.IsNullOrEmpty(textToAppend))
            {
                return;
            }
            // If we're starting off, or if the previous text ended with a newline,
            // we have to append the current indent first.
            if (((this.GenerationEnvironment.Length == 0) 
                        || this.endsWithNewline))
            {
                this.GenerationEnvironment.Append(this.currentIndentField);
                this.endsWithNewline = false;
            }
            // Check if the current text ends with a newline
            if (textToAppend.EndsWith(global::System.Environment.NewLine, global::System.StringComparison.CurrentCulture))
            {
                this.endsWithNewline = true;
            }
            // This is an optimization. If the current indent is "", then we don't have to do any
            // of the more complex stuff further down.
            if ((this.currentIndentField.Length == 0))
            {
                this.GenerationEnvironment.Append(textToAppend);
                return;
            }
            // Everywhere there is a newline in the text, add an indent after it
            textToAppend = textToAppend.Replace(global::System.Environment.NewLine, (global::System.Environment.NewLine + this.currentIndentField));
            // If the text ends with a newline, then we should strip off the indent added at the very end
            // because the appropriate indent will be added when the next time Write() is called
            if (this.endsWithNewline)
            {
                this.GenerationEnvironment.Append(textToAppend, 0, (textToAppend.Length - this.currentIndentField.Length));
            }
            else
            {
                this.GenerationEnvironment.Append(textToAppend);
            }
        }
        /// <summary>
        /// Write text directly into the generated output
        /// </summary>
        public void WriteLine(string textToAppend)
        {
            this.Write(textToAppend);
            this.GenerationEnvironment.AppendLine();
            this.endsWithNewline = true;
        }
        /// <summary>
        /// Write formatted text directly into the generated output
        /// </summary>
        public void Write(string format, params object[] args)
        {
            this.Write(string.Format(global::System.Globalization.CultureInfo.CurrentCulture, format, args));
        }
        /// <summary>
        /// Write formatted text directly into the generated output
        /// </summary>
        public void WriteLine(string format, params object[] args)
        {
            this.WriteLine(string.Format(global::System.Globalization.CultureInfo.CurrentCulture, format, args));
        }
        /// <summary>
        /// Raise an error
        /// </summary>
        public void Error(string message)
        {
            System.CodeDom.Compiler.CompilerError error = new global::System.CodeDom.Compiler.CompilerError();
            error.ErrorText = message;
            this.Errors.Add(error);
        }
        /// <summary>
        /// Raise a warning
        /// </summary>
        public void Warning(string message)
        {
            System.CodeDom.Compiler.CompilerError error = new global::System.CodeDom.Compiler.CompilerError();
            error.ErrorText = message;
            error.IsWarning = true;
            this.Errors.Add(error);
        }
        /// <summary>
        /// Increase the indent
        /// </summary>
        public void PushIndent(string indent)
        {
            if ((indent == null))
            {
                throw new global::System.ArgumentNullException("indent");
            }
            this.currentIndentField = (this.currentIndentField + indent);
            this.indentLengths.Add(indent.Length);
        }
        /// <summary>
        /// Remove the last indent that was added with PushIndent
        /// </summary>
        public string PopIndent()
        {
            string returnValue = "";
            if ((this.indentLengths.Count > 0))
            {
                int indentLength = this.indentLengths[(this.indentLengths.Count - 1)];
                this.indentLengths.RemoveAt((this.indentLengths.Count - 1));
                if ((indentLength > 0))
                {
                    returnValue = this.currentIndentField.Substring((this.currentIndentField.Length - indentLength));
                    this.currentIndentField = this.currentIndentField.Remove((this.currentIndentField.Length - indentLength));
                }
            }
            return returnValue;
        }
        /// <summary>
        /// Remove any indentation
        /// </summary>
        public void ClearIndent()
        {
            this.indentLengths.Clear();
            this.currentIndentField = "";
        }
        #endregion
        #region ToString Helpers
        /// <summary>
        /// Utility class to produce culture-oriented representation of an object as a string.
        /// </summary>
        public class ToStringInstanceHelper
        {
            private System.IFormatProvider formatProviderField  = global::System.Globalization.CultureInfo.InvariantCulture;
            /// <summary>
            /// Gets or sets format provider to be used by ToStringWithCulture method.
            /// </summary>
            public System.IFormatProvider FormatProvider
            {
                get
                {
                    return this.formatProviderField ;
                }
                set
                {
                    if ((value != null))
                    {
                        this.formatProviderField  = value;
                    }
                }
            }
            /// <summary>
            /// This is called from the compile/run appdomain to convert objects within an expression block to a string
            /// </summary>
            public string ToStringWithCulture(object objectToConvert)
            {
                if ((objectToConvert == null))
                {
                    throw new global::System.ArgumentNullException("objectToConvert");
                }
                System.Type t = objectToConvert.GetType();
                System.Reflection.MethodInfo method = t.GetMethod("ToString", new System.Type[] {
                            typeof(System.IFormatProvider)});
                if ((method == null))
                {
                    return objectToConvert.ToString();
                }
                else
                {
                    return ((string)(method.Invoke(objectToConvert, new object[] {
                                this.formatProviderField })));
                }
            }
        }
        private ToStringInstanceHelper toStringHelperField = new ToStringInstanceHelper();
        /// <summary>
        /// Helper to produce culture-oriented representation of an object as a string
        /// </summary>
        public ToStringInstanceHelper ToStringHelper
        {
            get
            {
                return this.toStringHelperField;
            }
        }
        #endregion
    }
    #endregion
}
