using BizLogicBase.Validation;
using DataLayer.Common;
using DataLayer.Models;
using DataLayer.NotMapped;
using DataLayer.SupportClasses;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.ComponentModel.DataAnnotations;
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
    public override IImmutableList<ValidationResult> Errors
        => base.Errors.Concat(specificTypesService.Errors).ToImmutableList();
    public async Task<List<Goods>> AddNewGoods(AddGoodsToWarehouseDto dto)
    {
        SpecificType specificType;
        try
        {
            specificType = dto.NewSpecificTypeIsBeingAdded
                ? await specificTypesService.CreateSpecificType(dto.NewSpecificType, dto.KindOfGoods)
                : await specificTypesService.GetSpecificType(dto.SpecificType, dto.KindOfGoods);
        }
        catch
        {
            AddError("Некорректное значение типа товара");
            return null;
        }


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
        // Validated successfully
        var result = new List<Goods>();
        for (int i = 0; i < dto.NumberOfUnits; i++)
        {
            Goods goods = dto.KindOfGoods switch
            {
                // TODO refactoring
                KindOfGoods.MusicalInstruments => new MusicalInstrument
                {
                    ReleaseYear = (int)dto.GoodsKindSpecificDataDto.ReleaseYear,
                    ManufacturerType = (ManufacturerType)dto.GoodsKindSpecificDataDto.ManufacturerType,
                    Manufacturer = dto.GoodsKindSpecificDataDto.Manufacturer
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
            goods.Name = dto.Name;
            goods.Price = (int)dto.Price;
            goods.ReceiptDate = receiptDate;
            goods.SoftDeleted = false;
            goods.Status = dto.Status;
            goods.SpecificTypeId = specificType.SpecificTypeId;
            // TODO specific type
            //goods.SpecificType = specificTypeEntity;

            result.Add(goods);
            await context.AddAsync(goods);
        }
        await context.SaveChangesAsync();
        return result;
    }
}
