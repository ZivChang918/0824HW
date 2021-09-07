using System;
using System.Collections.Generic;
using System.Text;

namespace HomeWork.DTO.Model.ApiDTO
{
    public class PagedData<T>
    {
        /// <summary>
        /// 資料集合
        /// </summary>
        public T Datas { get; set; }
        /// <summary>
        /// 分頁資料
        /// </summary>
        public PageList Paged { get; set; }
    }
}
