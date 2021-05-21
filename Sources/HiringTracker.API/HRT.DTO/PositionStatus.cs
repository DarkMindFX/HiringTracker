
using System.Text.Json.Serialization;

namespace HRT.DTO
{
    public class PositionStatus : HateosDto
    {
        [JsonPropertyName("_statusId")]
        public long StatusID
        {
            get; set;
        }

        [JsonPropertyName("_name")]
        public string Name
        {
            get; set;
        }
    }
}
