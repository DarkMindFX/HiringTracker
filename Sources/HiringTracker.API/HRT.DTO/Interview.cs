

using System.Text.Json.Serialization;

namespace HRT.DTO
{
    public class Interview : HateosDto
    {
				[JsonPropertyName("ID")]
		public System.Int64? ID { get; set; }

				[JsonPropertyName("ProposalID")]
		public System.Int64 ProposalID { get; set; }

				[JsonPropertyName("InterviewTypeID")]
		public System.Int64 InterviewTypeID { get; set; }

				[JsonPropertyName("StartTime")]
		public System.DateTime StartTime { get; set; }

				[JsonPropertyName("EndTime")]
		public System.DateTime EndTime { get; set; }

				[JsonPropertyName("InterviewStatusID")]
		public System.Int64 InterviewStatusID { get; set; }

				[JsonPropertyName("CreatedByID")]
		public System.Int64 CreatedByID { get; set; }

				[JsonPropertyName("CretedDate")]
		public System.DateTime CretedDate { get; set; }

				[JsonPropertyName("ModifiedByID")]
		public System.Int64? ModifiedByID { get; set; }

				[JsonPropertyName("ModifiedDate")]
		public System.DateTime? ModifiedDate { get; set; }

				
    }
}
