using AdventOfCode2019.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdventOfCode2019.Day13
{
    public class ArcadeCabinet : Solver<long>
    {
        private LocationOnlyComparer<Tile> LocationOnlyComparer = new LocationOnlyComparer<Tile>();

        public ArcadeCabinet() : base(13) { }

        public void SolvePartOne()
        {
            var output = RunGame().Result;
            var i = 0;
            var tiles = new List<Tile>();
            while (i < output.Count - 2)
            {
                var x = (int) output[i];
                var y = (int) output[i + 1];
                var type = (TileId) output[i + 2];
                var newTile = new Tile(x, y, type);
                tiles = tiles.Where(t => !LocationOnlyComparer.Equals(t, newTile)).ToList();
                tiles.Add(newTile);
                i += 3;
            }
            var blocks = tiles.Count(t => t.TileType == TileId.Block);
            Console.WriteLine($"Blocks {blocks}");
        }

        public void SolvePartTwo()
        {
            var score = RunInteractiveGame().Result;
            Console.WriteLine($"Final Score: {score}");
        }

        private async Task<List<long>> RunGame()
        {
            var programme = ReadInput().ToList();
            var signal = new BasicSignal();
            var computer = new IntcodeComputer(programme, signal);
            await computer.RunProgramme();
            return signal.GetOutput();
        }

        private async Task<long> RunInteractiveGame()
        {
            var programme = ReadInput().ToList();
            var signal = new ArcadeSignal(false);
            var computer = new IntcodeComputer(programme, signal);
            computer.SetZeroValue(2);
            await computer.RunProgramme();
            return signal.GetScore();
        }

        protected override long ParseLine(string line)
        {
            return long.Parse(line);
        }
    }
}
