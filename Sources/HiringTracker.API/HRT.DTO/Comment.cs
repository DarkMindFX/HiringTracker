

using System.Text.Json.Serialization;

namespace HRT.DTO
{
    public class Comment : HateosDto
    {
				[JsonPropertyName("ID")]
		public System.Int64? ID { get; set; }

				[JsonPropertyName("Text")]
		public System.String Text { get; set; }

				[JsonPropertyName("CreatedDate")]
		public System.DateTime CreatedDate { get; set; }

				[JsonPropertyName("CreatedByID")]
		public System.Int64 CreatedByID { get; set; }

				[JsonPropertyName("ModifiedDate")]
		public System.DateTime? ModifiedDate { get; set; }

				[JsonPropertyName("ModifiedByID")]
		public System.Int64? ModifiedByID { get; set; }

				
    }
}
