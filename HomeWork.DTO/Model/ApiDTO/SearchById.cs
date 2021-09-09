using System;
using System.Collections.Generic;
using System.Text;
using HomeWork.DTO.Model.ServiceDTO;

namespace HomeWork.DTO.Model.ApiDTO
{
    public class SearchById 
    {
        /// <summary>
        /// 上層名稱
        /// </summary>
        public string UpperName { get; set; }
        /// <summary>
        /// 編號
        /// </summary>
        public int? Id { get; set; }

        /// <summary>
        /// 標題
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 內容
        /// </summary>
        public string Conten { get; set; }

        /// <summary>
        /// 子集合
        /// </summary>
        public IEnumerable<SubFAQ> SubSearch { get; set; }
    }
}
