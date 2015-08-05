using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace aldemo.logic.Entities
{
    public class Project
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Line>  Lines { get; set; }

        public ICollection<Status> Statuses { get; set; }
    }
}
