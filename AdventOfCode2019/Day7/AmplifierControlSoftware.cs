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
            var outputA = GetOutput(phaseSettings[0], 0).Result;
            var outputB = GetOutput(phaseSettings[1], outputA).Result;
            var outputC = GetOutput(phaseSettings[2], outputB).Result;
            var outputD = GetOutput(phaseSettings[3], outputC).Result;
            var outputE = GetOutput(phaseSettings[4], outputD).Result;
            return outputE;
        }

        private async Task<int> GetOutput(int phaseSetting, int input)
        {
            var signal = new BasicSignal(phaseSetting, input);
            var amplifier = new IntcodeComputer(originalProgramme, signal);
            await amplifier.RunProgramme();
            return signal.GetOutput().First();
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
            var amplifierA = new IntcodeComputer(originalProgramme, signalE, signalA);
            var amplifierB = new IntcodeComputer(originalProgramme, signalA, signalB);
            var amplifierC = new IntcodeComputer(originalProgramme, signalB, signalC);
            var amplifierD = new IntcodeComputer(originalProgramme, signalC, signalD);
            var amplifierE = new IntcodeComputer(originalProgramme, signalD, signalE);
            await Task.WhenAll(
                amplifierA.RunProgramme(), 
                amplifierB.RunProgramme(),
                amplifierC.RunProgramme(),
                amplifierD.RunProgramme(),
                amplifierE.RunProgramme()
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
