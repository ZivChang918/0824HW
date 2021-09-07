using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace HomeWork.DTO.Model.ApiDTO
{
    public class Paged
    {
        private int _nowPage;
        private int _pageSize = 10;

        /// <summary>
        /// 頁面大小
        /// </summary>
        [JsonPropertyName("pagesize")]
        public int PageSize
        {
            get => _pageSize <= 0 ? 1 : _pageSize;
            set => _pageSize = value;
        }

        /// <summary>
        /// 現在頁面
        /// </summary>
        [JsonPropertyName("nowpage")]
        public int NowPage
        {
            get => _nowPage <= 0 ? 1 : _nowPage;
            set => _nowPage = value;
        }

        /// <summary>
        /// 排序欄位
        /// </summary>
        [JsonPropertyName("orderbyname")]
        public string OrderByName { get; set; }

        /// <summary>
        /// 排序方式
        /// </summary>
        [JsonPropertyName("orderby")]
        public string OrderBy { get; set; }

    }
}
