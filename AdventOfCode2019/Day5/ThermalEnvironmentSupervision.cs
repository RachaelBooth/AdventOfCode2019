using AdventOfCode2019.Shared;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2019.Day5
{
    public class ThermalEnvironmentSupervision : Solver<int>
    {
        public ThermalEnvironmentSupervision() : base(5) { }

        public void SolvePartOne()
        {
            var computer = new IntcodeComputer(ReadInput().ToList());
            var outputCodes = new List<int>();
            static int InputProvider() => 1;
            computer.Run(InputProvider, out outputCodes);
            Console.WriteLine($"Diagnostic Code: {outputCodes.Last()}");
        }

        public void SolvePartTwo()
        {
            var computer = new IntcodeComputer(ReadInput().ToList());
            var outputCodes = new List<int>();
            static int InputProvider() => 5;
            computer.Run(InputProvider, out outputCodes);
            Console.WriteLine($"Diagnostic Code: {outputCodes.Last()}");
        }

        protected override int ParseLine(string line)
        {
            return int.Parse(line);
        }
    }
}
