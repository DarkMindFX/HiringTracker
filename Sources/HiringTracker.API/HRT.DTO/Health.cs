using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace HRT.DTO
{
    public class HealthResponse
    {
        [JsonPropertyName("_message")]
        public string Message
        {
            get;
            set;
        }
    }
}
