using Autofac;

namespace Newbe.ObjectVisitor.FluentApiGeneratorConsole
{
    public class FluentApiGeneratorConsoleModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
            builder.RegisterType<FluentApiScanner>()
                .As<IFluentApiScanner>()
                .SingleInstance();
        }
    }
}