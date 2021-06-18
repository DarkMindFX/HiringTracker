using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRT.Interfaces.Entities
{
    public class Position
    {
		public System.Int64? ID { get; set; }

		public System.Int64? DepartmentID { get; set; }

		public System.String Title { get; set; }

		public System.String ShortDesc { get; set; }

		public System.String Description { get; set; }

		public System.Int64 StatusID { get; set; }

		public System.DateTime CreatedDate { get; set; }

		public System.Int64 CreatedByID { get; set; }

		public System.DateTime? ModifiedDate { get; set; }

		public System.Int64? ModifiedByID { get; set; }


    }
}
