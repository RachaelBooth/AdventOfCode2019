using AdventOfCode2019.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2019.Day15
{
    public class DroidSignal : Signal
    {
        private bool manualControl;
        private Coordinate droidLocation;
        private List<MappedLocation> map;
        private DroidMovementCommand lastMovementCommand;
        private List<Coordinate> shortestPath;
        private bool oxygenFound;
        
        public DroidSignal(bool manualControl = false)
        {
            this.manualControl = manualControl;
            droidLocation = new Coordinate(0, 0);
            map = new List<MappedLocation> { new MappedLocation(0, 0, Type.Empty) };
            shortestPath = new List<Coordinate>();
            oxygenFound = false;
        }

        public void Output(long value)
        {
            var newLocation = getNextLocation();
           
            switch ((DroidStatus) value)
            {
                case DroidStatus.Moved:
                    droidLocation = newLocation;
                    map.Add(new MappedLocation(newLocation.x, newLocation.y, Type.Empty));
                    break;
                case DroidStatus.Wall:
                    map.Add(new MappedLocation(newLocation.x, newLocation.y, Type.Wall));
                    break;
                case DroidStatus.OxygenFound:
                    droidLocation = newLocation;
                    oxygenFound = true;
                    map.Add(new MappedLocation(newLocation.x, newLocation.y, Type.Oxygen));
                    break;
                default:
                    throw new Exception("Unexpected status");
            }

            UpdateShortestPath(newLocation, (DroidStatus) value);

            if (manualControl || oxygenFound)
            {
                Draw();
            }

            if (oxygenFound)
            {
                Console.WriteLine($"Found oxygen, shortest path to current location is {shortestPath.Count} steps");
            }
        }

        private Coordinate getNextLocation()
        {
            switch (lastMovementCommand)
            {
                case DroidMovementCommand.North:
                    return new Coordinate(droidLocation.x, droidLocation.y + 1);
                case DroidMovementCommand.South:
                    return new Coordinate(droidLocation.x, droidLocation.y - 1);
                case DroidMovementCommand.East:
                    return new Coordinate(droidLocation.x + 1, droidLocation.y);
                case DroidMovementCommand.West:
                    return new Coordinate(droidLocation.x - 1, droidLocation.y);
                default:
                    throw new Exception("Unexpected direction");
            }
        }

        private void UpdateShortestPath(Coordinate newLocation, DroidStatus status)
        {
            if (status == DroidStatus.Wall)
            {
                return;
            }

            if (shortestPath.Any(c => new LocationOnlyComparer<Coordinate>().Equals(c, newLocation)))
            {
                var index = shortestPath.FindIndex(c => new LocationOnlyComparer<Coordinate>().Equals(c, newLocation));
                shortestPath = shortestPath.Where((c, i) => i < index).ToList();
            }
            shortestPath.Add(newLocation);
        }

        private void Draw()
        {
            Console.Clear();
            var y = map.Max(l => l.y);
            while (y >= map.Min(l => l.y))
            {
                var x = map.Min(l => l.x);
                var line = new StringBuilder();
                while (x <= map.Max(l => l.x))
                {
                    var location = map.FirstOrDefault(m => m.x == x && m.y == y);
                    if (droidLocation.x == x && droidLocation.y == y)
                    {
                        if (location != null && location.type == Type.Oxygen)
                        {
                            line.Append("@");
                        }
                        else
                        {
                            line.Append("D");
                        }
                    }
                    else if (location == null)
                    {
                        line.Append(" ");
                    }
                    else
                    {
                        switch (location.type)
                        {
                            case Type.Empty:
                                line.Append(".");
                                break;
                            case Type.Wall:
                                line.Append("#");
                                break;
                            case Type.Oxygen:
                                line.Append("0");
                                break;
                            default:
                                throw new Exception("Unexpected type");
                        }
                    }
                    x += 1;
                }
                Console.WriteLine(line.ToString());
                y -= 1;
            }
        }

        public async Task<long> Input()
        {
            if (manualControl || oxygenFound)
            {
                var key = Console.ReadKey();
                if (key.Key == ConsoleKey.UpArrow)
                {
                    lastMovementCommand = DroidMovementCommand.North;
                    return (long)lastMovementCommand;
                }
                if (key.Key == ConsoleKey.DownArrow)
                {
                    lastMovementCommand = DroidMovementCommand.South;
                    return (long)lastMovementCommand;
                }
                if (key.Key == ConsoleKey.LeftArrow)
                {
                    lastMovementCommand = DroidMovementCommand.West;
                    return (long)lastMovementCommand;
                }
                if (key.Key == ConsoleKey.RightArrow)
                {
                    lastMovementCommand = DroidMovementCommand.East;
                    return (long)lastMovementCommand;
                }
                throw new Exception("Unexpected direction");
            }
            else
            {
                var up = map.FirstOrDefault(l => l.x == droidLocation.x && l.y == droidLocation.y + 1);
                if (up == null)
                {
                    lastMovementCommand = DroidMovementCommand.North;
                    return (long)lastMovementCommand;
                }
                var down = map.FirstOrDefault(l => l.x == droidLocation.x && l.y == droidLocation.y - 1);
                if (down == null)
                {
                    lastMovementCommand = DroidMovementCommand.South;
                    return (long)lastMovementCommand;
                }
                var left = map.FirstOrDefault(l => l.x == droidLocation.x - 1 && l.y == droidLocation.y);
                if (left == null)
                {
                    lastMovementCommand = DroidMovementCommand.West;
                    return (long)lastMovementCommand;
                }
                var right = map.FirstOrDefault(l => l.x == droidLocation.x + 1 && l.y == droidLocation.y);
                if (right == null)
                {
                    lastMovementCommand = DroidMovementCommand.East;
                    return (long)lastMovementCommand;
                }
                var back = shortestPath[shortestPath.Count - 2];
                if (back.x == droidLocation.x && back.y == droidLocation.y + 1)
                {
                    lastMovementCommand = DroidMovementCommand.North;
                    return (long)lastMovementCommand;
                }
                if (back.x == droidLocation.x && back.y == droidLocation.y - 1)
                {
                    lastMovementCommand = DroidMovementCommand.South;
                    return (long)lastMovementCommand;
                }
                if (back.x == droidLocation.x + 1 && back.y == droidLocation.y)
                {
                    lastMovementCommand = DroidMovementCommand.East;
                    return (long)lastMovementCommand;
                }
                lastMovementCommand = DroidMovementCommand.West;
                return (long)lastMovementCommand;
            }
        }
    }
}
