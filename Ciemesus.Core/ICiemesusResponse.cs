using System.Collections.Generic;
using FluentValidation.Results;

namespace Ciemesus.Core
{
    public interface ICiemesusResponse
    {
        IEnumerable<ValidationFailure> Errors { get; set; }

        bool IsValid { get; }
    }

    public interface ICiemesusResponse<T> : ICiemesusResponse
    {
        T Result { get; set; }
    }
}
