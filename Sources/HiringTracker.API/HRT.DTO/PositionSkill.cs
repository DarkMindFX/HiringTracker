using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace HRT.DTO
{
    public class PositionSkill : HateosDto
    {
		[JsonPropertyName("PositionID")]
		public Position PositionID { get; set; }

		[JsonPropertyName("SkillID")]
		public Skill SkillID { get; set; }

		[JsonPropertyName("IsMandatory")]
		public System.Boolean IsMandatory { get; set; }

		[JsonPropertyName("SkillProficiencyID")]
		public SkillProficiency SkillProficiencyID { get; set; }


    }
}
