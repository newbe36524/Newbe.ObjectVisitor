using System;
using System.Collections.Generic;
using System.Linq;

namespace Newbe.ObjectVisitor.Validator
{
    public class ValidateResult<T> : IValidateResult<T>
    {
        public ValidateResult(T source, IEnumerable<string>? errors)
        {
            Source = source;
            Errors = errors?.ToArray() ?? Array.Empty<string>();
        }

        public T Source { get; }
        public string[] Errors { get; }
        public bool Success => Errors.Any() != true;
    }
}