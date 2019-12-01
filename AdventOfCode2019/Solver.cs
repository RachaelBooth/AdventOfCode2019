using System.Collections.Generic;
using System.IO;

namespace AdventOfCode2019
{
    public interface ISolver
    {
        void SolvePartOne();
        void SolvePartTwo();
    }

    public abstract class Solver
    {
        private string inputFilePath;

        public Solver(int day)
        {
            inputFilePath = $"c:/users/rachael/documents/programming/adventofcode2019/adventofcode2019/day{day}/input.txt";
        }


        public void SolvePartOne()
        {

        }

        protected IEnumerable<T> ReadInput<T>()
        {
            return ReadInputFromFile<T>(inputFilePath);
        }

        protected IEnumerable<T> ReadInputFromFile<T>(string filePath)
        {
            var reader = new StreamReader(filePath);
            string line;

            while ((line = reader.ReadLine()) != null)
            {
                yield return ParseLine<T>(line);
            }

            reader.Close();
        }

        protected abstract T ParseLine<T>(string line);
    }
}
