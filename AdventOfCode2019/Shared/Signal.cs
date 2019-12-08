using System.Threading.Tasks;

namespace AdventOfCode2019.Shared
{
    public interface Signal
    {
        void Output(int value);
        Task<int> Input();
    }
}
