using System;
using System.Reflection;
using NUnit.Framework;

namespace Newbe.ObjectVisitor.Tests
{
    public class PropertySetterTest
    {
        private readonly Yueluo _yueluo;
        private readonly PropertyInfo _nameProperty;
        private readonly PropertyInfo _ageProperty;
        private readonly Action<Yueluo, string> _nameFunc;

        public PropertySetterTest()
        {
            _yueluo = Yueluo.Create();
            _nameProperty = typeof(Yueluo).GetProperty(nameof(Yueluo.Name))!;
            _ageProperty = typeof(Yueluo).GetProperty(nameof(Yueluo.Age))!;
            _nameFunc = ValueSetter<Yueluo, string, string>.GetSetter(_nameProperty);
            ValueSetter<Yueluo, int, int>.GetSetter(_ageProperty).Invoke(_yueluo, 16);
            ValueSetter<Yueluo>.GetSetter(_nameProperty).Invoke(_yueluo, "dalao");
        }

        [Test]
        public void DirectlyString() => _yueluo.Name = "dalao";

        [Test]
        public void DirectlyInt() => _yueluo.Age = 16;

        [Test]
        public void ReflectString() => _nameProperty.SetValue(_yueluo, "dalao");

        [Test]
        public void ReflectInt() => _ageProperty.SetValue(_yueluo, 16);

        [Test]
        public void GetterString()
            => ValueSetter<Yueluo, string, string>.GetSetter(_nameProperty).Invoke(_yueluo, "dalao");

        [Test]
        public void GetterInt()
            => ValueSetter<Yueluo, int, int>.GetSetter(_ageProperty).Invoke(_yueluo, 16);

        [Test]
        public void GetterObject()
            => ValueSetter<Yueluo>.GetSetter(_nameProperty).Invoke(_yueluo, "dalao");

        [Test]
        public void GetterCached()
            => _nameFunc.Invoke(_yueluo, "dalao");
    }
}