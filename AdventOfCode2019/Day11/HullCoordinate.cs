using AdventOfCode2019.Shared;

namespace AdventOfCode2019.Day11
{
    public class HullCoordinate : Coordinate
    {
        public Colour colour;
        public bool hasBeenPainted;

        public HullCoordinate(int x, int y, Colour startColour = Colour.Black) : base(x, y)
        {
            hasBeenPainted = false;
            colour = startColour;
        }

        public void Paint(Colour newColour)
        {
            hasBeenPainted = true;
            colour = newColour;
        }
    }
}
