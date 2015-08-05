using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace aldemo.logic.Entities
{
    public class Status
    {
        public int Id { get; set; }
        public string Text { get; set; }

        public int ProjectId { get; set; }
        public virtual Project Project { get; set; }

        public int LineId { get; set; }
        public virtual Line Line { get; set; }
    }
}
