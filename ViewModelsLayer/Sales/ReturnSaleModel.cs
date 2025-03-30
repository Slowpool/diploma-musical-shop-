using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModelsLayer.Goods;

namespace ViewModelsLayer.Sales;

public record ReturnSaleModel(Guid SaleId, int Refund, List<GoodsUnitSearchModel> GoodsItems) : ISaleModel;
