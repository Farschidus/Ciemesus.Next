using FluentValidation.Results;
using System.Collections.Generic;
using System.Linq;

namespace Ciemesus.Core
{
    public class CiemesusResponse<T> : ICiemesusResponse<T>
    {
        public CiemesusResponse()
        {
            Errors = new List<ValidationFailure>();
        }

        public bool IsValid
        {
            get
            {
                return !Errors.Any();
            }

            private set
            {
            }
        }

        public IEnumerable<ValidationFailure> Errors { get; set; }

        public T Result { get; set; }
    }

    public class CiemesusResponse : ICiemesusResponse
    {
        public CiemesusResponse()
        {
            Errors = new List<ValidationFailure>();
        }

        public bool IsValid
        {
            get
            {
                return !Errors.Any();
            }

            private set
            {
            }
        }

        public IEnumerable<ValidationFailure> Errors { get; set; }
    }
}
