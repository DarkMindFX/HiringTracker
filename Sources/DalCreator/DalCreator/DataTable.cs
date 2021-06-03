using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalCreator
{
    public class DataTable
    {
        public string Name { get; set; }

        public IList<DataColumn> Columns { get; set; }

        public override string ToString()
        {
            return $"[{Name}]";
        }
    }
}
