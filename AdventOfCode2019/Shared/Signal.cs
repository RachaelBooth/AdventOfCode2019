using System.Threading.Tasks;

namespace AdventOfCode2019.Shared
{
    public interface Signal
    {
        void Output(long value);
        Task<long> Input();
    }
}
