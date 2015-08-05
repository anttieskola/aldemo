using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace aldemo.logic.Entities
{
    public class Line
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<Project> Projects { get; set; }
    }
}
