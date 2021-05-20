using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace HRT.DTO
{
    public class Error
    {
        [JsonPropertyName("_message")]
        public string Message
        {
            get;
            set;
        }

        [JsonPropertyName("_code")]
        public int Code
        {
            get;
            set;
        }

    }
}
