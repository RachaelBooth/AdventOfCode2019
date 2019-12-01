using System;
using System.Collections.Generic;
using System.Text;
using AdventOfCode2019.Day1;
using FluentAssertions;
using NUnit.Framework;

namespace AdventOfCode2019Tests.Day1
{
    public class FuelCounterUpperTests
    {
        [TestCase(654, 966)]
        [TestCase(33583, 50346)]
        public void FuelIncludingFuelTests(int initialFuel, int expectedResult)
        {
            var result = new FuelCounterUpper().GetRequiredFuelForFuel(initialFuel);
            result.Should().Be(expectedResult);
        }
    }
}
