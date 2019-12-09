using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
            var result = GetProgrammeResult(12, 2).Result;
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
                    var result = GetProgrammeResult(noun, verb).Result;
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

        private async Task<int> GetProgrammeResult(int noun, int verb)
        {
            var computer = new IntcodeComputer(initialProgramme);
            computer.SetValues(noun, verb);
            await computer.RunProgramme();
            return computer.GetZeroValue();
        }

        protected override int ParseLine(string line)
        {
            return int.Parse(line);
        }
    }
}
