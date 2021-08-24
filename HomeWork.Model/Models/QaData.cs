using System;
using System.Collections.Generic;

namespace HomeWork.Model.Models
{
    public partial class QaData
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
