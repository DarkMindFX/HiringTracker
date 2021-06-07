using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRT.Interfaces.Entities
{
    public class Department
    {
		public System.Int64? ID { get; set; }

		public System.String Name { get; set; }

		public System.String UUID { get; set; }

		public System.Int64? ParentID { get; set; }

		public System.Int64 ManagerID { get; set; }


    }
}
