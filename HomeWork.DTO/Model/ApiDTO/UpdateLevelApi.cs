using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HomeWork.DTO.Model.ApiDTO
{
    public class UpdateLevelApi 
    {
        
        /// <summary>
        /// 編號
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 上層編號
        /// </summary>
        public int? UpperId { get; set; }

        /// <summary>
        /// 排序順序
        /// </summary>
        public int Sort { get; set; }

    }
}
