using System.Threading.Tasks;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Newbe.ObjectVisitor.FluentApiGeneratorConsole
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var sc = new ServiceCollection();
            sc.AddLogging(logging => logging.AddConsole());
            var builder = new ContainerBuilder();
            builder.Populate(sc);
            builder.RegisterModule<FluentApiGeneratorConsoleModule>();
       
            var container = builder.Build();
            var fluentApiScanner = container.Resolve<IFluentApiScanner>();
            await fluentApiScanner.ScanAsync();
        }
    }
}