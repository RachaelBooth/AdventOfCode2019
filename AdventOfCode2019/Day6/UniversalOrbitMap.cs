using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2019.Day6
{
    public class UniversalOrbitMap : Solver<OrbitData>
    {
        public UniversalOrbitMap() : base(6) {}

        public void SolvePartOne()
        {
            var immediateOrbitData = ReadInput().ToList();
            // Puzzle constraints give exactly one of these
            var centre = immediateOrbitData.Select(d => d.Centre).First(c => !immediateOrbitData.Exists(o => o.Orbiter == c));
            var orbitCount = 0;
            var level = 1;
            var currentOuter = new List<string> {centre};
            while (currentOuter.Any())
            {
                var nextLevel = new List<string>();
                foreach (var obj in currentOuter)
                {
                    var orbiters = immediateOrbitData.Where(d => d.Centre == obj).Select(d => d.Orbiter).ToList();
                    orbitCount += orbiters.Count * level;
                    nextLevel.AddRange(orbiters);
                }

                level += 1;
                currentOuter = nextLevel;
            }
            Console.WriteLine($"Total Orbits: {orbitCount}");
        }

        public void SolvePartTwo()
        {
            var immediateOrbitData = ReadInput().ToList();
            var santaAncestors = GetAncestors("SAN", immediateOrbitData);
            var youAncestors = GetAncestors("YOU", immediateOrbitData);
            var transfers = 0;
            while (true)
            {
                if (santaAncestors.Contains(youAncestors[transfers]))
                {
                    transfers += santaAncestors.IndexOf(youAncestors[transfers]);
                    break;
                }

                transfers += 1;
            }
            Console.WriteLine($"Total transfers: {transfers}");
        }

        private List<string> GetAncestors(string obj, List<OrbitData> immediateOrbitData)
        {
            var ancestors = new List<string>();
            var current = obj;
            while (true)
            {
                var next = immediateOrbitData.FirstOrDefault(d => d.Orbiter == current);
                if (next == null)
                {
                    break;
                }

                ancestors.Add(next.Centre);
                current = next.Centre;
            }

            return ancestors;
        }

        protected override OrbitData ParseLine(string line)
        {
            return new OrbitData(line);
        }
    }
}
