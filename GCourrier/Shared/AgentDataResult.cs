using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GCourrier.Shared
{
    public class AgentDataResult
    {
        public IEnumerable<Agent> Agent { get; set; }
        public int Count { get; set; }
    }
}
