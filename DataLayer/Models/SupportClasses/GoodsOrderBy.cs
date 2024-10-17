using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Models.SupportClasses;

public enum GoodsOrderBy
{
    /// <summary>
    /// Random sorting, strictly speaking.
    /// </summary>
    Relevance,
    PriceAscending,
    PriceDescending,
    ReceiptDateAscending,
    ReceiptDateDescending
}
