

using System.Text.Json.Serialization;

namespace HRT.DTO
{
    public class InterviewRole : HateosDto
    {
				[JsonPropertyName("InterviewID")]
		public System.Int64 InterviewID { get; set; }

				[JsonPropertyName("UserID")]
		public System.Int64 UserID { get; set; }

				[JsonPropertyName("RoleID")]
		public System.Int64 RoleID { get; set; }

				
    }
}
