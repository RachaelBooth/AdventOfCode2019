using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2019.Day8
{
    public class SpaceFormatImageProcessor : Solver<int>
    {
        const int imageWidth = 25;
        const int imageHeight = 6;

        public SpaceFormatImageProcessor() : base (8) { }

        public void SolvePartOne()
        {
            var layers = ReadLayers();
            var fewestZerosLayer = layers.OrderBy(l => l.CountOf(0)).First();
            var checkValue = fewestZerosLayer.CountOf(1) * fewestZerosLayer.CountOf(2);
            Console.WriteLine($"Value: {checkValue}");
        }

        public void SolvePartTwo()
        {
            var layers = ReadLayers();
            var y = 0;
            while (y < imageHeight)
            {
                var x = 0;
                var line = new List<char>();
                while (x < imageWidth)
                {
                    line.Add(GetDisplayAt(layers, x, y));
                    x += 1;
                }
                Console.WriteLine(string.Join("", line));
                y += 1;
            }
        }

        // N.B. x is across, y is down - confusing
        private char GetDisplayAt(List<Layer> layers, int x, int y)
        {
            var firstNonTransparentLayer = layers.FirstOrDefault(l => l.ValueAt(x, y) != 2);
            if (firstNonTransparentLayer == null)
            {
                return '?';
            }
            var value = firstNonTransparentLayer.ValueAt(x, y);
            if (value == 0)
            {
                return '£';
            }
            return '.';
        }

        private List<Layer> ReadLayers()
        {
            var pixels = ReadInput();
            var layers = new List<Layer>();
            var currentLayer = new Layer(imageWidth, imageHeight);
            foreach (var pixel in pixels)
            {
                currentLayer.PushPixel(pixel);
                if (currentLayer.IsFull())
                {
                    layers.Add(currentLayer);
                    currentLayer = new Layer(imageWidth, imageHeight);
                }
            }
            return layers;
        }

        protected override int ParseLine(string line)
        {
            return int.Parse(line);
        }
    }
}
