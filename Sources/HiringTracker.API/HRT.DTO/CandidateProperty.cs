

using System.Text.Json.Serialization;

namespace HRT.DTO
{
    public class CandidateProperty : HateosDto
    {
				[JsonPropertyName("ID")]
		public System.Int64? ID { get; set; }

				[JsonPropertyName("Name")]
		public System.String Name { get; set; }

				[JsonPropertyName("Value")]
		public System.String Value { get; set; }

				[JsonPropertyName("CandidateID")]
		public System.Int64 CandidateID { get; set; }

				
    }
}
