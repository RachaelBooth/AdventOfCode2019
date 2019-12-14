using AdventOfCode2019.Shared;

namespace AdventOfCode2019.Day13
{
    public class Tile : Coordinate
    {
        public TileId TileType;

        public Tile(int x, int y, TileId type) : base(x, y)
        {
            TileType = type;
        }
    }
}
