using AdventOfCode2019.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode2019.Day25
{
    public class Cyrostasis : Solver<long>
    {
        public Cyrostasis() : base(25) { }

        public void Run()
        {
            var signal = new InteractiveAsciiSignal();
            var computer = new IntcodeComputer(ReadInput().ToList(), signal);
            computer.RunProgramme().RunSynchronously();
        }

        protected override long ParseLine(string line)
        {
            return long.Parse(line);
        }
    }
}
