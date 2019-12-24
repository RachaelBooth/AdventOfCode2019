using AdventOfCode2019.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode2019.Day15
{
    public class OxygenSystem : Solver<long>
    {
        public OxygenSystem() : base(15) { }

        public void ControlDroid()
        {
            var manualDroidSignal = new DroidSignal();
            var droidComputer = new IntcodeComputer(ReadInput().ToList(), manualDroidSignal);
            droidComputer.RunProgramme().RunSynchronously();
        }

        protected override long ParseLine(string line)
        {
            return long.Parse(line);
        }
    }

    public enum DroidMovementCommand
    {
        North = 1,
        South = 2,
        West = 3,
        East = 4
    }

    public enum DroidStatus
    {
        Wall = 0,
        Moved = 1,
        OxygenFound = 2
    }
}
