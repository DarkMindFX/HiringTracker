

using System.Text.Json.Serialization;

namespace HRT.DTO
{
    public class UserRolePosition : HateosDto
    {
				[JsonPropertyName("PositionID")]
		public System.Int64 PositionID { get; set; }

				[JsonPropertyName("UserID")]
		public System.Int64 UserID { get; set; }

				[JsonPropertyName("RoleID")]
		public System.Int64 RoleID { get; set; }

				
    }
}
