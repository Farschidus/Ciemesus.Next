using FluentValidation.Results;
using System.Collections.Generic;
using System.Linq;

namespace Ciemesus.Core.Contracts
{
    public class ResponseBase<T> : ResponseBase, IResponseBase<T>
        where T : new()
    {
        public T Result
        {
            get
            {
                return IsValid ? ResultObject : default;
            }

            set
            {
                ResultObject = value;
            }
        }

        private T ResultObject { get; set; } = new T();
    }

    public class ResponseBase : IResponseBase
    {
        public ResponseBase()
        {
            Errors = Enumerable.Empty<ValidationFailure>();
        }

        public ResponseBase(IEnumerable<ValidationFailure> errors)
        {
            Errors = errors;
        }

        public bool IsValid
        {
            get
            {
                return !Errors.Any();
            }

            set
            {
            }
        }

        public IEnumerable<ValidationFailure> Errors { get; set; }
    }
}
