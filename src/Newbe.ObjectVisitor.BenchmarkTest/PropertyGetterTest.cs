using System;
using System.Reflection;
using BenchmarkDotNet.Attributes;

namespace Newbe.ObjectVisitor.BenchmarkTest
{
    [Config(typeof(Config))]
    public class PropertyGetterTest
    {
        private readonly Yueluo _yueluo;
        private readonly PropertyInfo _nameProperty;
        private readonly PropertyInfo _ageProperty;
        private readonly Func<Yueluo, string> _nameFunc;

        public PropertyGetterTest()
        {
            _yueluo = Yueluo.Create();
            _nameProperty = typeof(Yueluo).GetProperty(nameof(Yueluo.Name));
            _ageProperty = typeof(Yueluo).GetProperty(nameof(Yueluo.Age));
            _nameFunc = ValueGetter<Yueluo, string, string>.GetGetter(_nameProperty);
            _ = ValueGetter<Yueluo, int, int>.GetGetter(_ageProperty).Invoke(_yueluo);
            _ = ValueGetter<Yueluo>.GetGetter(_nameProperty).Invoke(_yueluo);
        }

        [Benchmark(Baseline = true)]
        public string DirectlyString() => _yueluo.Name;

        [Benchmark]
        public int DirectlyInt() => _yueluo.Age;

        [Benchmark]
        public string ReflectString() => (string) _nameProperty.GetValue(_yueluo);

        [Benchmark]
        public int ReflectInt() => (int) _ageProperty.GetValue(_yueluo);

        [Benchmark]
        public string GetterString()
            => ValueGetter<Yueluo, string, string>.GetGetter(_nameProperty).Invoke(_yueluo);

        [Benchmark]
        public int GetterInt()
            => ValueGetter<Yueluo, int, int>.GetGetter(_ageProperty).Invoke(_yueluo);

        [Benchmark]
        public string GetterObject()
            => (string) ValueGetter<Yueluo>.GetGetter(_nameProperty).Invoke(_yueluo);

        [Benchmark]
        public string GetterCached()
            => _nameFunc.Invoke(_yueluo);
    }
}