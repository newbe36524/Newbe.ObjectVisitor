using System;
using System.Text;
using BenchmarkDotNet.Attributes;

namespace Newbe.ObjectVisitor.BenchmarkTest
{
    [Config(typeof(Config))]
    public class FormatStringTest
    {
        private readonly ICachedObjectVisitor<Yueluo, StringBuilder> _cachedObjectVisitor;
        private readonly Yueluo _yueluo;

        public FormatStringTest()
        {
            _yueluo = Yueluo.Create();
            var _ = _yueluo.FormatToString();
            _cachedObjectVisitor = _yueluo.V()
                .WithExtendObject<Yueluo, StringBuilder>()
                .ForEach((name, value, sb) => sb.AppendFormat("{0}:{1}{2}", name, value, Environment.NewLine))
                .Cache();
        }


        [Benchmark(Baseline = true)]
        public string Directly() => _yueluo.FormatString();

        [Benchmark]
        public string QuickStyle() => _yueluo.FormatToString();

        [Benchmark]
        public string CacheVisitor()
        {
            var sb = new StringBuilder();
            _cachedObjectVisitor.Run(_yueluo, sb);
            return sb.ToString();
        }
    }
}