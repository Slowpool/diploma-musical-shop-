using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Runtime.Intrinsics.X86;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;
using static System.Reflection.Metadata.BlobBuilder;

namespace Common;
public static class CommonExtensions
{
    public static string Humanize(this bool value)
    {
        return value ? "Да" : "Нет";
    }

    public static string ToStringOrDefaultValue<T>(this T value)
    {
        return value?.ToString() ?? "-";
    }

    public static IQueryable<T> Page<T>(this IQueryable<T> query, int pageNumber, int pageSize)
    {
        if (pageSize <= 0)
            throw new ArgumentOutOfRangeException(nameof(pageSize), "pageSize cannot be zero or less than zero.");
        if (pageNumber <= 0)
            throw new ArgumentOutOfRangeException(nameof(pageSize), "pageNum cannot be zero or less than zero.");
        if (pageNumber != 1)
            query = query.Skip((pageNumber - 1) * pageSize);
        return query.Take(pageSize);
    }

    
}
