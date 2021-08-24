using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HomeWork.DTO.Model.ServiceDTO
{
    public class AddFAQ :ShareFAQ
    {
        /// <summary>
        /// 標題
        /// </summary>
        [Required]
        public string Title { get; set; }
        /// <summary>
        /// 開始時間
        /// </summary>
        [Required]
        public DateTime StarTime { get; set; }
        /// <summary>
        /// 結束時間
        /// </summary>
        public DateTime? EndTime { get; set; }

    }
}
