using System;

namespace Newbe.ObjectVisitor.Tests
{
    public static class TestHelper
    {
        public static void RunTest(IOvBuilderContext context, Action assertion)
        {
            try
            {
                assertion();
            }
            finally
            {
                Console.WriteLine($"exp:{Environment.NewLine}{context.GetDebugInfo()}");
            }
        }
    }
}