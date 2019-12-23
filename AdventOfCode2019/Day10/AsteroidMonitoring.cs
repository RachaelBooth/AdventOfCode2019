using AdventOfCode2019.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode2019.Day10
{
    class AsteroidMonitoring : Solver<List<Coordinate>>
    {
        private int y;
        

        public AsteroidMonitoring() : base(10) 
        {
            y = 0;
        }

        public void SolvePartOne()
        {
            var asteroids = ReadInput().SelectMany(l => l).ToList();
            var bestVisible = 0;
            foreach (var asteroid in asteroids)
            {
                var visible = CountVisibleAsteroids(asteroid, asteroids);
                if (visible > bestVisible)
                {
                    bestVisible = visible;
                }
            }
            Console.WriteLine($"Best location can see {bestVisible} asteroids");
        }

        public void SolvePartTwo()
        {
            var asteroids = ReadInput().SelectMany(l => l).ToList();
            var bestVisible = 0;
            Coordinate bestAsteroid = new Coordinate(-1, -1);
            foreach (var asteroid in asteroids)
            {
                var visible = CountVisibleAsteroids(asteroid, asteroids);
                if (visible > bestVisible)
                {
                    bestVisible = visible;
                    bestAsteroid = asteroid;
                }
            }
            var asteroidVectors = asteroids.Where(a => !new LocationOnlyComparer<Coordinate>().Equals(a, bestAsteroid)).Select(a => new NormalisedVector(a.x - bestAsteroid.x, a.y - bestAsteroid.y));
            var firstQuadrant = asteroidVectors.Where(v => v.x >= 0 && v.y >= 0).ToList();
            var secondQuadrant = asteroidVectors.Where(v => v.x >= 0 && v.y < 0).ToList();
            var thirdQuadrant = asteroidVectors.Where(v => v.x < 0 && v.y < 0).ToList();
            var fourthQuadrant = asteroidVectors.Where(v => v.x < 0 && v.y >= 0).ToList();

            var hitOrder = new List<NormalisedVector>();

            var nextVector = firstQuadrant.Where(v => v.x == firstQuadrant.Min(v => v.x));

            Console.WriteLine($"Best asteroid at {bestAsteroid.x}, {bestAsteroid.y}");
        }

        private int CountVisibleAsteroids(Coordinate asteroid, List<Coordinate> allAsteroids)
        {
            var asteroidVectors = new List<NormalisedVector>();
            foreach (var ast in allAsteroids)
            {
                var vector = new NormalisedVector(ast.x - asteroid.x, ast.y - asteroid.y);
                
                if (asteroidVectors.Count(v => v.x == vector.x && v.y == vector.y) == 0)
                {
                    asteroidVectors.Add(vector);
                }
            }
            return asteroidVectors.Count(v => v.x != 0 || v.y != 0);
        }

        protected override List<Coordinate> ParseLine(string line)
        {
            var asteroids = new List<Coordinate>();
            var x = 0;
            while (x < line.Length)
            {
                if (line[x] == '#')
                {
                    asteroids.Add(new Coordinate(x, y));
                }
                x += 1;
            }
            y += 1;
            return asteroids;
        }
    }
}
