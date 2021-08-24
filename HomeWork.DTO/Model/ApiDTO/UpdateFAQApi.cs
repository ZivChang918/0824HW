using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HomeWork.DTO.Model.ApiDTO
{
    public class UpdateFAQApi : AddFAQApi
    {
        
        /// <summary>
        /// 編號
        /// </summary>
        public int id { get; set; }

    }
}
