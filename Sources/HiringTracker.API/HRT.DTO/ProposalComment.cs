

using System.Text.Json.Serialization;

namespace HRT.DTO
{
    public class ProposalComment : HateosDto
    {
				[JsonPropertyName("ProposalID")]
		public System.Int64 ProposalID { get; set; }

				[JsonPropertyName("CommentID")]
		public System.Int64 CommentID { get; set; }

				
    }
}
