using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2019.Day2
{
    public class IntcodeComputer : Solver<int>
    {
        private List<int> Programme;

        public IntcodeComputer() : base(2) {}

        public void SolvePartOne()
        {
            SetUpProgramme(12, 2);
            var result = GetResult();
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
                    SetUpProgramme(noun, verb);
                    var result = GetResult();
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

        private int GetResult()
        {
            RunProgramme();
            return Programme[0];
        }

        private void SetUpProgramme(int noun, int verb)
        {
            Programme = ReadInput().ToList();
            SetNoun(noun);
            SetVerb(verb);
        }

        private void SetNoun(int noun)
        {
            Programme[1] = noun;
        }

        private void SetVerb(int verb)
        {
            Programme[2] = verb;
        }

        private void RunProgramme()
        {
            var instructionPointer = 0;

            while (true)
            {
                var opcode = Programme[instructionPointer];
                if (opcode == 99)
                {
                    // Halt
                    break;
                }

                var inputOnePosition = Programme[instructionPointer + 1];
                var inputTwoPosition = Programme[instructionPointer + 2];
                var outputPosition = Programme[instructionPointer + 3];

                var inputOne = Programme[inputOnePosition];
                var inputTwo = Programme[inputTwoPosition];

                Programme[outputPosition] = opcode switch
                {
                    1 => (inputOne + inputTwo),
                    2 => (inputOne * inputTwo),
                    _ => throw new Exception("Unknown opcode")
                };

                instructionPointer += 4;
            }

        }

        protected override int ParseLine(string line)
        {
            return int.Parse(line);
        }
    }
}
