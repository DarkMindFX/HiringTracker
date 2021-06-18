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

		[JsonPropertyName("StatusID")]
		public PositionStatus StatusID { get; set; }

		[JsonPropertyName("CreatedDate")]
		public System.DateTime CreatedDate { get; set; }

		[JsonPropertyName("CreatedByID")]
		public User CreatedByID { get; set; }

		[JsonPropertyName("ModifiedDate")]
		public System.DateTime? ModifiedDate { get; set; }

		[JsonPropertyName("ModifiedByID")]
		public User ModifiedByID { get; set; }


    }
}
