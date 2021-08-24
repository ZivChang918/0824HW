using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HomeWork.Model.Models.Partial
{
    public partial class FAQDBContext
    {
        [MetadataType(typeof(QaDataMetaData))]
        public partial class QaData
        {
            
        }
        public partial class QaDataMetaData
        {
            public int Id { get; set; }
            public string Title { get; set; }
            public string Conten { get; set; }
            public int? UpperId { get; set; }
            public int Sort { get; set; }
            public DateTime StartOn { get; set; }
            public DateTime? EndOn { get; set; }
            public bool Remove { get; set; }
            public DateTime CrtTime { get; set; }
            public string CrtBy { get; set; }
            public DateTime? UpdateTime { get; set; }
            public string UpdateBy { get; set; }
        }
    }
}
