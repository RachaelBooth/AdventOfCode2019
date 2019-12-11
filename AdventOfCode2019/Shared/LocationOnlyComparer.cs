using System;
using System.Collections.Generic;

namespace AdventOfCode2019.Shared
{
    public class LocationOnlyComparer<T> : IEqualityComparer<T> where T : Coordinate
    {
        public bool Equals(T coordinateOne, T coordinateTwo)
        {
            if (coordinateOne == null && coordinateTwo == null)
            {
                return true;
            }

            if (coordinateOne == null && coordinateTwo != null)
            {
                return false;
            }

            if (coordinateOne != null && coordinateTwo == null)
            {
                return false;
            }

            return coordinateOne.x == coordinateTwo.x && coordinateOne.y == coordinateTwo.y;
        }

        public int GetHashCode(T coordinate)
        {
            return HashCode.Combine(coordinate.x, coordinate.y);
        }
    }
}
