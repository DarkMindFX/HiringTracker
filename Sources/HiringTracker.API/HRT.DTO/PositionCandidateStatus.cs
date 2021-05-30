using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace HRT.DTO
{
    public class PositionCandidateStatus : HateosDto
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
