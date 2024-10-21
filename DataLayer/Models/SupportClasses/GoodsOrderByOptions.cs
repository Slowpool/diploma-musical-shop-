using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Models.SupportClasses;
public record class GoodsOrderByOptions(GoodsOrderBy OrderBy, bool AscendingOrder);