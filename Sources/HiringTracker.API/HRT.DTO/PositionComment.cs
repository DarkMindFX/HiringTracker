

using System.Text.Json.Serialization;

namespace HRT.DTO
{
    public class PositionComment : HateosDto
    {
				[JsonPropertyName("PositionID")]
		public System.Int64 PositionID { get; set; }

				[JsonPropertyName("CommentID")]
		public System.Int64 CommentID { get; set; }

				
    }
}
