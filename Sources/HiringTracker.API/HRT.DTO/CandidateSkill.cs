using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace HRT.DTO
{
    public class CandidateSkill
    {
        [JsonPropertyName("Skill")]
        public Skill Skill
        {
            get; set;
        }

        [JsonPropertyName("Proficiency")]
        public SkillProficiency Proficiency
        {
            get; set;
        }
    }
}
