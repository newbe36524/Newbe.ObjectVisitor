using System.Text.RegularExpressions;
using FluentAssertions;
using NUnit.Framework;

namespace Newbe.ObjectVisitor.Tests
{
    public class ChangePasswordTest
    {
        [Test]
        public void CoverSensitiveDataTest()
        {
            // here is a model
            var userModel = new UserModel
            {
                Username = "newbe36524",
                Password = "newbe.pro",
                Phone = "12345678901"
            };

            // create a data visitor to cover sensitive data
            var visitor = userModel.V()
                .ForEach<string>(x => CoverSensitiveData(x))
                .Cache();

            visitor.Run(userModel);

            var expected = new UserModel
            {
                Username = "newbe36524",
                Password = "***",
                Phone = "123****8901",
            };
            userModel.Should().BeEquivalentTo(expected);
        }

        private void CoverSensitiveData(IObjectVisitorContext<UserModel, string> c)
        {
            var value = c.Value;
            if (!string.IsNullOrEmpty(value))
            {
                c.Value = Regex.Replace(value, "(\\d{3})\\d{4}(\\d{4})", "$1****$2");
            }

            if (c.Name == nameof(UserModel.Password))
            {
                c.Value = "***";
            }
        }

        public class UserModel
        {
            public string Username { get; set; } = null!;
            public string Phone { get; set; } = null!;
            public string Password { get; set; } = null!;
        }
    }
}