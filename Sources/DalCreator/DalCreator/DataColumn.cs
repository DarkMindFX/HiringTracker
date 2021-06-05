using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalCreator
{
    public class DataColumn
    {
        public string Name { get; set; }

        public bool IsIdentity { get; set; }

        public bool IsNullable { get; set; }

        public bool IsComputed { get; set; }

        public bool IsPK { get; set; }

        public string SqlType { get; set; }

        public int? CharMaxLength { get; set; }

        public int? Precision { get; set; }

        public int? Scale { get; set; }

        public string FKRefTable { get; set; }

        public string FKRefColumn { get; set; }

        public override string ToString()
        {
            return $"[{Name}] : {SqlType + (!string.IsNullOrEmpty(FKRefTable) ? $", FK -> {FKRefTable}:{FKRefColumn}"  : string.Empty)} ";
        }


    }
}
