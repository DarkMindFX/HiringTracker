

using System.Text.Json.Serialization;

namespace HRT.DTO
{
    public class User : HateosDto
    {
        [JsonPropertyName("ID")]
        public System.Int64? ID { get; set; }

        [JsonPropertyName("Login")]
        public System.String Login { get; set; }

        [JsonPropertyName("FirstName")]
        public System.String FirstName { get; set; }

        [JsonPropertyName("LastName")]
        public System.String LastName { get; set; }

        [JsonPropertyName("Email")]
        public System.String Email { get; set; }

        [JsonPropertyName("Description")]
        public System.String Description { get; set; }

        [JsonPropertyName("Password")]
        public System.String Password { get; set; }




    }
}
