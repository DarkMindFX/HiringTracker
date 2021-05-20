using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace HRT.DTO
{
    public class HateosDto
    {
        public HateosDto()
        {
            Links = new List<Link>();
        }

        [JsonPropertyName("links")]
        public List<Link> Links
        {
            get;
            set;
        }
    }
}
