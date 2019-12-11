using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdventOfCode2019.Shared
{
    public class BasicSignal : Signal
    {
        private List<long> inputs;
        private int nextInputIndex = 0;
        private List<long> output;

        public BasicSignal(params long[] inputValues)
        {
            output = new List<long>();
            inputs = inputValues.ToList();
        }

        public void Output(long value)
        {
            output.Add(value);
        }

        public Task<long> Input()
        {
            if (nextInputIndex > inputs.Count)
            {
                throw new Exception("Requested unexpected number of inputs");
            }
            var value = inputs[nextInputIndex];
            nextInputIndex += 1;
            return Task.FromResult(value);
        }

        public List<long> GetOutput()
        {
            return output;
        }
    }
}
