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
using ViewModelsLayer.Stock;

namespace ServiceLayer.StockServices;

public interface IAddNewGoodsService : IErrorAdder
{
    Task<List<Goods>> AddNewGoods(AddGoodsToWarehouseDto dto);
}
public class AddNewGoodsService(MusicalShopDbContext context, ISpecificTypeService specificTypesService) : ErrorAdder, IAddNewGoodsService
{
    public async Task<List<Goods>> AddNewGoods(AddGoodsToWarehouseDto dto)
    {
        var specificTypeEntity = await specificTypesService.GetSpecificType(dto.SpecificType);

#warning business logic is here, but little
        if (dto.Price <= 0)
#warning how to use these properties
            AddError("Цена не может быть меньше или равна 0");//, nameof(price
        if (dto.NumberOfUnits <= 0)
            AddError("Количество единиц товара не может быть меньше или равно 0");
        DateTimeOffset? receiptDate = default;
        switch (dto.Status)
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
        for (int i = 0; i < dto.NumberOfUnits; i++)
        {
            Goods goods = dto.KindOfGoods switch
            {
                KindOfGoods.MusicalInstruments => new MusicalInstrument
                {
                    ReleaseYear = (int)dto.GoodsKindSpecificDataDto.ReleaseYear
                },
                KindOfGoods.Accessories => new Accessory
                {
                    Color = dto.GoodsKindSpecificDataDto.Color,
                    Size = dto.GoodsKindSpecificDataDto.Size
                },
                KindOfGoods.AudioEquipmentUnits => new AudioEquipmentUnit(),
                KindOfGoods.SheetMusicEditions => new SheetMusicEdition
                {
                    ReleaseYear = (int)dto.GoodsKindSpecificDataDto.ReleaseYear,
                    Author = dto.GoodsKindSpecificDataDto.Author
                },
                _ => throw new Exception()
            };
            goods.Description = dto.Description;
            goods.Name = dto.GoodsName;
            goods.Price = dto.Price;
            goods.ReceiptDate = receiptDate;
            goods.SoftDeleted = false;
            goods.Status = dto.Status;
            goods.Type = specificTypeEntity;

            result.Add(goods);
            await context.AddAsync(goods);
        }
        await context.SaveChangesAsync();
        return result;
    }
}
