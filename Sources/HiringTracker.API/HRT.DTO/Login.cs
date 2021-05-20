using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace HRT.DTO
{
    public class LoginRequest
    {
        [JsonPropertyName("_email")]
        public string Email
        {
            get;
            set;
        }

        [JsonPropertyName("_pwd")]
        public string Password
        {
            get;
            set;
        }
    }

    public class LoginResponse
    {
        [JsonPropertyName("_user")]
        public User User
        {
            get; set;
        }

        [JsonPropertyName("_token")]
        public string Token
        {
            get; set;
        }

        [JsonPropertyName("_expires")]
        public DateTime Expires
        {
            get; set;
        }
    }
}
