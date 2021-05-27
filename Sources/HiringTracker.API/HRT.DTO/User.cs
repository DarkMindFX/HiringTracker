using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace HRT.DTO
{
    public class User : HateosDto
    {
        [JsonPropertyName("_userId")]
        public long? UserID
        {
            get; set;
        }

        [JsonPropertyName("_login")]
        public string Login
        {
            get; set;
        }

        [JsonPropertyName("_fname")]
        public string FirstName
        {
            get; set;
        }


        [JsonPropertyName("_lname")]
        public string LastName
        {
            get; set;
        }

        [JsonPropertyName("_email")]
        public string Email
        {
            get; set;
        }

        [JsonPropertyName("_desc")]
        public string Description
        {
            get; set;
        }

        [JsonPropertyName("_pwd")]
        public string Password
        {
            get; set;
        }
    }
}
