

using System.Text.Json.Serialization;

namespace HRT.DTO
{
    public class InterviewFeedback : HateosDto
    {
				[JsonPropertyName("ID")]
		public System.Int64 ID { get; set; }

				[JsonPropertyName("Comment")]
		public System.String Comment { get; set; }

				[JsonPropertyName("Rating")]
		public System.Int32 Rating { get; set; }

				[JsonPropertyName("InterviewID")]
		public System.Int64 InterviewID { get; set; }

				[JsonPropertyName("InterviewerID")]
		public System.Int64 InterviewerID { get; set; }

				[JsonPropertyName("CreatedByID")]
		public System.Int64 CreatedByID { get; set; }

				[JsonPropertyName("CreatedDate")]
		public System.DateTime CreatedDate { get; set; }

				[JsonPropertyName("ModifiedByID")]
		public System.Int64? ModifiedByID { get; set; }

				[JsonPropertyName("ModifiedDate")]
		public System.DateTime? ModifiedDate { get; set; }

				
    }
}
