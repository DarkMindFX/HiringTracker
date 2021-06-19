using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRT.Interfaces.Entities
{
    public class Proposal : TrackedEntity
	{
		public System.Int64? ID { get; set; }

		public System.Int64 PositionID { get; set; }

		public System.Int64 CandidateID { get; set; }

		public System.DateTime Proposed { get; set; }

		public System.Int64 CurrentStepID { get; set; }

		public System.DateTime StepSetDate { get; set; }

		public System.Int64? NextStepID { get; set; }

		public System.DateTime? DueDate { get; set; }

		public System.Int64 StatusID { get; set; }
    }
}
