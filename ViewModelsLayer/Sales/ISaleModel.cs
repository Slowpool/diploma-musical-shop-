using DataLayer.SupportClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModelsLayer.Sales;

public interface ISaleModel
{
    public Guid SaleId { get; init; }
}
