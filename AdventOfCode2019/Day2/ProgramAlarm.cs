using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode2019.Shared;

namespace AdventOfCode2019.Day2
{
    public class ProgramAlarm : Solver<int>
    {
        private List<int> initialProgramme;

        public ProgramAlarm() : base(2)
        {
            initialProgramme = ReadInput().ToList();
        }

        public void SolvePartOne()
        {
            var result = new IntcodeComputer(initialProgramme).GetResult(12, 2);
            Console.WriteLine(result);
        }

        public void SolvePartTwo()
        {
            var noun = 0;
            while (noun < 100)
            {
                var verb = 0;
                while (verb < 100)
                {
                    var result = new IntcodeComputer(initialProgramme).GetResult(noun, verb);
                    if (result == 19690720)
                    {
                        // Done
                        Console.WriteLine($"Noun: {noun}, Verb: {verb}, answer: {(100 * noun) + verb}");
                        return;
                    }

                    verb++;
                }

                noun++;
            }
            Console.WriteLine("Not found");
        }

        protected override int ParseLine(string line)
        {
            return int.Parse(line);
        }
    }
}
