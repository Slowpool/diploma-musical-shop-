using System.Collections.Immutable;
using System.ComponentModel.DataAnnotations;

namespace BizLogicBase.Validation;

public interface IErrorStorage
{
    IImmutableList<ValidationResult> Errors { get; }
    bool HasErrors { get; }
}
