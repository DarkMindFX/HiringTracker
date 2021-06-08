using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace HRT.DTO
{
    public class Comment : HateosDto
    {
		[JsonPropertyName("ID")]
		public System.Int64? ID { get; set; }

		[JsonPropertyName("Text")]
		public System.String Text { get; set; }

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
