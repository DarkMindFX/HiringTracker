
using System.Text.Json.Serialization;

namespace HRT.DTO
{
    public class SkillProficiency : HateosDto
    {
        [JsonPropertyName("_id")]
        public long ProficiencyID
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
