

using System.Text.Json.Serialization;

namespace HRT.DTO
{
    public class CandidateSkill : HateosDto
    {
				[JsonPropertyName("CandidateID")]
		public System.Int64 CandidateID { get; set; }

				[JsonPropertyName("SkillID")]
		public System.Int64 SkillID { get; set; }

				[JsonPropertyName("SkillProficiencyID")]
		public System.Int64 SkillProficiencyID { get; set; }

				
    }
}
