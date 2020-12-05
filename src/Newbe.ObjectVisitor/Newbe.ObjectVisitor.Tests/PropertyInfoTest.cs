using System.Linq;
using System.Reflection;
using FluentAssertions;
using NUnit.Framework;

namespace Newbe.ObjectVisitor.Tests
{
    public class PropertyInfoTest
    {
        [Test]
        public void MetaTokenTest()
        {
            var propertyInfos = typeof(TestModel).GetProperties().ToArray();
            var target = new PropertyInfo[propertyInfos.Length];
            foreach (var info in propertyInfos)
            {
                var key = GetKey(info);
                var index = key % propertyInfos.Length;
                target[index] = info;
            }

            target.All(x => x != null).Should().BeTrue();
        }

        [Test]
        public void MetaTokenBTest()
        {
            var propertyInfos = typeof(TestModel).GetProperties().ToArray();
            var length = 2;
            var i = 1;
            while (length < propertyInfos.Length)
            {
                i++;
                length *= 2;
            }

            var time = i;

            var target = new PropertyInfo[length];
            foreach (var info in propertyInfos)
            {
                var key = GetKey(info);
                var index = GetIndex(key, time);
                target[index] = info;
            }

            target.Where(x => x != null).ToArray().Length.Should().Be(propertyInfos.Length);
        }


        [TestCase(9, 8, 3, 1)]
        [TestCase(10, 8, 3, 2)]
        [TestCase(11, 8, 3, 3)]
        [TestCase(16, 8, 3, 0)]
        public void ModTest(int value, int length, int time, int result)
        {
            var b = GetIndex(value, time);
            b.Should().Be(result);
        }

        private static int GetIndex(int value, int time)
        {
            const int i = sizeof(int) * 8;
            var u = (uint) value;
            var b = u << (i - time) >> (i - time);
            // var b = value - (value >> time << time);
            return (int) b;
        }

        public static int GetKey(PropertyInfo info)
        {
            var token = info.MetadataToken;
            return token;
        }

        public class TestModel
        {
            public string Name { get; set; }
            public string NickName { get; set; }
            public int Age { get; set; }
            public string Type { get; set; }
        }
    }
}