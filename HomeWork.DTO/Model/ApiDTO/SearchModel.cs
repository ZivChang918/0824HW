using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace HomeWork.DTO.Model.ApiDTO
{
    public class SearchModel : Paged
    {
        [JsonPropertyName("keyword")]
        public string KeyWord { get; set; }
        [JsonPropertyName("starton")]
        public DateTime? StartOn { get; set; }
        [JsonPropertyName("endon")]
        public DateTime? EndOn { get; set; }
    }
}
