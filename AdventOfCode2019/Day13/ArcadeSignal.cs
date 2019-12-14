using AdventOfCode2019.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AdventOfCode2019.Day13
{
    public class ArcadeSignal : Signal
    {
        private Queue<long> output;
        private List<Tile> tiles;
        private long score;
        private LocationOnlyComparer<Tile> LocationOnlyComparer = new LocationOnlyComparer<Tile>();
        private bool manualPlay;

        public ArcadeSignal(bool manualPlay)
        {
            this.manualPlay = manualPlay;
            output = new Queue<long>();
            tiles = new List<Tile>();
        }

        public void Output(long value)
        {
            output.Enqueue(value);
            if (output.Count >= 3)
            {
                var x = output.Dequeue();
                var y = output.Dequeue();
                var v = output.Dequeue();
                if (x == -1 && y == 0)
                {
                    score = v;
                } 
                else
                {
                    var tile = new Tile((int) x, (int) y, (TileId) v);
                    tiles = tiles.Where(t => !LocationOnlyComparer.Equals(t, tile)).ToList();
                    tiles.Add(tile);
                }
            }
        }

        private void Draw()
        {
            if (!tiles.Any())
            {
                return;
            }
            Console.SetCursorPosition(0, 0);
            Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
            Console.WriteLine($"SCORE: {score}");
            var y = 0;
            while (y <= tiles.Max(t => t.y))
            {
                var x = 0;
                var line = new StringBuilder();
                while (x <= tiles.Max(t => t.x))
                {
                    var tile = tiles.FirstOrDefault(t => t.x == x && t.y == y);
                    if (tile == null)
                    {
                        line.Append(" ");
                    }
                    else
                    {
                        switch (tile.TileType)
                        {
                            case TileId.Empty:
                                line.Append(" ");
                                break;
                            case TileId.Wall:
                                line.Append("#");
                                break;
                            case TileId.Block:
                                line.Append(".");
                                break;
                            case TileId.Paddle:
                                line.Append("-");
                                break;
                            case TileId.Ball:
                                line.Append("o");
                                break;
                            default:
                                throw new Exception("Unexpected type of tile");
                        }
                    }
                    x += 1;
                }
                Console.WriteLine(line.ToString());
                y += 1;
            }
            Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
        }

        public async Task<long> Input()
        {
            Draw();
            if (manualPlay)
            {
                var key = Console.ReadKey();
                if (key.Key == ConsoleKey.LeftArrow)
                {
                    return -1;
                }
                if (key.Key == ConsoleKey.RightArrow)
                {
                    return 1;
                }
                return 0;
            }

            var ballX = tiles.Where(t => t.TileType == TileId.Ball).Select(b => b.x).First();
            var paddleX = tiles.Where(t => t.TileType == TileId.Paddle).Select(p => p.x).First();
            if (ballX < paddleX)
            {
                return -1;
            }
            if (ballX > paddleX)
            {
                return 1;
            }
            return 0;
        }

        public long GetScore()
        {
            return score;
        }
    }
}
