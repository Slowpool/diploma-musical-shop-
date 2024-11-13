using DataLayer.SupportClasses;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModelsLayer.Stock;
public record class AddGoodsToWarehouseModel(AddGoodsToWarehouseDto AddGoodsToWarehouseDto, Dictionary<KindOfGoods, List<string>> SpecificTypes, List<ValidationResult> Errors);
