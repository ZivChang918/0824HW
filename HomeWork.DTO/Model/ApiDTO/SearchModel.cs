using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace HomeWork.DTO.Model.ApiDTO
{
    public class SearchModel : Paged
    {
        /// <summary>
        /// 關鍵字
        /// </summary>
        [JsonPropertyName("keyword")]
        public string KeyWord { get; set; }
        /// <summary>
        /// 上架時間
        /// </summary>
        [JsonPropertyName("startOn")]
        public DateTime? StartOn { get; set; }
        /// <summary>
        /// 下架時間
        /// </summary>
        [JsonPropertyName("endOn")]
        public DateTime? EndOn { get; set; }
    }
}
