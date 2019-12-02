using System.Collections.Generic;
using System.IO;

namespace AdventOfCode2019
{
    public interface ISolver
    {
        void SolvePartOne();
        void SolvePartTwo();
    }

    public abstract class Solver<T>
    {
        private readonly string inputFilePath;

        protected Solver(int day)
        {
            inputFilePath = $"../../../day{day}/input.txt";
        }

        protected IEnumerable<T> ReadInput()
        {
            return ReadInputFromFile(inputFilePath);
        }

        protected IEnumerable<T> ReadInputFromFile(string filePath)
        {
            var reader = new StreamReader(filePath);
            string line;

            while ((line = reader.ReadLine()) != null)
            {
                yield return ParseLine(line);
            }

            reader.Close();
        }

        protected abstract T ParseLine(string line);
    }
}
