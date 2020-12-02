namespace AdventOfCode
{
    internal class MainRunner
    {
        public static void Main() => RunnerContainer.Current.GetLatestRunner().Run();
    }
}