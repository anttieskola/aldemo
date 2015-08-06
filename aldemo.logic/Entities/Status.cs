using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace aldemo.logic.Entities
{
    public class Status
    {
        public int id { get; set; }
        public string Text { get; set; }

        public int ProjectId { get; set; }
        [JsonIgnore]
        public virtual Project Project { get; set; }

        public int LineId { get; set; }
        [JsonIgnore]
        public virtual Line Line { get; set; }
    }
}
