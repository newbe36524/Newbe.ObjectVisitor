using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using FluentAssertions;
using NUnit.Framework;

namespace Newbe.ObjectVisitor.Tests
{
    public class DataConnectionModelTest
    {
        public const string ConnectionStringValue =
            "Host=www.newbe.pro;Port=36524;Username=newbe36524;Password=newbe.pro;";

        [Test]
        public void JoinToString()
        {
            var model = new DataConnectionModel
            {
                Host = "www.newbe.pro",
                Port = 36524,
                Username = "newbe36524",
                Password = "newbe.pro",
            };
            var connectionString = model.ToString();

            connectionString.Should().Be(ConnectionStringValue);
        }

        [Test]
        public void BuildFromString()
        {
            var model = DataConnectionModel.FromString(ConnectionStringValue);
            var expected = new DataConnectionModel
            {
                Host = "www.newbe.pro",
                Port = 36524,
                Username = "newbe36524",
                Password = "newbe.pro",
            };
            model.Should().BeEquivalentTo(expected);
        }


        public class DataConnectionModel
        {
            public string Host { get; set; } = null!;
            public ushort? Port { get; set; } = null!;
            public string Username { get; set; } = null!;
            public string Password { get; set; } = null!;
            public int? MaxPoolSize { get; set; } = null!;

            private static readonly ICachedObjectVisitor<DataConnectionModel, StringBuilder> ConnectionStringBuilder =
                default(DataConnectionModel)!
                    .V()
                    .WithExtendObject<DataConnectionModel, StringBuilder>()
                    .ForEach((name, value, sb) => AppendValueIfNotNull(name, value, sb))
                    .Cache();

            private static void AppendValueIfNotNull(string name, object value, StringBuilder sb)
            {
                if (value != null)
                {
                    sb.Append($"{name}={value};");
                }
            }

            public override string ToString()
            {
                var sb = new StringBuilder();
                ConnectionStringBuilder.Run(this, sb);
                return sb.ToString();
            }

            private static readonly ICachedObjectVisitor<DataConnectionModel, Dictionary<string, string>>
                ConnectionStringConstructor
                    =
                    default(DataConnectionModel)!
                        .V()
                        .WithExtendObject<DataConnectionModel, Dictionary<string, string>>()
                        .ForEach(context => SetValueIfFound(context))
                        .Cache();

            private static void SetValueIfFound(
                IObjectVisitorContext<DataConnectionModel, Dictionary<string, string>, object> c)
            {
                if (c.ExtendObject.TryGetValue(c.Name, out var stringValue))
                {
                    TypeConverter conv = TypeDescriptor.GetConverter(c.PropertyInfo.PropertyType);
                    c.Value = conv.ConvertFrom(stringValue)!;
                }
            }

            public static DataConnectionModel FromString(string connectionString)
            {
                var dic = connectionString.Split(';')
                    .Where(x => !string.IsNullOrEmpty(x))
                    .Select(x => x.Split('='))
                    .ToDictionary(x => x[0], x => x[1]);
                var re = new DataConnectionModel();
                ConnectionStringConstructor.Run(re, dic);
                return re;
            }
        }
    }
}