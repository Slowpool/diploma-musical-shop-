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

    public static string ToMoney(this int number) =>
        number.ToString() + " руб.";

    public static DateTimeOffset? LocalToUniversal(this DateTime? dateTime) => dateTime is null ? null : (DateTimeOffset?)new DateTimeOffset((DateTime)dateTime).ToUniversalTime();

    public static string NameToLowerMysql(this Type type)
    {
        string typeName = type.Name;
        var stringBuilder = new StringBuilder();
        stringBuilder.Append(char.ToLower(typeName[0]));
        for(int i = 1; i < typeName.Length; i++)
        {
            if (char.IsUpper(typeName[i]))
                stringBuilder.Append("_");
            stringBuilder.Append(typeName[i]);
        }
        return stringBuilder.ToString();
    }
}
