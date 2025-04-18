﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModelsLayer.Goods;

namespace ViewModelsLayer.Stock.Delivery;

public record DeliverySearchModel(DeliveryFilterOptions Filter, DeliveryOrderByOptions OrderBy, List<DeliveryUnitSearchModel> DeliveryUnitModels);