using System.Collections.Immutable;
using System.ComponentModel.DataAnnotations;

namespace BizLogicBase.Validation;

public abstract class ErrorStorage : IErrorStorage
{
    public virtual IImmutableList<ValidationResult> Errors { get; protected set; } = new List<ValidationResult>().ToImmutableList();
    public virtual bool HasErrors => Errors.Any();
}
