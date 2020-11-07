using System;
using System.Text;

namespace Newbe.ObjectVisitor.Tests
{
    public class Yueluo
    {
        public string Name { get; set; } = null!;
        public int Age { get; set; }
        public string NickName { get; set; } = null!;
        public long Level { get; set; }

        public static Yueluo Create()
        {
            return new Yueluo
            {
                Age = 16,
                Name = "yueluo",
                NickName = "dalao",
                Level = long.MaxValue
            };
        }

        public string FormatString()
        {
            var sb = new StringBuilder();
            sb.AppendFormat("{0}:{1}{2}", nameof(Name), Name, Environment.NewLine);
            sb.AppendFormat("{0}:{1}{2}", nameof(Age), Age, Environment.NewLine);
            sb.AppendFormat("{0}:{1}{2}", nameof(NickName), NickName, Environment.NewLine);
            sb.AppendFormat("{0}:{1}{2}", nameof(Level), Level, Environment.NewLine);
            var re = sb.ToString();
            return re;
        }
        
        public string FormatOnlyString()
        {
            var sb = new StringBuilder();
            sb.AppendFormat("{0}:{1}{2}", nameof(Name), Name, Environment.NewLine);
            sb.AppendFormat("{0}:{1}{2}", nameof(NickName), NickName, Environment.NewLine);
            var re = sb.ToString();
            return re;
        }
    }
}