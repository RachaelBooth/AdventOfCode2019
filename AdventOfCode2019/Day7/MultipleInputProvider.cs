using System;

namespace AdventOfCode2019.Day7
{
    public class MultipleInputProvider
    {
        private int[] returnValues;
        private int nextIndex = 0;

        public MultipleInputProvider(params int[] returnValues)
        {
            this.returnValues = returnValues;
        }

        public int GetInput()
        {
            var value = returnValues[nextIndex];
            nextIndex += 1;
            return value;
        }
    }
}
