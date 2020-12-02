using AdventOfCode.Core;
using Autofac;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
    public class RunnerContainer
    {
        #region Singleton and Autofac Container
        private IContainer container;

        private RunnerContainer()
        {
            var thisAssembly = this.GetType().Assembly;
            var builder = new ContainerBuilder();
            builder.RegisterAssemblyTypes(thisAssembly).AsImplementedInterfaces();

            this.container = builder.Build();
        }

        private static RunnerContainer _instance;
        public static RunnerContainer Current
        {
            get
            {
                {
                    if (_instance == null)
                    {
                        _instance = new RunnerContainer();
                    }
                    return _instance;
                }

            }
        }
        #endregion

        public T Resolve<T>() => container.Resolve<T>();

        public IRunner GetLatestRunner() => container.Resolve<IEnumerable<IRunner>>().OrderBy(r => r.Day).Last();

        public IRunner GetRunnerForDay(int day) => container.Resolve<IEnumerable<IRunner>>().First(r => r.Day == day);

    }
}
