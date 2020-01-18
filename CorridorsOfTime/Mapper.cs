using System.Collections.Generic;
using CorridorsOfTime.Models;

namespace CorridorsOfTime
{
    public static class Mapper
    {
        public static void Build(List<Hexagon> hexagons)
        {
            foreach (var hexagon1 in hexagons)
            {
                hexagon1.ConnectedTo = new List<Hexagon>();
                foreach (var hexagon2 in hexagons)
                {
                    if(hexagon1 == hexagon2)
                        continue;

                    var connected = IsConnected(hexagon1, hexagon2);
                    if (connected && !AlreadyConnected(hexagon1, hexagon2))
                        hexagon1.ConnectedTo.Add(hexagon2);
                }
            }
        }

        private static bool AlreadyConnected(Hexagon hexagon1, Hexagon hexagon2)
        {
            if (hexagon2.ConnectedTo == null)
                return false;

            foreach (var hex1Connected in hexagon1.ConnectedTo)
            {
                if (hexagon2 == hex1Connected)
                    return true;
            }

            foreach (var hex2Connected in hexagon2.ConnectedTo)
            {
                if (hexagon1 == hex2Connected)
                    return true;
            }

            return false;
        }

        private static bool IsConnected(Hexagon headHexagon, Hexagon regularHexagon)
        {
            foreach (var headHexagonNode in headHexagon.Nodes)
            {
                foreach (var regularHexagonNode in regularHexagon.Nodes)
                {
                    if (headHexagonNode.Value != "BBBBBBB" && regularHexagonNode.Value != "BBBBBBB")
                    {
                        if ((headHexagonNode.Value == regularHexagonNode.Value)
                            && headHexagonNode.HasOpenWall
                            && regularHexagonNode.HasOpenWall)
                        {
                            return true;
                        }
                    }
                }
            }

            return false;
        }

    }
}
