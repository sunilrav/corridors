using System.Collections.Generic;

namespace CorridorsOfTime.Models
{
    public class Hexagon
    {
        public string Name { get; set; }
        public string Value { get; set; }
        public List<Node> Nodes { get; set; }
        public bool IsEdge { get; set; }
    }

    public class Node
    {
        public string Value { get; set; }
        public bool IsEdgeNode { get; set; }
        public bool HasOpenWall { get; internal set; }
    }
}
