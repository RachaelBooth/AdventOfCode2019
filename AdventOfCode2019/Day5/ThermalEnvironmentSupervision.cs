using AdventOfCode2019.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdventOfCode2019.Day5
{
    public class ThermalEnvironmentSupervision : Solver<long>
    {
        public ThermalEnvironmentSupervision() : base(5) { }

        public void SolvePartOne()
        {
            var outputCodes = GetOutputCodes(1).Result;
            Console.WriteLine($"Diagnostic Code: {outputCodes.Last()}");
        }

        public void SolvePartTwo()
        {
            var outputCodes = GetOutputCodes(5).Result;
            Console.WriteLine($"Diagnostic Code: {outputCodes.Last()}");
        }

        private async Task<List<long>> GetOutputCodes(int inputValue)
        {
            var signal = new BasicSignal(inputValue);
            var computer = new IntcodeComputer(ReadInput().ToList(), signal);
            await computer.RunProgramme();
            return signal.GetOutput();
        }

        protected override long ParseLine(string line)
        {
            return long.Parse(line);
        }
    }
}
