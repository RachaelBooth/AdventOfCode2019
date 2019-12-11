using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode2019.Shared;

namespace AdventOfCode2019.Day11
{
    public class HullPaintingRobotTracker
    {
        private HullCoordinate currentLocation;
        private List<HullCoordinate> visitedLocations;
        private Direction facingDirection;

        private readonly LocationOnlyComparer<HullCoordinate> locationOnlyComparer = new LocationOnlyComparer<HullCoordinate>();

        public HullPaintingRobotTracker(Colour initialLocationColour = Colour.Black)
        {
            currentLocation = new HullCoordinate(0, 0, initialLocationColour);
            visitedLocations = new List<HullCoordinate> { currentLocation };
            facingDirection = Direction.Up;
        }

        public void Paint(Colour colour)
        {
            currentLocation.Paint(colour);
        }

        public void TurnAndMove(TurnDirection turnDirection)
        {
            facingDirection = GetNewDirection(turnDirection);
            currentLocation = GetNextLocation();
        }

        public Colour GetCurrentColour()
        {
            return currentLocation.colour;
        }

        public List<HullCoordinate> GetVisitedLocations()
        {
            return visitedLocations;
        }

        private HullCoordinate PaintIfCurrent(HullCoordinate coordinate, Colour colour)
        {
            if (locationOnlyComparer.Equals(coordinate, currentLocation))
            {
                coordinate.Paint(colour);
            }

            return coordinate;
        }

        private Direction GetNewDirection(TurnDirection turnDirection)
        {
            return facingDirection switch
            {
                Direction.Up => (turnDirection == TurnDirection.Clockwise ? Direction.Right : Direction.Left),
                Direction.Right => (turnDirection == TurnDirection.Clockwise ? Direction.Down : Direction.Up),
                Direction.Down => (turnDirection == TurnDirection.Clockwise ? Direction.Left : Direction.Right),
                Direction.Left => (turnDirection == TurnDirection.Clockwise ? Direction.Up : Direction.Down),
                _ => throw new Exception("Unexpected direction")
            };
        }

        private HullCoordinate GetNextLocation()
        {
            var location = facingDirection switch
            {
                Direction.Up => new HullCoordinate(currentLocation.x, currentLocation.y + 1),
                Direction.Down => new HullCoordinate(currentLocation.x, currentLocation.y - 1),
                Direction.Right => new HullCoordinate(currentLocation.x + 1, currentLocation.y),
                Direction.Left => new HullCoordinate(currentLocation.x - 1, currentLocation.y),
                _ => throw new Exception("Unexpected direction")
            };

            var matchingLocationVisited = visitedLocations.FirstOrDefault(l => locationOnlyComparer.Equals(l, location));
            if (matchingLocationVisited != null)
            {
                return matchingLocationVisited;
            }

            visitedLocations.Add(location);
            return location;
        }
    }
}
