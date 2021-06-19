using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace HRT.DTO
{
    public class CandidateUpsert
    {
        [JsonPropertyName("Candidate")]
        public Candidate Candidate { get; set; }

        [JsonPropertyName("Skills")]
        public IList<CandidateSkill> Skills { get; set; }
    }

    public class CandidateUpsertResponse
    {
        [JsonPropertyName("Candidate")]
        public Candidate Candidate
        {
            get; set;
        }
    }
}
