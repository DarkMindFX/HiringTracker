using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace HRT.DTO
{
    public class Candidate : HateosDto
	{
		[JsonPropertyName("ID")]
		public System.Int64? ID { get; set; }

		[JsonPropertyName("FirstName")]
		public System.String FirstName { get; set; }

		[JsonPropertyName("MiddleName")]
		public System.String MiddleName { get; set; }

		[JsonPropertyName("LastName")]
		public System.String LastName { get; set; }

		[JsonPropertyName("Email")]
		public System.String Email { get; set; }

		[JsonPropertyName("Phone")]
		public System.String Phone { get; set; }

		[JsonPropertyName("CVLink")]
		public System.String CVLink { get; set; }

		[JsonPropertyName("CreatedByID")]
		public User CreatedByID { get; set; }

		[JsonPropertyName("CreatedDate")]
		public System.DateTime CreatedDate { get; set; }

		[JsonPropertyName("ModifiedByID")]
		public User ModifiedByID { get; set; }

		[JsonPropertyName("ModifiedDate")]
		public System.DateTime? ModifiedDate { get; set; }


    }
}
