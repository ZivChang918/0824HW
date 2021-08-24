using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HomeWork.DTO.Model.ServiceDTO
{
    public class FAQ :ShareFAQ
    {
        /// <summary>
        /// 編號
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// 上層名稱
        /// </summary>
        public string UpperName { get; set; }
        /// <summary>
        /// 開始時間
        /// </summary>
        public DateTime StarTime { get; set; }
        /// <summary>
        /// 結束時間
        /// </summary>
        public DateTime? EndTime { get; set; }

    }
}
