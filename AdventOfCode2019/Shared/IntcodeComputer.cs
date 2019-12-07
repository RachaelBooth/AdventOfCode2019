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

        public void Run(Func<int> inputProvider, out List<int> output)
        {
            RunProgramme(inputProvider, out output);
        }

        // QQ - nasty
        public int GetResult(int noun, int verb)
        {
            SetUpProgramme(noun, verb);
            static int dummyInputProvider() => 0;
            var output = new List<int>();
            RunProgramme(dummyInputProvider, out output);
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

        // For now, only one input
        private void RunProgramme(Func<int> inputProvider, out List<int> output)
        {
            output = new List<int>();

            var instructionPointer = 0;

            var running = true;
            while (running)
            {
                var instruction = Programme[instructionPointer];
                var opcode = instruction % 100;
                int value;
                int outputPosition;
                switch (opcode)
                {
                    case 99:
                        // Halt
                        running = false;
                        break;
                    case 1:
                        value = ParameterValue(1, instruction, instructionPointer) + ParameterValue(2, instruction, instructionPointer);
                        outputPosition = OutputPosition(3, instructionPointer);
                        Programme[outputPosition] = value;
                        instructionPointer += 4;
                        break;
                    case 2:
                        value = ParameterValue(1, instruction, instructionPointer) * ParameterValue(2, instruction, instructionPointer);
                        outputPosition = OutputPosition(3, instructionPointer);
                        Programme[outputPosition] = value;
                        instructionPointer += 4;
                        break;
                    case 3:
                        value = inputProvider();
                        outputPosition = OutputPosition(1, instructionPointer);
                        Programme[outputPosition] = value;
                        instructionPointer += 2;
                        break;
                    case 4:
                        value = ParameterValue(1, instruction, instructionPointer);
                        output.Add(value);
                        instructionPointer += 2;
                        break;
                    case 5:
                        if (ParameterValue(1, instruction, instructionPointer) != 0)
                        {
                            var newLocation = ParameterValue(2, instruction, instructionPointer);
                            instructionPointer = newLocation;
                        } else
                        {
                            instructionPointer += 3;
                        }
                        break;
                    case 6:
                        if (ParameterValue(1, instruction, instructionPointer) == 0)
                        {
                            var newLocation = ParameterValue(2, instruction, instructionPointer);
                            instructionPointer = newLocation;
                        }
                        else
                        {
                            instructionPointer += 3;
                        }
                        break;
                    case 7:
                        outputPosition = OutputPosition(3, instructionPointer);
                        if (ParameterValue(1, instruction, instructionPointer) < ParameterValue(2, instruction, instructionPointer))
                        {
                            Programme[outputPosition] = 1;
                        }
                        else
                        {
                            Programme[outputPosition] = 0;
                        }
                        instructionPointer += 4;
                        break;
                    case 8:
                        outputPosition = OutputPosition(3, instructionPointer);
                        if (ParameterValue(1, instruction, instructionPointer) == ParameterValue(2, instruction, instructionPointer))
                        {
                            Programme[outputPosition] = 1;
                        }
                        else
                        {
                            Programme[outputPosition] = 0;
                        }
                        instructionPointer += 4;
                        break;
                    default:
                        throw new NotImplementedException($"Opcode {opcode} not implemented");
                }
            }
        }

        private int ParameterValue(int paramNumber, int instruction, int instructionPointer)
        {
            switch (GetParameterMode(instruction, paramNumber))
            {
                case ParameterMode.Immediate:
                    return Programme[instructionPointer + paramNumber];
                case ParameterMode.Position:
                    var position = Programme[instructionPointer + paramNumber];
                    return Programme[position];
                default:
                    throw new Exception("Unrecognised parameter mode");
            }
        }

        private int OutputPosition(int paramNumber, int instructionPointer)
        {
            return Programme[instructionPointer + paramNumber];
        }

        private ParameterMode GetParameterMode(int instruction, int paramNumber)
        {
            return (ParameterMode) ((instruction / Math.Pow(10, 1 + paramNumber)) % 10);
        }
    }
}
