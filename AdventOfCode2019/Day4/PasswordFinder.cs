using System;

namespace AdventOfCode2019.Day4
{
    public class PasswordFinder
    {
        public void SolvePartOne()
        {
            var count = 0;
            var start = new int[] {1, 5, 8, 8, 8, 8}; // From input, first increasing value
            var current = start;
            while (IsBelowTopOfRange(current))
            {
                if (ContainsDouble(current))
                {
                    count += 1;
                }

                current = GetNextIncreasing(current);
            }
            Console.WriteLine($"{count} potential passwords");
        }

        public void SolvePartTwo()
        {
            var count = 0;
            var start = new int[] { 1, 5, 8, 8, 8, 8 }; // From input, first increasing value
            var current = start;
            while (IsBelowTopOfRange(current))
            {
                if (ContainsStrictDouble(current))
                {
                    count += 1;
                }

                current = GetNextIncreasing(current);
            }
            Console.WriteLine($"{count} potential passwords");
        }

        private bool IsBelowTopOfRange(int[] potentialPassword)
        {
            var end = new int[] { 6, 2, 4, 5, 7, 4 }; // From Input
            var i = 0;
            while (i < 6)
            {
                if (potentialPassword[i] < end[i])
                {
                    return true;
                }

                if (potentialPassword[i] > end[i])
                {
                    return false;
                }

                i += 1;
            }

            return false;
        }

        private bool ContainsDouble(int[] potentialPassword)
        {
            var i = 0;
            while (i < 5)
            {
                if (potentialPassword[i] == potentialPassword[i + 1])
                {
                    return true;
                }

                i += 1;
            }

            return false;
        }

        private bool ContainsStrictDouble(int[] potentialPassword)
        {
            // Eww
            var i = 0;
            while (i < 5)
            {
                if (potentialPassword[i] == potentialPassword[i + 1])
                {
                    if (i == 0)
                    {
                        if (potentialPassword[0] != potentialPassword[2])
                        {
                            return true;
                        }
                    }
                    else if (i < 4)
                    {
                        if (potentialPassword[i - 1] != potentialPassword[i] && potentialPassword[i + 2] != potentialPassword[i])
                        {
                            return true;
                        }
                    }
                    else
                    {
                        if (potentialPassword[3] != potentialPassword[5])
                        {
                            return true;
                        }
                    }
                }

                i += 1;
            }

            return false;
        }

        private int[] GetNextIncreasing(int[] current)
        {
            var next = (int[])current.Clone();

            if (current[5] < 9)
            {
                next[5] = current[5] + 1;
                return next;
            }

            if (current[4] < 9)
            {
                next[4] = current[4] + 1;
                next[5] = next[4];
                return next;
            }

            if (current[3] < 9)
            {
                next[3] = current[3] + 1;
                next[4] = next[3];
                next[5] = next[3];
                return next;
            }

            if (current[2] < 9)
            {
                next[2] = current[2] + 1;
                next[3] = next[2];
                next[4] = next[2];
                next[5] = next[2];
                return next;
            }

            if (current[1] < 9)
            {
                next[1] = current[1] + 1;
                next[2] = next[1];
                next[3] = next[1];
                next[4] = next[1];
                next[5] = next[1];
                return next;
            }

            if (current[0] < 9)
            {
                next[0] = current[0] + 1;
                next[1] = next[0];
                next[2] = next[0];
                next[3] = next[0];
                next[4] = next[0];
                next[5] = next[0];
                return next;
            }

            throw new Exception("Values got too high");
        }
    }
}
