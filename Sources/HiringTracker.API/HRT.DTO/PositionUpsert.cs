using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace HRT.DTO
{
    public class PositionUpsert
    {
        [JsonPropertyName("_position")]
        public Position Position { get; set; }

        [JsonPropertyName("_skills")]
        public IList<PositionSkill> Skills { get; set; }
    }

    public class PositionUpsertResponse
    {
        [JsonPropertyName("_positionId")]
        public long PositionID
        {
            get; set;
        }
    }
}
