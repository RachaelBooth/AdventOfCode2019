using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2019.Day3
{
    public class WireLayout
    {
        public List<WireCoordinate> LocationsCovered = new List<WireCoordinate>();

        public WireLayout(string directions)
        {
            PopulateLocationsCovered(directions.Split(',').Select(d => new DirectionInstruction(d)).ToList());
        }

        private void PopulateLocationsCovered(List<DirectionInstruction> directions)
        {
            var currentLocation = new WireCoordinate(0, 0);
            foreach (var direction in directions)
            {
                var i = 0;
                while (i < direction.Steps)
                {
                    currentLocation = currentLocation.NextCoordinate(direction.Direction);
                    LocationsCovered.Add(currentLocation);
                    i += 1;
                }
            }
        }
    }
}
