using System;
using System.Reflection;
using System.Text;
using BenchmarkDotNet.Attributes;

namespace Newbe.ObjectVisitor.BenchmarkTest
{
    [Config(typeof(Config))]
    public class CacheVisitorTest
    {
        private readonly ICachedObjectVisitor<Yueluo, StringBuilder> _cachedObjectVisitor;
        private readonly Yueluo _yueluo;
        private readonly PropertyInfo[] _propertyInfos;

        public CacheVisitorTest()
        {
            _yueluo = Yueluo.Create();
            var _ = _yueluo.FormatToString();
            _cachedObjectVisitor = _yueluo.V()
                .WithExtendObject<Yueluo, StringBuilder>()
                .ForEach((name, value, sb) => sb.AppendFormat("{0}:{1}{2}", name, value, Environment.NewLine))
                .Cache();

            _propertyInfos = typeof(Yueluo).GetProperties();
        }

        [Benchmark(Baseline = true)]
        public string CacheVisitor()
        {
            var sb = new StringBuilder();
            _cachedObjectVisitor.Run(_yueluo, sb);
            return sb.ToString();
        }

        [Benchmark]
        public string NoCacheVisitor()
        {
            var sb = new StringBuilder();
            _yueluo
                .V()
                .WithExtendObject(sb)
                .ForEach((name, value, s) => s.AppendFormat("{0}:{1}{2}", name, value, Environment.NewLine))
                .Run();
            return sb.ToString();
        }

        [Benchmark]
        public string ReflectProperty()
        {
            var sb = new StringBuilder();
            foreach (var propertyInfo in _propertyInfos)
            {
                sb.AppendFormat("{0}:{1}{2}", propertyInfo.Name, propertyInfo.GetValue(_yueluo), Environment.NewLine);
            }

            return sb.ToString();
        }
    }
}