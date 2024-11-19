using DataLayer.SupportClasses;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModelsLayer.Stock.CustomAttributes;

namespace ViewModelsLayer.Stock;
//[DoesNotContainHtmlTags]
public record class GoodsKindSpecificDataDto
    (
    [RussianRequired("Вид")]
    KindOfGoods KindOfGoods,
    // Accessory
    [RussianStringLength(ConstValues.AccessoryColorMaxLength, "Цвет")]
    [RequiredWhenKindOfGoodsIs(KindOfGoods.Accessories, "Цвет")]
    // TODO to cyrillic
    string? Color,
    [RussianStringLength(ConstValues.AccessorySizeMaxLength, "Размер")]
    [RequiredWhenKindOfGoodsIs(KindOfGoods.Accessories, "Размер")]
    string? Size,
    // Sheet music edition
    [RussianStringLength(ConstValues.SheetMusicEditionAuthorMaxLength, "Автор")]
    string? Author,
    // Musical instrument
    [RequiredWhenKindOfGoodsIs(KindOfGoods.MusicalInstruments, "Год выпуска")]
    [RequiredWhenKindOfGoodsIs(KindOfGoods.SheetMusicEditions, "Год выпуска")]
    int? ReleaseYear,
    [RequiredWhenKindOfGoodsIs(KindOfGoods.MusicalInstruments, "Тип производителя")]
    ManufacturerType? ManufacturerType,
    [RequiredWhenKindOfGoodsIs(KindOfGoods.MusicalInstruments, "Производитель")]
    [RussianStringLength(ConstValues.MusicalInstrumentManufacturerMaxLength, "Производитель")]
    string? Manufacturer
    );