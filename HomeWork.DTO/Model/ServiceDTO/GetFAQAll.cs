using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace HomeWork.DTO.Model.ServiceDTO
{
    public class GetFAQAll
    {
        /// <summary>
        /// 所有資料
        /// </summary>
        public IQueryable<FAQ> AllFAQs { get; set; }
    }
}
