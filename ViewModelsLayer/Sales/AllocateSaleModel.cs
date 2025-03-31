using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModelsLayer.Goods;

namespace ViewModelsLayer.Sales;

public record AllocateSaleModel(Guid SaleId, List<GoodsUnitSearchModel> GoodsItems, string SecretWord) : ISaleModel;
