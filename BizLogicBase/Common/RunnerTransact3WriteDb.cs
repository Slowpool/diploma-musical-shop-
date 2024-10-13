using BizLogicBase.Validation;
using Microsoft.EntityFrameworkCore;

namespace BizLogicBase.Common;

public class RunnerTransact3WriteDb<TIn, TPass1, TPass2, TOut>(IBizAction<TIn, TPass1> actionPart1, IBizAction<TPass1, TPass2> actionPart2, IBizAction<TPass2, TOut> actionPart3, DbContext context) : ErrorStorage
    where TPass1 : class?
    where TPass2 : class?
    where TOut : class?
{
    private readonly IBizAction<TIn, TPass1> actionPart1 = actionPart1;
    private readonly IBizAction<TPass1, TPass2> actionPart2 = actionPart2;
    private readonly IBizAction<TPass2, TOut> actionPart3 = actionPart3;
    private readonly DbContext context = context;

    public TOut Run(TIn argument)
    {
        using (var transaction = context.Database.BeginTransaction())
        {
            TPass1 pass1 = RunPart(actionPart1, argument);
            if (HasErrors)
                return null!;

            TPass2 pass2 = RunPart(actionPart2, pass1);
            if (HasErrors)
                return null!;

            TOut result = RunPart(actionPart3, pass2);
            if (!HasErrors)
                transaction.Commit();

            return result;
        }
    }

    private TPartOut RunPart<TPartIn, TPartOut>(IBizAction<TPartIn, TPartOut> actionPart, TPartIn argument)
    {
        TPartOut result = actionPart.Action(argument);
        Errors = actionPart.Errors;
        if (!HasErrors)
        {
            context.SaveChanges();
        }
        return result;
    }
}
