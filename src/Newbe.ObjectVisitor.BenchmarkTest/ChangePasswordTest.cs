using System.Text.RegularExpressions;
using BenchmarkDotNet.Attributes;

namespace Newbe.ObjectVisitor.BenchmarkTest
{
    [Config(typeof(Config))]
    public class ChangePasswordTest
    {
        private readonly ICachedObjectVisitor<UserModel> _visitor;

        public ChangePasswordTest()
        {
            // create a data visitor to cover sensitive data
            _visitor = default(UserModel).V()
                .ForEach<UserModel, string>(x => CoverSensitiveData(x))
                .Cache();
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


        [Benchmark(Baseline = true)]
        public void Directly()
        {
            var model = new UserModel
            {
                Username = "newbe36524",
                Password = "newbe.pro",
                Phone = "12345678901"
            };
            model.Phone = Regex.Replace(model.Phone, "(\\d{3})\\d{4}(\\d{4})", "$1****$2");
            model.Password = "***";
        }

        [Benchmark]
        public void UsingVisitor()
        {
            var model = new UserModel
            {
                Username = "newbe36524",
                Password = "newbe.pro",
                Phone = "12345678901"
            };

            _visitor.Run(model);
        }


        public class UserModel
        {
            public string Username { get; set; }
            public string Phone { get; set; }
            public string Password { get; set; }
        }
    }
}