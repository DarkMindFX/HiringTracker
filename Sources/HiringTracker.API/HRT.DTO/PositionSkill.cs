

using System.Text.Json.Serialization;

namespace HRT.DTO
{
    public class PositionSkill : HateosDto
    {
				[JsonPropertyName("ID")]
		public System.Int64? ID { get; set; }

				[JsonPropertyName("PositionID")]
		public System.Int64 PositionID { get; set; }

				[JsonPropertyName("SkillID")]
		public System.Int64 SkillID { get; set; }

				[JsonPropertyName("IsMandatory")]
		public System.Boolean IsMandatory { get; set; }

				[JsonPropertyName("SkillProficiencyID")]
		public System.Int64 SkillProficiencyID { get; set; }

				
    }
}
