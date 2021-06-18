using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRT.Interfaces.Entities
{
    public class ProposalStep
    {
		public System.Int64? ID { get; set; }

		public System.String Name { get; set; }

		public System.Boolean ReqDueDate { get; set; }

		public System.Int32? RequiresRespInDays { get; set; }


    }
}
