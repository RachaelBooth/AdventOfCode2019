using System;
using System.Threading.Tasks;

namespace AdventOfCode2019.Shared
{
    public class DummySignal : Signal
    {
        public void Output(long value)
        {
            throw new Exception("Dummy signal should never be called");
        }

        public Task<long> Input()
        {
            throw new Exception("Dummy signal should never be called");
        }
    }
}
