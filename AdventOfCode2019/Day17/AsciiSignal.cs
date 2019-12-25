using AdventOfCode2019.Shared;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2019.Day17
{
    public class AsciiSignal : Signal
    {
        private Queue<char> output;

        public AsciiSignal()
        {
            output = new Queue<char>();
        }

        public void Output(long value)
        {
            var c = Convert.ToChar(value);
            output.Enqueue(c);
        }

        public Task<long> Input()
        {
            throw new NotImplementedException();
        }

        public Queue<char> GetOutput()
        {
            return output;
        }
    }
}
