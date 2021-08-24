using System;
using System.Collections.Generic;
using System.Text;

namespace HomeWork.DTO.Model.ApiDTO
{
    public class ShareFAQApi
    {
        /// <summary>
        /// 標題
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 內容
        /// </summary>
        public string Conten { get; set; }

        /// <summary>
        /// 順序
        /// </summary>
        public int Sort { get; set; }

        /// <summary>
        /// 上層編號
        /// </summary>
        public int? UpperId { get; set; }
    }
}
