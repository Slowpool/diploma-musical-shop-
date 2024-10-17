using DataLayer.Models.SupportClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.GoodsServices.Support;

/// <summary>
/// Filters goods, where <paramref name="FilterValue"/> is a value or comma-separated values (depends upon <paramref name="GoodsFilter"/> value).
/// Pass <paramref="FilterValue"/> as something like a string.Join([nameof(type1), nameof(typen)], ',').
/// </summary>
/// <param name="GoodsFilter"></param>
/// <param name="FilterValue"></param>
public record class GoodsFilterOptions(GoodsFilter GoodsFilter, string FilterValue);
