using System;
using System.Linq;
using System.Threading.Tasks;
using AdventOfCode2019.Shared;

namespace AdventOfCode2019.Day9
{
    public class SensorBoost : Solver<long>
    {
        public SensorBoost() : base(9) {}

        public void SolvePartOne()
        {
            var keycode = GetFirstOutputForInput(1).Result;
            Console.WriteLine($"Keycode: {keycode}");
        }

        public void SolvePartTwo()
        {
            var coordinates = GetFirstOutputForInput(2).Result;
            Console.WriteLine($"Coordinates: {coordinates}");
        }

        private async Task<long> GetFirstOutputForInput(int input)
        {
            var programme = ReadInput().ToList();
            var signal = new BasicSignal(input);
            var computer = new IntcodeComputer(programme, signal);
            await computer.RunProgramme();
            return signal.GetOutput().First();
        }

        protected override long ParseLine(string line)
        {
            return long.Parse(line);
        }
    }
}
