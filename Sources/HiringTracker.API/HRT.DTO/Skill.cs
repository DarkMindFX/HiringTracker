
using System.Text.Json.Serialization;

namespace HRT.DTO
{
    public class Skill : HateosDto
    {
        [JsonPropertyName("_skillId")]
        public long SkillID
        {
            get; set;
        }

        [JsonPropertyName("_name")]
        public long Name
        {
            get; set;
        }
    }
}
