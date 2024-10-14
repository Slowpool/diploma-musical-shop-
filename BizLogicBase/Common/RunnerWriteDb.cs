using BizLogicBase.Validation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BizLogicBase.Common;
public class RunnerWriteDb<TIn, TOut>(DbContext context, IBizAction<TIn, TOut> action) : ErrorStorage
    where TOut : class?
{
    public override IImmutableList<ValidationResult> Errors => action.Errors;
    public TOut Run(TIn argument)
    {
        var result = action.Action(argument);
        if (!action.HasErrors)
            context.SaveChanges();
        return result;
    }
}
