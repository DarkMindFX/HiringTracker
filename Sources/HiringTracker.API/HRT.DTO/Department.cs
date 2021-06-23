

using System.Text.Json.Serialization;

namespace HRT.DTO
{
    public class Department : HateosDto
    {
				[JsonPropertyName("ID")]
		public System.Int64? ID { get; set; }

				[JsonPropertyName("Name")]
		public System.String Name { get; set; }

				[JsonPropertyName("UUID")]
		public System.String UUID { get; set; }

				[JsonPropertyName("ParentID")]
		public System.Int64? ParentID { get; set; }

				[JsonPropertyName("ManagerID")]
		public System.Int64 ManagerID { get; set; }

				
    }
}
