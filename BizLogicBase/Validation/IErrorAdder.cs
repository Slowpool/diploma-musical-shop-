namespace BizLogicBase.Validation;

public interface IErrorAdder : IErrorStorage
{
    void AddError(string errorMessage, params string[] propertyNames);
}
