using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace HRT.DTO
{
    public class CandidateUpsert
    {
        [JsonPropertyName("_candidate")]
        public Candidate Candidate { get; set; }

        [JsonPropertyName("_skills")]
        public IList<CandidateSkill> Skills { get; set; }
    }

    public class CandidateUpsertResponse
    {
        [JsonPropertyName("_candidateId")]
        public long CandidateID
        {
            get; set;
        }
    }
}
