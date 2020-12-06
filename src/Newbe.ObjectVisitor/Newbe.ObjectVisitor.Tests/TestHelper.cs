using System;

namespace Newbe.ObjectVisitor.Tests
{
    public static class TestHelper
    {
        public static void RunTest<T, TExtend>(OVBuilderExt<T, TExtend>.IOVBuilderExt_V context, Action assertion)
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
        
        public static void RunTest<T>(OVBuilder<T>.IOVBuilder_V context, Action assertion)
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