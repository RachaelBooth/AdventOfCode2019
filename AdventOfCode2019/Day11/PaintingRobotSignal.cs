using System.Threading.Tasks;
using AdventOfCode2019.Shared;

namespace AdventOfCode2019.Day11
{
    public class PaintingRobotSignal : Signal
    {
        private readonly HullPaintingRobotTracker robotTracker;
        private bool moveOnNextOutput;

        public PaintingRobotSignal(Colour initialPanelColour = Colour.Black)
        {
            robotTracker = new HullPaintingRobotTracker(initialPanelColour);
            moveOnNextOutput = false;
        }

        public void Output(long value)
        {
            if (moveOnNextOutput)
            {
                robotTracker.TurnAndMove((TurnDirection)value);
                moveOnNextOutput = false;
            }
            else
            {
                robotTracker.Paint((Colour) value);
                moveOnNextOutput = true;
            }
        }

        public Task<long> Input()
        {
            return Task.FromResult((long) robotTracker.GetCurrentColour());
        }

        public HullPaintingRobotTracker GetTracker()
        {
            return robotTracker;
        }
    }

}
