using BizLogicBase.Validation;
using DataLayer.Common;
using DataLayer.Models;
using DataLayer.NotMapped;
using DataLayer.SupportClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.StockServices;

public interface IAddNewGoodsService : IErrorAdder
{
    Task<List<Goods>> AddNewGoods(string goodsName, KindOfGoods kindOfGoods, SpecificType specificType, int price, GoodsStatus status, string description, int numberOfUnits);
}
public class AddNewGoodsService(MusicalShopDbContext context) : ErrorAdder, IAddNewGoodsService
{
    public async Task<List<Goods>> AddNewGoods(string goodsName, KindOfGoods kindOfGoods, SpecificType specificType, int price, GoodsStatus status, string description, int numberOfUnits)
    {
#warning business logic is here, but little
        if (price <= 0)
#warning how to use these properties
            AddError("Цена не может быть меньше или равна 0");//, nameof(price
        if (numberOfUnits <= 0)
#warning how to use these properties
            AddError("Количество единиц товара не может быть меньше или равно 0");
        DateTimeOffset? receiptDate = default;
        switch (status)
        {
            case GoodsStatus.InStock:
                receiptDate = DateTimeOffset.UtcNow;
                break;
            case GoodsStatus.AwaitingDelivery:
                receiptDate = null;
                break;
            default:
                AddError("Вы не можете выбрать данный статус");
                break;
        }
        if (HasErrors)
            return [];
        var result = new List<Goods>();
        for (int i = 0; i < numberOfUnits; i++)
        {
#warning what about factory
            Goods goods = new()
            {
                Description = description,
                Name = goodsName,
                Price = price,
                ReceiptDate = receiptDate,
                SoftDeleted = false,
                Status = status,
                Type = specificType,
            };
            dynamic typifiedGoodsUnit = kindOfGoods switch
            {
                KindOfGoods.MusicalInstruments => (MusicalInstrument)goods,
                KindOfGoods.Accessories => (Accessory)goods,
                KindOfGoods.AudioEquipmentUnits => (AudioEquipmentUnit)goods,
                KindOfGoods.SheetMusicEditions => (SheetMusicEdition)goods,
                _ => throw new Exception()
            };
            result.Add(typifiedGoodsUnit);
            await context.AddAsync(typifiedGoodsUnit);
            #region uuuu
            //switch (kindOfGoods)
            //{
            //    case KindOfGoods.MusicalInstruments:
            //        goods = new MusicalInstrument
            //        {
            //            Description 
            //        };
            //            break;
            //} 
            #endregion
        }
        await context.SaveChangesAsync();
        return result;
    }
}
