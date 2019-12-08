using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2019.Day8
{
    public class Layer
    {
        private readonly List<List<int>> pixels;
        private readonly int width;
        private readonly int height;

        public Layer(int width, int height)
        {
            pixels = new List<List<int>>();
            this.width = width;
            this.height = height;
        }

        public void PushPixel(int pixel)
        {
            if (!pixels.Any() || pixels.Last().Count == width)
            {
                var row = new List<int>();
                pixels.Add(row);
            }
            pixels.Last().Add(pixel);
        }

        public bool IsFull()
        {
            return pixels.Count >= height && pixels.Last().Count >= width;
        }

        public int CountOf(int value)
        {
            return pixels.Sum(row => row.Count(p => p == value));
        }

        public int ValueAt(int x, int y)
        {
            return pixels[y][x];
        }
    }
}
