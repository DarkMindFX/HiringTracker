using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace HRT.DTO
{
    public class HealthResponse
    {
        [JsonPropertyName("Message")]
        public string Message
        {
            get;
            set;
        }
    }
}
