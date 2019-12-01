using System;
using System.Collections.Generic;
using System.IO;

namespace AdventOfCode2019.Day1
{
    public class FuelCounterUpper : ISolver
    {
        public FuelCounterUpper()
        {

        }

        public void SolvePartOne()
        {
            Console.WriteLine(GetRequiredFuelForModules());
        }

        public int GetRequiredFuelForModules()
        {
            var moduleMasses = Input();
            var sum = 0;
            foreach (var mass in moduleMasses)
            {
                sum += FuelRequired(mass);
            }
            return sum;
        }

        public void SolvePartTwo()
        {

            Console.WriteLine(GetRequiredFuelRecursive());
        }

        public int GetRequiredFuelRecursive()
        {
            var moduleMasses = Input();
            var sum = 0;
            foreach (var mass in moduleMasses)
            {
                sum += FuelRequiredRecursive(mass);
            }
            return sum;
        }

        private int FuelRequiredRecursive(int mass)
        {
            var fuel = FuelRequired(mass);
            if (fuel <= 0)
            {
                return 0;
            }
            return fuel + FuelRequiredRecursive(fuel);
        }

        public int GetRequiredFuelIncludingFuel()
        {
            return GetRequiredFuelForFuel(GetRequiredFuelForModules());
        }

        public int GetRequiredFuelForFuel(int initialFuel)
        {
            var totalFuel = initialFuel;
            var fuelNeedingFuelAdded = totalFuel;

            while (true)
            {
                var fuelToAdd = FuelRequired(fuelNeedingFuelAdded);
                if (fuelToAdd <= 0)
                {
                    return totalFuel;
                }

                Console.WriteLine($"Fuel to add: {fuelToAdd}");

                totalFuel += fuelToAdd;
                fuelNeedingFuelAdded = fuelToAdd;

                Console.WriteLine($"New Total: {totalFuel}");
                Console.WriteLine($"New amount to calculate for: {fuelNeedingFuelAdded}");
            }
        }

        private int FuelRequired(int moduleMass)
        {
            return (int) Math.Floor((decimal) (moduleMass / 3)) - 2;
        }

        private IEnumerable<int> Input()
        {
            var reader = new StreamReader("c:/users/rachael/documents/programming/adventofcode2019/adventofcode2019/day1/input.txt");
            string line;

            while ((line = reader.ReadLine()) != null) {
                yield return int.Parse(line);
            }

            reader.Close();
        }
    }
}
