using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Ciemesus.Api.Contracts
{
    public class ValidationFailureResult
    {
        public ValidationFailureResult()
        {
        }

        public ValidationFailureResult(IEnumerable<Error> errors)
        {
            Errors = errors;
        }

        public IEnumerable<Error> Errors { get; set; } = new List<Error>();

        [SuppressMessage("Microsoft.Naming", "CA1716", Justification = "Ignore this warning for now")]
        public class Error
        {
            public string PropertyName { get; set; }
            public string ErrorMessage { get; set; }
            public object AttemptedValue { get; set; }
            public string ErrorCode { get; set; }
        }
    }
}
