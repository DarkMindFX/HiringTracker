using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace HRT.DTO
{
    public class PositionCandidateStep : HateosDto
    {
        [JsonPropertyName("_stepId")]
        public long StepID
        {
            get; set;
        }

        [JsonPropertyName("_name")]
        public string Name
        {
            get; set;
        }

        [JsonPropertyName("_reqDueDate")]
        public bool ReqDueDate
        {
            get; set;
        }

        [JsonPropertyName("_requiresRespInDays")]
        public int RequiresRespInDays
        {
            get; set;
        }
    }
}
