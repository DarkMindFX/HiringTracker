using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace HRT.DTO
{
    public class PositionUpsert
    {
        [JsonPropertyName("Position")]
        public Position Position { get; set; }

        [JsonPropertyName("Skills")]
        public IList<PositionSkill> Skills { get; set; }
    }

    public class PositionUpsertResponse
    {
        [JsonPropertyName("PositionId")]
        public long PositionID
        {
            get; set;
        }
    }
}
