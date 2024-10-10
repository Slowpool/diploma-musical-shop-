using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Models;
public class ReturnPurchase
{
    public int ReturnPurchaseId { get; set; }
    public DateTimeOffset Date { get; set; }
}
