using System;
using System.Collections.Generic;
using System.Text;
using AdventOfCode2019.Shared;

namespace AdventOfCode2019.Day18
{
    public class TunnelLocation : Coordinate
    {
        public List<string> KeysCollected;

        public TunnelLocation(int x, int y, List<string> keys = null) : base(x, y)
        {
            KeysCollected = keys ?? new List<string>();
        }
    }
}
