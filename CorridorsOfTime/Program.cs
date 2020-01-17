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
            var jsons = PatternJsons.Get();

            var count = 1;
            var roots = new List<Root>();
            foreach (var json in jsons)
            {
                var pattern = JsonConvert.DeserializeObject<Pattern>(json);
                var nodes = FormatNodes(pattern);
                var root = new Root
                {
                    Name = $"root{count:000000}", 
                    Value = pattern.Center,
                    Nodes = nodes, 
                    IsEdge = IsEdgeRoot(nodes)
                };
                
                roots.Add(root);
                count++;
            }           
            
            foreach (var root in roots)
            {
                Console.Write($"{root.Name} ");
                Console.Write($"{root.Value} ");
                foreach(var node in root.Nodes)
                {
                    if(node.IsEdgeNode && node.HasOpenWall)
                        Console.ForegroundColor = ConsoleColor.Red;
                    else
                        Console.ForegroundColor = ConsoleColor.White;

                    Console.Write($"{node.Value} ");
                    Console.ResetColor();
                }
                if(root.IsEdge)
                    Console.Write("<---");

                Console.WriteLine();
            }

            Console.ReadLine();            
        }

        private static List<Node> FormatNodes(Pattern pattern)
        {
            return pattern.Nodes.Select((t, i) => FormatNode(t, pattern.Walls[i])).ToList();
        }

        private static Node FormatNode(List<string> nodesStrList, bool IsOpenWall)
        {
            var node = new Node
            {
                Value = nodesStrList.Aggregate((i, j) => i + j),
                IsEdgeNode = nodesStrList.All(x => x == nodesStrList.First()),
                HasOpenWall = !IsOpenWall
            };
            return node;
        }

        private static bool IsEdgeRoot(List<Node> nodes)
        {
            return nodes.Any(node => node.IsEdgeNode && node.HasOpenWall);
        }

        
    }
}
