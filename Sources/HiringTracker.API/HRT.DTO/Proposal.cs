using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace HRT.DTO
{
    public class Proposal : HateosDto
    {
		[JsonPropertyName("ID")]
		public System.Int64? ID { get; set; }

		[JsonPropertyName("Position")]
		public Position Position { get; set; }

		[JsonPropertyName("Candidate")]
		public Candidate Candidate { get; set; }

		[JsonPropertyName("Proposed")]
		public System.DateTime Proposed { get; set; }

		[JsonPropertyName("CurrentStep")]
		public ProposalStep CurrentStep { get; set; }

		[JsonPropertyName("StepSetDate")]
		public System.DateTime StepSetDate { get; set; }

		[JsonPropertyName("NextStep")]
		public ProposalStep NextStep { get; set; }

		[JsonPropertyName("DueDate")]
		public System.DateTime? DueDate { get; set; }

		[JsonPropertyName("Status")]
		public ProposalStatus Status { get; set; }

		[JsonPropertyName("CreatedBy")]
		public User CreatedBy { get; set; }

		[JsonPropertyName("CreatedDate")]
		public System.DateTime? CreatedDate { get; set; }

		[JsonPropertyName("ModifiedBy")]
		public User ModifiedBy { get; set; }

		[JsonPropertyName("ModifiedDate")]
		public System.DateTime? ModifiedDate { get; set; }


    }
}
