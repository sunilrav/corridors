using System.Collections.Generic;

namespace CorridorsOfTime.Models
{
    public class Pattern
    {
        public string Center { get; set; }
        public List<bool> Walls { get; set; }        
        public List<List<string>> Nodes { get; set; }
    }
}
