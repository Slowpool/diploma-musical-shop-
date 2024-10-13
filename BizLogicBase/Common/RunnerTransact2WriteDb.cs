using BizLogicBase.Validation;
using Microsoft.EntityFrameworkCore;

namespace BizLogicBase.Common;
public class RunnerTransact2WriteDb<TIn, TPass, TOut>(IBizAction<TIn, TPass> actionPart1, IBizAction<TPass, TOut> actionPart2, DbContext context) : ErrorStorage
    where TPass : class?
    where TOut : class?
{
    private readonly IBizAction<TIn, TPass> actionPart1 = actionPart1;
    private readonly IBizAction<TPass, TOut> actionPart2 = actionPart2;
    private readonly DbContext context = context;

    public TOut Run(TIn argument)
    {
        using (var transaction = context.Database.BeginTransaction())
        {
            var pass1 = RunPart(actionPart1, argument);
            if (HasErrors)
                return null!;

            var result = RunPart(actionPart2, pass1);

            if (!HasErrors)
                transaction.Commit();

            return result;
        }
    }

    private TPartOut RunPart<TPartIn, TPartOut>(IBizAction<TPartIn, TPartOut> actionPart, TPartIn argument)
        where TPartOut : class?
    {
        var result = actionPart.Action(argument);
        Errors = actionPart.Errors;
        if (!HasErrors)
            context.SaveChanges();
        return result;
    }
}
