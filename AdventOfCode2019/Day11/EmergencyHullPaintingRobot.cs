using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventOfCode2019.Shared;

namespace AdventOfCode2019.Day11
{
    public class EmergencyHullPaintingRobot : Solver<long>
    {
        public EmergencyHullPaintingRobot() : base(11) { }

        public void SolvePartOne()
        {
            var paintedPanels = CountPaintedPanels().Result;
            Console.WriteLine($"Painted panels: {paintedPanels}");
        }

        public void SolvePartTwo()
        {
            var panels = GetPanels(Colour.White).Result;
            var minX = panels.Min(p => p.x);
            var maxX = panels.Max(p => p.x);
            var minY = panels.Min(p => p.y);
            var maxY = panels.Max(p => p.y);

            var y = maxY + 1;
            while (y >= minY - 1)
            {
                var row = new StringBuilder();
                var x = maxX + 1;
                while (x >= minX - 1)
                {
                    var panel = panels.FirstOrDefault(p => p.x == x && p.y == y);
                    var colour = panel?.colour ?? Colour.Black;
                    switch (colour)
                    {
                        case Colour.Black:
                            row.Append("#");
                            break;
                        case Colour.White:
                            row.Append(" ");
                            break;
                        default:
                            throw new Exception("Unrecognised colour");
                    }

                    x -= 1;
                }
                Console.WriteLine(row.ToString());
                y -= 1;
            }
        }

        private async Task<int> CountPaintedPanels()
        {
            var locations = await GetPanels();
            return locations.Count(l => l.hasBeenPainted);
        }

        private async Task<List<HullCoordinate>> GetPanels(Colour initialPanelColour = Colour.Black)
        {
            var programme = ReadInput().ToList();
            var signal = new PaintingRobotSignal(initialPanelColour);
            var computer = new IntcodeComputer(programme, signal);
            await computer.RunProgramme();
            var tracker = signal.GetTracker();
            return tracker.GetVisitedLocations();
        }

        protected override long ParseLine(string line)
        {
            return long.Parse(line);
        }
    }
}
