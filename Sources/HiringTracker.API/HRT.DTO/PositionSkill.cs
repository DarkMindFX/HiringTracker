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

		[JsonPropertyName("Skill")]
		public Skill Skill { get; set; }

		[JsonPropertyName("IsMandatory")]
		public System.Boolean IsMandatory { get; set; }

		[JsonPropertyName("SkillProficiency")]
		public SkillProficiency SkillProficiency { get; set; }


    }
}
