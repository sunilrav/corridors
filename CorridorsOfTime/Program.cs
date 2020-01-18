using CorridorsOfTime.Models;
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

            var hexagons = Formatter.FormatHexagons(patterns);
            //DisplayGrid(hexagons);

            Mapper.Build(hexagons);
            //DisplayConnections(hexagons);

            var edgeHexagons = hexagons.Where(x => x.IsEdge).ToList();
            DisplayPath(edgeHexagons[0]);

            Console.ReadLine();            
        }

        private static void DisplayPath(Hexagon start)
        {
            Console.Write($"{start.Name}[{start.Value}]-->");
            if (start.ConnectedTo.Count != 0)
            {
                foreach (var connected in start.ConnectedTo)
                {
                    DisplayPath(connected);
                }
            }
            else
            {
                Console.WriteLine();
            }
        }

        private static void DisplayConnections(List<Hexagon> hexagons)
        {
            foreach (var hexagon in hexagons)
            {
                Console.Write($"{hexagon.Name}[{hexagon.Value}]-->");
                foreach (var connectedHexagon in hexagon.ConnectedTo)
                {
                    Console.Write($"{connectedHexagon.Name}[{connectedHexagon.Value}],");
                }
                Console.WriteLine();
                Console.ResetColor();
            }
        }

        private static void DisplayGrid(List<Hexagon> hexagons)
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

        
    }
}
