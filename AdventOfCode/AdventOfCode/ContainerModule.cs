using Autofac;

namespace AdventOfCode
{
    public class ContainerModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(ThisAssembly).AsSelf().AsImplementedInterfaces();
        }
    }
}