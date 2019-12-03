using System;

namespace AdventOfCode2019.Day3
{
    public class DirectionInstruction
    {
        public Direction Direction;
        public int Steps;

        public DirectionInstruction(string instruction)
        {
            Direction = ParseDirection(instruction[0]);
            Steps = int.Parse(instruction.Substring(1));
        }

        private Direction ParseDirection(char direction)
        {
            return direction switch
            {
                'U' => Direction.Up,
                'D' => Direction.Down,
                'L' => Direction.Left,
                'R' => Direction.Right,
                _ => throw new Exception("Unexpected direction code"),
            };
        }
    }

    public enum Direction
    {
        Up,
        Down,
        Left,
        Right
    }
}
