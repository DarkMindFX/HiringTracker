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
		[JsonPropertyName("CandidateID")]
		public Candidate CandidateID { get; set; }

		[JsonPropertyName("CommentID")]
		public Comment CommentID { get; set; }


    }
}
