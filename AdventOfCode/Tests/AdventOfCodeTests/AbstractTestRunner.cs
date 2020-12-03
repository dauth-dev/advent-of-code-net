using AdventOfCode;
using AdventOfCode.Core;
using Autofac;

namespace AdventOfCodeTests
{
    public abstract class AbstractTestRunner
    {
        private readonly IContainer container;

        public AbstractTestRunner()
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule<ContainerModule>();
            container = builder.Build();
        }

        protected IRunner GetRunnerForDay(int day) => container.Resolve<RunnerStarter>().GetRunnerForDay(day);

        protected T GetRunner<T>() where T : IRunner => container.Resolve<T>();
    }
}
