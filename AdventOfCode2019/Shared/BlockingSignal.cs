using System.Collections.Generic;
using System.Threading.Tasks;

namespace AdventOfCode2019.Shared
{
    public class BlockingSignal : Signal
    {
        private readonly Queue<int> signal;

        public BlockingSignal(params int[] initialValues)
        {
            signal = new Queue<int>();
            foreach (var value in initialValues)
            {
                signal.Enqueue(value);
            }
        }

        public void Output(int value)
        {
            signal.Enqueue(value);
        }

        public async Task<int> Input()
        {
            while (true)
            {
                var dequeueSuccessful = signal.TryDequeue(out var result);
                if (dequeueSuccessful)
                {
                    return result;
                }
                await Task.Delay(100);
            }
            
        }
    }
}
