using System;
using System.Collections.Generic;

namespace AdventOfCode2019.Day3
{
    public class LocationOnlyComparer : IEqualityComparer<WireCoordinate>
    {
        public bool Equals(WireCoordinate coordinateOne, WireCoordinate coordinateTwo)
        {
            if (coordinateOne == null && coordinateTwo == null)
            {
                return true;
            } 
            else if (coordinateOne == null && coordinateTwo != null)
            {
                return false;
            }
            else if (coordinateOne != null && coordinateTwo == null)
            {
                return false;
            }
            else
            {
                return coordinateOne.x == coordinateTwo.x && coordinateOne.y == coordinateTwo.y;
            }
        }

        public int GetHashCode(WireCoordinate coordinate)
        {
            return HashCode.Combine(coordinate.x, coordinate.y);
        }
    }
}
