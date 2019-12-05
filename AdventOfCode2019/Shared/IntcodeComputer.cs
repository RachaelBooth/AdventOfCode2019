using System;
using System.Collections.Generic;

namespace AdventOfCode2019.Shared
{
    public class IntcodeComputer
    {
        private List<int> Programme;

        public IntcodeComputer(List<int> programme)
        {
            Programme = programme;
        }

        public int GetResult(int noun, int verb)
        {
            SetUpProgramme(noun, verb);
            RunProgramme();
            return Programme[0];
        }

        private void SetUpProgramme(int noun, int verb)
        {
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
    }
}
