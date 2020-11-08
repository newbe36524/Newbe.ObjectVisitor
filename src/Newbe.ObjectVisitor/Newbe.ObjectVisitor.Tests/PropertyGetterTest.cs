using System;
using System.Reflection;
using NUnit.Framework;

namespace Newbe.ObjectVisitor.Tests
{
    public class PropertyGetterTest
    {
        private readonly Yueluo _yueluo;
        private readonly PropertyInfo _nameProperty;
        private readonly PropertyInfo _ageProperty;
        private readonly Func<Yueluo, string> _nameFunc;

        public PropertyGetterTest()
        {
            _yueluo = Yueluo.Create();
            _nameProperty = typeof(Yueluo).GetProperty(nameof(Yueluo.Name))!;
            _ageProperty = typeof(Yueluo).GetProperty(nameof(Yueluo.Age))!;
            _nameFunc = ValueGetter<Yueluo, string, string>.GetGetter(_nameProperty);
            _ = ValueGetter<Yueluo, int, int>.GetGetter(_ageProperty).Invoke(_yueluo);
            _ = ValueGetter<Yueluo>.GetGetter(_nameProperty).Invoke(_yueluo);
        }

        [Test]
        public void DirectlyString() => _ = _yueluo.Name;

        [Test]
        public void DirectlyInt() => _ = _yueluo.Age;

        [Test]
        public void ReflectString() => _ = (string) _nameProperty.GetValue(_yueluo);

        [Test]
        public void ReflectInt() => _ = (int) _ageProperty.GetValue(_yueluo);

        [Test]
        public void GetterString()
            => ValueGetter<Yueluo, string, string>.GetGetter(_nameProperty).Invoke(_yueluo);

        [Test]
        public void GetterInt()
            => ValueGetter<Yueluo, int, int>.GetGetter(_ageProperty).Invoke(_yueluo);

        [Test]
        public void GetterObject()
            => _ = (string) ValueGetter<Yueluo>.GetGetter(_nameProperty).Invoke(_yueluo);

        [Test]
        public void GetterCached()
            => _nameFunc.Invoke(_yueluo);
    }
}