using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace HRT.DTO
{
    public class Position : HateosDto
    {
        [JsonPropertyName("_positionId")]
        public long PositionID
        {
            get; set;
        }

        [JsonPropertyName("_title")]
        public string Title
        {
            get; set;
        }

        [JsonPropertyName("_shortDesc")]
        public string ShortDesc
        {
            get; set;
        }

        [JsonPropertyName("_desc")]
        public string Description
        {
            get; set;
        }

        [JsonPropertyName("_status")]
        public PositionStatus Status
        {
            get; set;
        }

        [JsonPropertyName("_createdDate")]
        public DateTime CreatedDate
        {
            get; set;
        }

        [JsonPropertyName("_createdBy")]
        public User CreatedBy
        {
            get; set;
        }

        [JsonPropertyName("_modifiedDate")]
        public DateTime? ModifiedDate
        {
            get; set;
        }

        [JsonPropertyName("_modifiedBy")]
        public User ModifiedBy
        {
            get; set;
        }
    }
}
