using System;
using System.Collections.Generic;
using System.Text;

namespace HomeWork.DTO.Model.ApiDTO
{
    public class PageList
    {
        /// <summary>
        /// 分頁大小
        /// </summary>
        public int PageSize{get; set;}
        /// <summary>
        /// 總筆數
        /// </summary>
        public int TotalCount { get; set; }
        /// <summary>
        /// 總頁數
        /// </summary>
        public int TotalPage { get; set; }
        /// <summary>
        /// 現在頁數
        /// </summary>
        public int NowPage { get; set; }
    }
}
