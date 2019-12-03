using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2019.Day3
{
    public class WireFinder : Solver<WireLayout>
    {
        private readonly LocationOnlyComparer LocationOnlyComparer = new LocationOnlyComparer();

        public WireFinder() : base(3) { }

        public void SolvePartOne()
        {
            var wires = ReadInput().ToList();
            var intersections = wires[0].LocationsCovered.Intersect(wires[1].LocationsCovered, LocationOnlyComparer);
            var minimum = intersections.Min(i => i.ManhattenDistanceFromOrigin());
            Console.WriteLine($"Minimum distance: {minimum}");
        }

        public void SolvePartTwo()
        {
            var wires = ReadInput().ToList();
            var intersections = wires[0].LocationsCovered.Intersect(wires[1].LocationsCovered, LocationOnlyComparer);
            var stepDistances = intersections.Select(i => MinStepsToIntersection(wires[0], i) + MinStepsToIntersection(wires[1], i));
            var minimum = stepDistances.Min();
            Console.WriteLine($"Minimum steps: {minimum}");
        }

        private int MinStepsToIntersection(WireLayout wireLayout, WireCoordinate intersection)
        {
            return wireLayout.LocationsCovered.Where(l => LocationOnlyComparer.Equals(l, intersection)).Min(c => c.steps);
        }

        protected override WireLayout ParseLine(string line)
        {
            return new WireLayout(line);
        }
    }
}
