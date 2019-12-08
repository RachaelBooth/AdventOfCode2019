using AdventOfCode2019.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdventOfCode2019.Day7
{
    public class AmplifierControlSoftware : Solver<int>
    {
        private readonly List<int> originalProgramme;

        public AmplifierControlSoftware() : base(7)
        {
            originalProgramme = ReadInput().ToList();
        }

        public void SolvePartOne()
        {
            var phaseSettingValues = new[] { 0, 1, 2, 3, 4 };
            var maxThrusterSignal = int.MinValue;
            foreach (var permutation in phaseSettingValues.Permutations())
            {
                var thrusterSignal = ThrusterSignal(permutation.ToList());
                if (thrusterSignal > maxThrusterSignal)
                {
                    maxThrusterSignal = thrusterSignal;
                }
            }
            Console.WriteLine($"Max thruster signal: {maxThrusterSignal}");
        }

        private int ThrusterSignal(List<int> phaseSettings)
        {
            var outputA = GetOutput(phaseSettings[0], 0);
            var outputB = GetOutput(phaseSettings[1], outputA);
            var outputC = GetOutput(phaseSettings[2], outputB);
            var outputD = GetOutput(phaseSettings[3], outputC);
            var outputE = GetOutput(phaseSettings[4], outputD);
            return outputE;
        }

        private int GetOutput(int phaseSetting, int input)
        {
            var amplifier = new IntcodeComputer(originalProgramme);
            amplifier.Run(new MultipleInputProvider(phaseSetting, input).GetInput, out var output);
            return output.First();
        }

        public void SolvePartTwo()
        {
            // QQ - eww
            var maxThrusterSignal = GetMaxThrusterSignalFromFeedbackLoop().Result;
            Console.WriteLine($"Max thruster signal: {maxThrusterSignal}");
        }

        // QQ - This is slow, there should be a faster way
        private async Task<int> GetMaxThrusterSignalFromFeedbackLoop()
        {
            var phaseSettingValues = new[] { 5, 6, 7, 8, 9 };
            var maxThrusterSignal = int.MinValue;
            foreach (var permutation in phaseSettingValues.Permutations())
            {
                var thrusterSignal = await ThrusterSignalFromFeedbackLoop(permutation.ToList());
                if (thrusterSignal > maxThrusterSignal)
                {
                    maxThrusterSignal = thrusterSignal;
                }
            }
            return maxThrusterSignal;
        }

        private async Task<int> ThrusterSignalFromFeedbackLoop(List<int> phaseSettings)
        {
            var signalA = new WaitingSignal(phaseSettings[0]);
            var signalB = new WaitingSignal(phaseSettings[1]);
            var signalC = new WaitingSignal(phaseSettings[2]);
            var signalD = new WaitingSignal(phaseSettings[3]);
            var signalE = new WaitingSignal(phaseSettings[4], 0);
            var amplifierA = new IntcodeComputer(originalProgramme);
            var amplifierB = new IntcodeComputer(originalProgramme);
            var amplifierC = new IntcodeComputer(originalProgramme);
            var amplifierD = new IntcodeComputer(originalProgramme);
            var amplifierE = new IntcodeComputer(originalProgramme);
            await Task.WhenAll(
                amplifierA.RunProgramme(inputSignal: signalE, outputSignal: signalA), 
                amplifierB.RunProgramme(inputSignal: signalA, outputSignal: signalB),
                amplifierC.RunProgramme(inputSignal: signalB, outputSignal: signalC),
                amplifierD.RunProgramme(inputSignal: signalC, outputSignal: signalD),
                amplifierE.RunProgramme(inputSignal: signalD, outputSignal: signalE)
               );

            // This assumes that the final signal E sends has not been read - i.e. they do all work out nicely
            return await signalE.Input();
        }

        protected override int ParseLine(string line)
        {
            return int.Parse(line);
        }
    }
}
