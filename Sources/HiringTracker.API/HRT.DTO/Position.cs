using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace HRT.DTO
{
    public class Position : HateosDto
    {
		[JsonPropertyName("ID")]
		public System.Int64? ID { get; set; }

		[JsonPropertyName("DepartmentID")]
		public System.Int64? DepartmentID { get; set; }

		[JsonPropertyName("Title")]
		public System.String Title { get; set; }

		[JsonPropertyName("ShortDesc")]
		public System.String ShortDesc { get; set; }

		[JsonPropertyName("Description")]
		public System.String Description { get; set; }

		[JsonPropertyName("Status")]
		public PositionStatus Status { get; set; }

		[JsonPropertyName("CreatedDate")]
		public System.DateTime CreatedDate { get; set; }

		[JsonPropertyName("CreatedBy")]
		public User CreatedBy { get; set; }

		[JsonPropertyName("ModifiedDate")]
		public System.DateTime? ModifiedDate { get; set; }

		[JsonPropertyName("ModifiedBy")]
		public User ModifiedBy { get; set; }


    }
}
