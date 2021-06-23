

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRT.Interfaces.Entities
{
    public class PositionSkill 
    {
				public System.Int64? ID { get; set; }

				public System.Int64 PositionID { get; set; }

				public System.Int64 SkillID { get; set; }

				public System.Boolean IsMandatory { get; set; }

				public System.Int64 SkillProficiencyID { get; set; }

				
    }
}
