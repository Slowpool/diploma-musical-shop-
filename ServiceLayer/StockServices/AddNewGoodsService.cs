using BizLogicBase.Validation;
using DataLayer.Common;
using DataLayer.Models;
using DataLayer.NotMapped;
using DataLayer.SupportClasses;
using ServiceLayer.StockServices.Delivery;
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
    Task<Guid?> AddNewGoods(AddGoodsToWarehouseDto dto);
}

public class AddNewGoodsService(MusicalShopDbContext context, ISpecificTypeService specificTypesService, IGetDeliveryService deliveryService) : ErrorAdder, IAddNewGoodsService
{
    public override IImmutableList<ValidationResult> Errors
        => base.Errors.Concat(specificTypesService.Errors).ToImmutableList();
    public async Task<Guid?> AddNewGoods(AddGoodsToWarehouseDto dto)
    {
        SpecificType specificType;
        try
        {
            specificType = dto.NewSpecificTypeIsBeingAdded
                ? await specificTypesService.CreateSpecificType(dto.NewSpecificType, dto.GoodsKindSpecificDataDto.KindOfGoods)
                : await specificTypesService.GetSpecificType(dto.SpecificType, dto.GoodsKindSpecificDataDto.KindOfGoods);
        }
        catch
        {
            AddError("Некорректное значение типа товара");
            return null;
        }

        if (HasErrors)
            return null;

        // Validated successfully
        Guid deliveryId;

        if (dto.ToPreviousDelivery)
            // TODO get delivery anyway
            if (await deliveryService.Exists((Guid)dto.DeliveryId!))
                deliveryId = (Guid)dto.DeliveryId;
            else
            {
                AddError("Delivery with such an id does not exist");
                return null;
            }
        else
        {
            var delivery = new GoodsDelivery
            {
                GoodsDeliveryId = Guid.NewGuid(),
                LocalActualDeliveryDate = DateTime.Now
            };
            context.Add(delivery);
            deliveryId = delivery.GoodsDeliveryId;
        }

        DateTimeOffset? receiptDate = default;
        switch (dto.Status)
        {
            case GoodsStatus.InStock:
                receiptDate = DateTimeOffset.Now;
                break;
            case GoodsStatus.AwaitingDelivery:
                receiptDate = null;
                break;
            default:
                AddError("Вы не можете выбрать данный статус");
                break;
        }

        var result = new List<Goods>();
        for (int i = 0; i < dto.NumberOfUnits; i++)
        {
            Goods goods = dto.GoodsKindSpecificDataDto.KindOfGoods switch
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
            goods.Name = dto.Name;
            goods.DeliveryId = deliveryId;
            goods.Price = (int)dto.Price;
            goods.Description = dto.Description;
            goods.ReceiptDate = receiptDate;
            goods.Status = dto.Status;
            goods.SoftDeleted = false;
            goods.SpecificTypeId = specificType.SpecificTypeId;
            // TODO specific type
            //goods.SpecificType = specificTypeEntity;

            result.Add(goods);
            await context.AddAsync(goods);
        }

        if (!HasErrors)
            await context.SaveChangesAsync();
        return deliveryId;
    }
}
