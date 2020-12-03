using System;

namespace Newbe.ObjectVisitor
{
    internal class SourceObjectOvBuilderContextItem : IOvBuilderContextItem
    {
        public object? SourceObject { get; set; }
        public Type InputType { get; set; } = null!;
    }
}