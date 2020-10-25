using BenchmarkDotNet.Columns;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Environments;
using BenchmarkDotNet.Exporters;
using BenchmarkDotNet.Exporters.Csv;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Mathematics;

namespace Newbe.ObjectVisitor.BenchmarkTest
{
    public class Config : ManualConfig
    {
        public Config()
        {
            AddColumn(
                TargetMethodColumn.Method,
                new CategoriesColumn(),
                StatisticColumn.Mean,
                StatisticColumn.Error,
                StatisticColumn.StdDev,
                new RankColumn(NumeralSystem.Arabic));
            AddExporter(CsvMeasurementsExporter.Default);
            AddExporter(RPlotExporter.Default);
            AddExporter(MarkdownExporter.GitHub);

            AddJob(
                Job.Default
                    .WithId("net461")
                    .WithRuntime(ClrRuntime.Net461),
                Job.Default
                    .WithId("net48")
                    .WithRuntime(ClrRuntime.Net48),
                Job.Default
                    .WithId("netcoreapp21")
                    .WithRuntime(CoreRuntime.Core21),
                Job.Default
                    .WithId("netcoreapp31")
                    .WithRuntime(CoreRuntime.Core31),
                Job.Default
                    .WithId("netcoreapp5")
                    .WithRuntime(CoreRuntime.Core50));
        }
    }
}