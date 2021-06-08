using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRT.Interfaces.Entities
{
    public class Comment
    {
		public System.Int64? ID { get; set; }

		public System.String Text { get; set; }

		public System.DateTime CreatedDate { get; set; }

		public System.Int64 CreatedByID { get; set; }

		public System.DateTime? ModifiedDate { get; set; }

		public System.Int64? ModifiedByID { get; set; }


    }
}
