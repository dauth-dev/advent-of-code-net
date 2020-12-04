namespace AdventOfCode.Core
{
    public interface IRunner
    {
        string Day { get; }
        bool IsActive { get; }
        void Run();
    }
}
