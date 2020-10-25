using System;

namespace Newbe.ObjectVisitor
{
    public class ExtendObjectOvBuilderContextItem : IOvBuilderContextItem
    {
        public object? ExtendObject { get; set; }
        public Type ExtendObjectType { get; set; } = null!;
    }
}