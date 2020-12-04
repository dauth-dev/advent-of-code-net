namespace AdventOfCode.Core
{
    public interface IRunner
    {
        int Day { get; }
        bool IsActive { get; }
        void Run();
    }
}
