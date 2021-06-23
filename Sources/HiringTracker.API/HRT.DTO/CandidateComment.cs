

using System.Text.Json.Serialization;

namespace HRT.DTO
{
    public class CandidateComment : HateosDto
    {
				[JsonPropertyName("CandidateID")]
		public System.Int64 CandidateID { get; set; }

				[JsonPropertyName("CommentID")]
		public System.Int64 CommentID { get; set; }

				
    }
}
