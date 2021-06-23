

using System.Text.Json.Serialization;

namespace HRT.DTO
{
    public class ProposalStep : HateosDto
    {
				[JsonPropertyName("ID")]
		public System.Int64? ID { get; set; }

				[JsonPropertyName("Name")]
		public System.String Name { get; set; }

				[JsonPropertyName("ReqDueDate")]
		public System.Boolean ReqDueDate { get; set; }

				[JsonPropertyName("RequiresRespInDays")]
		public System.Int32? RequiresRespInDays { get; set; }

				
    }
}
