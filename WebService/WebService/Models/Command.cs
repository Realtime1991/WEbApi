using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service1.Models
{
    public class Command
    {
        public long PlaneId { get; set; }
        public string ActionToDo { get; set; }
    }
}
