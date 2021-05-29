using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace HRT.DTO
{
    public class Candidate : HateosDto
    {
        [JsonPropertyName("_candidateId")]
        public long? CandidateID
        {
            get; set;
        }

        [JsonPropertyName("_fname")]
        public string FirstName
        {
            get; set;
        }

        [JsonPropertyName("_mname")]
        public string MiddleName
        {
            get; set;
        }

        [JsonPropertyName("_lname")]
        public string LastName
        {
            get; set;
        }

        [JsonPropertyName("_email")]
        public string Email
        {
            get; set;
        }

        [JsonPropertyName("_phone")]
        public string Phone
        {
            get; set;
        }

        [JsonPropertyName("_cvlink")]
        public string CVLink
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
