

using System.Text.Json.Serialization;

namespace HRT.DTO
{
    public class UserRoleCandidate : HateosDto
    {
				[JsonPropertyName("CandidateID")]
		public System.Int64 CandidateID { get; set; }

				[JsonPropertyName("UserID")]
		public System.Int64 UserID { get; set; }

				[JsonPropertyName("RoleID")]
		public System.Int64 RoleID { get; set; }

				
    }
}
