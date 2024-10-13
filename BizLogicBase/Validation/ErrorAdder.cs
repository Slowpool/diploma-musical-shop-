using System.Collections.Immutable;
using System.ComponentModel.DataAnnotations;

namespace BizLogicBase.Validation;

public abstract class ErrorAdder : ErrorStorage, IErrorAdder
{
    private readonly List<ValidationResult> errors = [];
    public override IImmutableList<ValidationResult> Errors => errors.ToImmutableList();

    public void AddError(string errorMessage, params string[] propertyNames)
    {
        errors.Add(new ValidationResult(errorMessage, propertyNames));
    }
}
