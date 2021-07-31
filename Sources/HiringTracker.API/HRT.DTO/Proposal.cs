

using System.Text.Json.Serialization;

namespace HRT.DTO
{
    public class Proposal : HateosDto
    {
        [JsonPropertyName("ID")]
        public System.Int64? ID { get; set; }

        [JsonPropertyName("PositionID")]
        public System.Int64 PositionID { get; set; }

        [JsonPropertyName("CandidateID")]
        public System.Int64 CandidateID { get; set; }

        [JsonPropertyName("Proposed")]
        public System.DateTime Proposed { get; set; }

        [JsonPropertyName("CurrentStepID")]
        public System.Int64 CurrentStepID { get; set; }

        [JsonPropertyName("StepSetDate")]
        public System.DateTime StepSetDate { get; set; }

        [JsonPropertyName("NextStepID")]
        public System.Int64? NextStepID { get; set; }

        [JsonPropertyName("DueDate")]
        public System.DateTime? DueDate { get; set; }

        [JsonPropertyName("StatusID")]
        public System.Int64 StatusID { get; set; }

        [JsonPropertyName("CreatedByID")]
        public System.Int64? CreatedByID { get; set; }

        [JsonPropertyName("CreatedDate")]
        public System.DateTime? CreatedDate { get; set; }

        [JsonPropertyName("ModifiedByID")]
        public System.Int64? ModifiedByID { get; set; }

        [JsonPropertyName("ModifiedDate")]
        public System.DateTime? ModifiedDate { get; set; }


    }
}
