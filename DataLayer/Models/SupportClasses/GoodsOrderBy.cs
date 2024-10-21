using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#warning don't like this place for these enumerations
namespace DataLayer.Models.SupportClasses;

public enum GoodsOrderBy
{
    /// <summary>
    /// Random sorting, strictly speaking.
    /// </summary>
    Relevance,
    Price,
    ReceiptDate,
}
