

using System.Text.Json.Serialization;

namespace HRT.DTO
{
    public class UserRoleSystem : HateosDto
    {
				[JsonPropertyName("UserID")]
		public System.Int64 UserID { get; set; }

				[JsonPropertyName("RoleID")]
		public System.Int64 RoleID { get; set; }

				
    }
}
