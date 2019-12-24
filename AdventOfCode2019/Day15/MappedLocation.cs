using AdventOfCode2019.Shared;

namespace AdventOfCode2019.Day15
{
    public class MappedLocation : Coordinate
    {
        public Type type;

        public MappedLocation(int x, int y, Type type) : base (x, y)
        {
            this.type = type;
        }
    }

    public enum Type
    {
        Wall,
        Empty,
        Oxygen
    }
}
