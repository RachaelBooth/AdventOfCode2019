using System;
using AdventOfCode2019.Shared;

namespace AdventOfCode2019.Day3
{
    public class WireCoordinate : Coordinate
    {
        public int steps;

        public WireCoordinate(int x, int y, int steps = 0) : base(x, y)
        {
            this.steps = steps;
        }

        public int ManhattenDistanceFromOrigin()
        {
            return Math.Abs(x) + Math.Abs(y);
        }

        public WireCoordinate NextCoordinate(Direction direction)
        {
            return direction switch
            {
                Direction.Up => new WireCoordinate(x, y + 1, steps + 1),
                Direction.Down => new WireCoordinate(x, y - 1, steps + 1),
                Direction.Left => new WireCoordinate(x - 1, y, steps + 1),
                Direction.Right => new WireCoordinate(x + 1, y, steps + 1),
                _ => throw new Exception("Unexpected direction"),
            };
        }
    }
}
