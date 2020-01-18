using System.Collections.Generic;
using System.Linq;
using CorridorsOfTime.Models;
using Newtonsoft.Json;

namespace CorridorsOfTime
{
    public static class Formatter
    {
        public static List<Hexagon> FormatHexagons(List<string> jsons)
        {
            var count = 1;
            var hexagons = new List<Hexagon>();
            foreach (var json in jsons)
            {
                var pattern = JsonConvert.DeserializeObject<Pattern>(json);
                var nodes = FormatNodes(pattern);
                var hexagon = new Hexagon
                {
                    Name = $"root{count:000000}",
                    Value = pattern.Center,
                    Nodes = nodes,
                    IsEdge = IsEdgeRoot(nodes)
                };

                hexagons.Add(hexagon);
                count++;
            }

            return hexagons;
        }

        private static List<Node> FormatNodes(Pattern pattern)
        {
            return pattern.Nodes.Select((t, i) => FormatNode(t, pattern.Walls[i])).ToList();
        }

        private static Node FormatNode(List<string> nodesStrList, bool isOpenWall)
        {
            var node = new Node
            {
                Value = nodesStrList.Aggregate((i, j) => i + j),
                IsEdgeNode = nodesStrList.All(x => x == nodesStrList.First()),
                HasOpenWall = !isOpenWall
            };
            return node;
        }

        private static bool IsEdgeRoot(List<Node> nodes)
        {
            return nodes.Any(node => node.IsEdgeNode && node.HasOpenWall);
        }
    }
}
