using AdventOfCode2019.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2019.Day17
{
    public class ScaffoldingControl : Solver<long>
    {
        public ScaffoldingControl() : base(17) { }

        public void SolvePartOne()
        {
            var output = GetProgrammeOutput().Result;
            var res = new StringBuilder();
            while (output.TryDequeue(out var result))
            {
                res.Append(result);
            }
            Console.Write(res.ToString());
        }

        private async Task<Queue<char>> GetProgrammeOutput()
        {
            var signal = new AsciiSignal();
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
