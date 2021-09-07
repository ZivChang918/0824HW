using System;
using System.Collections.Generic;
using System.Text;

namespace HomeWork.DTO.Model.ServiceDTO
{
    public class LevelList
    {
        public int id { get; set; }

        public string Titel { get; set; }

        public string Conten { get; set; }

        public int Sort { get; set; }

        public int? UpperId { get; set; }

        public IEnumerable<LevelList> SubLevelList { get; set; }
    }
}
