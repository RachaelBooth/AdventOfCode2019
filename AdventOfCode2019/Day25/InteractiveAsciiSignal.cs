using AdventOfCode2019.Shared;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AdventOfCode2019.Day25
{
    public class InteractiveAsciiSignal : Signal
    {
        private Queue<long> ToInput;

        public InteractiveAsciiSignal()
        {
            ToInput = new Queue<long>();
        }

        public void Output(long value)
        {
            var c = Convert.ToChar(value);
            Console.Write(c);
        }

        public async Task<long> Input()
        {
            if (ToInput.TryDequeue(out var result))
            {
                return result;
            }
            var input = Console.ReadLine();
            foreach (var c in input.ToCharArray())
            {
                ToInput.Enqueue(Convert.ToInt32(c));
            }
            // Newline, not included in string returned from read line
            ToInput.Enqueue(10);
            return ToInput.Dequeue();
        }
    }
}
