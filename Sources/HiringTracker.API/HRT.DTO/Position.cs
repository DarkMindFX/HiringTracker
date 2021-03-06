

using System.Text.Json.Serialization;

namespace HRT.DTO
{
    public class Position : HateosDto
    {
				[JsonPropertyName("ID")]
		public System.Int64? ID { get; set; }

				[JsonPropertyName("DepartmentID")]
		public System.Int64? DepartmentID { get; set; }

				[JsonPropertyName("Title")]
		public System.String Title { get; set; }

				[JsonPropertyName("ShortDesc")]
		public System.String ShortDesc { get; set; }

				[JsonPropertyName("Description")]
		public System.String Description { get; set; }

				[JsonPropertyName("StatusID")]
		public System.Int64 StatusID { get; set; }

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
