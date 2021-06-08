using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRT.Interfaces.Entities
{
    public class Candidate
    {
		public System.Int64? ID { get; set; }

		public System.String FirstName { get; set; }

		public System.String MiddleName { get; set; }

		public System.String LastName { get; set; }

		public System.String Email { get; set; }

		public System.String Phone { get; set; }

		public System.String CVLink { get; set; }

		public System.Int64 CreatedByID { get; set; }

		public System.DateTime CreatedDate { get; set; }

		public System.Int64? ModifiedByID { get; set; }

		public System.DateTime? ModifiedDate { get; set; }


    }
}
