using System.Collections.Immutable;
using System.ComponentModel.DataAnnotations;

namespace BizLogicBase.Validation;

public abstract class ErrorAdder : ErrorStorage, IErrorAdder
{
    private readonly List<ValidationResult> _errors = [];
    public override IImmutableList<ValidationResult> Errors => _errors.ToImmutableList();

    public void AddError(string errorMessage, params string[] propertyNames)
    {
        _errors.Add(new ValidationResult(errorMessage, propertyNames));
    }
}
