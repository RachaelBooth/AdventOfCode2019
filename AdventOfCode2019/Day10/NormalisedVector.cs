using System;

namespace AdventOfCode2019.Day10
{
    public class NormalisedVector
    {
        public readonly decimal x;
        public readonly decimal y;
        public readonly int scale;

        public NormalisedVector(int x, int y)
        {
            if (x == 0 && y == 0)
            {
                this.x = 0;
                this.y = 0;
                return;
            }

            scale = Math.Abs(x) + Math.Abs(y);
            this.x = (decimal) x / (decimal) scale;
            this.y = (decimal) y / (decimal) scale;
        }
    }
}
