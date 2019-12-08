using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AdventOfCode2019.Shared
{
    public class BasicSignal : Signal
    {
        private Func<int> inputProvider;
        private List<int> output;

        public BasicSignal(Func<int> inputProvider)
        {
            output = new List<int>();
            this.inputProvider = inputProvider;
        }

        public void Output(int value)
        {
            output.Add(value);
        }

        public Task<int> Input()
        {
            return Task.FromResult(inputProvider());
        }

        public List<int> GetOutput()
        {
            return output;
        }
    }
}
