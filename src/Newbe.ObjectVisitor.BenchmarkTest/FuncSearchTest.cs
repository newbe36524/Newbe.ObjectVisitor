using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using BenchmarkDotNet.Attributes;

namespace Newbe.ObjectVisitor.BenchmarkTest
{
    [Config(typeof(Config))]
    public class FuncSearchTest
    {
        private Func<Yueluo, object>[] _target;
        private readonly Yueluo _yueluo;
        private readonly Func<Yueluo, string> _func;
        private readonly PropertyInfo _nameP;
        private readonly Func<PropertyInfo, Func<Yueluo, object>> _switcher;
        private readonly Dictionary<PropertyInfo, Func<Yueluo, object>> _dic;

        public FuncSearchTest()
        {
            _yueluo = Yueluo.Create();
            var propertyInfos = typeof(Yueluo).GetProperties().ToArray();

            CreateCacheArrayD(propertyInfos);

            _switcher = ValueGetter.CreateGetter<Yueluo, object>(propertyInfos,
                info => Expression.SwitchCase(Expression.Constant(CreateFunc(info)), Expression.Constant(info)));
            _dic = propertyInfos.ToDictionary(x => x, CreateFunc);

            _nameP = typeof(Yueluo).GetProperty(nameof(Yueluo.Name));
            _func = x => x.Name;
        }

        private void CreateCacheArrayD(IReadOnlyCollection<PropertyInfo> propertyInfos)
        {
            _target = new Func<Yueluo, object>[propertyInfos.Count];
            foreach (var info in propertyInfos)
            {
                var key = GetKey(info);
                var index = key % propertyInfos.Count;
                _target[index] = CreateFunc(info);
            }
        }

        private static Func<Yueluo, object> CreateFunc(PropertyInfo info)
        {
            var pExp = Expression.Parameter(typeof(Yueluo), "x");
            var bodyExp = Expression.Property(pExp, info);
            var finalExp =
                Expression.Lambda<Func<Yueluo, object>>(Expression.Convert(bodyExp, typeof(object)), pExp);
            return finalExp.Compile();
        }

        private static int GetKey(MemberInfo info)
        {
            var token = info.MetadataToken;
            return token;
        }

        [Benchmark(Baseline = true)]
        public string Directly() => _func(_yueluo);

        [Benchmark]
        public string ArrayIndex() => (string) _target[_nameP.MetadataToken % _target.Length](_yueluo);

        [Benchmark]
        public string SwitchExp() => (string) _switcher(_nameP)(_yueluo);

        [Benchmark]
        public string Dic() => (string) _dic[_nameP](_yueluo);
    }
}