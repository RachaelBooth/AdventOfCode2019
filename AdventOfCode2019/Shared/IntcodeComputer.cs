using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AdventOfCode2019.Shared
{
    public class IntcodeComputer
    {
        private readonly List<long> Programme;
        private readonly Signal inputSignal;
        private readonly Signal outputSignal;
        private long relativeBase;
        private long instructionPointer;

        public IntcodeComputer(List<long> programme, Signal inputSignal, Signal outputSignal)
        {
            Programme = programme;
            this.inputSignal = inputSignal;
            this.outputSignal = outputSignal;
            relativeBase = 0;
        }

        public IntcodeComputer(List<long> programme, Signal signal)
        {
            Programme = programme;
            inputSignal = signal;
            outputSignal = signal;
            relativeBase = 0;
        }

        public IntcodeComputer(List<long> programme)
        {
            Programme = programme;
            var signal = new DummySignal();
            inputSignal = signal;
            outputSignal = signal;
            relativeBase = 0;
        }

        public void SetValues(long noun, long verb)
        {
            SetNoun(noun);
            SetVerb(verb);
        }

        public long GetZeroValue()
        {
            return Programme[0];
        }

        public void SetZeroValue(long value)
        {
            Programme[0] = value;
        }

        private void SetNoun(long noun)
        {
            Programme[1] = noun;
        }

        private void SetVerb(long verb)
        {
            Programme[2] = verb;
        }

        public async Task RunProgramme()
        {
            instructionPointer = 0;

            var running = true;
            while (running)
            {
                var instruction = GetProgrammeDataAt(instructionPointer);
                var opcode = instruction % 100;
                long value;
                long outputPosition;
                switch (opcode)
                {
                    case 99:
                        // Halt
                        running = false;
                        break;
                    case 1:
                        value = ParameterValue(1, instruction) + ParameterValue(2, instruction);
                        outputPosition = OutputPosition(3, instruction);
                        SetProgrammeDataAt(outputPosition, value);
                        instructionPointer += 4;
                        break;
                    case 2:
                        value = ParameterValue(1, instruction) * ParameterValue(2, instruction);
                        outputPosition = OutputPosition(3, instruction);
                        SetProgrammeDataAt(outputPosition, value);
                        instructionPointer += 4;
                        break;
                    case 3:
                        value = await inputSignal.Input();
                        outputPosition = OutputPosition(1, instruction);
                        SetProgrammeDataAt(outputPosition, value);
                        instructionPointer += 2;
                        break;
                    case 4:
                        value = ParameterValue(1, instruction);
                        outputSignal.Output(value);
                        instructionPointer += 2;
                        break;
                    case 5:
                        if (ParameterValue(1, instruction) != 0)
                        {
                            var newLocation = ParameterValue(2, instruction);
                            instructionPointer = newLocation;
                        }
                        else
                        {
                            instructionPointer += 3;
                        }
                        break;
                    case 6:
                        if (ParameterValue(1, instruction) == 0)
                        {
                            var newLocation = ParameterValue(2, instruction);
                            instructionPointer = newLocation;
                        }
                        else
                        {
                            instructionPointer += 3;
                        }
                        break;
                    case 7:
                        outputPosition = OutputPosition(3, instruction);
                        if (ParameterValue(1, instruction) < ParameterValue(2, instruction))
                        {
                            SetProgrammeDataAt(outputPosition, 1);
                        }
                        else
                        {
                            SetProgrammeDataAt(outputPosition, 0);
                        }
                        instructionPointer += 4;
                        break;
                    case 8:
                        outputPosition = OutputPosition(3, instruction);
                        if (ParameterValue(1, instruction) == ParameterValue(2, instruction))
                        {
                            SetProgrammeDataAt(outputPosition, 1);
                        }
                        else
                        {
                            SetProgrammeDataAt(outputPosition, 0);
                        }
                        instructionPointer += 4;
                        break;
                    case 9:
                        var adjustmentValue = ParameterValue(1, instruction);
                        relativeBase += adjustmentValue;
                        instructionPointer += 2;
                        break;
                    default:
                        throw new NotImplementedException($"Opcode {opcode} not implemented");
                }
            }
        }

        private long ParameterValue(int paramNumber, long instruction)
        {
            switch (GetParameterMode(instruction, paramNumber))
            {
                case ParameterMode.Immediate:
                    return GetProgrammeDataAt(instructionPointer + paramNumber);
                case ParameterMode.Position:
                    var position = GetProgrammeDataAt(instructionPointer + paramNumber);
                    return GetProgrammeDataAt(position);
                case ParameterMode.Relative:
                    var relativePosition = GetProgrammeDataAt(instructionPointer + paramNumber);
                    return GetProgrammeDataAt(relativePosition + relativeBase);
                default:
                    throw new Exception("Unrecognised parameter mode");
            }
        }

        private long OutputPosition(int paramNumber, long instruction)
        {
            switch (GetParameterMode(instruction, paramNumber))
            {
                case ParameterMode.Position:
                    return GetProgrammeDataAt(instructionPointer + paramNumber);
                case ParameterMode.Relative:
                    return GetProgrammeDataAt(instructionPointer + paramNumber) + relativeBase;
                default:
                    throw new Exception("Unrecognised parameter mode for output");
            }
            
        }

        private long GetProgrammeDataAt(long address)
        {
            if (address < 0)
            {
                throw new Exception("Address cannot be less than 0");
            }

            while (address >= Programme.Count)
            {
                Programme.Add(0);
            }

            return Programme[(int) address];
        }

        private void SetProgrammeDataAt(long address, long value)
        {
            if (address < 0)
            {
                throw new Exception("Address cannot be less than 0");
            }

            while (address >= Programme.Count)
            {
                Programme.Add(0);
            }

            Programme[(int) address] = value;
        }

        private static ParameterMode GetParameterMode(long instruction, int paramNumber)
        {
            return (ParameterMode) ((instruction / Math.Pow(10, 1 + paramNumber)) % 10);
        }
    }
}
