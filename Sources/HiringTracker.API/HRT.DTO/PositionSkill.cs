using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace HRT.DTO
{
    public class PositionSkill
    {
        [JsonPropertyName("_skill")]
        public Skill Skill
        {
            get; set;
        }

        [JsonPropertyName("_proficiency")]
        public SkillProficiency Proficiency
        {
            get; set;
        }

        [JsonPropertyName("_isMandatory")]
        public bool IsMandatory
        {
            get; set;
        }
    }
}
