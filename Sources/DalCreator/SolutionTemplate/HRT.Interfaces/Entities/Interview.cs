

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRT.Interfaces.Entities
{
    public class Interview 
    {
				public System.Int64? ID { get; set; }

				public System.Int64 ProposalID { get; set; }

				public System.Int64 InterviewTypeID { get; set; }

				public System.DateTime StartTime { get; set; }

				public System.DateTime EndTime { get; set; }

				public System.Int64 InterviewStatusID { get; set; }

				public System.Int64 CreatedByID { get; set; }

				public System.DateTime CretedDate { get; set; }

				public System.Int64? ModifiedByID { get; set; }

				public System.DateTime? ModifiedDate { get; set; }

				
    }
}
