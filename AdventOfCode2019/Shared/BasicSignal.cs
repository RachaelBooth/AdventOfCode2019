using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdventOfCode2019.Shared
{
    public class BasicSignal : Signal
    {
        private List<int> inputs;
        private int nextInputIndex = 0;
        private List<int> output;

        public BasicSignal(params int[] inputValues)
        {
            output = new List<int>();
            inputs = inputValues.ToList();
        }

        public void Output(int value)
        {
            output.Add(value);
        }

        public Task<int> Input()
        {
            if (nextInputIndex > inputs.Count)
            {
                throw new Exception("Requested unexpected number of inputs");
            }
            var value = inputs[nextInputIndex];
            nextInputIndex += 1;
            return Task.FromResult(value);
        }

        public List<int> GetOutput()
        {
            return output;
        }
    }
}
