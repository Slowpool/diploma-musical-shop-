using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
}
