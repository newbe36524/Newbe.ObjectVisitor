using System;
using System.Collections.Generic;
using System.Linq;

namespace Newbe.ObjectVisitor.Validation
{
    internal class ValidationResult<T> : IValidationResult<T>
    {
        public ValidationResult(T source, IEnumerable<string>? errors)
        {
            Source = source;
            Errors = errors?.ToArray() ?? Array.Empty<string>();
        }

        public T Source { get; }
        public string[] Errors { get; }
        public bool Success => Errors.Any() != true;
    }
}