using CorridorsOfTime.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CorridorsOfTime
{
    class Program
    {
        static void Main(string[] args)
        {
            var patterns = PatternJsons.Get();

            var hexagons = FormatHexagons(patterns);

            var edgeHexagons = GetEdgeRoots(hexagons);

            Display(hexagons);

            BuildPath(hexagons, edgeHexagons);

            

            Console.ReadLine();            
        }

        private static List<Hexagon> FormatHexagons(List<string> jsons)
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

        private static void Display(List<Hexagon> hexagons)
        {
            foreach (var hexagon in hexagons)
            {
                Console.Write($"{hexagon.Name} ");
                Console.Write($"{hexagon.Value} ");
                foreach (var node in hexagon.Nodes)
                {
                    if (node.IsEdgeNode && node.HasOpenWall)
                        Console.ForegroundColor = ConsoleColor.Red;
                    else
                        Console.ForegroundColor = ConsoleColor.White;

                    Console.Write($"{node.Value} ");
                    Console.ResetColor();
                }

                if (hexagon.IsEdge)
                    Console.Write("<---");

                Console.WriteLine();
            }
        }

        private static bool IsEdgeRoot(List<Node> nodes)
        {
            return nodes.Any(node => node.IsEdgeNode && node.HasOpenWall);
        }

        private static List<Hexagon> GetEdgeRoots(List<Hexagon> roots)
        {
            return roots.Where(x => x.IsEdge).ToList();
        }

        private static void BuildPath(List<Hexagon> hexagons, List<Hexagon> edgeRoots)
        {
            var path = new List<Hexagon>();

            var head = edgeRoots[0];
            path.Add(head);

            foreach (var hexagon in hexagons)
            {
                if(head == hexagon || !path.Any(x => x.Value == head.Value))
                    continue;
                    
                var connected = IsConnected(head, hexagon);
                if (connected)
                {
                    Console.WriteLine($"Connected: {head.Name} to {hexagon.Name}");
                    head = hexagon;

                    path.Add(head);
                }
                Console.WriteLine();
            }
          
        }

        private static bool IsConnected(Hexagon headHexagon, Hexagon regularHexagon)
        {
            Console.WriteLine($"Comparing {headHexagon.Name} and {regularHexagon.Name}");
            foreach (var headHexagonNode in headHexagon.Nodes)
            {
                foreach (var regularHexagonNode in regularHexagon.Nodes)
                {
                    if (headHexagonNode.Value == "BBBBBBB" || regularHexagonNode.Value == "BBBBBBB")
                    {
                        return false;
                    }
                    if ((headHexagonNode.Value == regularHexagonNode.Value)
                                        && headHexagonNode.HasOpenWall 
                                        && regularHexagonNode.HasOpenWall)
                    {
                        Console.WriteLine($"Head: {headHexagonNode.Value}, Tail:{regularHexagonNode.Value}");
                        return true;
                    }
                }
            }

            return false;
        }
    }
}
