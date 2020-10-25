using System;

namespace Newbe.ObjectVisitor
{
    public class SourceObjectOvBuilderContextItem : IOvBuilderContextItem
    {
        public object? SourceObject { get; set; }
        public Type InputType { get; set; } = null!;
    }
}