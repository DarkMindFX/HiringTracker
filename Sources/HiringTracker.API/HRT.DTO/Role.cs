

using System.Text.Json.Serialization;

namespace HRT.DTO
{
    public class Role : HateosDto
    {
				[JsonPropertyName("ID")]
		public System.Int64? ID { get; set; }

				[JsonPropertyName("Name")]
		public System.String Name { get; set; }

				
    }
}
