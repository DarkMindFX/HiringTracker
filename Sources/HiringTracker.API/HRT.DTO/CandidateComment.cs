using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace HRT.DTO
{
    public class CandidateComment : HateosDto
    {
		[JsonPropertyName("Candidate")]
		public Candidate Candidate { get; set; }

		[JsonPropertyName("Comment")]
		public Comment Comment { get; set; }


    }
}
